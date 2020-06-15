﻿using Native.Sdk.Cqp.EventArgs;

namespace cc.wnapp.whuHelper.Code.CommandRouter
{
    public abstract class AbstractCommand
    {
        ///<summary>事件类型</summary>
        public EventType EventType { get; set; }
        
        ///<summary>真实事件类型</summary>
        public EventType ActualEventType { get; set; }

        ///<summary>匹配模式</summary>
        public MatchType MatchType { get; set; }

        ///<summary>匹配字符串</summary>
        public string MatchStr { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public CQEventEventArgs CQEventArgsArgs { get; set; }

        /// <summary>
        /// 指令的执行逻辑
        /// </summary>
        /// <returns>0:忽略 1:拦截</returns>
        public abstract int Handle();
    }
}