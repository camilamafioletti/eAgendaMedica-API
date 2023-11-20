using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloCirurgia;
using eAgendaMedica.Dominio.ModuloConsulta;

namespace eAgendaMedica.Dominio.ModuloMedico
{
    public class Medico : Entidade
    {
        public string Crm { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public bool Disponibilidade { get; set; }
        public List<Consulta> Consultas { get; set; }
        public List<Cirurgia> Cirurgias { get; set; }

        public Medico(){ }

        public Medico(string crm, string nome, string telefone)
        {
            Crm = crm;
            Nome = nome;
            Telefone = telefone;
        }
    }
}
