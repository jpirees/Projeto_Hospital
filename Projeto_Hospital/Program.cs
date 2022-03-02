using System;

namespace Projeto_Hospital
{

    internal class Program
    {
        public static Fila fila_normal = new Fila();
        public static Fila fila_preferencial = new Fila();

        static void Main(string[] args)
        {

            string acao;

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
                        Console.Clear();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Ação inválida");
                        break;
                }

                Console.ReadKey();

            } while (acao != "0");
        }



        public static string Menu()
        {
            Console.WriteLine("******************** Menu *******************");
            Console.WriteLine("[1] Chamar Paciente"); // Cadastra cliente e faz triagem
            Console.WriteLine("[2] Chamar Paciente para Exame"); // Faz exames e anamnese
            Console.WriteLine("");
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

            paciente = paciente.BuscarInformacoesPaciente(cpf);

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
            {
                fila_preferencial.Inserir(paciente);
                fila_preferencial.InserirInformacoesDoPaciente(paciente, "FilaPreferencial");
            }
            else
            {
                fila_normal.Inserir(paciente);
                fila_normal.InserirInformacoesDoPaciente(paciente, "FilaNormal");
            }
        }

        public static Paciente CadastrarPaciente(string cpf)
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

            paciente.SalvarInformacoesPaciente();

            return paciente;
        }


    }
}
