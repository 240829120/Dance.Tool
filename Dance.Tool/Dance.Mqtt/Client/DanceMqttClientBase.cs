using log4net;
using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt客户端基类
    /// </summary>
    public abstract class DanceMqttClientBase : DanceObject, IDanceMqttClient
    {
        /// <summary>
        /// Mqtt客户端基类
        /// </summary>
        /// <param name="option">设置</param>
        public DanceMqttClientBase(DanceMqttClientOption option)
        {
            this.Option = option;
        }

        // ===============================================================================================
        // Field

        /// <summary>
        /// MQTT客户端
        /// </summary>
        protected IMqttClient? MqttClient;

        /// <summary>
        /// 重连线程
        /// </summary>
        protected DanceThread? ReConnectThread;

        // ===============================================================================================
        // Event

        /// <summary>
        /// 连接断开
        /// </summary>
        public event EventHandler<EventArgs>? Disconnected;

        /// <summary>
        /// 开始连接
        /// </summary>
        public event EventHandler<EventArgs>? Connecting;

        /// <summary>
        /// 连接结束
        /// </summary>
        public event EventHandler<EventArgs>? Connected;

        /// <summary>
        /// 错误时触发
        /// </summary>
        public event EventHandler<Exception>? Error;

        // ===============================================================================================
        // Property

        /// <summary>
        /// 参数
        /// </summary>
        public DanceMqttClientOption Option { get; private set; }

        // ===============================================================================================
        // Public Function

        /// <summary>
        /// 链接服务
        /// </summary>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public async Task Connect()
        {
#if DEBUG
            Debug.WriteLine($"============================================");
            Debug.WriteLine($"MQTT 开始链接");
            Debug.WriteLine($"Client ID: {this.Option.ClientID}");
            Debug.WriteLine($"User Name: {this.Option.UserName}");
            Debug.WriteLine($"Password:  {this.Option.Password}");
            Debug.WriteLine($"============================================");
#endif

            var mqttFactory = new MqttFactory();

            this.MqttClient = mqttFactory.CreateMqttClient();
            var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(this.Option.Url, this.Option.Port)
                                                                  .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
                                                                  .WithClientId(this.Option.ClientID)
                                                                  .WithCredentials(this.Option.UserName, this.Option.Password)
                                                                  .WithWillQualityOfServiceLevel(this.Option.Level)
                                                                  .WithKeepAlivePeriod(this.Option.KeepAlive)
                                                                  .Build();

            this.MqttClient.ApplicationMessageReceivedAsync += OnReceivedAsync;
            this.MqttClient.DisconnectedAsync += MqttClient_DisconnectedAsync;
            this.MqttClient.ConnectingAsync += MqttClient_ConnectingAsync;
            this.MqttClient.ConnectedAsync += MqttClient_ConnectedAsync;

            await this.MqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                                                  .WithTopicFilter(
                                                      f =>
                                                      {
                                                          foreach (string topic in this.Option.Topics)
                                                          {
                                                              if (string.IsNullOrWhiteSpace(topic))
                                                                  continue;

                                                              f.WithTopic(topic);
                                                          }
                                                      })
                                                  .Build();

            await this.MqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

        /// <summary>
        /// 是否链接
        /// </summary>
        /// <returns>是否链接</returns>
        public bool IsConnected()
        {
            return this.MqttClient != null && this.MqttClient.IsConnected;
        }

        // ===============================================================================================
        // Protected Function

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="topic">主题</param>
        /// <param name="responseTopic">返回主题</param>
        /// <param name="userProperties">用户数据</param>
        /// <param name="buffer">二进制数据</param>
        protected async Task Send(string topic, string responseTopic, Dictionary<string, string> userProperties, byte[] buffer)
        {
            if (this.MqttClient == null)
                return;

            MqttApplicationMessage msg = new()
            {
                ResponseTopic = responseTopic,
                Topic = topic,
                PayloadSegment = buffer,
                UserProperties = new()
            };
            foreach (var kv in userProperties)
            {
                msg.UserProperties.Add(new(kv.Key, kv.Value));
            }

            await this.MqttClient.PublishAsync(msg);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            if (this.MqttClient == null)
                return;

            IMqttClient client = this.MqttClient;
            this.MqttClient = null;

            client.DisconnectAsync().Wait();
            client.Dispose();
        }

        /// <summary>
        /// 触发错误
        /// </summary>
        /// <param name="ex">异常</param>
        protected void TriggerError(Exception ex)
        {
            this.Error?.Invoke(this, ex);
        }

        /// <summary>
        /// 当接收消息时触发
        /// </summary>
        protected abstract Task OnReceivedAsync(MqttApplicationMessageReceivedEventArgs e);

        // ===============================================================================================
        // Private Function

        /// <summary>
        /// 链接断开
        /// </summary>
        private Task MqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            if (this.ReConnectThread != null)
                return Task.CompletedTask;

            // 触发链接断开事件
            this.Disconnected?.Invoke(this, new EventArgs());

            // 启动重连线程
            this.ReConnectThread = new DanceThread(c =>
            {
                while (this.MqttClient != null && !this.MqttClient.IsConnected)
                {
                    try
                    {
                        this.MqttClient.ReconnectAsync().Wait();
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }
                    finally
                    {
                        Task.Delay(5000).Wait();
                    }
                }
            });

            this.ReConnectThread.Start();

            return Task.CompletedTask;
        }

        /// <summary>
        /// 链接完成
        /// </summary>
        private Task MqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            this.Connected?.Invoke(this, new EventArgs());

            return Task.CompletedTask;
        }

        /// <summary>
        /// 开始链接
        /// </summary>
        private Task MqttClient_ConnectingAsync(MqttClientConnectingEventArgs arg)
        {
            this.Connecting?.Invoke(this, new EventArgs());

            return Task.CompletedTask;
        }
    }
}
