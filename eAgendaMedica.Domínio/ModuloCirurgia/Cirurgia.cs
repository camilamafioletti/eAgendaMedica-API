using eAgendaMedica.Dominio.Compartilhado;
using eAgendaMedica.Dominio.ModuloMedico;

namespace eAgendaMedica.Dominio.ModuloCirurgia
{
    public class Cirurgia : Entidade
    {
        public string Titulo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraTermino { get; set; }
        public List<Medico> Medicos { get; set; }

        public Cirurgia() { }

        public Cirurgia(string titulo, TimeSpan horaInicio)
        {
            Titulo = titulo;
            HoraInicio = horaInicio;
        }
    }
}
