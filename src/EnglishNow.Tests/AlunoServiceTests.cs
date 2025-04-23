using EnglishNow.Repositories;
using EnglishNow.Repositories.Entities;
using EnglishNow.Services;
using EnglishNow.Services.Models.Aluno;
using Moq;

namespace EnglishNow.Tests
{
    [TestClass]
    public class AlunoServiceTests
    {
        private readonly IAlunoService _alunoService;
        private readonly Mock<IAlunoRepository> _alunoRepositoryMock;
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;

        public AlunoServiceTests()
        {
            _alunoRepositoryMock = new Mock<IAlunoRepository>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();

            _alunoService = new AlunoService(_alunoRepositoryMock.Object, _usuarioRepositoryMock.Object);
        }

        [TestMethod]
        public void Criar_AlunoCriado_RetornaSucesso()
        {
            //Arrange
            var request = new CriarAlunoRequest
            { 
                Email = "renato@teste.com",
                Login = "renato",
                Nome = "Renato",
                Senha = "senha123"
            };

            _usuarioRepositoryMock
                .Setup(c => c.ObterPorLogin(request.Login))
                .Returns((Usuario?)null);

            _usuarioRepositoryMock
                .Setup(c => c.Inserir(It.IsAny<Usuario>()))
                .Returns(1);

            _alunoRepositoryMock
                .Setup(c => c.Inserir(It.IsAny<Aluno>()))
                .Returns(1);

            //Act
            var result = _alunoService.Criar(request);

            //Assert
            _usuarioRepositoryMock.Verify(c => c.ObterPorLogin(request.Login), Times.Once);

            _usuarioRepositoryMock.Verify(c => c.Inserir(It.IsAny<Usuario>()), Times.Once);

            _alunoRepositoryMock.Verify(c => c.Inserir(It.IsAny<Aluno>()), Times.Once);

            Assert.IsTrue(
                result != null &&
                result.Sucesso);
        }

        [TestMethod]
        public void Criar_RequestVazio_RetornaErro()
        {
            //Arrange
            CriarAlunoRequest? request = null;

            //Act
            var result = _alunoService.Criar(request);

            //Assert
            Assert.IsTrue(
                result != null &&
                result.Sucesso == false &&
                result.MensagemErro == "Objeto CriarAlunoRequest obrigatório");
        }
    }
}
