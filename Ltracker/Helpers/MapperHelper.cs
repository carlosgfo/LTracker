using Ltracker.Models;
using Ltracker.data.Entities;
using AutoMapper;

namespace Ltracker.Helpers
{
    public class MapperHelper
    {
        internal static IMapper mapper;

        static MapperHelper()
        {
            var config = new MapperConfiguration(x=>
            {
                x.CreateMap<Individual, IndividualViewModel>().ReverseMap();
                x.CreateMap<Course, CourseViewModel>().ReverseMap();
                x.CreateMap<Topic, TopicViewModel>().ReverseMap();
                x.CreateMap<Course, CourseDetailViewModel>();
                x.CreateMap<NewAssignmentViewModel, AssignedCourse>().ReverseMap();
                x.CreateMap<AssignmentViewModel, AssignedCourse>().ReverseMap();
                x.CreateMap<EditAssignmentViewModel, AssignedCourse>().ReverseMap();
                x.CreateMap < EditEvaluationsViewModel, Evaluation>().ReverseMap();
            });
            mapper = config.CreateMapper();  
        }

        public static T Map<T>(object source)
        {
            return mapper.Map<T>(source);
        }
    }
}