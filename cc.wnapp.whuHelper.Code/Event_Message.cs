﻿using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.Model;
using RestSharp;
using System;
using System.IO;
using System.Net.Configuration;
using System.Threading;

namespace cc.wnapp.whuHelper.Code
{
    public class event_Message : IGroupMessage, IPrivateMessage
    {
        /// <summary>
        /// 群消息处理
        /// </summary>
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            /*
           CQ.Api.SendGroupMessage(e.FromGroup, "外部Call Api示例");
           CQ.Log.Debug("测试", "外部Call Log示例");
           // 获取 At 某人对象
           CQCode cqat = e.FromQQ.CQCode_At();
           // 往来源群发送一条群消息, 下列对象会合并成一个字符串发送
           e.FromGroup.SendGroupMessage(cqat, " 您发送了一条消息: ", e.Message);
           // 设置该属性, 表示阻塞本条消息, 该属性会在方法结束后传递给酷Q
           e.Handler = true;
            */

        }

        /// <summary>
        /// 私聊消息处理
        /// </summary>
        public void PrivateMessage(object sender, CQPrivateMessageEventArgs e)
        {
            QQ Botqq = CQ.Api.GetLoginQQ();
            string msg = e.Message;
            string fromqq = e.FromQQ;
            if (msg.Contains("绑定教务系统"))
            {
                var mp1 = new PrivateMsgProcess() { fromQQ = fromqq, message = msg, botQQ = Convert.ToString(Botqq.Id) };
                Thread t1 = new Thread(mp1.BindEasAccount);
                t1.Start();
            }


            //查询课程表模块
            if (msg.Contains("课程表"))
            {
                //Sudocat注：按上面写法改成线程方式调用
                msgProcess.QueryCourseTable(fromqq);
            }

            if (msg.Contains("查询功能菜单"))
            {
                //Sudocat注：放入处理函数中，按上面写法改成线程方式调用
                string menu =
                    "课程表查询菜单：\n" +
                    "1. 按课头号查询\n" +
                    "2. 按课程名查询\n" +
                    "3. 按学分查询\n" +
                    "4. 按授课学院查询\n" +
                    "5. 按专业查询\n" +
                    "6. 按授课教师查询\n" +
                    "请按指令格式查询：按{{查询模式}}查询 | {{查询关键字}}";
                CQ.Api.SendPrivateMessage(Convert.ToInt64(fromqq), menu);
            }

            if (msg.Contains("按") && msg.Contains("查询"))
            {
                //Sudocat注：按上面写法改成线程方式调用
                msgProcess.QueryFunction(fromqq, msg);
            }
        }
    }
}
