using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskModelVMs
{
    public partial class TaskModelSearcher : BaseSearcher
    {
        [Display(Name = "任务名称")]
        public String TaskName { get; set; }
        [Display(Name = "任务描述")]
        public String TaskDescription { get; set; }
        [Display(Name = "开始时间")]
        public DateRange StartTime { get; set; }
        [Display(Name = "结束时间")]
        public DateRange EndTime { get; set; }

        protected override void InitVM()
        {
        }

    }
}
