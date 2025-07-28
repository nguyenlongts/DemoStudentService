using DemoCQRS_MediatR.APP.Application.Commands;
using DemoCQRS_MediatR.APP.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoCQRS_MediatR.APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var query = new GetStudentsQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var query = new GetStudentByIDQuery(id);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateStudentCommand cmd)
        {
            try
            {
                var result = await _mediator.Send(cmd);
                if (!result)
                {
                    return BadRequest();
                }
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
