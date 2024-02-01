using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionVoiture.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [DisplayName("Titre")]
        public string? Titre { get; set; }

        [Required]
        [DisplayName("Nom")]
        public string? Nom { get; set; }

        [Required]
        [DisplayName("Prenom")]
        public string? Prenom { get; set; }

        

        [Required]
        [DisplayName("Adresse")]
        public string? Adresse { get; set; }

        [Required]
        [DisplayName("Date de naissance")]
        public DateTime? DateNaissance { get; set; }

        [Required]
        [DisplayName("Ville")]
        public string? Ville { get; set; }

        [DisplayName("Code Postal")]
        public string? CodePostal { get; set; }

        [Required]
        [DisplayName("Téléphone")]
        public string? Phone { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
