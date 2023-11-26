using eAgendaMedica.Aplicacao.ModuloCirurgia;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloMedico;
using FluentResults;
using FluentResults.Extensions.FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AgendaMedica.TestesUnitarios.Aplicacao.ModuloCirurgia
{
    [TestClass]
    public class ServicoCirurgiaTest
    {
        Mock<IRepositorioCirurgia> repositorioCirurgiaMoq;
        Mock<IRepositorioConsulta> repositorioConsultaMoq;
        Mock<ValidadorCirurgia> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        private ServicoCirurgia servicoCirurgia;

        public ServicoCirurgiaTest()
        {
            repositorioCirurgiaMoq = new Mock<IRepositorioCirurgia>();
            repositorioConsultaMoq = new Mock<IRepositorioConsulta>();
            validadorMoq = new Mock<ValidadorCirurgia>();
            contextoMoq = new Mock<IContextoPersistencia>();
            servicoCirurgia = new ServicoCirurgia(repositorioCirurgiaMoq.Object, repositorioConsultaMoq.Object, contextoMoq.Object);
        }

        [TestMethod]
        public async Task Deve_inserir_cirurgia_caso_ela_seja_valida() //cenário 1
        {
            //arrange 
            Medico medico = new Medico();
            var cirurgia = BuildCirurgia("CirurgiaTeste", medico, TimeSpan.MinValue, TimeSpan.MinValue);

            //action
            Result<Cirurgia> resultado = await servicoCirurgia.InserirAsync(cirurgia);

            //assert 
            resultado.Should().BeSuccess();
            repositorioCirurgiaMoq.Verify(x => x.InserirAsync(cirurgia), Times.Once());
        }

        [TestMethod]
        public async Task Nao_Deve_inserir_cirurgia_caso_ela_seja_invalida() //cenário 1
        {
            //arrange 
            Medico medico = new Medico();
            var cirurgia = BuildCirurgia("", medico, TimeSpan.Zero, TimeSpan.Zero);

            //action
            Result<Cirurgia> resultado = await servicoCirurgia.InserirAsync(cirurgia);

            //assert 
            resultado.Should().BeFailure();
            repositorioCirurgiaMoq.Verify(x => x.InserirAsync(cirurgia), Times.Never());
        }

        [TestMethod]
        public async Task Deve_editar_cirurgia_caso_ela_for_valida() //cenário 1
        {
            //arrange 
            Medico medico = new Medico();
            var cirurgia = BuildCirurgia("CirurgiaTeste", medico, TimeSpan.MinValue, TimeSpan.MaxValue);

            //action
            Result<Cirurgia> resultado = await servicoCirurgia.EditarAsync(cirurgia);

            //assert 
            resultado.Should().BeSuccess();
            repositorioCirurgiaMoq.Verify(x => x.Editar(cirurgia), Times.Once());
        }

        [TestMethod]
        public async Task Deve_excluir_consulta_caso_ela_esteja_cadastrada()
        {
            // Arrange
            Medico medico = new Medico();
            var cirurgia = BuildCirurgia("CirurgiaTeste", medico, TimeSpan.MinValue, TimeSpan.MaxValue);

            repositorioCirurgiaMoq.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>()))
                                  .ReturnsAsync(cirurgia);

            // Action
            Result<Cirurgia> resultadoCirurgia = await servicoCirurgia.ExcluirAsync(cirurgia.Id);

            // Assert
            resultadoCirurgia.Should().BeSuccess();
            repositorioCirurgiaMoq.Verify(x => x.Excluir(cirurgia), Times.Once());
        }

        private Cirurgia BuildCirurgia(string titulo, Medico medico, TimeSpan horaInicio, TimeSpan horaTermino)
        {
            return new Cirurgia()
            {
                Titulo = titulo,
                Medicos = new List<Medico>() { medico },
                HoraInicio = horaInicio,
                HoraTermino = horaTermino
            };
        }
    }
}