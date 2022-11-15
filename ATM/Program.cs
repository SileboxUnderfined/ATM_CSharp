using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ATM
{
    public static class Program
    {
        private static bool running = true;
        private static Random rand = new Random();

        public static void Main(string[] args)
        {
            while (running)
            {
                Console.WriteLine("Нажми 1, чтобы создать пользователя\nНажми 2, чтобы воспользоваться банкоматом\nНажми 0 чтобы выйти.");
                Console.Write("Ваш выбор: ");
                int choice = (int)Console.ReadKey().Key;
                Console.Write("\n");
                switch (choice)
                {
                    case 48:
                        running = false;
                        break;

                    case 49:
                        CreateUser();
                        break;

                    case 50:
                        UseATM();
                        break;

                    default:
                        Console.WriteLine("Попробуйте нажать другую клавишу.");
                        break;

                }
            }

        }

        private static void CreateUser()
        {
            Console.Write("Введите имя и фамилию: ");
            string[] name = Console.ReadLine().ToString().Split();
            if (name.Length <= 1)
            {
                Console.WriteLine("Вы ввели неправильно!");
                return;
            }

            long cid = rand.NextInt64();
            User newUser = new User(name[0], name[1],cid,100);
            string filename = SaveUser(newUser);

            Console.WriteLine("Пользователь сохранён в файл {0}", filename);
            return;
        }

        private static void UseATM()
        {
            User user = LoadUser();
            ATM atm = new ATM(user);
            User newUser = atm.Use();
            SaveUser(newUser, newUser.filename);
        }

        private static User LoadUser()
        {
            string[] files = GetSaves();

            Console.WriteLine("Выберите файл: ");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine("{0}: {1}", i, files[i]);
            }
            int choice = Convert.ToInt32(Console.ReadLine());
            string file = files[choice];

            string jsoned = File.ReadAllText(file);
            User selectedUser = JsonSerializer.Deserialize<User>(jsoned);
            selectedUser.filename = file;

            return selectedUser;
        }

        private static string SaveUser(User user)
        {
            Directory.CreateDirectory("savedUsers");

            string filename = String.Format("{0}.json", selectFilename());

            user.filename = filename;

            var user1 = user;

            string jsoned = JsonSerializer.Serialize(user1);

            File.WriteAllText(String.Format("savedUsers/{0}",filename), jsoned);

            return filename;
        }

        private static void SaveUser(User user, string filename)
        {
            Directory.CreateDirectory("savedUsers");

            var user1 = user;

            string jsoned = JsonSerializer.Serialize(user1);

            File.WriteAllText(filename,jsoned);

            return;
        }

        private static string[] GetSaves()
        {
            string[] files = Directory.GetFiles("savedUsers");
            return files;
        }

        private static string selectFilename()
        {
            string[] files = GetSaves();
            int counter = 0;
            foreach (string file in files)
            {
                counter++;
            }
            return counter.ToString();
        }
    }
}