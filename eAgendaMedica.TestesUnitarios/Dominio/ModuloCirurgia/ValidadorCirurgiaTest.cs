using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eAgendaMedica.TestesUnitarios.Dominio.ModuloConsulta
{
    [TestClass]
    public class ValidadorCirurgiaTest
    {
        private Cirurgia cirurgia;
        private ValidadorCirurgia validador;

        public ValidadorCirurgiaTest()
        {
            cirurgia = new Cirurgia();
            validador = new ValidadorCirurgia();
        }

        [TestMethod]
        public void Titulo_cirurgia_nao_deve_ser_nulo_ou_vazio()
        {
            //action
            var resultado = validador.TestValidate(cirurgia);

            //assert
            resultado.ShouldHaveValidationErrorFor(x => x.Titulo);
        }

        [TestMethod]
        public void Titulo_cirurgia_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            cirurgia.Titulo = "ab";

            //action
            var resultado = validador.TestValidate(cirurgia);

            //assert
            resultado.ShouldHaveValidationErrorFor(x => x.Titulo);
        }

    }
}
