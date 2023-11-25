using eAgendaMedica.Dominio.ModuloConsulta;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eAgendaMedica.TestesUnitarios.Dominio.ModuloConsulta
{
    [TestClass]
    public class ValidadorConsultaTest
    {
        private Consulta consulta;
        private ValidadorConsulta validador;

        public ValidadorConsultaTest()
        {
            consulta = new Consulta();
            validador = new ValidadorConsulta();
        }

        [TestMethod]
        public void Titulo_consulta_nao_deve_ser_nulo_ou_vazio()
        {
            //action
            var resultado = validador.TestValidate(consulta);

            //assert
            resultado.ShouldHaveValidationErrorFor(x => x.Titulo);
        }

        [TestMethod]
        public void Titulo_consulta_deve_ter_no_minimo_3_caracteres()
        {
            //arrange
            consulta.Titulo = "ab";

            //action
            var resultado = validador.TestValidate(consulta);

            //assert
            resultado.ShouldHaveValidationErrorFor(x => x.Titulo);
        }
    }
}
