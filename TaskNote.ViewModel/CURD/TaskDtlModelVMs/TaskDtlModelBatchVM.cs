using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskDtlModelVMs
{
    public partial class TaskDtlModelBatchVM : BaseBatchVM<TaskDtlModel, TaskDtlModel_BatchEdit>
    {
        public TaskDtlModelBatchVM()
        {
            ListVM = new TaskDtlModelListVM();
            LinkedVM = new TaskDtlModel_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class TaskDtlModel_BatchEdit : BaseVM
    {
        public List<ComboSelectListItem> AlltaskGroups { get; set; }
        [Display(Name = "关联任务组")]
        public Guid? taskGroupId { get; set; }
        [Display(Name = "是否完成")]
        public Boolean? IsFinished { get; set; }

        protected override void InitVM()
        {
            AlltaskGroups = DC.Set<TaskGroupModel>().GetSelectListItems(Wtm, y => y.GroupName);
        }

    }

}
