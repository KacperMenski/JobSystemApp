using Domain;
using System.Data;
using System.Text.Json;
using UserManagementApp;

namespace JobSystemApp
{
    
    class Program
    {
        
        static List<JobOffer> jobOffers = new List<JobOffer>();

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            string choice;
            do
            {
                Console.Clear();
                Console.WriteLine("=== System Ofert Pracy ===");
                Console.WriteLine("1. Rejestracja");
                Console.WriteLine("2. Logowanie");
                Console.WriteLine("3. Dodaj ofertę pracy");
                Console.WriteLine("4. Wyświetl oferty pracy");
                Console.WriteLine("5. Zapisz oferty do pliku JSON");
                Console.WriteLine("6. Wczytaj oferty z pliku JSON");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        UserManager userManager = new UserManager();
                        Console.Write("Podaj nazwę użytkownika: ");
                        string username = Console.ReadLine();
                        Console.Write("Podaj hasło: ");
                        string password = Console.ReadLine();
                        Console.Write("Podaj rolę (Admin/User): ");
                        string role = Console.ReadLine().ToLower();
                            userManager.RegisterUser(username, password, role);

                        
                        
                        break;
                    case "2":
                        UserManager userManager2 = new UserManager();
                        Console.Write("Podaj nazwę użytkownika: ");
                        string loginUsername = Console.ReadLine();
                        Console.Write("Podaj hasło: ");
                        string loginPassword = Console.ReadLine();
                        User user = new User()
                        {
                            Username = loginUsername,
                            Password = loginPassword
                        };
                        userManager2.Login(user);
                        break;
                    case "3":
                        AddJobOffer();
                        break;
                    case "4":
                        DisplayJobOffers();
                        break;
                    case "5":
                        SaveToJSON();
                        break;
                    case "6":
                        LoadFromJSON();
                        break;
                }
            } while (choice != "0");
        }
        
        static void AddJobOffer()
        {
            Console.Write("Podaj nazwę stanowiska: ");
            string title = Console.ReadLine();
            Console.Write("Podaj nazwę firmy: ");
            string company = Console.ReadLine();
            Console.Write("Podaj lokalizację: ");
            string location = Console.ReadLine();

            var offer = new JobOffer(title, company, location);
            jobOffers.Add(offer);

            Console.WriteLine("Oferta została dodana.");
            Console.ReadKey();
        }

        static void DisplayJobOffers()
        {
            if (jobOffers.Count == 0)
            {
                Console.WriteLine("Brak ofert pracy.");
            }
            else
            {
                foreach (var offer in jobOffers)
                {
                    Console.WriteLine(offer.ToString());
                }
            }
            Console.ReadKey();
        }

        static void SaveToJSON()
        {
            Console.Write("Podaj nazwę pliku (np. oferty.json): ");
            string fileName = Console.ReadLine();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jobOffers, options);

            File.WriteAllText(fileName, json);
            Console.WriteLine("Oferty zapisano do pliku.");
            Console.ReadKey();
        }

        static void LoadFromJSON()
        {
            Console.Write("Podaj nazwę pliku (np. oferty.json): ");
            string fileName = Console.ReadLine();

            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                jobOffers = JsonSerializer.Deserialize<List<JobOffer>>(json) ?? new List<JobOffer>();
                Console.WriteLine("Oferty zostały wczytane.");
            }
            else
            {
                Console.WriteLine("Plik nie istnieje.");
            }
            Console.ReadKey();
        }
    }
}
