using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskDtlModelVMs
{
    public partial class TaskDtlModelListVM : BasePagedListVM<TaskDtlModel_View, TaskDtlModelSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("TaskDtlModel", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskDtlModel", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskDtlModel", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskDtlModel", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskDtlModel", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskDtlModel", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskDtlModel", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskDtlModel", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "CURD"),
            };
        }


        protected override IEnumerable<IGridColumn<TaskDtlModel_View>> InitGridHeader()
        {
            return new List<GridColumn<TaskDtlModel_View>>{
                this.MakeGridHeader(x => x.TaskName_view),
                this.MakeGridHeader(x => x.GroupName_view),
                this.MakeGridHeader(x => x.TaskContext),
                this.MakeGridHeader(x => x.IsFinished),
                this.MakeGridHeader(x => x.AttachmentId).SetFormat(AttachmentIdFormat),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> AttachmentIdFormat(TaskDtlModel_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.AttachmentId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.AttachmentId,640,480),
            };
        }


        public override IOrderedQueryable<TaskDtlModel_View> GetSearchQuery()
        {
            var query = DC.Set<TaskDtlModel>()
                .CheckEqual(Searcher.taskId, x=>x.taskId)
                .CheckEqual(Searcher.taskGroupId, x=>x.taskGroupId)
                .CheckContain(Searcher.TaskContext, x=>x.TaskContext)
                .CheckEqual(Searcher.IsFinished, x=>x.IsFinished)
                .Select(x => new TaskDtlModel_View
                {
				    ID = x.ID,
                    TaskName_view = x.task.TaskName,
                    GroupName_view = x.taskGroup.GroupName,
                    TaskContext = x.TaskContext,
                    IsFinished = x.IsFinished,
                    AttachmentId = x.AttachmentId,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class TaskDtlModel_View : TaskDtlModel{
        [Display(Name = "任务名称")]
        public String TaskName_view { get; set; }
        [Display(Name = "任务组名称")]
        public String GroupName_view { get; set; }

    }
}
