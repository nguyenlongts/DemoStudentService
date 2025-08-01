using DemoCQRS_MediatR.APP.Application;

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
                
                return Ok(result);

            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cmd = new DeleteStudentCommand(id);
                await _mediator.Send(cmd);
                
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentCommand cmd)
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
