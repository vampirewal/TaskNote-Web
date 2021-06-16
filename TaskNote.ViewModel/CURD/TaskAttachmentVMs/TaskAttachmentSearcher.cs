using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskAttachmentVMs
{
    public partial class TaskAttachmentSearcher : BaseSearcher
    {
        public List<ComboSelectListItem> AlltaskModels { get; set; }
        [Display(Name = "关联任务")]
        public Guid? taskModelId { get; set; }

        protected override void InitVM()
        {
            AlltaskModels = DC.Set<TaskModel>().GetSelectListItems(Wtm, y => y.TaskName);
        }

    }
}
