using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CorrelacionadorMir
{
    class Program
    {

        static List<Year> years = new List<Year>();
        static List<Palabra> palabras = new List<Palabra>();
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;
            for(int i = 2016; i<2017; i++)
            {
                Year year = new Year(i);
                years.Add(year);
                for (int j = 0; j< year.preguntas.Count; j++)
                {
                    string[] palabritas = GetWords(year.preguntas[j].texto);
                    foreach (string pal in palabritas)
                    {

                        bool repetida = false;
                        for (int k = 0; k < palabras.Count; k++)
                        {
                            if (palabras[k].texto == pal)
                            {
                                repetida = true;
                                Correlacionador(palabras[k], year.preguntas[j]);
                                break;
                            }
                        }
                        if (!repetida)
                        {                            
                           Palabra pa =  new Palabra(pal);
                           Correlacionador(pa, year.preguntas[j]);
                           palabras.Add(pa);
                        }
                    }                    
                }
            }
            int contador = 0;
            foreach (Palabra pa in palabras)
            {
                if (contador > 100)
                {
                    break;
                }
                contador++;
                Console.WriteLine(contador.ToString() + " -> " +  pa.texto);
                foreach (Correlation corr in pa.correlaciones)
                {
                    Console.WriteLine(corr.texto + " : Porcentaje: " + corr.porcentaje + "%, Total: " + corr.Total
                        + ", Ponderado: " + corr.porcentajePonderado + ", Positivas: " + corr.vecesPositivas
                        + ", Negativas: " + corr.vecesNegativas);
                }

            }
            
            Console.ReadLine();
        }
        static string[] GetWords(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = from m in matches.Cast<Match>()
                        where !string.IsNullOrEmpty(m.Value)
                        select TrimSuffix(m.Value);

            return words.ToArray();
        }

        static string TrimSuffix(string word)
        {
            int apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1)
            {
                word = word.Substring(0, apostropheLocation);
            }

            return word;
        }
        static void Correlacionador ( Palabra pal, Pregunta pre)
        {
            for (int i=0; i< pre.respuestas.Count; i++)
            {
                string[] palabritas = GetWords(pre.respuestas[i].texto);
                foreach (string pa in palabritas)
                {
                    bool repetido = false;
                    for (int j = 0; j < pal.correlaciones.Count; j++)
                    {
                        
                        if (pa == pal.correlaciones[j].texto)
                        {
                            repetido = true;
                            if (pre.respuestas[i].correcta)
                            {
                                pal.correlaciones[j].vecesPositivas++;
                            }
                            else
                            {
                                pal.correlaciones[j].vecesNegativas++;
                            }
                            pal.correlaciones[j].Actualizar();
                        }
                    }
                    if (!repetido)
                    {
                        Correlation corr = new Correlation(pa);
                        if (pre.respuestas[i].correcta)
                        {
                            corr.vecesPositivas++;
                        }
                        else
                        {
                            corr.vecesNegativas++;
                        }
                        corr.Actualizar();

                        pal.correlaciones.Add(corr);
                    }
                }                
            }
        }
    }
}
