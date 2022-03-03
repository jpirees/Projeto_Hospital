using System;

namespace Projeto_Hospital
{

    internal class Program
    {
        public static Fila fila_normal = new Fila();
        public static Fila fila_preferencial = new Fila();

        static void Main(string[] args)
        {
            fila_normal.CarregarDadosDoArquivo(fila_normal, "FilaNormal");
            fila_preferencial.CarregarDadosDoArquivo(fila_preferencial, "FilaPreferencial");

            string acao;
            int preferencial = 0;

            do
            {
                Console.Clear();

                switch (acao = Menu())
                {
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Saindo...");
                        break;

                    case "1":
                        NovoPaciente();
                        break;

                    case "2":
                        if (fila_preferencial.Elementos > 0 && preferencial < 2)
                        {
                            Console.WriteLine(fila_preferencial.Cabeca.ToString());
                            preferencial++;
                        }
                        else
                        {
                            Console.WriteLine(fila_normal.Cabeca.ToString());
                            preferencial = 0;
                        }
                        break;

                    case "3":
                        BuscarPacienteNaFila();
                        break;

                    case "8":
                        Console.Clear();
                        Console.WriteLine("******************** Fila Normal *******************");
                        fila_normal.Imprimir();
                        break;

                    case "9":
                        Console.Clear();
                        Console.WriteLine("******************** Fila Preferencial *******************");
                        fila_preferencial.Imprimir();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Ação inválida");
                        break;
                }

                Console.ReadKey();

            } while (acao != "0");
        }

        private static string Menu()
        {
            Console.WriteLine("******************** Menu *******************");
            Console.WriteLine("[1] Chamar Paciente");
            Console.WriteLine("[2] Chamar Paciente para Exame");
            Console.WriteLine("[3] Buscar Paciente na Fila");
            Console.WriteLine("*********************************************");
            Console.WriteLine("[8] Visualizar Pacientes na Fila Normal");
            Console.WriteLine("[9] Visualizar Pacientes na Fila Preferencial");
            Console.WriteLine("*********************************************");
            Console.WriteLine("[0] Sair");
            Console.WriteLine("*********************************************");
            Console.WriteLine();
            Console.Write(":: ");

            return Console.ReadLine();
        }

        private static void NovoPaciente()
        {
            Console.Clear();

            Paciente paciente = new Paciente();

            Console.WriteLine("****************** Buscar Paciente ****************");
            Console.Write("\nInforme o CPF: ");
            string cpf = Console.ReadLine();
            Console.WriteLine("\n***************************************************");

            paciente = paciente.BuscarPacienteNoArquivo(cpf);

            if (paciente != null)
            {
                Console.Clear();

                Console.WriteLine("****************** Ficha do Paciente ****************");
                Console.WriteLine(paciente.ToString());
                Console.WriteLine("\n*****************************************************");
            }
            else
                paciente = CadastrarPaciente(cpf);

            if ((DateTime.Now.Year - paciente.DataNasc.Year) >= 60)
                fila_preferencial.InserirDadosNoArquivo(paciente, "FilaPreferencial");
            else
                fila_normal.InserirDadosNoArquivo(paciente, "FilaNormal");

        }

        private static Paciente CadastrarPaciente(string cpf)
        {
            Console.Clear();

            string sexo;

            Console.WriteLine("********** Paciente **********");

            Console.WriteLine($"\nInforme o CPF:\n{cpf}");

            Console.WriteLine("\nInforme o Nome:");
            string nome = Console.ReadLine().ToUpper();

            Console.WriteLine("\nInforme o Data Nasc.:");
            DateTime dataNasc = DateTime.Parse(Console.ReadLine());

            #region Verifica Sexo
            do
            {
                Console.WriteLine("\nInforme o Sexo: [M]asculino ou [F]eminino");
                sexo = Console.ReadLine().ToUpper();

                if (sexo != "M" && sexo != "F") Console.WriteLine("Opção inválida.");

            } while (sexo != "M" && sexo != "F");

            Console.WriteLine("\n******************************");

            sexo = (sexo == "M") ? "MASCULINO" : "FEMININO";
            #endregion

            Paciente paciente = new Paciente(cpf, nome, dataNasc, sexo);

            paciente.SalvarInformacoesDoPacienteNoArquivo();

            return paciente;
        }

        private static void BuscarPacienteNaFila()
        {
            Console.Clear();

            Paciente paciente = new Paciente();

            Console.WriteLine("****************** Buscar Paciente na Fila ****************");
            Console.Write("\nInforme o CPF: ");
            string cpf = Console.ReadLine();
            Console.WriteLine("\n*********************************************************");

            if ((paciente = fila_normal.Buscar(cpf)) != null)
            {
                Console.WriteLine(paciente.ToString());
                Console.WriteLine("Aguardando na fila normal");
            }
            else if ((paciente = fila_preferencial.Buscar(cpf)) != null)
            {
                Console.WriteLine(paciente.ToString());
                Console.WriteLine("Aguardando na fila preferencial");
            }
            else
            {
                Console.WriteLine("Paciente não está em nenhuma fila.");
            }
        }

       
    }
}
