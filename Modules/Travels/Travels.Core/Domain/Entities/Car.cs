namespace Travels.Core.Domain.Entities;

public class Car
{
    public string Brand { get; private set; }
    
    public string Color { get; private set; }
    
    public string Number { get; private set; }

    private Car()
    {
        
    }

    public void Update(string brand,string color,string number)
    {
        Brand = brand;
        Color = color;
        Number = number;
    }
    
}