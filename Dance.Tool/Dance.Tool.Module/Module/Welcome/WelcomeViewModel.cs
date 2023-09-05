using CommunityToolkit.Mvvm.Input;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Tool.Module
{
    /// <summary>
    /// 欢迎视图模型
    /// </summary>
    [DanceSingleton(typeof(WelcomeViewModel))]
    public class WelcomeViewModel : DanceViewModel
    {
        public WelcomeViewModel()
        {
            // 初始化命令
            this.InitCommand();
        }

        /// <summary>
        /// 初始化命令
        /// </summary>
        private void InitCommand()
        {
            this.EnterCommand = new RelayCommand(this.Enter);
        }

        // ====================================================================================
        // Property



        // ====================================================================================
        // Command

        #region EnterCommand -- 确认命令

        /// <summary>
        /// 确认命令
        /// </summary>
        public RelayCommand? EnterCommand { get; set; }

        /// <summary>
        /// 确认
        /// </summary>
        private void Enter()
        {
            MainViewModel vm = DanceDomain.Current.LifeScope.Resolve<MainViewModel>();
            vm.CurrentNavigationModel = vm.NavigationModels?.FirstOrDefault(p => p.ViewType == typeof(HomeView));
        }

        #endregion
    }
}
