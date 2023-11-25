using eAgendaMedica.Aplicacao.ModuloConsulta;
using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloMedico;
using FluentAssertions;
using FluentResults;
using FluentResults.Extensions.FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace eAgendaMedica.TestesUnitarios.Aplicacao.ModuloConsulta
{
    [TestClass]
    public class ServicoConsultaTest
    {
        Mock<IRepositorioConsulta> repositorioConsultaMoq;
        Mock<ValidadorConsulta> validadorMoq;
        Mock<IContextoPersistencia> contextoMoq;

        private ServicoConsulta servicoConsulta;

        public ServicoConsultaTest()
        {
            repositorioConsultaMoq = new Mock<IRepositorioConsulta>();
            validadorMoq = new Mock<ValidadorConsulta>();
            contextoMoq = new Mock<IContextoPersistencia>();
            servicoConsulta = new ServicoConsulta(repositorioConsultaMoq.Object, contextoMoq.Object);
        }

        [TestMethod]
        public async Task Deve_inserir_consulta_caso_ela_seja_valida() //cenário 1
        {
            //arrange 
            Medico medico = new Medico();
            var consulta = BuildConsulta("CirurgiaTeste", medico, TimeSpan.MinValue, TimeSpan.MaxValue);

            //action
            Result<Consulta> resultado = await servicoConsulta.InserirAsync(consulta);

            //assert 
            resultado.Should().BeSuccess();
            repositorioConsultaMoq.Verify(x => x.InserirAsync(consulta), Times.Once());
        }

        [TestMethod]
        public async Task Nao_Deve_inserir_consulta_caso_ela_seja_invalida() //cenário 1
        {
            //arrange 
            Medico medico = new Medico();
            var consulta = BuildConsulta("", medico, TimeSpan.Zero, TimeSpan.Zero);

            //action
            Result<Consulta> resultado = await servicoConsulta.InserirAsync(consulta);

            //assert 
            resultado.Should().BeFailure();
            repositorioConsultaMoq.Verify(x => x.InserirAsync(consulta), Times.Never());
        }

        [TestMethod]
        public async Task Deve_editar_consulta_caso_ela_for_valida() //cenário 1
        {
            //arrange 
            Medico medico = new Medico();
            var consulta = BuildConsulta("teste", medico, TimeSpan.MinValue, TimeSpan.MaxValue);

            //action
            Result<Consulta> resultado = await servicoConsulta.EditarAsync(consulta);

            //assert 
            resultado.Should().BeSuccess();
            repositorioConsultaMoq.Verify(x => x.Editar(consulta), Times.Once());
        }

        [TestMethod]
        public async Task Deve_excluir_consulta_caso_ela_esteja_cadastrada()
        {
            // Arrange
            Medico medico = new Medico();
            var consulta = BuildConsulta("ConsultaParaExcluir", medico, TimeSpan.FromHours(9), TimeSpan.FromHours(10));

            repositorioConsultaMoq.Setup(x => x.SelecionarPorIdAsync(It.IsAny<Guid>()))
                                  .ReturnsAsync(consulta);

            // Action
            Result<Consulta> resultadoConsulta = await servicoConsulta.ExcluirAsync(consulta.Id);

            // Assert
            resultadoConsulta.Should().BeSuccess();
            repositorioConsultaMoq.Verify(x => x.Excluir(consulta), Times.Once());
        }


        private Consulta BuildConsulta(string titulo, Medico medico, TimeSpan horaInicio, TimeSpan horaTermino)
        {
            return new Consulta()
            {
                Titulo = titulo,
                Medico = new Medico(),
                HoraInicio = horaInicio,
                HoraTermino = horaTermino
            };
        }
    }
}