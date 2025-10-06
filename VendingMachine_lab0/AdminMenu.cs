namespace VendingMachine_lab0;

public class AdminMenu(VendingMachine machine)
{
    public void StartAdminMode()
    {
        Console.WriteLine("\n РЕЖИМ АДМИНИСТРАТОРА");
        
        var inAdminMode = true;
        while (inAdminMode)
        {
            ShowAdminMenu();
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    ViewProductsAndMoney();
                    break;
                    
                case "2":
                    AddQuantity();
                    break;
                    
                case "3":
                    AddNewProduct();
                    break;
                    
                case "4":
                    CollectMoney();
                    break;
                    
                case "5":
                    inAdminMode = false;
                    Console.WriteLine("Выйти из режима администратора");
                    break;
                    
                default:
                    Console.WriteLine("Ошибка. Введите номер действия");
                    break;
            }
        }
    }
    
    private void ShowAdminMenu()
    {
        Console.WriteLine("\nДоступные действия:");
        Console.WriteLine("1. Просмотреть товары и деньги");
        Console.WriteLine("2. Пополнить товар");
        Console.WriteLine("3. Добавить новый товар");
        Console.WriteLine("4. Собрать деньги");
        Console.WriteLine("5. Выйти из режима администратора");
        Console.Write("Выберете действие: ");
    }
    
    private void ViewProductsAndMoney()
    {
        machine.ShowProducts();
        Console.WriteLine($"\nТекущий баланс: {machine.GetTotalCollectedMoney():C}");
    }
    
    private void AddQuantity()
    {
        try
        {
            machine.ShowProducts();
            Console.Write("Введите номер товара: ");
            int productIndex = int.Parse(Console.ReadLine());
            Console.Write("Введите количество: ");
            int quantity = int.Parse(Console.ReadLine());
            
            machine.AddQuantity(productIndex, quantity);
            Console.WriteLine("\nТовар успешно пополнен!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    
    private void AddNewProduct()
    {
        try
        {
            Console.Write("Введите название товара: ");
            string name = Console.ReadLine();
            Console.Write("Введите цену: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Введите количество: ");
            int quantity = int.Parse(Console.ReadLine());
            
            machine.AddProduct(name, price, quantity);
            Console.WriteLine("\nТовар успешно добавлен!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    
    private void CollectMoney()
    {
        var collected = machine.GetMoney();
        Console.WriteLine($"\nСобрано денег: {collected:C}");
    }
}