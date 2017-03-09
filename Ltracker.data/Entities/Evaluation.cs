using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ltracker.data.Entities
{
    /// <summary>
    /// Entidad para los examenes y calificaciones.
    /// </summary>
    public class Evaluation
    {
        public int? EvaluationId { get; set; }
        public int? Theorical { get; set; }
        public int? Practical { get; set; }

        public decimal? Total
        {
            get { return (Theorical + Practical) / 2m; }
        }
        public int AssignedCourseId { get; set; }
        public AssignedCourse AssignedCourse { get; set; }
    }
}
