using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelacionadorMir
{
    class Respuesta
    {
        public bool correcta = false;
        public int numero;
        public string texto;
        public Pregunta pregunta;

        public Respuesta(string textoOrigen)
        {
            texto = textoOrigen;
        }

    }
    
}
