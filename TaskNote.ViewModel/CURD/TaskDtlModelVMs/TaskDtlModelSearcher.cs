﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskDtlModelVMs
{
    public partial class TaskDtlModelSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> Alltasks { get; set; }
        [Display(Name = "关联任务")]
        public Guid? taskId { get; set; }
        public List<ComboSelectListItem> AlltaskGroups { get; set; }
        [Display(Name = "关联任务组")]
        public Guid? taskGroupId { get; set; }
        [Display(Name = "内容")]
        public String TaskContext { get; set; }
        [Display(Name = "是否完成")]
        public Boolean? IsFinished { get; set; }

        protected override void InitVM()
        {
            Alltasks = DC.Set<TaskModel>().GetSelectListItems(Wtm, y => y.TaskName);
            AlltaskGroups = DC.Set<TaskGroupModel>().GetSelectListItems(Wtm, y => y.GroupName);
        }

    }
}
