using ClientCMGMGM.Models;
using ClientCMGMGM.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientCMGMGM.Controllers
{
    public class UserController : Controller
    {

        // GET: UserController
        [Route("/user/index")]
        public ActionResult Index()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("/api/CompUsers");
                response.EnsureSuccessStatusCode();
                List<Models.User> Users = response.Content.ReadAsAsync<List<Models.User>>().Result;
                ViewBag.Title = "All Users";
                return View(Users);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // GET: UserController/Details/5
        public ActionResult Details(string id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("/api/CompUsers/" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.User products = response.Content.ReadAsAsync<Models.User>().Result;
            ViewBag.Title = "User Details";
            return View(products);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User collection)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/CompUsers", collection);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(string id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("/api/CompUsers/" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.User User = response.Content.ReadAsAsync<Models.User>().Result;
            ViewBag.Title = "Edit User";
            return View(User);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, User collection)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PutResponse("/api/CompUsers/" + collection.UserId, collection);
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(string id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("/api/CompUsers/" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.User products = response.Content.ReadAsAsync<Models.User>().Result;
            ViewBag.Title = "Delete User";
            return View(products);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, User collection)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.DeleteResponse("/api/CompUsers/" + id.ToString());
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
