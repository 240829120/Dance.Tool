using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Module
{
    /// <summary>
    /// 插件模型
    /// </summary>
    public class PluginModel : DanceModel, IDancePluginInfo
    {
        /// <summary>
        /// 插件模型
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="viewType">视图类型</param>
        public PluginModel(string name, Type viewType)
        {
            this.Name = name;
            this.ViewType = viewType;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 视图类型
        /// </summary>
        public Type ViewType { get; set; }
    }
}
