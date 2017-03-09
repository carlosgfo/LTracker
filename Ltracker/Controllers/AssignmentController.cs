using Ltracker.data.Entities;
using Ltracker.data.Repositories;
using Ltracker.Helpers;
using Ltracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Ltracker.Controllers
{
    public class AssignmentController : BaseController
    {
        // GET: Assignment
        public ActionResult Index() //Lista de cursos (busqueda)
        {
            var repository = new AssignedCourseRepository(context);
            //Expression<>[]
            //Expression<Func<Type, bject>>[]{x=><x.propiedad}
            var includes = new Expression<Func<AssignedCourse, object>>[]
            {
                x=>x.Course, x=>x.Individual
            };
            var courses = repository.QueryIncluding(null, includes, "AssignmentDate");
            var models = MapperHelper.Map<ICollection<AssignmentViewModel>>(courses);
            return View(models);
        }

        //Assignment/Create
        public ActionResult Create()
        {
            var model = new NewAssignmentViewModel();
            model.CoursesList = PopulateCourses(model.CourseId);
            model.IndividualList = PopulateIndividuals(model.IndividualId);
            model.AssignmentDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(NewAssignmentViewModel model)
        {
            try
            {
                var repository = new AssignedCourseRepository(context);
                if (ModelState.IsValid)
                {
                    var assignedCourse = MapperHelper.Map<AssignedCourse>(model);
                    assignedCourse.IsCompleted = false;
                    Evaluation evaluation = new Evaluation();
                    repository.InsertAssignmentWithEvaluation(assignedCourse, evaluation);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                model.CoursesList = PopulateCourses();
                model.IndividualList = PopulateIndividuals();

                return View(model);
            }

            model.CoursesList = PopulateCourses();
            model.IndividualList = PopulateIndividuals();

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var repository = new AssignedCourseRepository(context);
            //Expression<>[]
            //Expression<Func<Type, bject>>[]{x=><x.propiedad}
            var includes = new Expression<Func<AssignedCourse, object>>[]
            {
                x=>x.Course, x=>x.Individual
            };
            var criteria = new AssignedCourse { Id = id };
            var course = repository.QueryByExampleIncludig(criteria, includes).SingleOrDefault();
            var models = MapperHelper.Map<EditAssignmentViewModel>(course);
            return View(models);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditAssignmentViewModel model)
        {
            var repository = new AssignedCourseRepository(context);
            try
            {
                if (ModelState.IsValid)
                {
                    var assignmentUpd = MapperHelper.Map<AssignedCourse>(model);
                    repository.Update(assignmentUpd);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
            return View(model);
        }

        public SelectList PopulateIndividuals(object selectedItem = null)
        {
            var repository = new IndividualRepository(context);
            var individuals = repository.Query(null, "Name").ToList();
            individuals.Insert(0, new Individual { Id = null, Name = "Seleccione" });
            return new SelectList(individuals, "Id", "Name", selectedItem);
        }

        public SelectList PopulateCourses(object selectedItem = null)
        {
            var repository = new CourseRepository(context);
            var course = repository.Query(null, "Name").ToList();
            course.Insert(0, new Course { Id = null, Name = "Seleccione" });
            return new SelectList(course, "Id", "Name", selectedItem);
        }
    }
}