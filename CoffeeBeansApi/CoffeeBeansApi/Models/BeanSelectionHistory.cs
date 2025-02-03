namespace CoffeeBeansApi.Models;

public class BeanSelectionHistory
{
    public int Id { get; set; }
    public int BeanId { get; set; }
    public DateTime SelectedDate { get; set; }
}
