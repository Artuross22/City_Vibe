using City_Vibe.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace City_Vibe.ValidationAttribute.AppointmentAtributes
{
    public class AdmissionRequestsFilterAttribute : ActionFilterAttribute
    {

        public readonly IUnitOfWork unitOfWorkRepository;

        public AdmissionRequestsFilterAttribute(IUnitOfWork unitOfWorkRepo)
        {
            unitOfWorkRepository = unitOfWorkRepo;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            int curItemId;

            if (filterContext.ActionArguments.ContainsKey("eventId"))
            {
                curItemId = (int)filterContext.ActionArguments["eventId"];
            }
            else
            {
                filterContext.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }

            var result = unitOfWorkRepository.AppointmentRepository.GetAppointmentsByEventId(curItemId);

            if (result == null)
                filterContext.Result = new NotFoundResult();

        }
    }
}
