using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionVoiture.Models;
using GestionVoiture.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace GestionVoiture.Controllers
{
    public class VoitureController : Controller

    {
        private readonly AppDbContext _dbContext;

        public VoitureController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var voitures = _dbContext.Voitures.ToList();
            return View(voitures);
        }
        [HttpGet]
        public IActionResult Ajouter()
        {
            var newVoiture = new Voiture();
            return View(newVoiture);
        }
        [HttpPost]
        public IActionResult Ajouter(Voiture voiture, IFormFile photoFile)
        {
            if (ModelState.IsValid)
            {
                if (photoFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        photoFile.CopyTo(memoryStream);
                        voiture.Photo = memoryStream.ToArray();
                    }
                }

                _dbContext.Voitures.Add(voiture); 
                _dbContext.SaveChanges(); 
                return RedirectToAction("Index");
            }

            return View(voiture);
        }

        [HttpGet]
        public IActionResult Modifier(int id)
        {
            var voiture = _dbContext.Voitures.Find(id);

            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }

        [HttpPost]
        public IActionResult Modifier(Voiture voiture, IFormFile photoFile)
        {
            if (ModelState.IsValid)
            {
                var existingVoiture = _dbContext.Voitures.Find(voiture.Id);

                if (existingVoiture == null)
                {
                    return NotFound();
                }

                
                existingVoiture.Marque = voiture.Marque;
                existingVoiture.Modele = voiture.Modele;
                existingVoiture.Type = voiture.Type;
                existingVoiture.AnneeImmatriculation = voiture.AnneeImmatriculation;
                existingVoiture.Kilometrage = voiture.Kilometrage;
                existingVoiture.Portes = voiture.Portes;
                existingVoiture.Depot = voiture.Depot;
                existingVoiture.AgeMinimalConducteur = voiture.AgeMinimalConducteur;
                existingVoiture.Carburant = voiture.Carburant;
                existingVoiture.CapaciteMoteur = voiture.CapaciteMoteur;
                existingVoiture.MaxPassagers = voiture.MaxPassagers;
                existingVoiture.PrixAPartir = voiture.PrixAPartir;
                existingVoiture.Isreserved = voiture.Isreserved;

                if (photoFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        photoFile.CopyTo(memoryStream);
                        existingVoiture.Photo = memoryStream.ToArray();
                    }
                }

                _dbContext.SaveChanges(); 
                return RedirectToAction("Index");
            }

            return View(voiture);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var voiture = _dbContext.Voitures.Find(id);

            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var voiture = _dbContext.Voitures.Find(id);

            if (voiture == null)
            {
                return NotFound();
            }

            _dbContext.Voitures.Remove(voiture);
            _dbContext.SaveChanges(); 
            return RedirectToAction("Index");
        }




    }

}