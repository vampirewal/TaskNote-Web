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
    public partial class TaskAttachmentTemplateVM : BaseTemplateVM
    {

	    protected override void InitVM()
        {
        }

    }

    public class TaskAttachmentImportVM : BaseImportVM<TaskAttachmentTemplateVM, TaskAttachment>
    {

    }

}
