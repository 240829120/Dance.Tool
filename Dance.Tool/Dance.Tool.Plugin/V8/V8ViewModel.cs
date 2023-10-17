using CommunityToolkit.Mvvm.Input;
using Dance.Wpf;
using Microsoft.ClearScript;
using Microsoft.ClearScript.JavaScript;
using Microsoft.ClearScript.V8;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Tool.Plugin
{
    /// <summary>
    /// V8视图模型
    /// </summary>
    [DanceSingleton]
    public class V8ViewModel : DanceViewModel
    {
        public V8ViewModel()
        {
            this.LoadedIndexCommand = new RelayCommand(this.LoadedIndex);
            this.RunCommand = new RelayCommand(this.Run);
            this.DebugCommand = new RelayCommand(this.Debug);
            this.OpenVsCodeCommand = new RelayCommand(this.OpenVsCode);
            this.SaveCommand = new RelayCommand(this.Save);

            this.CopyCommand = new RelayCommand(this.Copy, this.CanCopy);
            this.CutCommand = new RelayCommand(this.Cut, this.CanCut);
            this.PasteCommand = new RelayCommand(this.Paste);
        }

        // =====================================================================================
        // Field

        /// <summary>
        /// Google V8 引擎
        /// </summary>
        private V8ScriptEngine? Engine;

        // =====================================================================================
        // Property

        #region IsDebugging -- 是否正在调试

        private bool isDebugging;
        /// <summary>
        /// 是否正在调试
        /// </summary>
        public bool IsDebugging
        {
            get { return isDebugging; }
            set { isDebugging = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region IsRunning -- 是否正在运行

        private bool isRunning;
        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; this.OnPropertyChanged(); }
        }

        #endregion

        // =====================================================================================
        // Command

        #region LoadedIndexCommand -- 加载首页命令

        /// <summary>
        /// 加载首页命令
        /// </summary>
        public RelayCommand? LoadedIndexCommand { get; set; }

        /// <summary>
        /// 加载首页
        /// </summary>
        private void LoadedIndex()
        {
            if (this.View is not V8View view)
                return;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "V8", "Script", "index.js");

            using StreamReader sr = new(path, Encoding.UTF8);
            view.edit.Text = sr.ReadToEnd();
        }

        #endregion

        #region CopyCommand -- 复制命令

        /// <summary>
        /// 复制命令
        /// </summary>
        public RelayCommand? CopyCommand { get; set; }

        /// <summary>
        /// 是否可以复制
        /// </summary>
        /// <returns>是否可以复制</returns>
        private bool CanCopy()
        {
            if (this.View is not V8View view)
                return false;

            return view.edit.SelectionLength > 0;
        }

        /// <summary>
        /// 复制
        /// </summary>
        private void Copy()
        {
            if (this.View is not V8View view)
                return;

            view.edit.Copy();
        }

        #endregion

        #region CutCommand -- 剪切命令

        /// <summary>
        /// 剪切命令
        /// </summary>
        public RelayCommand? CutCommand { get; set; }

        /// <summary>
        /// 是否可以剪切
        /// </summary>
        /// <returns>是否可以剪切</returns>
        private bool CanCut()
        {
            if (this.View is not V8View view)
                return false;

            return view.edit.SelectionLength > 0;
        }

        /// <summary>
        /// 剪切
        /// </summary>
        private void Cut()
        {
            if (this.View is not V8View view)
                return;

            view.edit.Cut();
        }

        #endregion

        #region PasteCommand -- 粘贴命令

        /// <summary>
        /// 粘贴命令
        /// </summary>
        public RelayCommand? PasteCommand { get; set; }

        /// <summary>
        /// 粘贴
        /// </summary>
        private void Paste()
        {
            if (this.View is not V8View view)
                return;

            view.edit.Paste();
        }

        #endregion

        #region SaveCommand -- 保存命令

        /// <summary>
        /// 保存命令
        /// </summary>
        public RelayCommand? SaveCommand { get; set; }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (this.View is not V8View view)
                return;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "V8", "Script", "index.js");

            using StreamWriter sw = new(path, false, Encoding.UTF8);
            sw.Write(view.edit.Text);
            sw.Flush();
            sw.Dispose();
        }

        #endregion

        #region OpenVsCodeCommand -- 使用VSCode打开命令

        /// <summary>
        /// 使用VSCode打开命令
        /// </summary>
        public RelayCommand? OpenVsCodeCommand { get; set; }

        /// <summary>
        /// 使用VSCode打开
        /// </summary>
        private void OpenVsCode()
        {
            try
            {
                string? setupPath = Registry.ClassesRoot.OpenSubKey("Applications\\Code.exe\\shell\\open")?.GetValue("Icon")?.ToString();
                string workPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "V8", "Script");
                if (string.IsNullOrWhiteSpace(setupPath))
                {
                    DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Warning, "未找到VSCode安装路径", DanceMessageBoxAction.YES);

                    return;
                }
                Process.Start(setupPath, $"\"{workPath}\"");
            }
            catch (Exception ex)
            {
                DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Warning, ex.Message, DanceMessageBoxAction.YES);
            }

        }

        #endregion

        #region RunCommand -- 运行命令

        /// <summary>
        /// 运行命令
        /// </summary>
        public RelayCommand? RunCommand { get; set; }

        /// <summary>
        /// 运行
        /// </summary>
        private void Run()
        {
            if (this.IsRunning)
            {
                this.Engine?.Dispose();
                this.IsRunning = false;

                return;
            }

            this.IsRunning = true;

            Task.Run(() =>
            {
                try
                {
                    this.Engine?.Dispose();

                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "V8", "Script", "index.js");

                    this.Engine = new V8ScriptEngine("Dance Google V8 Engine", V8ScriptEngineFlags.EnableDynamicModuleImports);

                    this.Engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;
                    this.Engine.DocumentSettings.SearchPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "V8", "Script");
                    this.Engine.AddHostObject("V8Host", new V8Host());

                    this.Engine?.EvaluateDocument(path, ModuleCategory.Standard);

                    this.Engine?.Dispose();
                }
                catch (Exception ex)
                {
                    DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
                }
                finally
                {
                    Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        this.IsRunning = false;
                    });
                }
            });
        }

        #endregion

        #region DebugCommand -- 调试命令

        /// <summary>
        /// 调试命令
        /// </summary>
        public RelayCommand? DebugCommand { get; set; }

        /// <summary>
        /// 调试
        /// </summary>
        private void Debug()
        {
            if (this.IsDebugging)
            {
                this.Engine?.Dispose();
                this.IsDebugging = false;

                return;
            }

            this.IsDebugging = true;

            Task.Run(() =>
            {
                try
                {
                    this.Engine?.Dispose();

                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "V8", "Script", "index.js");

                    this.Engine = new V8ScriptEngine("Dance Google V8 Engine", V8ScriptEngineFlags.EnableDynamicModuleImports | V8ScriptEngineFlags.EnableDebugging |
                                                                               V8ScriptEngineFlags.EnableRemoteDebugging | V8ScriptEngineFlags.AwaitDebuggerAndPauseOnStart);

                    this.Engine.DocumentSettings.AccessFlags = DocumentAccessFlags.EnableFileLoading;
                    this.Engine.DocumentSettings.SearchPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "V8", "Script");
                    this.Engine.AddHostObject("V8Host", new V8Host());

                    DanceMessageExpansion.ShowNotify(System.Windows.Forms.ToolTipIcon.Info, "Google V8 通知", "已经启动调试，等待VSCode启动");

                    this.Engine?.EvaluateDocument(path, ModuleCategory.Standard);

                    this.Engine?.Dispose();
                }
                catch (Exception ex)
                {
                    DanceMessageExpansion.ShowMessageBox("错误", DanceMessageBoxIcon.Failure, ex.Message, DanceMessageBoxAction.YES);
                }
                finally
                {
                    Application.Current.Dispatcher.BeginInvoke(() =>
                    {
                        this.IsDebugging = false;
                    });
                }
            });
        }

        #endregion

        // =====================================================================================
        // Function

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Engine?.Dispose();
            this.Engine = null;
        }
    }
}
