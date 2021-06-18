using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskModelVMs
{
    public partial class TaskModelListVM : BasePagedListVM<TaskModel_View, TaskModelSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("TaskModel", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskModel", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskModel", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskModel", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "CURD", dialogWidth: 1000,dialogHeight:600),
                this.MakeStandardAction("TaskModel", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskModel", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskModel", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskModel", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "CURD"),
            };
        }


        protected override IEnumerable<IGridColumn<TaskModel_View>> InitGridHeader()
        {
            return new List<GridColumn<TaskModel_View>>{
                this.MakeGridHeader(x => x.TaskName),
                this.MakeGridHeader(x => x.TaskDescription),
                this.MakeGridHeader(x => x.StartTime).SetWidth(width:100),
                this.MakeGridHeader(x => x.EndTime).SetWidth(width:100),
                this.MakeGridHeader(x => x.NoFinishedTaskDtl),
                this.MakeGridHeader(x => x.FinishedTaskDtl),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<TaskModel_View> GetSearchQuery()
        {
            var query = DC.Set<TaskModel>()
                .CheckContain(Searcher.TaskName, x=>x.TaskName)
                .CheckContain(Searcher.TaskDescription, x=>x.TaskDescription)
                .CheckBetween(Searcher.StartTime?.GetStartTime(), Searcher.StartTime?.GetEndTime(), x => x.StartTime, includeMax: false)
                .CheckBetween(Searcher.EndTime?.GetStartTime(), Searcher.EndTime?.GetEndTime(), x => x.EndTime, includeMax: false)
                .CheckEqual(Searcher.NoFinishedTaskDtl, x=>x.NoFinishedTaskDtl)
                .CheckEqual(Searcher.FinishedTaskDtl, x=>x.FinishedTaskDtl)
                .Select(x => new TaskModel_View
                {
				    ID = x.ID,
                    TaskName = x.TaskName,
                    TaskDescription = x.TaskDescription,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    NoFinishedTaskDtl = x.NoFinishedTaskDtl,
                    FinishedTaskDtl = x.FinishedTaskDtl,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class TaskModel_View : TaskModel{

    }
}
