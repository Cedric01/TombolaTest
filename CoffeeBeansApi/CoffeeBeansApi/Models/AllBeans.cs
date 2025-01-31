namespace CoffeeBeansApi.Models;

public class AllBeans
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Origin { get; set; }
    public string RoastLevel { get; set; }
    public string Cost { get; set; }
    public string Image { get; set; }
    public string Colour { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public bool IsBOTD { get; set; }
}
