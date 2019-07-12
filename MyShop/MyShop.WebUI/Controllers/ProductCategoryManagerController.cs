﻿using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        // GET: ProductCategoryManager

        ProductCategoryRepository context;

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCatagoreis = context.Collection().ToList();
            return View(productCatagoreis);
        }

        public ActionResult Create()
        {
            ProductCategory productCatagory = new ProductCategory();
            return View(productCatagory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCatagory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCatagory);
            }
            else
            {
                context.Insert(productCatagory);
                context.commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCatagory = context.Find(Id);
            if (productCatagory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCatagory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCatagory, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCatagory);
                }
                productCategoryToEdit.Category = productCatagory.Category;


                context.commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategoryToDelete);
                }
                else
                {
                    context.Delete(Id);
                    context.commit();
                    return RedirectToAction("Index");
                }
            }


        }
    }
}