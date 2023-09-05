using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt属性键
    /// </summary>
    public static class DanceMqttPropertyKeys
    {
        /// <summary>
        /// 请求ID
        /// </summary>
        public const string DANCE_REQUEST_ID = "DANCE_REQUEST_ID";

        /// <summary>
        /// 数据类型
        /// <see cref="DanceMqttDataType"/>
        /// </summary>
        public const string DANCE_DATA_TYPE = "DANCE_DATA_TYPE";

        /// <summary>
        /// 请求路由
        /// </summary>
        public const string DANCE_REQUEST_ROUTE = "DANCE_REQUEST_ROUTE";

        /// <summary>
        /// 定向发送客户端ID
        /// </summary>
        public const string DANCE_CLIENT_ID = "DANCE_CLIENT_ID";
    }
}
