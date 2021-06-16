using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TaskNote.Model;


namespace TaskNote.ViewModel.CURD.TaskGroupModelVMs
{
    public partial class TaskGroupModelListVM : BasePagedListVM<TaskGroupModel_View, TaskGroupModelSearcher>
    {
        protected override List<GridAction> InitGridAction()
        {
            return new List<GridAction>
            {
                this.MakeStandardAction("TaskGroupModel", GridActionStandardTypesEnum.Create, Localizer["Sys.Create"],"CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskGroupModel", GridActionStandardTypesEnum.Edit, Localizer["Sys.Edit"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskGroupModel", GridActionStandardTypesEnum.Delete, Localizer["Sys.Delete"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskGroupModel", GridActionStandardTypesEnum.Details, Localizer["Sys.Details"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskGroupModel", GridActionStandardTypesEnum.BatchEdit, Localizer["Sys.BatchEdit"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskGroupModel", GridActionStandardTypesEnum.BatchDelete, Localizer["Sys.BatchDelete"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskGroupModel", GridActionStandardTypesEnum.Import, Localizer["Sys.Import"], "CURD", dialogWidth: 800),
                this.MakeStandardAction("TaskGroupModel", GridActionStandardTypesEnum.ExportExcel, Localizer["Sys.Export"], "CURD"),
            };
        }


        protected override IEnumerable<IGridColumn<TaskGroupModel_View>> InitGridHeader()
        {
            return new List<GridColumn<TaskGroupModel_View>>{
                this.MakeGridHeader(x => x.GroupName),
                this.MakeGridHeader(x => x.GroupBackgroundColor),
                this.MakeGridHeader(x => x.IsFinishedTag),
                this.MakeGridHeader(x => x.taskGroupType),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<TaskGroupModel_View> GetSearchQuery()
        {
            var query = DC.Set<TaskGroupModel>()
                .CheckContain(Searcher.GroupName, x=>x.GroupName)
                .CheckEqual(Searcher.IsFinishedTag, x=>x.IsFinishedTag)
                .CheckEqual(Searcher.taskGroupType, x=>x.taskGroupType)
                .Select(x => new TaskGroupModel_View
                {
				    ID = x.ID,
                    GroupName = x.GroupName,
                    GroupBackgroundColor = x.GroupBackgroundColor,
                    IsFinishedTag = x.IsFinishedTag,
                    taskGroupType = x.taskGroupType,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class TaskGroupModel_View : TaskGroupModel{

    }
}
