using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelacionadorMir
{
    class Program
    {
        static List<Pregunta> preguntas = new List<Pregunta>();
        static List<Respuesta> respuestas = new List<Respuesta>();
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Default;
            List<string> listaEnun = ReadFile("Mir/Preguntas2016.txt");

            CreaPreguntas(listaEnun);

            for (int i=0; i< preguntas.Count; i++)
            {
                Console.WriteLine(preguntas[i].texto);
            }

            for (int i = 0; i < respuestas.Count; i++)
            {
                Console.WriteLine(respuestas[i].texto);
            }

            Console.ReadLine();
        }
        static private void WriteFile(String[] content, String _filename)
        {
            for (int i = 0; i < content.Length; i++)
            {
                
            }
            File.WriteAllLines(@_filename, content);
        }
        static private List<string> ReadFile(String _filename)
        {
            // Array de strings formado por las distintas líneas de texto que haya en el documento
            String[] content = File.ReadAllLines(@_filename, Encoding.Default);

            List<string> listaEnunciados = new List<string>();
            int contador = 0;
            for (int i = 0; i< content.Length; i++)
            {

                if (contador>= listaEnunciados.Count)
                {
                    listaEnunciados.Add (content[i]);
                }
                else
                {
                    if ( empiezaConNumeroPunto(content[i]) )
                    {
                        contador++;
                        listaEnunciados.Add(content[i]);
                    }
                    else
                    {
                        String[] aUnir = { listaEnunciados[contador], content[i] };
                        listaEnunciados[contador] =  String.Join(" ", aUnir);
                    }
                }
                
            }
            return listaEnunciados;
        }
        static private bool empiezaConNumeroPunto(string entrada)
        {
            if (entrada.Length > 3 && Char.IsNumber(entrada[0]) && ((entrada[1] == '.' && entrada[2] == ' ')|| (Char.IsNumber(entrada[1]) && entrada[2] == '.' && entrada[3] == ' ') ||
                (Char.IsNumber(entrada[1]) && Char.IsNumber(entrada[2]) && entrada[3] == '.' && entrada[4] == ' ')))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static private void CreaPreguntas(List<string> enunciados)
        {
            int contaPreguntas = 0;
            for(int i=0; i< enunciados.Count; i++)
            {
                if(i< enunciados.Count - 1)
                {
                    if(enunciados[i+1][0]=='1'&& enunciados[i+1][1]== '.' && enunciados[i + 1][2] == ' ')
                    {
                        contaPreguntas++;
                        Pregunta pregunta = new Pregunta(enunciados[i]);
                        pregunta.numero = contaPreguntas;
                        preguntas.Add(pregunta);
                    }
                    else
                    {
                        Respuesta respuesta = new Respuesta(enunciados[i]);
                        respuesta.numero = enunciados[i][0];
                        respuesta.pregunta = preguntas[contaPreguntas - 1];
                        respuesta.pregunta.respuestas.Add(respuesta);
                        respuestas.Add(new Respuesta(enunciados[i]));
                        
                    }
                }
                else
                {
                    Respuesta respuesta = new Respuesta(enunciados[i]);
                    respuesta.pregunta = preguntas[contaPreguntas - 1];
                    respuesta.pregunta.respuestas.Add(respuesta);
                    respuestas.Add(new Respuesta(enunciados[i]));
                }
            }


        }

        static private verificadorRespuesta(int year, int nPregunta, int nRespuesta)
        {

        }

    }
}
