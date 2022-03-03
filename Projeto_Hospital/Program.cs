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

            int senhas = 0;

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
                        senhas++;

                        break;

                    case "2":

                        if (fila_preferencial.Elementos > 0 && preferencial < 2)
                        {
                            Console.WriteLine(fila_preferencial.Cabeca.ToString());
                            preferencial++;

                            ChamarExame(fila_preferencial.Cabeca);

                        }
                        else if (fila_normal.Elementos > 0)
                        {
                            Console.WriteLine(fila_normal.Cabeca.ToString());
                            preferencial = 0;

                            ChamarExame(fila_normal.Cabeca);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("As filas estão vazias.");
                            preferencial = 0;
                        }
                        break;

                    case "3":
                        BuscarPacienteNaFila();
                        break;

                    case "4":
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

        public static void ChamarExame(Paciente paciente)
        {
            string[] sintomas = new string[4] { "NÃO", "NÃO", "NÃO", "NÃO" };

            string febre, dorCabeca, semPaladar, semOfato;

            do
            {
                Console.WriteLine("\nEsta ou esteve com febre? [S - SIM] [N - NÃO]");
                febre = Console.ReadLine().ToUpper();
                if (febre == "S")
                {
                    sintomas[0] = "SIM";
                }
                else if (febre == "N")
                {
                    sintomas[0] = "NÃO";
                }
                else
                {
                    Console.WriteLine("Opção inválida!!!");
                }
            } while (febre != "S" && febre != "N");
            do
            {
                Console.WriteLine("\nEsta ou esteve com dor de cabeça? [S - SIM] [N - NÃO]");
                dorCabeca = Console.ReadLine().ToUpper();
                if (dorCabeca == "S")
                {
                    sintomas[1] = "SIM";
                }
                else if (dorCabeca == "N")
                {
                    sintomas[1] = "NÃO";
                }
                else
                {
                    Console.WriteLine("Opção inválida!!!");
                }
            } while (dorCabeca != "S" && dorCabeca != "N");
            do
            {
                Console.WriteLine("\nEsta ou esteve com falta de paladar [S - SIM] [N - NÃO]");
                semPaladar = Console.ReadLine().ToUpper();
                if (semPaladar == "S")
                {
                    sintomas[2] = "SIM";
                }
                else if (semPaladar == "N")
                {
                    sintomas[2] = "NÃO";
                }
                else
                {
                    Console.WriteLine("Opção inválida!!!");
                }
            } while (semPaladar != "S" && semPaladar != "N");
            do
            {
                Console.WriteLine("\nEsta ou esteve com falta de ofato? [S - SIM] [N - NÃO]");
                semOfato = Console.ReadLine().ToUpper();
                if (semOfato == "S")
                {
                    sintomas[3] = "SIM";
                }
                else if (semOfato == "N")
                {
                    sintomas[3] = "NÃO";
                }
                else
                {
                    Console.WriteLine("Opção inválida!!!");
                }

            } while (semOfato != "S" && semOfato != "N");

            Console.Clear();

            Console.WriteLine(paciente.ToString());

            Console.WriteLine($"\nFebre: {sintomas[0]} \n" +
                $"Dor de Cabeça: {sintomas[1]}\n" +
                $"Falta de Paladar: {sintomas[2]}\n" +
                $"Falta de Olfato:  {sintomas[3]}");

            Console.ReadLine();
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
