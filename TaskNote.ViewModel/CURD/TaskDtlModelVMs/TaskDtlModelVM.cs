using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskDtlModelVMs
{
    public partial class TaskDtlModelVM : BaseCRUDVM<TaskDtlModel>
    {
        public List<ComboSelectListItem> Alltasks { get; set; }

        public TaskDtlModelVM()
        {
            SetInclude(x => x.task);
        }

        protected override void InitVM()
        {
            Alltasks = DC.Set<TaskModel>().GetSelectListItems(Wtm, y => y.TaskName);
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
