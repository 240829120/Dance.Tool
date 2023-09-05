using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// 请求执行器
    /// </summary>
    public class DanceMqttRequestExecuter : DanceObject, IDisposable
    {
        /// <summary>
        /// 请求执行器
        /// </summary>
        /// <param name="route">路由</param>
        /// <param name="execute">执行器</param>
        public DanceMqttRequestExecuter(string route, Func<string, string> execute)
        {
            if (string.IsNullOrWhiteSpace(route))
                throw new ArgumentNullException(nameof(route));

            this.Route = route;
            this.Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// 请求路由
        /// </summary>
        public string Route { get; private set; }

        /// <summary>
        /// 执行器
        /// </summary>
        public Func<string, string> Execute { get; private set; }
    }
}