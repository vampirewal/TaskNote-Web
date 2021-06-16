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
    public partial class TaskModelBatchVM : BaseBatchVM<TaskModel, TaskModel_BatchEdit>
    {
        public TaskModelBatchVM()
        {
            ListVM = new TaskModelListVM();
            LinkedVM = new TaskModel_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class TaskModel_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
