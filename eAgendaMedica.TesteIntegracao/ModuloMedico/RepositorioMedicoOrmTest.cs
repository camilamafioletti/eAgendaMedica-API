using eAgendaMedica.Dominio.ModuloConsulta;
using eAgendaMedica.Dominio.ModuloMedico;
using eAgendaMedica.Infra.Orm.Compartilhado;
using eAgendaMedica.TestesIntegracao.Compartilhado;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eAgendaMedica.TesteIntegracao.ModuloMedico
{
    [TestClass]
    public class RepositorioMedicoEmOrmTest : TestesIntegracaoBase
    {

        [TestMethod]
        public async Task Deve_Inserir_Medico()
        {
            var medico = Builder<Medico>.CreateNew().Build();

            await repositorioMedico.InserirAsync(medico);

            await contextoPersistencia.GravarAsync();

            var medicoSelecionado = await repositorioMedico.SelecionarPorIdAsync(medico.Id);

            medicoSelecionado.Should().BeEquivalentTo(medico);
        }


        [TestMethod]
        public async Task Deve_Editar_Medico()
        {
            var medicoId = Builder<Medico>.CreateNew().Persist().Id;

            var medico = await repositorioMedico.SelecionarPorIdAsync(medicoId);

            medico.Nome = "Cleiton";

            repositorioMedico.Editar(medico);

            await contextoPersistencia.GravarAsync();

            var medicoEditado = await repositorioMedico.SelecionarPorIdAsync(medico.Id);

            medicoEditado.Should().BeEquivalentTo(medico);
        }

        [TestMethod]
        public async Task Deve_Excluir_Medico()
        {
            var medico = Builder<Medico>.CreateNew().Persist();

            repositorioMedico.Excluir(medico);

            await contextoPersistencia.GravarAsync();

            var medicoResposta = await repositorioMedico.SelecionarPorIdAsync(medico.Id);

            medicoResposta.Should().BeNull();
        }

        //[TestMethod]
        //public void Deve_selecionar_todos_Medico()
        //{
        //    var medico1 = Builder<Medico>.CreateNew().Persist();
        //    var medico2 = Builder<Medico>.CreateNew().Persist();

        //    var consultas = repositorioConsulta.SelecionarTodosAsync();

        //    consultas.Should().ContainInOrder(medico1, medico2);
        //    consultas.Should().HaveCount(2);
        //}


        [TestMethod]
        public void Deve_selecionar_medico_por_id()
        {
            var medico = Builder<Medico>.CreateNew().Persist();

            var medicoEncontrado = repositorioMedico.SelecionarPorId(medico.Id);

            medicoEncontrado.Should().Be(medico);
        }
    }
}
    
