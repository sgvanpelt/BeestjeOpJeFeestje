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
    public class AnimalsController : Controller
    {
        IAnimalRepository dbAnimal;
        IAccesoryRepository dbAccessory;

        public AnimalsController(IAnimalRepository db1, IAccesoryRepository db2)
        {
            dbAnimal = db1;
            dbAccessory = db2;
        }
        // GET: Animals
        public ActionResult Index()
        {
            return View(dbAnimal.GetAnimals());
        }

        // GET: Animals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = dbAnimal.FindById(Convert.ToInt32(id));
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // GET: Animals/Create
        public ActionResult Create()
        {
            Animal Animal = new Animal();
            List<Accessory> accessories = new List<Accessory>();
            foreach (var animal in dbAnimal.GetAnimals())
            {
                foreach(var acc in animal.Accessories)
                {
                    accessories.Add(acc);
                }
            }

            foreach (var acc in dbAccessory.GetAccesories().ToList())
            {
                if (!accessories.Select(a => a.Name).Contains(acc.Name))
                {
                    Animal.PossibleAccessories.Add(acc);
                }
            }

            return View(Animal);
        }

        // POST: Animals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Category,Image")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                if(CheckCategory(animal.Category))
                {
                    string path = "~/Images/kuiken.png";
                    animal.Image = path;
                    dbAnimal.Add(animal);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Categorie moet: Boerderij of Woestijn of Jungle of Sneeuw zijn.";
                    return View();
                }
            }

            return View(animal);
        }

        // GET: Animals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = dbAnimal.FindById(Convert.ToInt32(id));
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Category,Image")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                if (CheckCategory(animal.Category))
                {
                    dbAnimal.Edit(animal);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Categorie moet: Boerderij of Woestijn of Jungle of Sneeuw zijn.";
                    return View();
                }
            }
            return View(animal);
        }

        // GET: Animals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal animal = dbAnimal.FindById(Convert.ToInt32(id));
            if (animal == null)
            {
                return HttpNotFound();
            }
            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dbAnimal.Remove(id);
            return RedirectToAction("Index");
        }

        private bool CheckCategory(string input)
        {
            if (input.Equals("Boerderij") || input.Equals("Woestijn") || input.Equals("Jungle") || input.Equals("Sneeuw"))
            {
                return true;
            }
            return false;
        }

    }
}
