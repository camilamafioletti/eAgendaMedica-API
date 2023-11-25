using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.TestesIntegracao.Compartilhado;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eAgendaMedica.TesteIntegracao.ModuloCirurgia
{
    [TestClass]
    public class RepositorioCirurgiaOrmTest : TestesIntegracaoBase
    {
        [TestMethod]
        public async Task Deve_Inserir_Cirurgia()
        {
            // Arrange
            var cirurgia = Builder<Cirurgia>.CreateNew().Build();

            // Act
            await repositorioCirurgia.InserirAsync(cirurgia);
            await contextoPersistencia.GravarAsync();

            // Assert
            var cirurgiaSelecionada = await repositorioCirurgia.SelecionarPorIdAsync(cirurgia.Id);
            cirurgiaSelecionada.Should().BeEquivalentTo(cirurgia);
        }

        [TestMethod]
        public async Task Deve_Editar_Cirurgia()
        {
            // Arrange
            var cirurgiaId = Builder<Cirurgia>.CreateNew().Persist().Id;
            var cirurgia = await repositorioCirurgia.SelecionarPorIdAsync(cirurgiaId);
            cirurgia.Titulo = "Camila";

            // Act
            repositorioCirurgia.Editar(cirurgia);
            await contextoPersistencia.GravarAsync();

            // Assert
            var cirurgiaEditado = await repositorioCirurgia.SelecionarPorIdAsync(cirurgia.Id);
            cirurgiaEditado.Should().BeEquivalentTo(cirurgia);
        }

        [TestMethod]
        public async Task Deve_Excluir_Cirurgia()
        {
            // Arrange
            var cirurgia = Builder<Cirurgia>.CreateNew().Persist();

            // Act
            repositorioCirurgia.Excluir(cirurgia);
            await contextoPersistencia.GravarAsync();

            // Assert
            var cirurgiaResposta = await repositorioCirurgia.SelecionarPorIdAsync(cirurgia.Id);
            cirurgiaResposta.Should().BeNull();
        }

        [TestMethod]
        public async Task Deve_selecionar_todos_Cirurgia()
        {
            // Arrange
            var cirurgia1 = Builder<Cirurgia>.CreateNew().Persist();
            var cirurgia2 = Builder<Cirurgia>.CreateNew().Persist();

            // Act
            var cirurgias = await repositorioCirurgia.SelecionarTodosAsync();

            // Assert
            cirurgias.Should().NotBeEmpty();
            cirurgias.Should().Contain(cirurgia1);
            cirurgias.Should().Contain(cirurgia2);
        }

        [TestMethod]
        public void Deve_selecionar_cirurgia_por_id()
        {
            // Arrange
            var cirurgia = Builder<Cirurgia>.CreateNew().Persist();

            // Act
            var cirurgiaEncontrada = repositorioCirurgia.SelecionarPorId(cirurgia.Id);

            // Assert
            cirurgiaEncontrada.Should().Be(cirurgia);
        }
    }
}
