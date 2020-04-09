using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeestjeOpJeFeestje.Core;
using BeestjeOpJeFeestje.Core.Interfaces;
using BeestjeOpJeFeestje.Infrastructure;

namespace BeestjeOpJeFeestje.Controllers
{
    public class AccessoriesController : Controller
    {
        private IAccesoryRepository dbAccessory;
        private IAnimalRepository dbAnimal;

        public AccessoriesController(IAccesoryRepository db1, IAnimalRepository db2)
        {
            dbAccessory = db1;
            dbAnimal = db2;
        }
        // GET: Accessories
        public ActionResult Index()
        {
            return View(dbAccessory.GetAccesories());
        }

        // GET: Accessories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessory accessory = dbAccessory.FindById(Convert.ToInt32(id));
            if (accessory == null)
            {
                return HttpNotFound();
            }
            return View(accessory);
        }

        // GET: Accessories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Accessories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Image,AnimalName")] Accessory accessory)
        {
            if (ModelState.IsValid)
            {
                if (accessory.AnimalName != null)
                {
                    Animal animal = dbAnimal.GetAnimals().Where(a => a.Name.ToLower() == accessory.AnimalName.ToLower()).FirstOrDefault();
                    if (animal != null)
                    {
                        accessory.Image = "~/Images/accossoire.jpg";
                        dbAccessory.Add(accessory);
                        dbAnimal.AddAccessoireToAnimal(animal.Id, accessory.Id);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Helaas konden wij de naam van het ingevulde dier niet vinden, vul iets anders in";
                        return View("Create");
                    }
                }
                else
                {
                    dbAccessory.Add(accessory);
                }
                return RedirectToAction("Index");
            }

            return View(accessory);
        }

        // GET: Accessories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessory accessory = dbAccessory.FindById(Convert.ToInt32(id));
            if (accessory == null)
            {
                return HttpNotFound();
            }
            return View(accessory);
        }

        // POST: Accessories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] Accessory accessory)
        {
            if (ModelState.IsValid)
            {
                if(accessory.Image == null)
                {
                    accessory.Image = "~/Images/accossoire.jpg";
                }
                dbAccessory.Edit(accessory);
                return RedirectToAction("Index");
            }
            return View(accessory);
        }

        // GET: Accessories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accessory accessory = dbAccessory.FindById(Convert.ToInt32(id));
            if (accessory == null)
            {
                return HttpNotFound();
            }
            return View(accessory);
        }

        // POST: Accessories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dbAccessory.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
