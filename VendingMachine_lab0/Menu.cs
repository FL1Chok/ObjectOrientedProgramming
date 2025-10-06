namespace VendingMachine_lab0;

public class VendingMachine
{
    private readonly List<Product> _products;
    private decimal _currentBalance;
    private decimal _totalCollectedMoney;
    private readonly List<decimal> _validCoins;
    
    public VendingMachine()
    {
        _products = new List<Product>();
        _currentBalance = 0;
        _totalCollectedMoney = 0;
        _validCoins = new List<decimal> { 1, 2, 5, 10 };
        
        InitializeProducts();
    }
    
    private void InitializeProducts()
    {
        _products.Add(new Product("Дюшес", 40, 5));
        _products.Add(new Product("Протеиновый батончик", 45, 7));
        _products.Add(new Product("Шоколадка", 15, 15));
        _products.Add(new Product("Вода", 25, 10));
        _products.Add(new Product("Саперави", 40, 4));
        _products.Add(new Product("Жвачка", 5, 30));
    }
    
    public void ShowProducts()
    {
        Console.WriteLine("\nДОСТУПНЫЕ ТОВАРЫ");
        for (int i = 0; i < _products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_products[i]}");
        }
        Console.WriteLine("----------------\n");
    }
    
    public Product SelectProduct(int productIndex)
    {
        if (productIndex < 1 || productIndex > _products.Count)
        {
            throw new ArgumentException("Введите существующий номер");
        }
        
        Product selectedProduct = _products[productIndex - 1];
        
        if (selectedProduct.Quantity <= 0)
        {
            throw new InvalidOperationException("Товар закончился");
        }
        
        return selectedProduct;
    }
    
    public void InsertCoins(string coinsInput)
    {
        if (string.IsNullOrWhiteSpace(coinsInput))
        {
            throw new ArgumentException("Введите номиналы монет");
        }
        
        string[] coinValues = coinsInput.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        foreach (string coinValue in coinValues)
        {
            if (decimal.TryParse(coinValue, out decimal coin))
            {
                if (_validCoins.Contains(coin))
                {
                    _currentBalance += coin;
                    Console.WriteLine($"Внесена монета: {coin:C}");
                }
                else
                {
                    Console.WriteLine("Монета с таким номиналом не принимается. Возврат.");
                }
            }
        }
        
        Console.WriteLine($"Текущий баланс: {_currentBalance:C}");
    }
    
    public (bool success, decimal change) PurchaseProduct(Product product)
    {
        if (product.Price > _currentBalance)
        {
            return (false, 0);
        }
        
        if (product.Quantity <= 0)
        {
            throw new InvalidOperationException("Товар закончился");
        }
        
        decimal change = _currentBalance - product.Price;
        _totalCollectedMoney += product.Price;
        _currentBalance = 0;
        
        product.Quantity--;
        
        return (true, change);
    }
    
    public decimal CancelOperation()
    {
        decimal refund = _currentBalance;
        _currentBalance = 0;
        return refund;
    }
    
    public void AddProduct(string name, decimal price, int quantity)
    {
        _products.Add(new Product(name, price, quantity));
    }
    
    public decimal GetMoney()
    {
        decimal collected = _totalCollectedMoney;
        _totalCollectedMoney = 0;
        return collected;
    }
    
    public void AddQuantity(int productIndex, int quantity)
    {
        if (productIndex < 1 || productIndex > _products.Count)
        {
            throw new ArgumentException("Неверный номер");
        }
        
        _products[productIndex - 1].Quantity += quantity;
    }
    
    public decimal GetCurrentBalance() => _currentBalance;
    public decimal GetTotalCollectedMoney() => _totalCollectedMoney;
}