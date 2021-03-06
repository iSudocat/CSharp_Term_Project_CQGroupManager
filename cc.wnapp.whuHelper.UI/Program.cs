﻿using cc.wnapp.whuHelper.Code;
using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cc.wnapp.whuHelper.UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    

    public class OpenWindowA : IMenuCall
    {
        
        private Form1 _mainWindow = null;

        public void MenuCall(object sender, CQMenuCallEventArgs e)
        {
            if (Common.IsInitialized == true)
            {
                CQ.Api = e.CQApi;
                CQ.Log = e.CQLog;
                e.CQLog.Debug("菜单点击事件", $"打开界面-{e.Name}");
                if (this._mainWindow == null)
                {
                    this._mainWindow = new Form1();
                    this._mainWindow.Closing += MainWindow_Closing;
                    this._mainWindow.Show();    // 显示窗体
                }
                else
                {
                    this._mainWindow.Activate();    // 将窗体调制到前台激活
                }
            }
            else
            {
                MessageBox.Show("插件未初始化成功，建议重启再试。", "发生错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 对变量置 null, 因为被关闭的窗口无法重复显示
            this._mainWindow = null;
        }
    }
}
