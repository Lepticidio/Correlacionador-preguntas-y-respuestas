using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelacionadorMir
{
    class Palabra
    {
        public string texto;
        public List<Correlation> correlaciones = new List<Correlation>();

        public Palabra(string entrada)
        {
            texto = entrada;
        }

    }
}
