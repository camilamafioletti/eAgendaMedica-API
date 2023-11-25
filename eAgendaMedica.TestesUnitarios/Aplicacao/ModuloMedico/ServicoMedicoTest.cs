using eAgendaMedica.Aplicacao.ModuloMedico;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloMedico;
using FluentResults;
using FluentResults.Extensions.FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace eAgendaMedica.TestesUnitarios.Dominio.ModuloConsulta
{
    [TestClass]
    public class ServicoMedicoTest
    {
        Mock<IRepositorioMedico> repositorioMedicoMoq;
        Mock<ValidadorMedico> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        Mock<IRepositorioConsulta> repositorioConsultaMoq;
        Mock<IRepositorioCirurgia> repositorioCirurgiaMoq;

        private ServicoMedico servicoMedico;

        public ServicoMedicoTest()
        {
            repositorioMedicoMoq = new Mock<IRepositorioMedico>();
            validadorMoq = new Mock<ValidadorMedico>();
            contextoMoq = new Mock<IContextoPersistencia>();

            repositorioConsultaMoq = new Mock<IRepositorioConsulta>();
            repositorioCirurgiaMoq = new Mock<IRepositorioCirurgia>();
            servicoMedico = new ServicoMedico(repositorioMedicoMoq.Object, repositorioConsultaMoq.Object,repositorioCirurgiaMoq.Object, contextoMoq.Object);
        }

        [TestMethod]
        public async Task Deve_inserir_medico_caso_ele_seja_valido()
        {
            // Arrange
            var medico = BuildMedico("MedicoTeste", "12345-SC", "49 9999-9999");

            // Action
            Result<Medico> resultado = await servicoMedico.InserirAsync(medico);

            // Assert
            resultado.Should().BeSuccess();
            repositorioMedicoMoq.Verify(x => x.InserirAsync(medico), Times.Once());
        }

        [TestMethod]
        public async Task Nao_Deve_inserir_medico_caso_ele_seja_invalido()
        {
            // Arrange
            var medico = BuildMedico("A", "SC", "49 9999");

            // Action
            Result<Medico> resultado = await servicoMedico.InserirAsync(medico);

            // Assert
            resultado.Should().BeFailure();
            repositorioMedicoMoq.Verify(x => x.InserirAsync(medico), Times.Never());
        }

        [TestMethod]
        public async Task Deve_editar_medico_caso_ele_seja_valido()
        {
            // Arrange
            var medico = BuildMedico("MedicoTeste", "12345-SC", "49 9999-9999");

            // Action
            Result<Medico> resultado = await servicoMedico.EditarAsync(medico);

            // Assert
            resultado.Should().BeSuccess();
            repositorioMedicoMoq.Verify(x => x.Editar(medico), Times.Once());
        }

        [TestMethod]
        public async Task Deve_excluir_medico_caso_ele_esteja_cadastrado()
        {
            // Arrange
            var medico = BuildMedico("MedicoTeste", "12345-SC", "49 9999-9999");

            repositorioMedicoMoq.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>()))
                               .ReturnsAsync(medico);

            // Action
            Result<Medico> resultadoMedico = await servicoMedico.ExcluirAsync(medico.Id);

            // Assert
            resultadoMedico.Should().BeSuccess();
            repositorioMedicoMoq.Verify(x => x.Excluir(medico), Times.Once());
        }

        private Medico BuildMedico(string nome, string crm, string telefone)
        {
            return new Medico()
            {
                Nome = nome,
                Crm = crm,
                Telefone = telefone
            };
        }
    }
}
