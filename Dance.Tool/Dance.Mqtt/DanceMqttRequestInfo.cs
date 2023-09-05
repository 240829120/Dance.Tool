using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt请求信息
    /// </summary>
    public class DanceMqttRequestInfo : DanceObject, IDisposable
    {
        /// <summary>
        /// Mqtt请求数据
        /// </summary>
        /// <param name="requestData">请求数据</param>
        public DanceMqttRequestInfo(string requestData)
        {
            this.RequestData = requestData;
        }

        /// <summary>
        /// 请求ID
        /// </summary>
        public Guid RequestID { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// 请求数据
        /// </summary>
        public string RequestData { get; private set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 返回时间
        /// </summary>
        public DateTime? ResponseTime { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(10);

        /// <summary>
        /// 返回数据
        /// </summary>
        public string? ResponseData { get; set; }

        /// <summary>
        /// 是否返回
        /// </summary>
        public bool IsResponse { get; set; }
    }
}
