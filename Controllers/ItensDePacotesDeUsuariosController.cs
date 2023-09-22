﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HojeEuCaso.Controllers
{
    public class ItensDePacotesDeUsuariosController : Controller
    {
        // GET: ItensDePacotesDeUsuariosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ItensDePacotesDeUsuariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItensDePacotesDeUsuariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItensDePacotesDeUsuariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection) //Alterar para a entidade
        {
            try
            {
                TempData["SuccessMessage"] = "Salvo com sucesso!";
                return View();
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["ErrorMessage"] = "Ocorreu um erro!";
                return View();
            }
        }

        // GET: ItensDePacotesDeUsuariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItensDePacotesDeUsuariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItensDePacotesDeUsuariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ItensDePacotesDeUsuariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}