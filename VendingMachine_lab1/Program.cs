namespace VendingMachine_lab1;

class Program
{
    static void ShowMainMenu()
    {
        Console.WriteLine("\nВыберите действие:");
        Console.WriteLine("1. Купить товар");
        Console.WriteLine("2. Режим администратора");
        Console.WriteLine("3. Выйти");
        Console.Write("Ваш выбор: ");
    }
    
    
    static void Main(string [] args)
    {
        VendingMachine vendingMachine = new VendingMachine();
        BuyProduct buyProduct = new BuyProduct(vendingMachine);
        AdminMenu adminService = new AdminMenu(vendingMachine);
        
        bool isRunning = true;
        
        while (isRunning)
        {
            try
            {
                ShowMainMenu();
                string? choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        buyProduct.BuyProcess();
                        break;
                    case "2":
                        adminService.StartAdminMode();
                        break;
                    case "3":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}