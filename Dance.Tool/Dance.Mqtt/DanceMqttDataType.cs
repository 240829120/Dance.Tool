using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt数据类型
    /// </summary>
    public static class DanceMqttDataType
    {
        /// <summary>
        /// 正常数据
        /// </summary>
        public const string NORMAL = "NORMAL";

        /// <summary>
        /// 请求数据
        /// </summary>
        public const string REQUEST = "REQUEST";

        /// <summary>
        /// 返回数据
        /// </summary>
        public const string RESPONSE = "RESPONSE";
    }
}
