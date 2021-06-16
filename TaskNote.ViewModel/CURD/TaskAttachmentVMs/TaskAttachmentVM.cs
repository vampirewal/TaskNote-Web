using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskAttachmentVMs
{
    public partial class TaskAttachmentVM : BaseCRUDVM<TaskAttachment>
    {
        public List<ComboSelectListItem> AlltaskModels { get; set; }

        public TaskAttachmentVM()
        {
            SetInclude(x => x.taskModel);
        }

        protected override void InitVM()
        {
            AlltaskModels = DC.Set<TaskModel>().GetSelectListItems(Wtm, y => y.TaskName);
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
