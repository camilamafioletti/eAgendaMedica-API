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
            var resultado = validador.TestValidate(medico);

            resultado.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [TestMethod]
        public void Nome_medico_deve_ter_no_minimo_3_caracteres()
        {
            medico.Nome = "ab";

            var resultado = validador.TestValidate(medico);

            resultado.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [TestMethod]
        public void Nome_medico_deve_ser_composto_por_letras()
        {
            medico.Nome = "abc123";

            var resultado = validador.TestValidate(medico);

            resultado.ShouldHaveValidationErrorFor(x => x.Nome);
        }

        [TestMethod]

        public void Telefone_medico_deve_estar_no_formato_valido()
        {
            medico.Telefone = "49 9999999999-9999";

            var resultado = validador.TestValidate(medico);

            resultado.ShouldHaveValidationErrorFor(x => x.Telefone);
        }

        [TestMethod]
        public void Crm_medico_deve_estar_no_formato_valido()
        {
            medico.Crm = "55555sc";

            var resultado = validador.TestValidate(medico);

            resultado.ShouldHaveValidationErrorFor(x => x.Crm);
        }
    }
}
