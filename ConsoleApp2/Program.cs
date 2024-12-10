
using Domain;
using System.Data;
using System.Text.Json;
using UserManagementApp;


namespace JobSystemApp
{
    class Program
    {
        static List<JobOffer> jobOffers = new List<JobOffer>();
        static User currentUser = null;

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
                if (currentUser == null)
                {
                    Console.WriteLine("1. Rejestracja");
                    Console.WriteLine("2. Logowanie");
                    Console.WriteLine("0. Wyjście");
                }
                else
                {
                    Console.WriteLine("3. Wyświetl oferty pracy"); // Dostępne dla wszystkich zalogowanych
                    if (currentUser.Role == "admin")
                    {
                        Console.WriteLine("4. Dodaj ofertę pracy"); // Admin-only
                        Console.WriteLine("5. Zapisz oferty do pliku JSON");
                        Console.WriteLine("6. Wczytaj oferty z pliku JSON");
                    }
                    Console.WriteLine("0. Wyloguj");
                }

                Console.Write("Wybierz opcję: ");
                choice = Console.ReadLine();

                if (currentUser == null)
                {
                    switch (choice)
                    {
                        case "1":
                            RegisterUser();
                            break;
                        case "2":
                            LoginUser();
                            break;
                        case "0":
                            Console.WriteLine("Do widzenia!");
                            return; 
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case "3":
                            if (IsLoggedIn()) DisplayJobOffers();
                            break;
                        case "4":
                            if (IsLoggedIn() && currentUser.Role == "admin") AddJobOffer();
                            break;
                        case "5":
                            if (IsLoggedIn() && currentUser.Role == "admin") SaveToJSON();
                            break;
                        case "6":
                            if (IsLoggedIn() && currentUser.Role == "admin") LoadFromJSON();
                            break;
                        case "0":
                            currentUser = null; 
                            Console.WriteLine("Wylogowano. Wróć do logowania.");
                            Thread.Sleep(2000);
                            break;
                    }
                }

            } while (true);
        }

        static bool IsLoggedIn()
        {
            if (currentUser == null)
            {
                Console.WriteLine("Musisz być zalogowany, aby wykonać tą operację.");
                Thread.Sleep(2000);
                return false;
            }
            return true;
        }

        static void RegisterUser()
        {
            UserManager userManager = new UserManager();
            Console.Write("Podaj nazwę użytkownika: ");
            string username = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine();
            Console.Write("Podaj rolę (admin/user): ");
            string role = Console.ReadLine().ToLower();
            userManager.RegisterUser(username, password, role);
        }

        static void LoginUser()
        {
            UserManager userManager = new UserManager();
            Console.Write("Podaj nazwę użytkownika: ");
            string loginUsername = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            string loginPassword = Console.ReadLine();
            User user = new User()
            {
                Username = loginUsername,
                Password = loginPassword
            };
            var loggedInUser = userManager.Login(user);
            if (loggedInUser != null)
            {
                currentUser = loggedInUser;
            }
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
            Thread.Sleep(2000);
        }

        static void DisplayJobOffers()
        {
            if (!IsLoggedIn()) return;

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
            Thread.Sleep(2000);
        }

        static void SaveToJSON()
        {
            Console.Write("Podaj nazwę pliku (np. oferty.json): ");
            string fileName = Console.ReadLine();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jobOffers, options);

            File.WriteAllText(fileName, json);
            Console.WriteLine("Oferty zapisano do pliku.");
            Thread.Sleep(2000);
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
            Thread.Sleep(2000);
        }
    }
}
