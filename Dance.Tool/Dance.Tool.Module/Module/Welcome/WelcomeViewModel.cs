using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Module
{
    /// <summary>
    /// 欢迎视图模型
    /// </summary>
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

        #region EnterCommand -- 进入命令

        /// <summary>
        /// 进入命令
        /// </summary>
        public RelayCommand? EnterCommand { get; set; }

        /// <summary>
        /// 进入
        /// </summary>
        private void Enter()
        {

        }

        #endregion
    }
}
