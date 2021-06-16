using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using TaskNote.Controllers;
using TaskNote.ViewModel.CURD.TaskAttachmentVMs;
using TaskNote.Model;
using TaskNote.DataAccess;


namespace TaskNote.Test
{
    [TestClass]
    public class TaskAttachmentControllerTest
    {
        private TaskAttachmentController _controller;
        private string _seed;

        public TaskAttachmentControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<TaskAttachmentController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as TaskAttachmentListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(TaskAttachmentVM));

            TaskAttachmentVM vm = rv.Model as TaskAttachmentVM;
            TaskAttachment v = new TaskAttachment();
			
            v.taskModelId = AddTaskModel();
            v.FileId = AddFileAttachment();
            v.Order = 11;
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskAttachment>().Find(v.ID);
				
                Assert.AreEqual(data.Order, 11);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            TaskAttachment v = new TaskAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.taskModelId = AddTaskModel();
                v.FileId = AddFileAttachment();
                v.Order = 11;
                context.Set<TaskAttachment>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TaskAttachmentVM));

            TaskAttachmentVM vm = rv.Model as TaskAttachmentVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new TaskAttachment();
            v.ID = vm.Entity.ID;
       		
            v.Order = 19;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.taskModelId", "");
            vm.FC.Add("Entity.FileId", "");
            vm.FC.Add("Entity.Order", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskAttachment>().Find(v.ID);
 				
                Assert.AreEqual(data.Order, 19);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            TaskAttachment v = new TaskAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.taskModelId = AddTaskModel();
                v.FileId = AddFileAttachment();
                v.Order = 11;
                context.Set<TaskAttachment>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(TaskAttachmentVM));

            TaskAttachmentVM vm = rv.Model as TaskAttachmentVM;
            v = new TaskAttachment();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<TaskAttachment>().Find(v.ID);
                Assert.AreEqual(data, null);
            }

        }


        [TestMethod]
        public void DetailsTest()
        {
            TaskAttachment v = new TaskAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.taskModelId = AddTaskModel();
                v.FileId = AddFileAttachment();
                v.Order = 11;
                context.Set<TaskAttachment>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            TaskAttachment v1 = new TaskAttachment();
            TaskAttachment v2 = new TaskAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.taskModelId = AddTaskModel();
                v1.FileId = AddFileAttachment();
                v1.Order = 11;
                v2.taskModelId = v1.taskModelId; 
                v2.FileId = v1.FileId; 
                v2.Order = 19;
                context.Set<TaskAttachment>().Add(v1);
                context.Set<TaskAttachment>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(TaskAttachmentBatchVM));

            TaskAttachmentBatchVM vm = rv.Model as TaskAttachmentBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<TaskAttachment>().Find(v1.ID);
                var data2 = context.Set<TaskAttachment>().Find(v2.ID);
 				
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            TaskAttachment v1 = new TaskAttachment();
            TaskAttachment v2 = new TaskAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.taskModelId = AddTaskModel();
                v1.FileId = AddFileAttachment();
                v1.Order = 11;
                v2.taskModelId = v1.taskModelId; 
                v2.FileId = v1.FileId; 
                v2.Order = 19;
                context.Set<TaskAttachment>().Add(v1);
                context.Set<TaskAttachment>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(TaskAttachmentBatchVM));

            TaskAttachmentBatchVM vm = rv.Model as TaskAttachmentBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<TaskAttachment>().Find(v1.ID);
                var data2 = context.Set<TaskAttachment>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        private Guid AddTaskModel()
        {
            TaskModel v = new TaskModel();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.TaskName = "3bLS";
                v.TaskDescription = "lg2";
                v.NoFinishedTaskDtl = 71;
                v.FinishedTaskDtl = 17;
                context.Set<TaskModel>().Add(v);
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

                v.FileName = "RtmOq";
                v.FileExt = "DuDneAjKO";
                v.Path = "3qNW";
                v.Length = 59;
                v.SaveMode = "mXgS9EF";
                v.ExtraInfo = "051VCy";
                v.HandlerInfo = "pMXRfP7";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
