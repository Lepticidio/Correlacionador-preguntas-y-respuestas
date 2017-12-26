using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrelacionadorMir
{
    class Year
    {
        public int numero;
        public List<Pregunta> preguntas = new List<Pregunta>();

        public Year(int number)
        {
            numero = number;

            List<string> enunciados = ReadPreguntas("Mir/Preguntas" + number.ToString() + ".txt");
            CreaPreguntas(enunciados);
            List<int[]> respuestas = ReadRespuestas("Mir/Respuestas" + number.ToString() + ".txt");
            verificadorRespuestas(preguntas, respuestas);
            
        }
        private void WriteFile(String[] content, String _filename)
        {
            for (int i = 0; i < content.Length; i++)
            {

            }
            File.WriteAllLines(@_filename, content);
        }
        private List<string> ReadPreguntas(String _filename)
        {
            // Array de strings formado por las distintas líneas de texto que haya en el documento
            String[] content = File.ReadAllLines(@_filename, Encoding.Default);

            List<string> listaEnunciados = new List<string>();
            int contador = 0;
            for (int i = 0; i < content.Length; i++)
            {

                if (contador >= listaEnunciados.Count)
                {
                    listaEnunciados.Add(content[i]);
                }
                else
                {
                    if (empiezaConNumeroPunto(content[i]))
                    {
                        contador++;
                        listaEnunciados.Add(content[i]);
                    }
                    else
                    {
                        String[] aUnir = { listaEnunciados[contador], content[i] };
                        listaEnunciados[contador] = String.Join(" ", aUnir);
                    }
                }

            }
            return listaEnunciados;
        }
        private List<int[]> ReadRespuestas(String _filename)
        {
            // Array de strings formado por las distintas líneas de texto que haya en el documento
            String[] content = File.ReadAllLines(@_filename, Encoding.Default);

            List<int[]> listaRespuestas = new List<int[]>();
            int contador = 0;
            for (int i = 0; i < content.Length; i++)
            {

                string[] lineaDividida = content[i].Split('\t');
                for (int j = 0; j < lineaDividida.Length; j += 2)
                {
                    int number;
                    if (int.TryParse(lineaDividida[j + 1], out number))
                    {
                        int[] lineaRespuesta = { int.Parse(lineaDividida[j]), number };
                        listaRespuestas.Add(lineaRespuesta);
                    }
                    else
                    {
                        int[] lineaRespuesta = { int.Parse(lineaDividida[j]), 0 };
                        listaRespuestas.Add(lineaRespuesta);
                    }

                }

            }
            List<int[]> listaOrdenada = new List<int[]>();
            listaOrdenada.Add(listaRespuestas[0]);
            for (int i = 1; i < listaRespuestas.Count; i++)
            {
                bool added = false;
                for (int j = 0; j < listaOrdenada.Count; j++)
                {
                    if (listaRespuestas[i][0] < listaOrdenada[j][0] && !added)
                    {
                        listaOrdenada.Insert(j, listaRespuestas[i]);
                        added = true;
                    }

                }
                if (!added)
                {
                    listaOrdenada.Add(listaRespuestas[i]);
                }
            }
            return listaOrdenada;
        }
        private bool empiezaConNumeroPunto(string entrada)
        {
            if (entrada.Length > 3 && Char.IsNumber(entrada[0]) && ((entrada[1] == '.' && entrada[2] == ' ') || (Char.IsNumber(entrada[1]) && entrada[2] == '.' && entrada[3] == ' ') ||
                (Char.IsNumber(entrada[1]) && Char.IsNumber(entrada[2]) && entrada[3] == '.' && entrada[4] == ' ')))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void CreaPreguntas(List<string> enunciados)
        {
            int contaPreguntas = 0;
            for (int i = 0; i < enunciados.Count; i++)
            {
                if (i < enunciados.Count - 1)
                {
                    if (enunciados[i + 1][0] == '1' && enunciados[i + 1][1] == '.' && enunciados[i + 1][2] == ' ')
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

                    }
                }
                else
                {
                    Respuesta respuesta = new Respuesta(enunciados[i]);
                    respuesta.numero = enunciados[i][0];
                    respuesta.pregunta = preguntas[contaPreguntas - 1];
                    respuesta.pregunta.respuestas.Add(respuesta);
                }
            }


        }
        private void verificadorRespuestas(List<Pregunta> preguntillas, List<int[]> respuestillas)
        {
            for (int i = 0; i < preguntillas.Count; i++)
            {
                if (respuestillas[i][1] != 0)
                {
                    preguntillas[i].respuestas[respuestillas[i][1] - 1].correcta = true;
                }
            }
        }
    }
}
