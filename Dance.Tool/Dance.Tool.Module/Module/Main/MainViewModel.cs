using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Module
{
    /// <summary>
    /// 主视图模型
    /// </summary>
    [DanceSingleton]
    public class MainViewModel : DanceViewModel
    {
        public MainViewModel()
        {
            this.NavigationModels =
            [
                new NavigationModel { ViewType = typeof(WelcomeView) },
                new NavigationModel { ViewType = typeof(HomeView) }
            ];

            this.CurrentNavigationModel = this.NavigationModels.FirstOrDefault();
        }

        // ===============================================================================================
        // Property

        #region NavigationModels -- 导航模型集合

        private ObservableCollection<NavigationModel>? navigationModels;
        /// <summary>
        /// 导航模型集合
        /// </summary>
        public ObservableCollection<NavigationModel>? NavigationModels
        {
            get { return navigationModels; }
            set { navigationModels = value; this.OnPropertyChanged(nameof(NavigationModels)); }
        }

        #endregion

        #region CurrentNavigationModel -- 当前导航模型

        private NavigationModel? currentNavigationModel;
        /// <summary>
        /// 当前导航模型
        /// </summary>
        public NavigationModel? CurrentNavigationModel
        {
            get { return currentNavigationModel; }
            set { currentNavigationModel = value; this.OnPropertyChanged(nameof(CurrentNavigationModel)); }
        }

        #endregion
    }
}
