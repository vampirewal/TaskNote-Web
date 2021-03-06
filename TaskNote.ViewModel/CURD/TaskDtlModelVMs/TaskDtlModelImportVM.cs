using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskDtlModelVMs
{
    public partial class TaskDtlModelTemplateVM : BaseTemplateVM
    {
        [Display(Name = "关联任务")]
        public ExcelPropety task_Excel = ExcelPropety.CreateProperty<TaskDtlModel>(x => x.taskId);
        [Display(Name = "任务情况")]
        public ExcelPropety taskGroup_Excel = ExcelPropety.CreateProperty<TaskDtlModel>(x => x.taskGroup);
        [Display(Name = "内容")]
        public ExcelPropety TaskContext_Excel = ExcelPropety.CreateProperty<TaskDtlModel>(x => x.TaskContext);
        [Display(Name = "是否完成")]
        public ExcelPropety IsFinished_Excel = ExcelPropety.CreateProperty<TaskDtlModel>(x => x.IsFinished);

	    protected override void InitVM()
        {
            task_Excel.DataType = ColumnDataType.ComboBox;
            task_Excel.ListItems = DC.Set<TaskModel>().GetSelectListItems(Wtm, y => y.TaskName);
        }

    }

    public class TaskDtlModelImportVM : BaseImportVM<TaskDtlModelTemplateVM, TaskDtlModel>
    {

    }

}
