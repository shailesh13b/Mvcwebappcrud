using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuickKartDataAccessLayer;
using QuickKartDataAccessLayer.Models;

namespace quickkart2.Controllers
{
    public class ProductController : Controller
    {
        private readonly QuickKartContext _context;
        QuickKartRepository repObj;

        private readonly IMapper _mapper;

        public ProductController(QuickKartContext context, IMapper mapper)
        {
            _context = context;
            repObj = new QuickKartRepository(_context);
            _mapper = mapper;
        }

        public IActionResult ViewProducts()
        {
            var lstEntityProducts = repObj.GetProducts();
            List<Models.Product> lstModelProducts = new List<Models.Product>();
            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.Product>(product));
            }
            return View(lstModelProducts);
        }

        public IActionResult AddProduct()
        {
            string productId = repObj.GetNextProductId();
            ViewBag.NextProductId = productId;
            return View();
        }

        [HttpPost]
        public IActionResult SaveAddedProduct(Models.Product product)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {

                    status = repObj.AddProduct(_mapper.Map<Products>(product));
                    if (status)
                        return RedirectToAction("ViewProducts");
                    else
                        return View("Error");
                }
                catch (Exception)
                {

                    return View("Error");
                }
            }
            else return View("AddProduct", product);
        }

        public IActionResult UpdateProduct(Models.Product prodObj)
        {
                TempData["OldPrice"] = prodObj.Price;
                return View(prodObj);
        }

            [HttpPost]
        public IActionResult SaveUpdatedProduct(Models.Product product)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    status = repObj.UpdateProduct(_mapper.Map<Products>(product));
                    if (status)
                        return RedirectToAction("ViewProducts");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("UpdateProduct", product);
        }

        public IActionResult DeleteProduct(Models.Product product)
        {
            return View(product);
        }

        [HttpPost]
        public IActionResult SaveDeletion(string productId)
        {
            bool status = false;
            try
            {
                status = repObj.DeleteProduct(productId);
                if (status)
                    return RedirectToAction("ViewProducts");
                else
                    return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


    }
}
