using EnglishNow.Repositories;
using EnglishNow.Repositories.Entities;
using EnglishNow.Services;
using EnglishNow.Services.Enums;
using Moq;

namespace EnglishNow.Tests
{
    [TestClass]
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;

        private readonly IUsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();

            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
        }

        [TestMethod]
        public void ValidarLogin_LoginValido_RetornaUsuario()
        {
            //Arrange
            var login = "administrador";
            var senha = "senha123";

            var usuario = new Usuario
            {
                Id = 1,
                Login = login,
                Senha = senha,
                PapelId = (int)Papel.Administrador
            };

            _usuarioRepositoryMock
                .Setup(c => c.ObterPorLogin(login))
                .Returns(usuario);

            //Act
            var result = _usuarioService.ValidarLogin(login, senha);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Once);

            Assert.IsTrue(
                result != null &&
                result.Sucesso == true &&
                result.Usuario != null &&
                result.Usuario.Login == login);
        }

        [TestMethod]
        public void ValidarLogin_SenhaInvalida_RetornarErro()
        {
            //Arrange
            var login = "administrador";
            var senha = "senha123";

            var usuario = new Usuario
            {
                Id = 1,
                Login = login,
                Senha = "123",
                PapelId = (int)Papel.Administrador
            };

            _usuarioRepositoryMock
                .Setup(c => c.ObterPorLogin(login))
                .Returns(usuario);

            //Act
            var result = _usuarioService.ValidarLogin(login, senha);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Once);

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.MensagemErro == "Senha inválida");
        }

        [TestMethod]
        public void ValidarLogin_LoginInvalido_RetornarErro()
        {
            //Arrange
            var login = "administrador";
            var senha = "senha123";

            _usuarioRepositoryMock
                .Setup(c => c.ObterPorLogin(login))
                .Returns((Usuario?)null);

            //Act
            var result = _usuarioService.ValidarLogin(login, senha);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Once);

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.MensagemErro == "Usuário não encontrado");
        }

        [TestMethod]
        public void ValidarLogin_LoginVazio_RetornarErro()
        {
            //Arrange
            var login = "";
            var senha = "senha123";

            //Act
            var result = _usuarioService.ValidarLogin(login, senha);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Never);

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.Usuario == null &&
                result.MensagemErro == "Usuário é obrigatório");
        }

        [TestMethod]
        public void ValidarLogin_SenhaVazia_RetornarErro()
        {
            //Arrange
            var login = "administrador";
            var senha = "";

            //Act
            var result = _usuarioService.ValidarLogin(login, senha);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(login), Times.Never);

            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.Usuario == null &&
                result.MensagemErro == "Senha é obrigatória");
        }
    }
}
