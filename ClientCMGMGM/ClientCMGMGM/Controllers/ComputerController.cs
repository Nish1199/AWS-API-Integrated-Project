using ClientCMGMGM.Models;
using ClientCMGMGM.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ClientCMGMGM.Controllers
{
    public class ComputerController : Microsoft.AspNetCore.Mvc.Controller
    {
        // GET: ComputerController
        [Route("/computer/index")]
        public ActionResult Index()
        {
           try
           {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/Computers");
                response.EnsureSuccessStatusCode();
                List<Models.Computer> computers = response.Content.ReadAsAsync<List<Models.Computer>>().Result;
                ViewBag.Title = "All Computers";
                return View(computers);
           }
            catch (Exception)
            {
               throw;
            }
        }

       
        // GET: ComputerController/Details/5
        public ActionResult Details(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("/api/Computers/" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.Computer products = response.Content.ReadAsAsync<Models.Computer>().Result;
            ViewBag.Title = "Computer Details";
            return View(products);
        }

        // GET: ComputerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComputerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Computer collection)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("/api/Computers", collection);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ComputerController/Edit/5
        public ActionResult Edit(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("/api/Computers/" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.Computer computer = response.Content.ReadAsAsync<Models.Computer>().Result;
            ViewBag.Title = "Edit Computer";
            return View(computer);
        }

        // POST: ComputerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Computer collection)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PutResponse("/api/Computers/"+collection.Sn, collection);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");                
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: ComputerController/Delete/5
        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("/api/Computers/" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.Computer products = response.Content.ReadAsAsync<Models.Computer>().Result;
            ViewBag.Title = "Delete Computer";
            return View(products);
        }

        // POST: ComputerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Computer collection)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.DeleteResponse("/api/Computers/" + id.ToString());
                response.EnsureSuccessStatusCode();               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
