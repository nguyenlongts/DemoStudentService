

namespace StudentService.APP.Controllers
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
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("{id}/marks")]
        public async Task<IActionResult> GetMark(int id)
        {
            try
            {
                var query = new GetStudentMarksQuery(id);
                var result = await _mediator.Send(query);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var query = new GetStudentByIDQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
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
        [HttpPost("add-mark")]
        public async Task<IActionResult> AddMark(AddMarkCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
        [HttpPost("assgin-class")]
        public async Task<IActionResult> AssignClass(AssignNewClassCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var cmd = new DeleteStudentCommand(id);
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
