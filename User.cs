using System;
using System.Collections.Generic;
using System.Linq;

namespace CarMarket
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public int Age { get; set; }
        public int Balance { get; set; } = 50000;

        public static List<User> Users { get; set; } = new List<User>();
        
        public List<Car> PurchasedCars { get; set; } = new List<Car>();
        public override string ToString()
        {
            return $"{Name} {Password} {Role} {Balance} {Age}";
        }

        public void CheckBalance()
        {
            Console.WriteLine($"Your balance is: {Balance} $");
        }

        
        public static void Register(string name, string password, string role, int age)
        {

            if (age <= 18)
            {
                throw new Exception($"exception: you are under 18");
            }
            else {
                Users.Add(new User { Name = name, Password = password, Role = role, Age = age });
            Console.WriteLine("Registered successfully.");
            }
            

        }
        public void WriteFile(string content)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\users.txt";
            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(content);
                }
            }
        }

        public void ReadFile()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\users.txt";
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }
            }
        }

        public  User Login(string name, string password)
        {
            var user = Users.FirstOrDefault(x => x.Name == name && x.Password == password);
            Console.WriteLine(user.ToString());
            WriteFile(user.ToString());
            if (user != null)
            {
                Console.WriteLine($"Logged in as {user.Role}");
                return user;  
            }
            else
            {
                Console.WriteLine("Incorrect username or password.");
                return null;
            }
        }
        public void BuyCar(Car car)
        {
            if (Balance < car.Price)
            {
                throw new CarException($"you dont have enough money for {car.Model}. You need atleast {car.Price - Balance}");
            }

            Balance -= car.Price;
            PurchasedCars.Add(car);
            Console.WriteLine($"You have bought the car {car.Model} for {car.Price}. Your balance = {Balance}.");
        }

        public void SellCar(Car car)
        {
            if (!PurchasedCars.Contains(car))
            {
                throw new CarException($"Exception: you dont own {car.Model}");
            }

            Balance += car.Price;
            PurchasedCars.Remove(car);
            Console.WriteLine($"You have sold the car {car.Model} for {car.Price}. Your new balance is {Balance}.");
        }

        public void DisplayPurchasedCars()
        {
            if (PurchasedCars.Count == 0)
            {
                Console.WriteLine("You have not purchased any cars.");
            }
            else
            {
                Console.WriteLine("Purchased Cars:");
                foreach (var car in PurchasedCars)
                {
                    Console.WriteLine($"Car Model: {car.Model}, Price: {car.Price}, Year: {car.Year}, Manufacturer: {car.Manufacturer}");
                }
            }
        }

     
    }
}
