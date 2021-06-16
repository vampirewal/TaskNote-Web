using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using TaskNote.Model;
using TaskNote.ViewModel.CURD.TaskDtlModelVMs;
using TaskNote.ViewModel.CURD.TaskAttachmentVMs;

namespace TaskNote.ViewModel.CURD.TaskModelVMs
{
    public partial class TaskModelVM : BaseCRUDVM<TaskModel>
    {
        public TaskDtlModelListVM TaskDtl { get; set; } = new TaskDtlModelListVM();

        public TaskAttachmentListVM TaskAttachment { get; set; } = new TaskAttachmentListVM();

        public TaskModelVM()
        {
        }

        protected override void InitVM()
        {
            TaskDtl.CopyContext(this);
            TaskDtl.Searcher.taskId = Entity.ID;

            TaskAttachment.CopyContext(this);
            TaskAttachment.Searcher.taskModelId = Entity.ID;
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
