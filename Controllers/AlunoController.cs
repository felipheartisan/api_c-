using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using RevisaoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RevisaoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private RevisaoContext _dbcontext;

        public AlunoController(RevisaoContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        // GET: api/<AlunoController>
        [HttpGet("BuscarTodos")]
        public async Task<IActionResult> BuscarTodos()
        {
            var resultApplication = new ResultApplication();
            var result = await _dbcontext.Alunos.ToListAsync();
            resultApplication.Dados = result;

            return Ok(resultApplication);
        }

        // GET api/<AlunoController>/5
        [HttpGet("Buscar({id})")]
        public async Task<IActionResult> Buscar(int id)
        {
            var resultApplication = new ResultApplication();
            var result = await _dbcontext.Alunos.FindAsync(id);
            
            if(result == null)
            {
                resultApplication.Mensagem = "Registro não encontrado";
                return BadRequest(resultApplication);
            }
            resultApplication.Mensagem = "Registro encontrado com sucesso";
            resultApplication.Sucesso = true;
            resultApplication.Aluno = result;
            return Ok(resultApplication);
        }

        // POST api/<AlunoController>
        [HttpPost("Inserir")]
        public async Task<IActionResult> Inserir([FromBody] Aluno aluno)
        {
            var resultApplication = new ResultApplication();

            try
            {
                await _dbcontext.Alunos.AddAsync(aluno);
                await _dbcontext.SaveChangesAsync();
                resultApplication.Sucesso = true;
                resultApplication.Mensagem = "Registro salvo com sucesso";
                return Ok(resultApplication);
            }
            catch (Exception ex)
            {
                resultApplication.Mensagem = "Ocorreu um erro interno";
                resultApplication.Erro = ex.InnerException.Message;
                
                return StatusCode(StatusCodes.Status500InternalServerError, resultApplication);
            }

        }

        // PUT api/<AlunoController>/5
        [HttpPut("Atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] Aluno aluno)
        {
            var resultApplication = new ResultApplication();

            try
            {
                var alunoSelecionado = await _dbcontext.Alunos.FindAsync(aluno.Codigo);

                if (alunoSelecionado != null)
                {
                    alunoSelecionado.Nome = aluno.Nome;

                    await Task.FromResult(_dbcontext.Alunos.Update(alunoSelecionado));

                    await _dbcontext.SaveChangesAsync();

                    resultApplication.Sucesso = true;
                    resultApplication.Mensagem = "Registro alterado com sucesso";
                    return Ok(resultApplication);

                    
                }
                else
                {
                    resultApplication.Mensagem = "Registro não encontrado";
                    return BadRequest(resultApplication);
                }

                
            }
            catch (Exception ex)
            {
                resultApplication.Mensagem = "Ocorreu um erro interno";
                resultApplication.Erro = ex.InnerException.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, resultApplication);
            }

        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("Excluir({codigo})")]
        public async Task<IActionResult> Excluir(int codigo)
        {
            var resultApplication = new ResultApplication();

            try
            {
                var alunoSelecionado = await _dbcontext.Alunos.FindAsync(codigo);

                if (alunoSelecionado == null)
                {
                    resultApplication.Mensagem = "Registro não encontrado";
                    return BadRequest(resultApplication);
                }

                await Task.FromResult(_dbcontext.Alunos.Remove(alunoSelecionado));

                await _dbcontext.SaveChangesAsync();

                resultApplication.Sucesso = true;
                resultApplication.Mensagem = "Registro excluído com sucesso";
                return Ok(resultApplication);
            }
            catch (Exception ex)
            {
                resultApplication.Mensagem = "Ocorreu um erro interno";
                resultApplication.Erro = ex.InnerException.Message;

                return StatusCode(StatusCodes.Status500InternalServerError, resultApplication);
            }
        }
    }
}
