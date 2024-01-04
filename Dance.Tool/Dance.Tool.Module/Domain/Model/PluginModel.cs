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
    /// <param name="name">名称</param>
    /// <param name="viewType">视图类型</param>
    public class PluginModel(string name, Type viewType) : DanceModel, IDancePluginInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = name;

        /// <summary>
        /// 视图类型
        /// </summary>
        public Type ViewType { get; set; } = viewType;
    }
}
