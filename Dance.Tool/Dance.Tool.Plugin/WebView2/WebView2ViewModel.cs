using CommunityToolkit.Mvvm.Input;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Plugin
{
    /// <summary>
    /// WebView2 视图模型
    /// </summary>
    [DanceSingleton]
    public class WebView2ViewModel : DanceViewModel
    {
        public WebView2ViewModel()
        {
            this.GotoCommand = new RelayCommand(this.Goto);
            this.ClickCommand = new RelayCommand(this.Click);
        }

        // ============================================================================
        // Property

        #region Url -- 地址

        private string? url = "http://www.baidu.com";
        /// <summary>
        /// 地址
        /// </summary>
        public string? Url
        {
            get { return url; }
            set { url = value; this.OnPropertyChanged(); }
        }

        #endregion

        // ============================================================================
        // Command

        #region GotoCommand -- 跳转命令

        /// <summary>
        /// 跳转命令
        /// </summary>
        public RelayCommand? GotoCommand { get; set; }

        /// <summary>
        /// 跳转
        /// </summary>
        private void Goto()
        {
            if (this.View is not WebView2View view)
                return;

            view.web.Source = string.IsNullOrWhiteSpace(this.Url) ? null : new Uri(this.Url);
        }

        #endregion

        #region ClickCommand -- 点击命令

        /// <summary>
        /// 点击命令
        /// </summary>
        public RelayCommand? ClickCommand { get; set; }

        /// <summary>
        /// 点击
        /// </summary>
        private void Click()
        {
            DanceMessageExpansion.ShowMessageBox("WebView2 提示", DanceMessageBoxIcon.Info, "点击 WPF Button 按钮", DanceMessageBoxAction.YES);
        }

        #endregion
    }
}
