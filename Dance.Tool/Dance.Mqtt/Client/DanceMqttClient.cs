using MQTTnet.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt客户端
    /// </summary>
    public class DanceMqttClient : DanceMqttClientBase
    {
        /// <summary>
        /// Mqtt客户端
        /// </summary>
        /// <param name="option">设置</param>
        public DanceMqttClient(DanceMqttClientOption option) : base(option)
        {

        }

        // ===============================================================================================
        // Field

        /// <summary>
        /// 请求字典
        /// </summary>
        private readonly ConcurrentDictionary<Guid, DanceMqttRequestInfo> RequestDic = new();

        /// <summary>
        /// 执行器集合
        /// </summary>
        private readonly ConcurrentDictionary<string, DanceMqttRequestExecuter> ExecuterDic = new();

        // ===============================================================================================
        // Event

        /// <summary>
        /// 发送后触发
        /// </summary>
        public event EventHandler<DanceMqttTransferEventArgs>? Sended;

        /// <summary>
        /// 接收后触发
        /// </summary>
        public event EventHandler<DanceMqttTransferEventArgs>? Received;

        // ===============================================================================================
        // Public Function

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="data">数据</param>
        public async Task Send(string topic, object data)
        {
            string json = JsonConvert.SerializeObject(data);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            Dictionary<string, string> userProperties = new()
            {
                [DanceMqttPropertyKeys.DANCE_DATA_TYPE] = DanceMqttDataType.NORMAL
            };

            await this.Send(topic, string.Empty, userProperties, buffer);

            this.Sended?.Invoke(this, new(topic, string.Empty, userProperties, json));
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="topic">主题</param>
        /// <param name="responseTopic">返回主题</param>
        /// <param name="data">数据</param>
        /// <returns>返回</returns>
        public async Task<T?> Request<T>(string topic, string responseTopic, object data) where T : new()
        {
            string json = JsonConvert.SerializeObject(data);
            DanceMqttRequestInfo info = new(json);

            if (!this.RequestDic.TryAdd(info.RequestID, info))
                throw new Exception($"add request error. id: {info.RequestID}, topic: {topic}, responseTopic: {responseTopic}, data: {json}");

            DateTime timeout = info.RequestTime + info.Timeout;
            while (!info.IsResponse && DateTime.Now < timeout)
            {
                await Task.Delay(500);
            }

            this.RequestDic.TryRemove(info.RequestID, out _);

            if (string.IsNullOrWhiteSpace(info.ResponseData))
                return default;

            return JsonConvert.DeserializeObject<T>(info.ResponseData);
        }

        // ===============================================================================================
        // Protected Function

        /// <summary>
        /// 当接收消息时触发
        /// </summary>
        protected override async Task OnReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                Dictionary<string, string> userProperties = e.ApplicationMessage.UserProperties?.ToDictionary(p => p.Name, p => p.Value) ?? new();

                userProperties.TryGetValue(DanceMqttPropertyKeys.DANCE_DATA_TYPE, out string? dataType);
                userProperties.TryGetValue(DanceMqttPropertyKeys.DANCE_REQUEST_ID, out string? requestId);
                userProperties.TryGetValue(DanceMqttPropertyKeys.DANCE_REQUEST_ROUTE, out string? requestRoute);

                string topic = e.ApplicationMessage.Topic;
                string responseTopic = e.ApplicationMessage.ResponseTopic;
                byte[] buffer = e.ApplicationMessage.PayloadSegment.ToArray();
                string json = Encoding.UTF8.GetString(buffer);

                // 未识别数据 或 正常数据
                if (string.IsNullOrWhiteSpace(dataType) || dataType == DanceMqttDataType.NORMAL)
                {
                    this.Received?.Invoke(this, new(topic, responseTopic, userProperties, json));

                    return;
                }

                // 请求数据
                if (dataType == DanceMqttDataType.REQUEST)
                {
                    if (string.IsNullOrWhiteSpace(requestRoute) || !this.ExecuterDic.TryGetValue(requestRoute, out DanceMqttRequestExecuter? executer))
                        return;

                    string? response = executer?.Execute(json);

                    await this.Send(responseTopic, response ?? string.Empty);
                    return;
                }

                // 返回数据
                if (dataType == DanceMqttDataType.RESPONSE)
                {
                    if (Guid.TryParse(requestId, out Guid id) || this.RequestDic.TryGetValue(id, out DanceMqttRequestInfo? info) || info == null)
                        return;

                    info.ResponseData = json;
                    info.ResponseTime = DateTime.Now;
                    info.IsResponse = true;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);

                this.TriggerError(ex);
            }
        }
    }
}