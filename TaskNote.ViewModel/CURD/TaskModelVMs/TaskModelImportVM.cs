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
    public partial class TaskModelTemplateVM : BaseTemplateVM
    {
        [Display(Name = "任务名称")]
        public ExcelPropety TaskName_Excel = ExcelPropety.CreateProperty<TaskModel>(x => x.TaskName);
        [Display(Name = "任务描述")]
        public ExcelPropety TaskDescription_Excel = ExcelPropety.CreateProperty<TaskModel>(x => x.TaskDescription);
        [Display(Name = "开始时间")]
        public ExcelPropety StartTime_Excel = ExcelPropety.CreateProperty<TaskModel>(x => x.StartTime);
        [Display(Name = "结束时间")]
        public ExcelPropety EndTime_Excel = ExcelPropety.CreateProperty<TaskModel>(x => x.EndTime);

	    protected override void InitVM()
        {
        }

    }

    public class TaskModelImportVM : BaseImportVM<TaskModelTemplateVM, TaskModel>
    {

    }

}
