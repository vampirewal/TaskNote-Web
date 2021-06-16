using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskGroupModelVMs
{
    public partial class TaskGroupModelSearcher : BaseSearcher
    {
        [Display(Name = "任务组名称")]
        public String GroupName { get; set; }
        [Display(Name = "是否完成标记")]
        public Boolean? IsFinishedTag { get; set; }
        [Display(Name = "任务组类型")]
        public TaskGroupType? taskGroupType { get; set; }

        protected override void InitVM()
        {
        }

    }
}
