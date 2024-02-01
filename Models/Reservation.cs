using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionVoiture.Models
{
	public class Reservation
	{
		[Key]
		public int Id { get; set; }

        [Required]
        [DisplayName("Date de depart")]
        public DateTime? DateDepart { get; set; }

        [Required]
        [DisplayName("Date de retour")]
        public DateTime? DateRetour { get; set; }

        [Required]
        [DisplayName("localisation de depart")]
        public string? localisationDepart { get; set; }

        [Required]
        [DisplayName("localisation de retour")]
        public string? localisationRetour { get; set; }

        [ForeignKey("Voiture")] 
        public int? VoitureId { get; set; }

        [ForeignKey("User")]
        public String? UserId { get; set; }






    }


}


