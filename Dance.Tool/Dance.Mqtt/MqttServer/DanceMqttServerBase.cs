using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Mqtt
{
    /// <summary>
    /// Mqtt服务
    /// </summary>
    public class DanceMqttServerBase : DanceObject, IDisposable
    {
        /// <summary>
        /// Mqtt服务
        /// </summary>
        /// <param name="option">设置</param>
        public DanceMqttServerBase(DanceMqttServerOption option)
        {
            this.Option = option;
        }


        /// <summary>
        /// Mqtt服务
        /// </summary>
        protected MqttServer? MqttServer { get; private set; }

        /// <summary>
        /// 设置
        /// </summary>
        public DanceMqttServerOption Option { get; private set; }


        public void Start()
        {
            //mqttServer = new MqttServerFactory().CreateMqttServer(options) as MqttServer;
            //mqttServer.ApplicationMessageReceived += MqttServer_ApplicationMessageReceived;
            //mqttServer.ClientConnected += MqttServer_ClientConnected;
            //mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;
        }
    }
}
