using Microsoft.AspNetCore.Mvc;

namespace ProjectSchool_API.Controllers
{
    [Route("api/[controller]")]   //api/aluno
    [ApiController]
    public class AlunoController : Controller
    {

        public AlunoController()
        {
        }

        [HttpGet]
        public IActionResult get()
        {
            return Ok();
        }

        [HttpGet("{AlunoId}")]
        public IActionResult get(int AlunoId)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult post()
        {
            return Ok();
        }
        
        [HttpPut("{AlunoId}")]
        public IActionResult put(int AlunoId)
        {
            return Ok();
        }

        [HttpDelete("{AlunoId}")]
        public IActionResult delete()
        {
            return Ok();
        }
    }
}