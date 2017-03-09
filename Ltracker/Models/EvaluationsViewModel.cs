using Ltracker.data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ltracker.Models
{
    public class EditEvaluationsViewModel
    {
        [Required]
        public int? EvaluationId { get; set; }

        [DisplayName("Examen Teorico")]
        [Range(0, 100, ErrorMessage = "Inserte una calificacion del 0 al 100")]
        public int? Theorical { get; set; }

        [DisplayName("Examen Practico")]
        [Range(0, 100, ErrorMessage = "Inserte una calificacion del 0 al 100")]
        public int? Practical { get; set; }

        [DisplayName("Promedio")]
        public decimal? Total{ get; set; }

        [Required]
        public int AssignedCourseId { get; set; }
    }

}