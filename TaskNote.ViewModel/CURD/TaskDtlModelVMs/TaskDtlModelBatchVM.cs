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

        protected override void InitVM()
        {
        }

    }

}
