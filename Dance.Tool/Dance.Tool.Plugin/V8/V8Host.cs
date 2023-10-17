using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Tool.Plugin
{
    /// <summary>
    /// Google V8 Host对象
    /// </summary>
    [ComVisible(true)]
    public class V8Host
    {
        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void ShowMessage(string msg)
        {
            DanceMessageExpansion.ShowMessageBox("Google V8 信息", DanceMessageBoxIcon.Info, msg, DanceMessageBoxAction.YES);
        }

        /// <summary>
        /// 显示通知
        /// </summary>
        /// <param name="msg">消息</param>
        public void ShowNotify(string msg)
        {
            DanceMessageExpansion.ShowNotify(System.Windows.Forms.ToolTipIcon.Info, "Google V8 通知", msg);
        }
    }
}
