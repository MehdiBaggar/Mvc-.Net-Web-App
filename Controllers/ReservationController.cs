using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using GestionVoiture.Data;
using GestionVoiture.Models;
using Microsoft.AspNetCore.Identity;
using MySqlConnector;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionVoiture.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _dbContext;
        

        public ReservationController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(DateTime dateDepart, DateTime dateRetour, string localisationDepart, string localisationRetour)
        {
            TempData["DateDepart"] = dateDepart;
            TempData["DateRetour"] = dateRetour;
            TempData["localisationDepart"] = localisationDepart;  
            TempData["localisationRetour"] = localisationRetour;  

            TempData.Keep();

            return RedirectToAction("ShowAvailableCars");
        }


        [HttpGet]
        public IActionResult ShowAvailableCars()
        {
            var dateDepart = (DateTime)TempData["DateDepart"];
            var dateRetour = (DateTime)TempData["DateRetour"];
            var localisationDepart = (string)TempData["localisationDepart"];  
            var localisationRetour = (string)TempData["localisationRetour"];  

            var availableCars = _dbContext.Voitures
                .Where(v => !v.Isreserved)
                .ToList();

            TempData["DateDepart"] = dateDepart;
            TempData["DateRetour"] = dateRetour;
            TempData["localisationDepart"] = localisationDepart;  
            TempData["localisationRetour"] = localisationRetour;  

            TempData.Keep();

            return View(availableCars);
        }


        [HttpGet]
        public IActionResult Reserve(int voitureId)
        {
            var dateDepart = (DateTime)TempData["DateDepart"];
            var dateRetour = (DateTime)TempData["DateRetour"];
            var localisationDepart = (string)TempData["localisationDepart"];  
            var localisationRetour = (string)TempData["localisationRetour"];



            var selectedCar = _dbContext.Voitures.FirstOrDefault(v => v.Id == voitureId && !v.Isreserved);

            if (selectedCar != null)
            {
                
                selectedCar.Isreserved = true;
                _dbContext.SaveChanges();
                TempData["VoitureId"] = voitureId;
                TempData["DateDepart"] = dateDepart;
                TempData["DateRetour"] = dateRetour;
                TempData["LocalisationDepart"] = localisationDepart;
                TempData["LocalisationRetour"] = localisationRetour;



                return RedirectToAction("User");
            }
            else
            {
                
                return View("CarNotAvailable");
            }
        }

        [HttpGet]
        public IActionResult User()
        {
            return View();
        }



        [HttpPost]
        public IActionResult User(ApplicationUser user)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges();


                    var reservation = new Reservation
                    {
                        VoitureId = (int)TempData["voitureId"],
                        DateDepart = (DateTime)TempData["DateDepart"],
                        DateRetour = (DateTime)TempData["DateRetour"],
                        localisationDepart = (string)TempData["localisationDepart"],
                        localisationRetour = (string)TempData["localisationRetour"]
                    };
                    TempData["UserTitre"] = user.Titre;
                    TempData["UserNom"] = user.Nom;
                    TempData["UserPrenom"] = user.Prenom;
                    TempData["UserAdresse"] = user.Adresse;
                    TempData["UserDateNaissance"] = user.DateNaissance;
                    TempData["UserEmail"] = user.Email;
                    TempData["UserCodePostal"] = user.CodePostal;
                    TempData["UserVille"] = user.Ville;
                    TempData["UserPhone"] = user.Phone;
                    TempData["UserPass"] = user.PasswordHash;
                    if (reservation != null)
                    {
                        reservation.UserId = user.Id;
                        TempData["VoitureId"] = reservation.VoitureId;
                        TempData["DateDepart"] = reservation.DateDepart;
                        TempData["DateRetour"] = reservation.DateRetour;
                        TempData["LocalisationDepart"] = reservation.localisationDepart;
                        TempData["LocalisationRetour"] = reservation.localisationRetour;
                        TempData["UserId"] = reservation.UserId.ToString();

                        return RedirectToAction("Confirmation");
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;

                
                if (innerException is MySqlException mysqlException && mysqlException.Number == 1062)
                {
                    
                    ModelState.AddModelError("Email", "Email is already in use.");
                    return View(user);
                }

                
                throw;
            }


            return View(user);
        }
        

        [HttpGet]
        public IActionResult Confirmation()
        {

            var user = new ApplicationUser
            {
                Titre = (string)TempData["UserTitre"],
                Nom = (string)TempData["UserNom"],
                Prenom = (string)TempData["UserPrenom"],
                Adresse = (string)TempData["UserAdresse"],
                DateNaissance = (DateTime)TempData["UserDateNaissance"],
                Email = (string)TempData["UserEmail"],
                CodePostal = (string)TempData["UserCodePostal"],
                Ville = (string)TempData["UserVille"],
                Phone = (string)TempData["UserPhone"],
                PasswordHash = (string)TempData["UserPass"],

            };
            var reservation = new Reservation
            {
                VoitureId = (int)TempData["voitureId"],
                DateDepart = (DateTime)TempData["DateDepart"],
                DateRetour = (DateTime)TempData["DateRetour"],
                localisationDepart = (string)TempData["localisationDepart"],
                localisationRetour = (string)TempData["localisationRetour"],
                UserId = TempData["UserId"].ToString(),
                
            };

            if (user != null && reservation != null)
            {

                
                _dbContext.Reservations.Add(reservation);
                _dbContext.SaveChanges();

                TempData.Clear();


                ViewBag.SuccessMessage = "Reservation confirmed successfully!";

                ViewBag.User = user;
                ViewBag.Reservation = reservation;

                return View();
            }
            else
            {
                
                return RedirectToAction("Error");
            }
        }
        

        [HttpGet]
        public IActionResult AllReservations()
        {
            var allReservations = _dbContext.Reservations.ToList(); 

            return View(allReservations);
        }
        [HttpPost]
        public IActionResult DeleteReservation(int reservationId)
        {
           
            var reservationToDelete = _dbContext.Reservations.Find(reservationId);

            if (reservationToDelete != null)
            {
               
                var associatedCar = _dbContext.Voitures.Find(reservationToDelete.VoitureId);

                if (associatedCar != null)
                {
                    
                    associatedCar.Isreserved = false;
                }

                
                _dbContext.Reservations.Remove(reservationToDelete);

                
                _dbContext.SaveChanges();
            }

            
            return RedirectToAction("AllReservations");
        }
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }



    }
}

