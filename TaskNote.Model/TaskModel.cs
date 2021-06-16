﻿#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：Task
// 创 建 者：杨程
// 创建时间：2021/6/15 16:16:33
// 文件版本：V1.0.0
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace TaskNote.Model
{
    [Table("Tasks")]
    public class TaskModel:TopBasePoco,IBasePoco,IPersistPoco
    {
        

        #region 属性
        [Display(Name = "任务名称")]
        public string TaskName { get; set; }
        [Display(Name = "任务描述")]
        public string TaskDescription { get; set; }
        [Display(Name = "开始时间")]
        public DateTime StartTime { get; set; }
        [Display(Name = "结束时间")]
        public DateTime EndTime { get; set; }

        public List<FileAttachment> Attachments { get; set; } = new List<FileAttachment>();
        #endregion

        #region 接口属性
        [Display(Name ="创建时间")]
        public DateTime? CreateTime { get ; set ; }
        [Display(Name = "创建人")]
        public string CreateBy { get ; set ; }
        [Display(Name = "修改时间")]
        public DateTime? UpdateTime { get ; set ; }
        [Display(Name = "修改人")]
        public string UpdateBy { get ; set ; }
        [Display(Name = "是否删除")]
        public bool IsValid { get ; set ; }
        #endregion

    }
}
