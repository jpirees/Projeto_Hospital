using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Hospital
{
    internal class Paciente
    {
        public string PathFile = @"C:\Users\Junior\source\repos\Projeto_Hospital\Projeto_Hospital\bin\Debug\net5.0";

        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNasc { get; set; }
        public string Sexo { get; set; }

        public Paciente Proximo { get; set; }
        public Paciente Anterior { get; set; }


        public Paciente() { }

        public Paciente(string cPF, string nome, DateTime dataNasc, string sexo)
        {
            CPF = cPF;
            Nome = nome;
            DataNasc = dataNasc;
            Sexo = sexo;
            Proximo = null;
            Anterior = null;
        }


        public override string ToString()
        {
            return $"\nCPF: {CPF}\nNome: {Nome}\nDt. Nasc.: {DataNasc.ToString("dd/MM/yyyy")}\nSexo: {Sexo}";
        }

        public void SalvarInformacoesPaciente()
        {
            try
            {
                StreamWriter sw = new StreamWriter(PathFile + "\\Pacientes.txt", append: true);
                sw.WriteLine($"{CPF};{Nome};{DataNasc.ToString("dd/MM/yyyy")};{Sexo};");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

        public Paciente BuscarInformacoesPaciente(string cpf)
        {
            Paciente paciente;

            try
            {
                if (!File.Exists($"{PathFile}\\Pacientes.txt"))
                {
                    File.Create($"{PathFile}\\Pacientes.txt").Close();
                    return null;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }

            try
            {
                StreamReader sr = new StreamReader($"{PathFile}\\Pacientes.txt");
                string line = sr.ReadLine();

                while (line != null)
                {
                    string[] dados = line.Split(";");

                    if (cpf == dados[0])
                    {
                        paciente = new Paciente(dados[0], dados[1], DateTime.Parse(dados[2]), dados[3]);

                        sr.Close();

                        return paciente;
                    }

                    line = sr.ReadLine();
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }

            return null;
        }
    }
}
