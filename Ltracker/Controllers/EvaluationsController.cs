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
    public class EvaluationsController : BaseController
    {      

        // GET: Evaluations/Edit/5
        public ActionResult Edit(int id)
        {
            var repository = new EvaluationRepository(context);
            var includes = new Expression<Func<Evaluation, object>>[] { x => x.AssignedCourse };
            var evaluation = repository.QueryIncluding(x => x.AssignedCourseId == id, includes).FirstOrDefault();
            var model = MapperHelper.Map<EditEvaluationsViewModel>(evaluation);
            return View(model);
        }

        // POST: Evaluations/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EditEvaluationsViewModel model)
        {
            var repository = new EvaluationRepository(context);
            try
            {
                if (ModelState.IsValid)
                {
                    var evaluationUpdate = MapperHelper.Map<Evaluation>(model);
                    repository.Update(evaluationUpdate);
                    context.SaveChanges();
                    var route = (new
                    {
                        controller = "Assignment",
                        action = "Index"
                    });
                    return RedirectToRoute(route);
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
            return View(model);
        }
      
    }
}
