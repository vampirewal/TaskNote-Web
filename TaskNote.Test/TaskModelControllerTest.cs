using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using TaskNote.Controllers;
using TaskNote.ViewModel.CURD.TaskModelVMs;
using TaskNote.Model;
using TaskNote.DataAccess;


namespace TaskNote.Test
{
    [TestClass]
    public class TaskModelControllerTest
    {
        private TaskModelController _controller;
        private string _seed;

        public TaskModelControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<TaskModelController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as TaskModelListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(TaskModelVM));

            TaskModelVM vm = rv.Model as TaskModelVM;
            TaskModel v = new TaskModel();
			
            v.TaskName = "GZsrmM";
            v.TaskDescription = "LTGDgJnyF";
            v.NoFinishedTaskDtl = 1;
            v.FinishedTaskDtl = 93;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskModel>().Find(v.ID);
				
                Assert.AreEqual(data.TaskName, "GZsrmM");
                Assert.AreEqual(data.TaskDescription, "LTGDgJnyF");
                Assert.AreEqual(data.NoFinishedTaskDtl, 1);
                Assert.AreEqual(data.FinishedTaskDtl, 93);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            TaskModel v = new TaskModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.TaskName = "GZsrmM";
                v.TaskDescription = "LTGDgJnyF";
                v.NoFinishedTaskDtl = 1;
                v.FinishedTaskDtl = 93;
                context.Set<TaskModel>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TaskModelVM));

            TaskModelVM vm = rv.Model as TaskModelVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new TaskModel();
            v.ID = vm.Entity.ID;
       		
            v.TaskName = "qYGEokVCG";
            v.TaskDescription = "mYkPpqd3";
            v.NoFinishedTaskDtl = 72;
            v.FinishedTaskDtl = 74;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.TaskName", "");
            vm.FC.Add("Entity.TaskDescription", "");
            vm.FC.Add("Entity.NoFinishedTaskDtl", "");
            vm.FC.Add("Entity.FinishedTaskDtl", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskModel>().Find(v.ID);
 				
                Assert.AreEqual(data.TaskName, "qYGEokVCG");
                Assert.AreEqual(data.TaskDescription, "mYkPpqd3");
                Assert.AreEqual(data.NoFinishedTaskDtl, 72);
                Assert.AreEqual(data.FinishedTaskDtl, 74);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            TaskModel v = new TaskModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.TaskName = "GZsrmM";
                v.TaskDescription = "LTGDgJnyF";
                v.NoFinishedTaskDtl = 1;
                v.FinishedTaskDtl = 93;
                context.Set<TaskModel>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TaskModelVM));

            TaskModelVM vm = rv.Model as TaskModelVM;
            v = new TaskModel();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskModel>().Find(v.ID);
                Assert.AreEqual(data.IsValid, false);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            TaskModel v = new TaskModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.TaskName = "GZsrmM";
                v.TaskDescription = "LTGDgJnyF";
                v.NoFinishedTaskDtl = 1;
                v.FinishedTaskDtl = 93;
                context.Set<TaskModel>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            TaskModel v1 = new TaskModel();
            TaskModel v2 = new TaskModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.TaskName = "GZsrmM";
                v1.TaskDescription = "LTGDgJnyF";
                v1.NoFinishedTaskDtl = 1;
                v1.FinishedTaskDtl = 93;
                v2.TaskName = "qYGEokVCG";
                v2.TaskDescription = "mYkPpqd3";
                v2.NoFinishedTaskDtl = 72;
                v2.FinishedTaskDtl = 74;
                context.Set<TaskModel>().Add(v1);
                context.Set<TaskModel>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(TaskModelBatchVM));

            TaskModelBatchVM vm = rv.Model as TaskModelBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<TaskModel>().Find(v1.ID);
                var data2 = context.Set<TaskModel>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            TaskModel v1 = new TaskModel();
            TaskModel v2 = new TaskModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.TaskName = "GZsrmM";
                v1.TaskDescription = "LTGDgJnyF";
                v1.NoFinishedTaskDtl = 1;
                v1.FinishedTaskDtl = 93;
                v2.TaskName = "qYGEokVCG";
                v2.TaskDescription = "mYkPpqd3";
                v2.NoFinishedTaskDtl = 72;
                v2.FinishedTaskDtl = 74;
                context.Set<TaskModel>().Add(v1);
                context.Set<TaskModel>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(TaskModelBatchVM));

            TaskModelBatchVM vm = rv.Model as TaskModelBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<TaskModel>().Find(v1.ID);
                var data2 = context.Set<TaskModel>().Find(v2.ID);
                Assert.AreEqual(data1.IsValid, false);
            Assert.AreEqual(data2.IsValid, false);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as TaskModelListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
