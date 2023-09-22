using Dance.Tool.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Plugin
{
    /// <summary>
    /// 插件生命周期 -- 粒子引擎
    /// </summary>
    public class ParticlePluginLifescope : DanceObject, IDancePluginLifescope
    {
        /// <summary>
        /// 注册插件
        /// </summary>
        /// <returns>插件信息</returns>
        public IDancePluginInfo Register()
        {
            return new PluginModel("粒子引擎", typeof(ParticleView));
        }

        /// <summary>
        /// 初始化插件
        /// </summary>
        public void Initialize()
        {

        }
    }
}
