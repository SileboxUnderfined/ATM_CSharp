namespace ATM
{
    public class ATM
    {
        private User servedUser { get; set; }
        private int money { get; set; }
        private bool running { get; set; }

        public ATM(User user)
        {
            servedUser = user;
            money = 100000;
            running = false;
        }

        public User Use()
        {
            running = true;
            Console.WriteLine("Добро пожаловать: {0} {1}", servedUser.first_name, servedUser.last_name);
            while (running)
            {
                Console.WriteLine("Выберите команду\n1 - положить деньги на счёт\n2 - снять деньги со счёта\n3 - узнать баланс\n0 - выйти");
                Console.Write("Ваш выбор: ");
                int choice = (int)Console.ReadKey().Key;
                Console.Write("\n");

                switch (choice)
                {
                    case 48:
                        running = false;
                        break;

                    case 49:
                        Deposit();
                        break;

                    case 50:
                        Withdraw();
                        break;

                    case 51:
                        Console.WriteLine("Ваш баланс составляет {0} рублей",servedUser.balance);
                        break;

                    default:
                        Console.WriteLine("Вы ввели неправильную команду. Повторите попытку.");
                        break;

                }
            }
            return servedUser;
        }

        private void Deposit()
        {
            Console.Write("Введите сумму, которую хотите внести на ваш счёт: ");
            int sum = Convert.ToInt32(Console.ReadLine());
            servedUser.balance += sum;
            money += sum;
            Console.WriteLine("Вы внесли {0} рублей на ваш счёт.", sum);
        }

        private void Withdraw()
        {
            Console.WriteLine("Введите сумму, которую хотите снять с вашего счёта: ");
            int sum = Convert.ToInt32(Console.ReadLine());
            if (servedUser.balance < sum) Console.WriteLine("Вы не можете снять столько денег со счёта");
            else if (money < sum) Console.WriteLine("Вы не можете снять больше денег, чем есть в банкомате");
            else
            {
                servedUser.balance -= sum;
                money -= sum;
                Console.WriteLine("Вы успешно сняли {0} рублей со своего счёта",sum);
            }
        }
    }
}

