using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelacionadorMir
{
    
    class Pregunta
    {
        public int numero;
        public string texto;
        public List<Respuesta> respuestas = new List<Respuesta>();

        public Pregunta(string textoOrigen)
        {
            texto = textoOrigen;
        }

    }
}
