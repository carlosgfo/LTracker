using Ltracker.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ltracker.data.Repositories
{

    public class AssignedCourseRepository : BaseRepository<AssignedCourse>
    {
        public AssignedCourseRepository(LearningContext context) : base(context)
        {

        }

        public void InsertAssignmentWithEvaluation(AssignedCourse assignment, Evaluation evaluation)
        {
            if (evaluation != null)
            {
                assignment.Evaluation = evaluation;
                _context.AssignedCourses.Add(assignment);
                _context.SaveChanges();
                evaluation.AssignedCourseId = assignment.Id.Value;
                _context.Evaluations.Add(evaluation);
            }
        }

    }

}
