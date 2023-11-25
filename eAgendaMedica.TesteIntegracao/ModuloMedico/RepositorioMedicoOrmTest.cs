using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.TestesIntegracao.Compartilhado;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eAgendaMedica.TesteIntegracao.ModuloMedico
{
    [TestClass]
    public class RepositorioMedicoEmOrmTest : TestesIntegracaoBase
    {

        [TestMethod]
        public async Task Deve_Inserir_Medico()
        {
            // Arrange
            var medico = Builder<Medico>.CreateNew().Build();

            // Act
            await repositorioMedico.InserirAsync(medico);
            await contextoPersistencia.GravarAsync();

            // Assert
            var medicoSelecionado = await repositorioMedico.SelecionarPorIdAsync(medico.Id);
            medicoSelecionado.Should().BeEquivalentTo(medico);
        }

        [TestMethod]
        public async Task Deve_Editar_Medico()
        {
            // Arrange
            var medicoId = Builder<Medico>.CreateNew().Persist().Id;
            var medico = await repositorioMedico.SelecionarPorIdAsync(medicoId);
            medico.Nome = "Camila";

            // Act
            repositorioMedico.Editar(medico);
            await contextoPersistencia.GravarAsync();

            // Assert
            var medicoEditado = await repositorioMedico.SelecionarPorIdAsync(medico.Id);
            medicoEditado.Should().BeEquivalentTo(medico);
        }

        [TestMethod]
        public async Task Deve_Excluir_Medico()
        {
            // Arrange
            var medico = Builder<Medico>.CreateNew().Persist();

            // Act
            repositorioMedico.Excluir(medico);
            await contextoPersistencia.GravarAsync();

            // Assert
            var medicoResposta = await repositorioMedico.SelecionarPorIdAsync(medico.Id);
            medicoResposta.Should().BeNull();
        }

        [TestMethod]
        public async Task Deve_selecionar_todos_Medico()
        {
            // Arrange
            var medico1 = Builder<Medico>.CreateNew().Persist();
            var medico2 = Builder<Medico>.CreateNew().Persist();

            // Act
            var medicos = await repositorioMedico.SelecionarTodosAsync();

            // Assert
            medicos.Should().NotBeEmpty();
            medicos.Should().Contain(medico1);
            medicos.Should().Contain(medico2);
        }

        [TestMethod]
        public void Deve_selecionar_medico_por_id()
        {
            // Arrange
            var medico = Builder<Medico>.CreateNew().Persist();

            // Act
            var medicoEncontrado = repositorioMedico.SelecionarPorId(medico.Id);

            // Assert
            medicoEncontrado.Should().Be(medico);
        }
    }
}
