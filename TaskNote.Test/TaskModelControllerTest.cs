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
			
            v.TaskName = "vivn5";
            v.TaskDescription = "8Q1j";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskModel>().Find(v.ID);
				
                Assert.AreEqual(data.TaskName, "vivn5");
                Assert.AreEqual(data.TaskDescription, "8Q1j");
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
       			
                v.TaskName = "vivn5";
                v.TaskDescription = "8Q1j";
                context.Set<TaskModel>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TaskModelVM));

            TaskModelVM vm = rv.Model as TaskModelVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new TaskModel();
            v.ID = vm.Entity.ID;
       		
            v.TaskName = "YG8hcE1J";
            v.TaskDescription = "370Gu40O";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.TaskName", "");
            vm.FC.Add("Entity.TaskDescription", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskModel>().Find(v.ID);
 				
                Assert.AreEqual(data.TaskName, "YG8hcE1J");
                Assert.AreEqual(data.TaskDescription, "370Gu40O");
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
        		
                v.TaskName = "vivn5";
                v.TaskDescription = "8Q1j";
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
				
                v.TaskName = "vivn5";
                v.TaskDescription = "8Q1j";
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
				
                v1.TaskName = "vivn5";
                v1.TaskDescription = "8Q1j";
                v2.TaskName = "YG8hcE1J";
                v2.TaskDescription = "370Gu40O";
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
				
                v1.TaskName = "vivn5";
                v1.TaskDescription = "8Q1j";
                v2.TaskName = "YG8hcE1J";
                v2.TaskDescription = "370Gu40O";
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
