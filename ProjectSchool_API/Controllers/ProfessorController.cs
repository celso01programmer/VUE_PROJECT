using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSchool_API.Data;
using ProjectSchool_API.Models;

namespace ProjectSchool_API.Controllers
{
    [Route("api/[controller]")]    //api/professor
    [ApiController]
    public class ProfessorController : Controller
    {
        private IRepository _repository { get; }

        public ProfessorController(IRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            try
            {
                var result = await _repository.GetAllProfessoresAsync(true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpGet("{ProfessorId}")]
        public async Task<IActionResult> getProfessorById(int ProfessorId)
        {
            try
            {
                var result = await _repository.GetProfessorAsyncById(ProfessorId, true);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> post(Professor model)
        {
            try
            {
                _repository.Add(model);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/aluno/{model.Id}", model);
                }

            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }

            return BadRequest();
        }

        [HttpPut("{ProfessorId}")]
        public async Task<IActionResult> put(int ProfessorId, Professor model)
        {
            try
            {
                var professor = await _repository.GetProfessorAsyncById(ProfessorId, false);
                if (professor == null){
                    return NotFound();
                }

                _repository.Update(model);

                if (await _repository.SaveChangesAsync()){
                    professor = await _repository.GetProfessorAsyncById(ProfessorId, true);
                    return Created($"/api/aluno/{model.Id}", professor);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            return BadRequest();
        }

        [HttpDelete("{ProfessorId}")]
        public async Task<IActionResult> delete(int ProfessorId)
        {
            try
            {
                var professor = await _repository.GetAlunoAsyncById(ProfessorId, false);
                if (professor == null){
                    return NotFound();
                }

                _repository.Delete(professor);

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