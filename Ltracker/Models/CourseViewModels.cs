using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ltracker.Models
{
    public class CourseViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Nombre del curso")]
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [DisplayName("Duracion promedio")]
        public decimal? DurationAVG { get; set; }
        [MaxLength(500)]
        [DisplayName("Descripción")]
        public string Description { get; set; }

        /// <summary>
        /// Sirve para desplegar los topics disponibles
        /// </summary>
        [DisplayName("Temas del  curso")]
        public ICollection<TopicViewModel> AvailableTopics { get; set; }

        /// <summary>
        /// Sirve para guardar la seleccion del usuario
        /// </summary>
        public int[] SelectedTopics { get; set; }

    }
    public class CourseDetailViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Nombre del curso")]
        public string Name { get; set; }

        [DisplayName("Duracion promedio")]
        public decimal? DurationAVG { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }

        /// <summary>
        /// Sirve para desplegar los topics disponibles
        /// </summary>
        [DisplayName("Temas del  curso")]
        public ICollection<TopicViewModel> Topics { get; set; }

    }

    public class TopicViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}