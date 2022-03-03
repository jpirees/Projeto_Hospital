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
        public Paciente Cabeca { get; set; }
        public Paciente Cauda { get; set; }
        public int Elementos { get; set; }

        public Fila()
        {
            Cabeca = null;
            Cauda = null;
            Elementos = 0;
        }


        public void Imprimir()
        {
            if (Elementos == 0)
            {
                Console.WriteLine("[ A fila está vazia ]");
                return;
            }

            Paciente paciente = Cabeca;

            do
            {
                Console.WriteLine(paciente.ToString());
                Console.WriteLine("\n********************************************");
                paciente = paciente.Proximo;

            } while (paciente != null);
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
                Cauda.Proximo = paciente;
                paciente.Anterior = Cauda;
                Cauda = paciente;
            }

            Elementos++;
        }

        public Paciente Buscar(string cpf)
        {
            if (Elementos == 0) return null;

            Paciente paciente = Cabeca;

            do
            {
                if (cpf == paciente.CPF)
                    return paciente;

                paciente = paciente.Proximo;

            } while (paciente != null);

            return null;
        }



        public void InserirDadosNoArquivo(Paciente paciente, string arquivo)
        {
            bool aguardandoNaFila = false;

            try
            {

                StreamReader sr = new StreamReader($"{arquivo}.txt");

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
                    StreamWriter sw = new StreamWriter($"{arquivo}.txt", append: true);
                    sw.WriteLine($"{paciente.CPF};{paciente.Nome};{paciente.DataNasc.ToString("dd/MM/yyyy")};{paciente.Sexo};");
                    sw.Close();

                    Inserir(paciente);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

        public void CarregarDadosDoArquivo(Fila fila, string arquivo)
        {
            bool arquivoFila = false;

            try
            {
                if (File.Exists($"{arquivo}.txt"))
                    arquivoFila = true;
                else
                    File.Create($"{arquivo}.txt").Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }


            if (arquivoFila)
            {
                try
                {
                    StreamReader sr = new StreamReader($"{arquivo}.txt");

                    string line = sr.ReadLine();

                    while (line != null)
                    {
                        string[] dados = line.Split(";");

                        Paciente paciente = new Paciente(dados[0], dados[1], DateTime.Parse(dados[2]), dados[3]);

                        fila.Inserir(paciente);

                        line = sr.ReadLine();
                    }

                    sr.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.ToString());
                }
            }

        }

        public bool Vazia()
        {
            return (Cabeca == null && Cauda == null) ? true : false;
        }
    }

}
