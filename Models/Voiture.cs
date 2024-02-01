using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestionVoiture.Models
{
    public class Voiture
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        [DisplayName("Marque")]
        public string? Marque { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        [DisplayName("Modele")]
        public string? Modele { get; set; }

        [Required]
        [DisplayName("Type")]
        public string? Type { get; set; }

        [Required]
        [DisplayName("Annee Immatriculation")]
        public DateTime? AnneeImmatriculation { get; set; }

        [Required]
        [DisplayName("Kilométrage")]
        public int? Kilometrage { get; set; }

        [DisplayName("Photo")]
        public byte[]? Photo { get; set; } // Assuming you want to store the photo as byte array

        [DisplayName("Portes")]
        public int? Portes { get; set; }

        [DisplayName("Age minimal du conducteur")]
        public int? AgeMinimalConducteur { get; set; }

        [DisplayName("Dépôt")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? Depot { get; set; }

        [DisplayName("Carburant")]
        public string? Carburant { get; set; }

        [DisplayName("Max passagers")]
        public int? MaxPassagers { get; set; }

        [DisplayName("Capacité moteur")]
        public string? CapaciteMoteur { get; set; }

        [DisplayName("Prix à partir")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? PrixAPartir { get; set; }

        [Required]
        public bool Isreserved { get; set; }

        


       


    }
}

