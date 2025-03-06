using EnglishNow.Services.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishNow.Services
{
    public interface IUsuarioService
    {
        ValidarLoginResult ValidarLogin(string usuario, string senha);
    }

    public class UsuarioService : IUsuarioService
    {
        public ValidarLoginResult ValidarLogin(string usuario, string senha)
        {
            var result = new ValidarLoginResult();

            result.Sucesso = false;

            result.MensagemErro = "Usuário ou senha inválidos";

            return result;
        }
    }
}
