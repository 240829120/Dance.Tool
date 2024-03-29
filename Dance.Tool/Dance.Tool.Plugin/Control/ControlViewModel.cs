﻿using CommunityToolkit.Mvvm.Input;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dance.Tool.Plugin
{
    /// <summary>
    /// 控件视图模型
    /// </summary>
    [DanceSingleton]
    public class ControlViewModel : DanceViewModel
    {
        public ControlViewModel()
        {
            this.itemsSource = [];

            for (int i = 0; i < 30; i++)
            {
                this.itemsSource.Add(new()
                {
                    Index = i,
                    Name = $"name_{i}",
                    Age = i + 17,
                    Sex = $"sex_{i}",
                    City = $"city_{i}",
                    School = $"school_{i}"
                });
            }

            this.ShowMessageBoxCommand = new RelayCommand(this.ShowMessageBox);
            this.ShowIconMessageBoxCommand = new RelayCommand(this.ShowIconMessageBox);
            this.ShowNotifyCommand = new RelayCommand(this.ShowNotify);
            this.DragBeginCommand = new RelayCommand<DanceDragBeginEventArgs>(this.DragBegin);
            this.DropCommand = new RelayCommand<DanceDragEventArgs>(this.Drop);
        }

        /// <summary>
        /// 随机数
        /// </summary>
        private readonly Random random = new();

        #region ItemsSource -- 数据源

        private ObservableCollection<ControlTestModel> itemsSource;
        /// <summary>
        /// 数据源
        /// </summary>
        public ObservableCollection<ControlTestModel> ItemsSource
        {
            get { return itemsSource; }
            set { itemsSource = value; this.OnPropertyChanged(); }
        }

        #endregion

        #region ShowMessageBoxCommand -- 显示消息框命令

        /// <summary>
        /// 显示消息框命令
        /// </summary>
        public RelayCommand ShowMessageBoxCommand { get; private set; }

        /// <summary>
        /// 显示消息框
        /// </summary>
        private void ShowMessageBox()
        {
            int length = random.Next(10, 100);
            StringBuilder sb = new();
            for (int i = 0; i < length; i++)
            {
                sb.Append($"{i}");
            }

            DanceMessageExpansion.ShowMessageBox("信息", DanceMessageBoxIcon.None, sb.ToString(), DanceMessageBoxAction.YES);
        }

        #endregion

        #region ShowIconMessageBoxCommand -- 显示图标消息框命令

        /// <summary>
        /// 显示图标消息框命令
        /// </summary>
        public RelayCommand ShowIconMessageBoxCommand { get; private set; }

        /// <summary>
        /// 显示图标消息框
        /// </summary>
        private void ShowIconMessageBox()
        {
            int type = random.Next(0, 4);
            int length = random.Next(10, 100);
            StringBuilder sb = new();
            for (int i = 0; i < length; i++)
            {
                sb.Append($"{i}");
            }

            switch (type)
            {
                case 0: DanceMessageExpansion.ShowMessageBox("成功", DanceMessageBoxIcon.Success, sb.ToString(), DanceMessageBoxAction.YES | DanceMessageBoxAction.CANCEL); break;
                case 1: DanceMessageExpansion.ShowMessageBox("失败", DanceMessageBoxIcon.Failure, sb.ToString(), DanceMessageBoxAction.YES | DanceMessageBoxAction.CANCEL); break;
                case 2: DanceMessageExpansion.ShowMessageBox("警告", DanceMessageBoxIcon.Warning, sb.ToString(), DanceMessageBoxAction.YES | DanceMessageBoxAction.CANCEL); break;
                case 3: DanceMessageExpansion.ShowMessageBox("信息", DanceMessageBoxIcon.Info, sb.ToString(), DanceMessageBoxAction.YES | DanceMessageBoxAction.CANCEL); break;
                default: break;
            }
        }

        #endregion

        #region ShowNotifyCommand -- 显示通知命令

        /// <summary>
        /// 显示通知命令
        /// </summary>
        public RelayCommand ShowNotifyCommand { get; private set; }

        /// <summary>
        /// 显示通知
        /// </summary>
        private void ShowNotify()
        {
            DanceMessageExpansion.ShowNotify(System.Windows.Forms.ToolTipIcon.Info, "通知标题", "通知内容");
        }

        #endregion

        #region DragBeginCommand -- 拖拽开始命令

        /// <summary>
        /// 拖拽开始命令
        /// </summary>
        public RelayCommand<DanceDragBeginEventArgs> DragBeginCommand { get; set; }

        /// <summary>
        /// 拖拽开始
        /// </summary>
        private void DragBegin(DanceDragBeginEventArgs? e)
        {
            if (e == null)
                return;

            e.Data = "拖拽测试";
        }

        #endregion

        #region DropCommand -- 放置命令

        /// <summary>
        /// 放置命令
        /// </summary>
        public RelayCommand<DanceDragEventArgs> DropCommand { get; set; }

        /// <summary>
        /// 放置
        /// </summary>
        private void Drop(DanceDragEventArgs? e)
        {
            if (e == null)
                return;

            string? data = e.EventArgs.Data.GetData(typeof(string))?.ToString();
            DanceMessageExpansion.ShowNotify(ToolTipIcon.Info, "拖拽测试", data ?? string.Empty);
        }

        #endregion
    }
}
