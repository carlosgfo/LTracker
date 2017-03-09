using Ltracker.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ltracker.data.Repositories
{

    public class EvaluationRepository : BaseRepository<Evaluation>
    {
        public EvaluationRepository(LearningContext context) : base(context)
        {

        }
    }

}
