using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskAttachmentVMs
{
    public partial class TaskAttachmentListVM : BasePagedListVM<TaskAttachment_View, TaskAttachmentSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("TaskAttachment", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskAttachment", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskAttachment", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskAttachment", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskAttachment", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskAttachment", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskAttachment", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskAttachment", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "CURD"),
            };
        }


        protected override IEnumerable<IGridColumn<TaskAttachment_View>> InitGridHeader()
        {
            return new List<GridColumn<TaskAttachment_View>>{
                this.MakeGridHeader(x => x.TaskName_view),
                this.MakeGridHeader(x => x.FileId).SetFormat(FileIdFormat),
                this.MakeGridHeader(x => x.Order),
                this.MakeGridHeaderAction(width: 200)
            };
        }
        private List<ColumnFormatInfo> FileIdFormat(TaskAttachment_View entity, object val)
        {
            return new List<ColumnFormatInfo>
            {
                ColumnFormatInfo.MakeDownloadButton(ButtonTypesEnum.Button,entity.FileId),
                ColumnFormatInfo.MakeViewButton(ButtonTypesEnum.Button,entity.FileId,640,480),
            };
        }


        public override IOrderedQueryable<TaskAttachment_View> GetSearchQuery()
        {
            var query = DC.Set<TaskAttachment>()
                .CheckEqual(Searcher.taskModelId, x=>x.taskModelId)
                .Select(x => new TaskAttachment_View
                {
				    ID = x.ID,
                    TaskName_view = x.taskModel.TaskName,
                    FileId = x.FileId,
                    Order = x.Order,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class TaskAttachment_View : TaskAttachment{
        [Display(Name = "任务名称")]
        public String TaskName_view { get; set; }

    }
}
