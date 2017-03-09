using Ltracker.data;
using Ltracker.data.Entities;
using Ltracker.data.Repositories;
using Ltracker.Helpers;
using Ltracker.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Linq;

namespace Ltracker.Controllers
{
    public class CourseController : BaseController
    {
        // GET: Course
        public ActionResult Index()
        {
            ViewBag.Title = "Cursos List";
            var repository = new CourseRepository(context);
            var courses = repository.Query(null, "Name");
            var models = MapperHelper.Map<IEnumerable<CourseViewModel>>(courses);
            return View(models);
        }

        // GET: Course/Details/5
        public ActionResult Details(int id)
        {
            var repository = new CourseRepository(context);
            var includes = new Expression<Func<Course, object>>[] { x => x.Topics };
            var course = repository.QueryIncluding(x=>x.Id==id, includes ).FirstOrDefault();
            var model = MapperHelper.Map<CourseDetailViewModel>(course);
            return View(model);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            var model = new CourseViewModel();
            model.AvailableTopics = LoadTopics();
            return View(model);
        }

        private ICollection<TopicViewModel> LoadTopics()
        {
            var topicRepository = new TopicRepository(context);
            var topics = topicRepository.Query(null, "Name DESC");
            return MapperHelper.Map<ICollection<TopicViewModel>>(topics);
        }

        // POST: Course/Create
        [HttpPost]
        public ActionResult Create(CourseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var repository = new CourseRepository(context);
                    var course = MapperHelper.Map<Course>(model);
                    course.Topics = new List<Topic>();
                    var topicRepo = new TopicRepository(context);
                    foreach (var topicId in model.SelectedTopics)
                    {
                        course.Topics.Add(topicRepo.Find(topicId));
                    }
                    repository.Insert(course);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    model.AvailableTopics = LoadTopics();
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            var repository = new CourseRepository(context);
            var includes = new Expression<Func<Course, object>>[] { x => x.Topics };
            var course = repository.QueryIncluding(x => x.Id == id, includes).FirstOrDefault();
            var model = MapperHelper.Map<CourseViewModel>(course);
            model.AvailableTopics = LoadTopics();
            model.SelectedTopics = course.Topics.Select(x => x.Id.Value).ToArray();
            return View(model);
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CourseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var repository = new CourseRepository(context);
                    var course = MapperHelper.Map<Course>(model);
                    course.Topics = new List<Topic>();
                    var topicRepo = new TopicRepository(context);
                    foreach (var topicId in model.SelectedTopics)
                    {
                        course.Topics.Add(new Topic { Id= topicId});
                    }
                    repository.UpdateCourseWithTopics(course);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    model.AvailableTopics = LoadTopics();
                    return View(model);
                }
            }
            catch
            {
                model.AvailableTopics = LoadTopics();
                return View(model);
            }
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int id)
        {
            var repository = new CourseRepository(context);
            var course = repository.Find(id);
            var model = MapperHelper.Map<CourseViewModel>(course);
            return View(model);
        }

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CourseViewModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return new HttpNotFoundResult();
                }
                // TODO: Add delete logic here
                var repository = new CourseRepository(context);
                var course = repository.Find(model.Id);
                repository.Delete(course);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }
    }
}
