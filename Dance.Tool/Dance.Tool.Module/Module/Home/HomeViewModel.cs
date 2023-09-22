using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Module
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    [DanceSingleton]
    public class HomeViewModel : DanceViewModel
    {
        public HomeViewModel()
        {
            IDancePluginManager pluginManager = DanceDomain.Current.LifeScope.Resolve<IDancePluginManager>();

            this.pluginModels = pluginManager.PluginDomains.Select(p => (PluginModel)p.PluginInfo).ToObservableCollection();
            this.selectedPluginModel = this.pluginModels.FirstOrDefault();
        }

        // ===============================================================================================
        // Property

        #region PluginModels -- 插件集合

        private ObservableCollection<PluginModel> pluginModels;
        /// <summary>
        /// 插件集合
        /// </summary>
        public ObservableCollection<PluginModel> PluginModels
        {
            get { return pluginModels; }
            set { pluginModels = value; this.OnPropertyChanged(nameof(PluginModels)); }
        }

        #endregion

        #region SelectedPluginModel -- 当前选中的插件

        private PluginModel? selectedPluginModel;
        /// <summary>
        /// 当前选中的插件
        /// </summary>
        public PluginModel? SelectedPluginModel
        {
            get { return selectedPluginModel; }
            set { selectedPluginModel = value; this.OnPropertyChanged(); }
        }

        #endregion
    }
}
