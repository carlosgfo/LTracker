using Ltracker.data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ltracker.Models
{
    public class NewAssignmentViewModel
    {
        [Required]
        [DisplayName("Fecha de asignación")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? AssignmentDate { get; set; }

        public SelectList IndividualList { get; set; }

        public SelectList CoursesList { get; set; }

        [DisplayName("Persona")]
        [Required]
        public int? IndividualId { get; set; }

        [DisplayName("Curso")]
        [Required]
        public int? CourseId { get; set; }

        public Evaluation Evaluation { get; set; }
    }

    public class AssignmentViewModel
    {
        public int? Id { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public decimal? TotalHours { get; set; }
        public CourseViewModel Course { get; set; }
        public IndividualViewModel Individual { get; set; }
    }

    public class EditAssignmentViewModel
    {
        //no editables
        public int? Id { get; set; }
        public DateTime? AssignmentDate { get; set; }

        //Editables
        [DisplayName("Completado")]
        public bool IsCompleted { get; set; }

        [DisplayName("Inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Fin")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FinishDate { get; set; }

        [DisplayName("Horas totales")]
        public decimal? TotalHours { get; set; }


        #region relaciones
        public IndividualViewModel Individual { get; set; }
        public int IndividualId { get; set; }
        public CourseViewModel Course { get; set; }
        public int CourseId { get; set; }

        #endregion
    }
}