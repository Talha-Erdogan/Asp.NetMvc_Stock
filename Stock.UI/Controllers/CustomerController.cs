﻿using Stock.Business;
using Stock.Data;
using Stock.UI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stock.UI.Controllers
{
    [AppAuthorize]
    public class CustomerController : Controller
    {
        #region Private Member
        private readonly CustomerService customerService;
        #endregion

        #region Constructor
        public CustomerController()
        {
            customerService = new CustomerService();
        }
        #endregion

        #region Customer List(Table) , Index
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            return this.Json(
            new
            {
                Result = (from cus in customerService.GetAll()
                          select new
                          {
                              Id = cus.Id,
                              Debt = cus.Debt,
                              Name = cus.Name,
                              Surname = cus.Surname,
                              Phone = cus.Phone,
                              Address = cus.Address,
                          })
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Add
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(Customer model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.RecordDate = DateTime.Now;
                    customerService.Add(model);
                    return Json("1");
                }
                else
                {
                    return Json("0");
                }
            }
            catch { return Json("0"); }
        }
        #endregion



        #region Edit
        [HttpPost]
        public JsonResult Edit (int id)
        {
            try
            {

                var Result = (from obj in customerService.GetAll().Where(x => x.Id == id)
                              select new
                              {
                                  Id = obj.Id,
                                  Debt = obj.Debt,
                                  Name = obj.Name,
                                  Surname = obj.Surname,
                                  Phone = obj.Phone,
                                  Address = obj.Address,
                              }).FirstOrDefault();

                return Json(Result);
            }
            catch { return Json("0"); }
        }
        [HttpPost]
        public JsonResult EditJSON(int id, string name, string surname, string phone, string address)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Customer model = new Customer();
                    model.Id = id;
                    model.Name = name;
                    model.Surname = surname;
                    model.Phone = phone;
                    model.Address = address;
                    model.RecordDate = DateTime.Now;
                    customerService.Update(model);
                    return Json("1");
                }
                else
                {
                    return Json("0");
                }
                //var musteri = DB.Musteri.Where(mus => mus.ID == id).FirstOrDefault();
                //musteri.Ad = ad;
                //musteri.Soyad = soyad;
                //musteri.Tel = tel;
                //musteri.Adres = adres;
                //DB.SaveChanges();
                
            }
            catch { return Json("0"); }
        }
        #endregion
    }
}