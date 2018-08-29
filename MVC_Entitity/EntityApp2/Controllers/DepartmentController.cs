﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityApp2.Controllers
{
    public class DepartmentController : Base
    {
        // GET: Department
        public ActionResult Index()
        {
            return View(bll.Department().Listele());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(LogicLayer.Entities.Department model)
        {
            var isOk = bll.Department().Ekle(model);
            if (isOk)
                return RedirectToAction("Index");
            else
                return View();
        }
        public ActionResult Edit(int id)
        {
            return View(bll.Department().Listele().FirstOrDefault(x=>x.id==id));
        }
        [HttpPost]
        public ActionResult Edit(int id,LogicLayer.Entities.Department new_value)
        {
            var isOk = bll.Department().Güncelle(id, new_value);
            if (isOk)
                return RedirectToAction("Index");
            else
                return View();
        }
        public ActionResult Delete(int id)
        {
            return View(bll.Department().Listele().FirstOrDefault(x=>x.id == id));
        }
        [HttpPost]
        public ActionResult Delete(int id,LogicLayer.Entities.Department value)
        {
            var isOk = bll.Department().Sil(id);
            return RedirectToAction("Index");
        }
    }
}