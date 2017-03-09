using Ltracker.data;
using Ltracker.data.Repositories;
using Ltracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ltracker.Helpers;
using Ltracker.data.Entities;

namespace Ltracker.Controllers
{
    public class IndividualController : BaseController
    {
        // GET: Individual
        /// <summary>
        /// Lista de individuals y se la va a pasar a la vista Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                var repository = new IndividualRepository(context);
                var individuals = repository.GetAll();
                var models = MapperHelper.Map<IEnumerable<IndividualViewModel>>(individuals);
                return View(models);
            }
            catch(Exception ex)
            {
                var model = new IndividualViewModel();
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }

        // GET: Individual/Details/5
        public ActionResult Details(int id)
        {
            var repository = new IndividualRepository(context);
            var individual = repository.Find(id);
            var model = MapperHelper.Map<IndividualViewModel>(individual);
           
            return View(model);
        }

        // GET: Individual/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Individual/Create
        [HttpPost]
        public ActionResult Create(IndividualViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var repository = new IndividualRepository(context);
                    var emailExiste = repository.Query(x => x.Email == model.Email).Count > 0;
                    if (!emailExiste)
                    {
                        var individual = MapperHelper.Map<Individual>(model);
                        repository.Insert(individual);
                        context.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "El correo electrónico esta ocupado");
                        return View(model);
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Individual/Edit/5
        public ActionResult Edit(int id)
        {
            var repository = new IndividualRepository(context);
            var individual = repository.Find(id);
            var model = MapperHelper.Map<IndividualViewModel>(individual);
            model.EmailAnterior = individual.Email;
            return View(model);
        }

        // POST: Individual/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IndividualViewModel model)
        {
            try
            {
                var repository = new IndividualRepository(context);
                if (ModelState.IsValid)
                {
                    if (model.Email != model.EmailAnterior)
                    {
                        var emailExiste = repository.Query(x => x.Email == model.Email).Count > 0;
                        if (!emailExiste)
                        {
                            var individual = MapperHelper.Map<Individual>(model);
                            repository.Update(individual);
                            context.SaveChanges();
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "El correo electrónico esta ocupado");
                            return View(model);
                        }
                    }
                    else
                    {

                        var individual = MapperHelper.Map<Individual>(model);
                        repository.Update(individual);
                        context.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Individual/Delete/5
        public ActionResult Delete(int id)
        {
            var repository = new IndividualRepository(context);
            var individual = repository.Find(id);
            var model = MapperHelper.Map<IndividualViewModel>(individual);
            return View(model);
        }

        // POST: Individual/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IndividualViewModel model)
        {
            try
            {
                var repository = new IndividualRepository(context);
                var individual = MapperHelper.Map<Individual>(model);
                repository.Delete(individual);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [AllowAnonymous]
        public JsonResult CheckEmail(string email)
        {
            var repository = new IndividualRepository(context);
            var emailExiste = repository.Query(x => x.Email == email).Count == 0;
            return Json(emailExiste, JsonRequestBehavior.AllowGet);
        }
    }
}