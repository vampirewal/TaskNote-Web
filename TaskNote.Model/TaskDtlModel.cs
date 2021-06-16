#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：TaskDtlModel
// 创 建 者：杨程
// 创建时间：2021/6/15 16:28:09
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
    [Table("TaskDtls")]
    public class TaskDtlModel : TopBasePoco, IBasePoco, IPersistPoco
    {


        #region 属性
        [Display(Name = "关联任务")]
        public Guid? taskId { get; set; }
        [Display(Name = "关联任务")]
        public TaskModel task { get; set; }
        [Display(Name = "任务情况")]
        public TaskGroup taskGroup { get; set; }

        [Display(Name = "内容")]
        public string TaskContext { get; set; }
        [Display(Name = "是否完成")]
        public bool IsFinished { get; set; }
        //[Display(Name = "附件")]
        //public Guid? AttachmentId { get; set; }
        //[Display(Name = "附件")]
        //public FileAttachment Attachment { get; set; }
        #endregion

        #region 接口属性
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }
        [Display(Name = "创建人")]
        public string CreateBy { get; set; }
        [Display(Name = "修改时间")]
        public DateTime? UpdateTime { get; set; }
        [Display(Name = "修改人")]
        public string UpdateBy { get; set; }
        [Display(Name = "是否删除")]
        public bool IsValid { get; set; }
        #endregion
    }

    public enum TaskGroup
    {
        [Display(Name = "准备去做")]
        ToDo =0,
        [Display(Name = "正在做")]
        Doing =1,
        [Display(Name = "已完成")]
        Done =2
    }
}
