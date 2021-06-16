using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using TaskNote.Controllers;
using TaskNote.ViewModel.CURD.TaskDtlModelVMs;
using TaskNote.Model;
using TaskNote.DataAccess;


namespace TaskNote.Test
{
    [TestClass]
    public class TaskDtlModelControllerTest
    {
        private TaskDtlModelController _controller;
        private string _seed;

        public TaskDtlModelControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<TaskDtlModelController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as TaskDtlModelListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(TaskDtlModelVM));

            TaskDtlModelVM vm = rv.Model as TaskDtlModelVM;
            TaskDtlModel v = new TaskDtlModel();
			
            v.taskId = AddTaskModel();
            v.taskGroupId = AddTaskGroupModel();
            v.TaskContext = "orS03ZI";
            v.IsFinished = true;
            v.AttachmentId = AddFileAttachment();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskDtlModel>().Find(v.ID);
				
                Assert.AreEqual(data.TaskContext, "orS03ZI");
                Assert.AreEqual(data.IsFinished, true);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            TaskDtlModel v = new TaskDtlModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.taskId = AddTaskModel();
                v.taskGroupId = AddTaskGroupModel();
                v.TaskContext = "orS03ZI";
                v.IsFinished = true;
                v.AttachmentId = AddFileAttachment();
                context.Set<TaskDtlModel>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TaskDtlModelVM));

            TaskDtlModelVM vm = rv.Model as TaskDtlModelVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new TaskDtlModel();
            v.ID = vm.Entity.ID;
       		
            v.TaskContext = "G5JGMv";
            v.IsFinished = false;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.taskId", "");
            vm.FC.Add("Entity.taskGroupId", "");
            vm.FC.Add("Entity.TaskContext", "");
            vm.FC.Add("Entity.IsFinished", "");
            vm.FC.Add("Entity.AttachmentId", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskDtlModel>().Find(v.ID);
 				
                Assert.AreEqual(data.TaskContext, "G5JGMv");
                Assert.AreEqual(data.IsFinished, false);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            TaskDtlModel v = new TaskDtlModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.taskId = AddTaskModel();
                v.taskGroupId = AddTaskGroupModel();
                v.TaskContext = "orS03ZI";
                v.IsFinished = true;
                v.AttachmentId = AddFileAttachment();
                context.Set<TaskDtlModel>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TaskDtlModelVM));

            TaskDtlModelVM vm = rv.Model as TaskDtlModelVM;
            v = new TaskDtlModel();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskDtlModel>().Find(v.ID);
                Assert.AreEqual(data.IsValid, false);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            TaskDtlModel v = new TaskDtlModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.taskId = AddTaskModel();
                v.taskGroupId = AddTaskGroupModel();
                v.TaskContext = "orS03ZI";
                v.IsFinished = true;
                v.AttachmentId = AddFileAttachment();
                context.Set<TaskDtlModel>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            TaskDtlModel v1 = new TaskDtlModel();
            TaskDtlModel v2 = new TaskDtlModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.taskId = AddTaskModel();
                v1.taskGroupId = AddTaskGroupModel();
                v1.TaskContext = "orS03ZI";
                v1.IsFinished = true;
                v1.AttachmentId = AddFileAttachment();
                v2.taskId = v1.taskId; 
                v2.taskGroupId = v1.taskGroupId; 
                v2.TaskContext = "G5JGMv";
                v2.IsFinished = false;
                v2.AttachmentId = v1.AttachmentId; 
                context.Set<TaskDtlModel>().Add(v1);
                context.Set<TaskDtlModel>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(TaskDtlModelBatchVM));

            TaskDtlModelBatchVM vm = rv.Model as TaskDtlModelBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.LinkedVM.IsFinished = false;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("LinkedVM.IsFinished", "");
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<TaskDtlModel>().Find(v1.ID);
                var data2 = context.Set<TaskDtlModel>().Find(v2.ID);
 				
                Assert.AreEqual(data1.IsFinished, false);
                Assert.AreEqual(data2.IsFinished, false);
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            TaskDtlModel v1 = new TaskDtlModel();
            TaskDtlModel v2 = new TaskDtlModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.taskId = AddTaskModel();
                v1.taskGroupId = AddTaskGroupModel();
                v1.TaskContext = "orS03ZI";
                v1.IsFinished = true;
                v1.AttachmentId = AddFileAttachment();
                v2.taskId = v1.taskId; 
                v2.taskGroupId = v1.taskGroupId; 
                v2.TaskContext = "G5JGMv";
                v2.IsFinished = false;
                v2.AttachmentId = v1.AttachmentId; 
                context.Set<TaskDtlModel>().Add(v1);
                context.Set<TaskDtlModel>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(TaskDtlModelBatchVM));

            TaskDtlModelBatchVM vm = rv.Model as TaskDtlModelBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<TaskDtlModel>().Find(v1.ID);
                var data2 = context.Set<TaskDtlModel>().Find(v2.ID);
                Assert.AreEqual(data1.IsValid, false);
            Assert.AreEqual(data2.IsValid, false);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as TaskDtlModelListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddTaskModel()
        {
            TaskModel v = new TaskModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.TaskName = "iTWD4TakN";
                v.TaskDescription = "1I9b";
                context.Set<TaskModel>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddTaskGroupModel()
        {
            TaskGroupModel v = new TaskGroupModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.GroupName = "fpEkc";
                v.GroupBackgroundColor = "POiD7jd";
                v.IsFinishedTag = true;
                v.taskGroupType = TaskNote.Model.TaskGroupType.SystemCreate;
                context.Set<TaskGroupModel>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "U5Zr5gTi";
                v.FileExt = "rk1IXzeuT";
                v.Path = "jGXL";
                v.Length = 25;
                v.SaveMode = "vMQxB0m";
                v.ExtraInfo = "cR8CLfwI";
                v.HandlerInfo = "LaKzJhkRL";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
