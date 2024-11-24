using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnionArchitectureApp.Application.Features.Commands.ProductTypes;
using OnionArchitectureApp.Application.Features.Queries.ProductTypes;

namespace OnionArchitectureApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductTypesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllProductTypeQuery();
            var response = await mediator.Send(query);

            return StatusCode((int)response.StatusCode, response);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductTypeCommand command)
        {
            var response = await mediator.Send(command);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductTypeCommand command)
        {
            var response = await mediator.Send(command);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
