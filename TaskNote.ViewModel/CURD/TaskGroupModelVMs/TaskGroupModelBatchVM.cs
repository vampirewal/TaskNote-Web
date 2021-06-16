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
    public partial class TaskGroupModelBatchVM : BaseBatchVM<TaskGroupModel, TaskGroupModel_BatchEdit>
    {
        public TaskGroupModelBatchVM()
        {
            ListVM = new TaskGroupModelListVM();
            LinkedVM = new TaskGroupModel_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class TaskGroupModel_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
