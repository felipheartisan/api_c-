using RevisaoApi.Models;

namespace RevisaoApi.Controllers
{
    public class ResultApplication
    {
        public bool Sucesso { get; set; } = false;

        /// <summary>
        /// Mensagens retornadas pela requisição
        /// </summary>
        public string Mensagem { get; set; } = string.Empty;

        /// <summary>
        /// Propriedade utilizada somente para exibir mensagens de erro retornadas pelo try catch
        /// </summary>
        public string Erro { get; set; } = string.Empty;

        public List<Aluno> Dados { get; set; } = [];

        public Aluno Aluno { get; set; }
    }
}
