using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.ModuloConsulta
{
    public class Consulta : Entidade
    {
        public string Titulo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public Medico Medico { get; set; }
        public Guid MedicoId { get; set; }

        public Consulta() { }

        public Consulta(string titulo, TimeSpan horaInicio, Medico medico, Guid medicoId)
        {
            Titulo = titulo;
            HoraInicio = horaInicio;
            Medico = medico;
            MedicoId = medicoId;
        }
    }
}
