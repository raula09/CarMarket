using CarMarket;

User loggedInUser = null;  
while (true)
{
    Console.WriteLine("Select an option: ");
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Login");
    Console.WriteLine("3. Exit");
    string option = Console.ReadLine();

    User user = new User();
    switch (option)
    {

        case "1":
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            

            Console.Write("Enter Role (admin/user): ");
            string role = Console.ReadLine();
            
            Console.WriteLine("enter age");
            int age = int.Parse(Console.ReadLine());
            

            User.Register(name, password, role, age);
        
           
            loggedInUser = user.Login(name, password);

         
            if (loggedInUser != null)
            {
                if (loggedInUser.Role == "admin")
                {
                    AdminMenu();
                }
                else if (loggedInUser.Role == "user")
                {
                    UserMenu(loggedInUser);
                }
            }
            break;

        case "2":
            Console.Write("Enter Name: ");
            name = Console.ReadLine();

            Console.Write("Enter Password: ");
            password = Console.ReadLine();

            
            loggedInUser = user.Login(name, password);

            if (loggedInUser != null)
            {
                if (loggedInUser.Role == "admin")
                {
                    AdminMenu();
                }
                else if (loggedInUser.Role == "user")
                {
                    UserMenu(loggedInUser);
                }
            }
            break;

        case "3":
            Console.WriteLine("Bye Bye");
            return;

        default:
            Console.WriteLine("Invalid");
            break;
    }
}
        

   
        static void AdminMenu()
{
    while (true)
    {
        Console.WriteLine("Admin Options: ");
        Console.WriteLine("1: Add Car");
        Console.WriteLine("2: Remove Car");
        Console.WriteLine("3: Display All Cars");
        Console.WriteLine("4: Log Out");

        string adminOption = Console.ReadLine();

        switch (adminOption)
        {
            case "1":
                Car.AddCar();
                break;

            case "2":

                Console.WriteLine("Enter the car ID to remove: ");
                int removeCarId = int.Parse(Console.ReadLine());
                Car.RemoveCar(removeCarId);
                break;

            case "3":
                Car.DisplayAllCars();
                break;

            case "4":
                Console.WriteLine("Bye bye");
                return;

            default:
                Console.WriteLine("Invalid");
                break;
        }
    }
}

static void UserMenu(User loggedInUser)
{
    Car carManager = new Car();
    while (true)
    {
        Console.WriteLine("User Options: ");
        Console.WriteLine("1: Buy Car");
        Console.WriteLine("2: My Car");
        Console.WriteLine("3: Display All Cars");
        Console.WriteLine("4: Check Balance");
        Console.WriteLine("5: Sell My Car");
        Console.WriteLine("6: Sort Price By Ascending");
        Console.WriteLine("7: Sort Price By Descending");
        Console.WriteLine("8: Log Out");

        string userOption = Console.ReadLine();

        switch (userOption)
        {
            case "1":
                Car.DisplayAllCars();
                Console.WriteLine("Enter the car ID to buy: ");
                int carId = int.Parse(Console.ReadLine());

                Car carToBuy = Car.GetCarById(carId);

                if (carToBuy != null)
                {
                    loggedInUser.BuyCar(carToBuy);
                }
                else
                {
                    Console.WriteLine("Car not found.");
                }
                break;

            case "2":
                Console.WriteLine("Your Cars: ");
                
                loggedInUser.DisplayPurchasedCars(); 
                break;

            case "3":
                Car.DisplayAllCars();
                break;

            case "4":
                loggedInUser.CheckBalance();
                break;
            case "5":
                Console.WriteLine("Your Cars: ");

                loggedInUser.DisplayPurchasedCars();
                Console.WriteLine("Enter the car ID to sell: ");
                int carId2 = int.Parse(Console.ReadLine());
                Car carToSell = Car.GetCarById(carId2);
                if (carToSell != null)
                {
                    loggedInUser.SellCar(carToSell);
                }
                else
                {
                    Console.WriteLine("Car not found.");
                }
                break;
                case "6":
                carManager.SortByAsc();
                break;
            case "7":
                carManager.SortByDesc();
                break;
            case "8":
                Console.WriteLine("Logging out...");
                return;

            default:
                Console.WriteLine("Invalid option. Please try again.");
                break;
        }
    }
}