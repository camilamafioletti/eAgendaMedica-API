using eAgendaMedica.Dominio.ModuloMedico;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaMedica.TestesUnitarios.Dominio.ModuloMedico
{
    [TestClass]
    public class ValidadorMedicoTest
    {

        private Medico medico;
        private ValidadorMedico validador;

        public ValidadorMedicoTest()
        {
            medico = new Medico();
            validador = new ValidadorMedico();
        }

        [TestMethod]
        public void Nome_medico_nao_deve_ser_nulo_ou_vazio()
        {
            // Arrange
            var resultado = validador.TestValidate(medico);

            // Act & Assert
            resultado.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [TestMethod]
        public void Nome_medico_deve_ter_no_minimo_3_caracteres()
        {
            // Arrange
            medico.Nome = "ab";

            // Act & Assert
            var resultado = validador.TestValidate(medico);
            resultado.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [TestMethod]
        public void Nome_medico_deve_ser_composto_por_letras()
        {
            // Arrange
            medico.Nome = "abc123";

            // Act & Assert
            var resultado = validador.TestValidate(medico);
            resultado.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [TestMethod]
        public void Telefone_medico_deve_estar_no_formato_valido()
        {
            // Arrange
            medico.Telefone = "49 8888888889999";

            // Act & Assert
            var resultado = validador.TestValidate(medico);
            resultado.ShouldHaveValidationErrorFor(x => x.Telefone);
        }

        [TestMethod]
        public void Crm_medico_deve_estar_no_formato_valido()
        {
            // Arrange
            medico.Crm = "12345sc";

            // Act & Assert
            var resultado = validador.TestValidate(medico);
            resultado.ShouldHaveValidationErrorFor(x => x.Crm);
        }
    }
}
