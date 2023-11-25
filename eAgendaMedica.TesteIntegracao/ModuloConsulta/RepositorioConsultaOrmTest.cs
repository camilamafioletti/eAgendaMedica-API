using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.TestesIntegracao.Compartilhado;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eAgendaMedica.TesteIntegracao.ModuloConsulta
{
    [TestClass]
    public class RepositorioConsultaEmOrmTest : TestesIntegracaoBase
    {
        [TestMethod]
        public async Task Deve_Inserir_Consulta()
        {
            // Arrange
            var medico = Builder<Medico>.CreateNew().Build();
            var consulta = Builder<Consulta>.CreateNew().Build();

            consulta.Medico = medico;
            consulta.MedicoId = medico.Id;

            // Act
            await repositorioConsulta.InserirAsync(consulta);
            await contextoPersistencia.GravarAsync();

            // Assert
            var consultaSelecionado = await repositorioConsulta.SelecionarPorIdAsync(consulta.Id);
            consultaSelecionado.Should().BeEquivalentTo(consulta);
        }

        [TestMethod]
        public async Task Deve_Editar_Consulta()
        {
            // Arrange
            var medico = Builder<Medico>.CreateNew().Persist();
            var consulta = Builder<Consulta>.CreateNew().With(c => c.Medico = medico).Persist().Id;

            // Act
            var consultaOriginal = await repositorioConsulta.SelecionarPorIdAsync(consulta);
            consultaOriginal.Titulo = "Camila";
            repositorioConsulta.Editar(consultaOriginal);
            await contextoPersistencia.GravarAsync();

            // Assert
            var consultaEditada = await repositorioConsulta.SelecionarPorIdAsync(consulta);
            consultaEditada.Should().BeEquivalentTo(consultaOriginal);
        }

        [TestMethod]
        public async Task Deve_Excluir_Consulta()
        {
            // Arrange
            var medico = Builder<Medico>.CreateNew().Persist();
            var consulta = Builder<Consulta>.CreateNew().With(c => c.Medico = medico).Persist();

            // Act
            repositorioConsulta.Excluir(consulta);
            repositorioMedico.Excluir(medico);
            await contextoPersistencia.GravarAsync();

            // Assert
            var consultaResposta = await repositorioConsulta.SelecionarPorIdAsync(consulta.Id);
            consultaResposta.Should().BeNull();
        }

        [TestMethod]
        public async Task Deve_selecionar_todos_Consulta()
        {
            // Arrange
            var medico1 = Builder<Medico>.CreateNew().Persist();
            var medico2 = Builder<Medico>.CreateNew().Persist();

            var consulta1 = Builder<Consulta>.CreateNew().With(x => x.Medico = medico1).Persist();
            var consulta2 = Builder<Consulta>.CreateNew().With(x => x.Medico = medico2).Persist();

            // Act
            var consultas = await repositorioConsulta.SelecionarTodosAsync();

            // Assert
            consultas.Should().NotBeEmpty();
            consultas.Should().Contain(consulta1);
            consultas.Should().Contain(consulta2);
        }

        [TestMethod]
        public void Deve_selecionar_consulta_por_id()
        {
            // Arrange
            var medico = Builder<Medico>.CreateNew().Persist();
            var consulta = Builder<Consulta>.CreateNew().With(x => x.Medico = medico).Persist();

            // Act
            var consultaEncontrada = repositorioConsulta.SelecionarPorId(consulta.Id);

            // Assert
            consultaEncontrada.Should().Be(consulta);
            consultaEncontrada.Medico.Should().Be(medico);
        }
    }
}
