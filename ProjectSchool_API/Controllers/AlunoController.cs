using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProjectSchool_API.Data;
using ProjectSchool_API.Models;
using System.Threading.Tasks;

namespace ProjectSchool_API.Controllers
{
    [Route("api/[controller]")]   //api/aluno
    [ApiController]
    public class AlunoController : Controller
    {
        private IRepository _repository { get; }

        public AlunoController(IRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            try
            {
                var result = await _repository.GetAllAlunosAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpGet("{AlunoId}")]
        public async Task<IActionResult> getByAlunoId(int AlunoId)
        {
            try
            {
                var result = await _repository.GetAlunoAsyncById(AlunoId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpGet("ByProfessorId/{ProfessorId}")]
        public async Task<IActionResult> getByProfessorId(int ProfessorId)
        {
            try
            {
                var result = await _repository.GetAlunosAsyncByProfessorId(ProfessorId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }



        [HttpPost]
        public async Task<IActionResult> post(Aluno model)
        {
            try
            {
                _repository.Add(model);
                
                if (await _repository.SaveChangesAsync()){
                    return Created($"/api/aluno/{model.Id}", model);
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            return BadRequest();
        }

        [HttpPut("{AlunoId}")]
        public async Task<IActionResult> put(int AlunoId, Aluno model)
        {
            try
            {
                var aluno = await _repository.GetAlunoAsyncById(AlunoId, false);
                if (aluno == null){
                    return NotFound();
                }

                _repository.Update(model);

                if (await _repository.SaveChangesAsync()){
                    aluno = await _repository.GetAlunoAsyncById(AlunoId, true);
                    return Created($"/api/aluno/{model.Id}", aluno);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            return BadRequest();
        }

        [HttpDelete("{AlunoId}")]
        public async Task<IActionResult> delete(int AlunoId)
        {
            try
            {
                var aluno = await _repository.GetAlunoAsyncById(AlunoId, false);
                if (aluno == null){
                    return NotFound();
                }

                _repository.Delete(aluno);

                if (await _repository.SaveChangesAsync()){
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            return BadRequest();
        }
    }
}