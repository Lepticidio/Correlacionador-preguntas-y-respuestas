using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelacionadorMir
{
    class Correlation
    {
        public string texto;
        public int vecesPositivas, vecesNegativas, Total;
        public float porcentaje, porcentajePonderado;
        
        public Correlation (string entrada)
        {
            texto = entrada;
        }

        public void Actualizar()
        {
            Total = vecesPositivas + vecesNegativas;
            porcentaje = ((float)vecesPositivas - (float)vecesNegativas)*100f / (float)Total;
            porcentajePonderado = porcentaje * (float)Total / 100f;
        }
    }
}
