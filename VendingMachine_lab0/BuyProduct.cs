namespace VendingMachine_lab0;

public class BuyProduct(VendingMachine machine)
{
    public void BuyProcess()
    {
        machine.ShowProducts();
        
        Console.Write("Выберите номер товара: ");
        if (!int.TryParse(Console.ReadLine(), out int productChoice))
        {
            Console.WriteLine("Неверный номер");
            return;
        }
        
        
        Product selectedProduct;
        try
        {
            selectedProduct = machine.SelectProduct(productChoice);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            return;
        }
        
        Console.WriteLine($"\nВы выбрали: {selectedProduct.Name}");
        Console.WriteLine($"Цена: {selectedProduct.Price:C}");
        Console.WriteLine($"Внесите монеты на сумму: {selectedProduct.Price:C}");
        
        
        bool purchasing = true;
        while (purchasing)
        {
            Console.WriteLine("\nВведите номиналы монет через пробел (например: 10 1 5 2):");
            Console.WriteLine("Доступные номиналы: 1, 2, 5, 10");
            Console.Write("Монеты: ");
            
            var coinsInput = Console.ReadLine();
            
            try
            {
                if (coinsInput != null) machine.InsertCoins(coinsInput);

                if (machine.GetCurrentBalance() >= selectedProduct.Price)
                {
                    var result = machine.PurchaseProduct(selectedProduct);
                    
                    if (result.success)
                    {
                        Console.WriteLine($"\nУспешно! Заберите ваш товар");
                        if (result.change > 0)
                        {
                            Console.WriteLine($"Возьмите сдачу: {result.change:C}");
                        }
                        purchasing = false;
                    }
                }
                else
                {
                    decimal remaining = selectedProduct.Price - machine.GetCurrentBalance();
                    Console.WriteLine($"Недостаточно средств. Внесите еще: {remaining:C}");
                    
                    Console.Write("Продолжить внесение монет? (да/нет): ");
                    string continueChoice = Console.ReadLine()!.ToLower();
                    
                    if (continueChoice != "да")
                    {
                        decimal refund = machine.CancelOperation();
                        Console.WriteLine($"\nОперация отменена. Возврат: {refund:C}");
                        purchasing = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}