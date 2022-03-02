using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Hospital
{
    internal class Fila
    {
        public string PathFile = @"C:\Users\Junior\source\repos\Projeto_Hospital\Projeto_Hospital\bin\Debug\net5.0";

        public Paciente Cabeca { get; set; }
        public Paciente Cauda { get; set; }
        public int Elementos { get; set; }

        public Fila()
        {
            Cabeca = null;
            Cauda = null;
            Elementos = 0;
        }



        public void Inserir(Paciente paciente)
        {
            if (Vazia())
            {
                Cabeca = paciente;
                Cauda = paciente;
            }
            else
            {
                Paciente aux = Cabeca;

                do
                {
                    if (paciente.CPF == aux.CPF)
                        break;
                    else
                    {
                        Cauda.Proximo = paciente;
                        paciente.Anterior = Cauda;
                        Cauda = paciente;
                    }
                } while (aux != null);

            }

            Elementos++;
        }
        public void InserirInformacoesDoPaciente(Paciente paciente, string arquivo)
        {
            bool aguardandoNaFila = false;

            try
            {
                if (!File.Exists($"{PathFile}\\{arquivo}.txt"))
                    File.Create($"{PathFile}\\{arquivo}.txt").Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }

            try
            {

                StreamReader sr = new StreamReader($"{PathFile}\\{arquivo}.txt");

                string line = sr.ReadLine();

                while (line != null)
                {
                    string[] dados = line.Split(";");

                    if (paciente.CPF == dados[0])
                    {
                        aguardandoNaFila = true;
                        sr.Close();
                        break;
                    }

                    line = sr.ReadLine();
                }

                sr.Close();

                if (!aguardandoNaFila)
                {
                    StreamWriter sw = new StreamWriter($"{PathFile}\\{arquivo}.txt", append: true);
                    sw.WriteLine($"{paciente.CPF};{paciente.Nome};{paciente.DataNasc.ToString("dd/MM/yyyy")};{paciente.Sexo};");
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

        public void Imprimir()
        {
            if (Elementos == 0) return;

            Paciente paciente = Cabeca;

            do
            {
                Console.WriteLine(paciente.ToString());

                paciente = paciente.Proximo;

            } while (paciente != null);
        }


        public bool Vazia()
        {
            return (Cabeca == null && Cauda == null) ? true : false;
        }
    }

}
