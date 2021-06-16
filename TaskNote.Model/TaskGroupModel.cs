#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：TaskGroupModel
// 创 建 者：杨程
// 创建时间：2021/6/15 16:33:19
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
    [Table("TaskGroups")]
    public class TaskGroupModel : TopBasePoco, IBasePoco
    {
        public TaskGroupModel()
        {
            //构造函数
        }

        #region 属性
        [Display(Name = "任务组名称")]
        public string GroupName { get; set; }
        [Display(Name = "任务组背景色")]
        public string GroupBackgroundColor { get; set; }
        [Display(Name = "是否完成标记")]
        public bool IsFinishedTag { get; set; }
        [Display(Name = "任务组类型")]
        public TaskGroupType taskGroupType { get; set; }
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
        
        #endregion
    }

    public enum TaskGroupType
    {
        [Display(Name = "系统创建")]
        SystemCreate = 0,
        [Display(Name = "用户创建")]
        CustomCreate = 1
    }
}
