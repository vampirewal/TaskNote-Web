using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using TaskNote.Controllers;
using TaskNote.ViewModel.CURD.TaskGroupModelVMs;
using TaskNote.Model;
using TaskNote.DataAccess;


namespace TaskNote.Test
{
    [TestClass]
    public class TaskGroupModelControllerTest
    {
        private TaskGroupModelController _controller;
        private string _seed;

        public TaskGroupModelControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<TaskGroupModelController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as TaskGroupModelListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(TaskGroupModelVM));

            TaskGroupModelVM vm = rv.Model as TaskGroupModelVM;
            TaskGroupModel v = new TaskGroupModel();
			
            v.GroupName = "coF1";
            v.GroupBackgroundColor = "RcmpozGdj";
            v.IsFinishedTag = true;
            v.taskGroupType = TaskNote.Model.TaskGroupType.SystemCreate;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskGroupModel>().Find(v.ID);
				
                Assert.AreEqual(data.GroupName, "coF1");
                Assert.AreEqual(data.GroupBackgroundColor, "RcmpozGdj");
                Assert.AreEqual(data.IsFinishedTag, true);
                Assert.AreEqual(data.taskGroupType, TaskNote.Model.TaskGroupType.SystemCreate);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            TaskGroupModel v = new TaskGroupModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.GroupName = "coF1";
                v.GroupBackgroundColor = "RcmpozGdj";
                v.IsFinishedTag = true;
                v.taskGroupType = TaskNote.Model.TaskGroupType.SystemCreate;
                context.Set<TaskGroupModel>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TaskGroupModelVM));

            TaskGroupModelVM vm = rv.Model as TaskGroupModelVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new TaskGroupModel();
            v.ID = vm.Entity.ID;
       		
            v.GroupName = "2A8t15b";
            v.GroupBackgroundColor = "oOBVOdg";
            v.IsFinishedTag = true;
            v.taskGroupType = TaskNote.Model.TaskGroupType.SystemCreate;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.GroupName", "");
            vm.FC.Add("Entity.GroupBackgroundColor", "");
            vm.FC.Add("Entity.IsFinishedTag", "");
            vm.FC.Add("Entity.taskGroupType", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskGroupModel>().Find(v.ID);
 				
                Assert.AreEqual(data.GroupName, "2A8t15b");
                Assert.AreEqual(data.GroupBackgroundColor, "oOBVOdg");
                Assert.AreEqual(data.IsFinishedTag, true);
                Assert.AreEqual(data.taskGroupType, TaskNote.Model.TaskGroupType.SystemCreate);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            TaskGroupModel v = new TaskGroupModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.GroupName = "coF1";
                v.GroupBackgroundColor = "RcmpozGdj";
                v.IsFinishedTag = true;
                v.taskGroupType = TaskNote.Model.TaskGroupType.SystemCreate;
                context.Set<TaskGroupModel>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TaskGroupModelVM));

            TaskGroupModelVM vm = rv.Model as TaskGroupModelVM;
            v = new TaskGroupModel();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskGroupModel>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            TaskGroupModel v = new TaskGroupModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.GroupName = "coF1";
                v.GroupBackgroundColor = "RcmpozGdj";
                v.IsFinishedTag = true;
                v.taskGroupType = TaskNote.Model.TaskGroupType.SystemCreate;
                context.Set<TaskGroupModel>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            TaskGroupModel v1 = new TaskGroupModel();
            TaskGroupModel v2 = new TaskGroupModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.GroupName = "coF1";
                v1.GroupBackgroundColor = "RcmpozGdj";
                v1.IsFinishedTag = true;
                v1.taskGroupType = TaskNote.Model.TaskGroupType.SystemCreate;
                v2.GroupName = "2A8t15b";
                v2.GroupBackgroundColor = "oOBVOdg";
                v2.IsFinishedTag = true;
                v2.taskGroupType = TaskNote.Model.TaskGroupType.SystemCreate;
                context.Set<TaskGroupModel>().Add(v1);
                context.Set<TaskGroupModel>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(TaskGroupModelBatchVM));

            TaskGroupModelBatchVM vm = rv.Model as TaskGroupModelBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<TaskGroupModel>().Find(v1.ID);
                var data2 = context.Set<TaskGroupModel>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            TaskGroupModel v1 = new TaskGroupModel();
            TaskGroupModel v2 = new TaskGroupModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.GroupName = "coF1";
                v1.GroupBackgroundColor = "RcmpozGdj";
                v1.IsFinishedTag = true;
                v1.taskGroupType = TaskNote.Model.TaskGroupType.SystemCreate;
                v2.GroupName = "2A8t15b";
                v2.GroupBackgroundColor = "oOBVOdg";
                v2.IsFinishedTag = true;
                v2.taskGroupType = TaskNote.Model.TaskGroupType.SystemCreate;
                context.Set<TaskGroupModel>().Add(v1);
                context.Set<TaskGroupModel>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(TaskGroupModelBatchVM));

            TaskGroupModelBatchVM vm = rv.Model as TaskGroupModelBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<TaskGroupModel>().Find(v1.ID);
                var data2 = context.Set<TaskGroupModel>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as TaskGroupModelListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
