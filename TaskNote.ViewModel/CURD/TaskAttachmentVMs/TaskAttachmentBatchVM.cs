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
    public partial class TaskAttachmentBatchVM : BaseBatchVM<TaskAttachment, TaskAttachment_BatchEdit>
    {
        public TaskAttachmentBatchVM()
        {
            ListVM = new TaskAttachmentListVM();
            LinkedVM = new TaskAttachment_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class TaskAttachment_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
