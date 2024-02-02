using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WWTBA.Core.DTOs;
using WWTBA.Core.Models;
using WWTBA.Core.Services;

namespace WWTBA.Web.Filters
{
    public class NotFoundFilter<Entity, Dto> : IAsyncActionFilter where Entity : BaseEntity where Dto : class
    {
        private readonly IService<Entity, Dto> _service;

        public NotFoundFilter(IService<Entity, Dto> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            object idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            int id = (int)idValue;

            bool anyEntity = (await _service.AnyAsync(x => x.Id == id)).Data;

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            ErrorViewModel errorViewModel = new();
            
            errorViewModel.Errors.Add($"{typeof(Entity).Name}  ({id}) not found!");

            context.Result = new RedirectToActionResult("Error", "Home", errorViewModel);
        }
    }
}

