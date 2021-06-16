using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using TaskNote.Model;
using TaskNote.ViewModel.CURD.TaskDtlModelVMs;

namespace TaskNote.ViewModel.CURD.TaskModelVMs
{
    public partial class TaskModelVM : BaseCRUDVM<TaskModel>
    {
        public TaskDtlModelListVM TaskDtls { get; set; } = new TaskDtlModelListVM();



        public TaskModelVM()
        {

        }

        protected override void InitVM()
        {
            TaskDtls.CopyContext(this);
            TaskDtls.Searcher.taskId = Entity.ID;
        }
        
        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
