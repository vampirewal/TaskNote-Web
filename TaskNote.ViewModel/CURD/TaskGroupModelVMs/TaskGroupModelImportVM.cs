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
    public partial class TaskGroupModelTemplateVM : BaseTemplateVM
    {
        [Display(Name = "任务组名称")]
        public ExcelPropety GroupName_Excel = ExcelPropety.CreateProperty<TaskGroupModel>(x => x.GroupName);
        [Display(Name = "任务组背景色")]
        public ExcelPropety GroupBackgroundColor_Excel = ExcelPropety.CreateProperty<TaskGroupModel>(x => x.GroupBackgroundColor);
        [Display(Name = "是否完成标记")]
        public ExcelPropety IsFinishedTag_Excel = ExcelPropety.CreateProperty<TaskGroupModel>(x => x.IsFinishedTag);
        [Display(Name = "任务组类型")]
        public ExcelPropety taskGroupType_Excel = ExcelPropety.CreateProperty<TaskGroupModel>(x => x.taskGroupType);

	    protected override void InitVM()
        {
        }

    }

    public class TaskGroupModelImportVM : BaseImportVM<TaskGroupModelTemplateVM, TaskGroupModel>
    {

    }

}
