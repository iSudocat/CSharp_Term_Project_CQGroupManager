﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{

    [DbConfigurationType(typeof(DbCommon.SQLiteConfiguration))]
    public class ScheduleContext : DbContext
    {
        public static string CurrentDirectory = "";
        public ScheduleContext() : base(new SQLiteConnection(@"Data Source=" + CurrentDirectory + @"\ScheduleDB.db;"), false)
        {

        }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<WeeklySchedule> WeeklySchedules { get; set; }

        
    }
    public static class InitializeDB
    {
        /// <summary>
        /// 初始化日程数据库信息
        /// </summary>
        public static void Init()
        {
            ScheduleContext.CurrentDirectory = System.Environment.CurrentDirectory + @"\data\app\cc.wnapp.whuHelper";

            using (var dbcontext = new ScheduleContext())
            {
                var objectContext = ((IObjectContextAdapter)dbcontext).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }

        }
    }
}

