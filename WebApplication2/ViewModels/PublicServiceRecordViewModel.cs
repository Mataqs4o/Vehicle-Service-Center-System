namespace WebApplication2.ViewModels;

public class PublicServiceRecordViewModel
{
    public int ServiceRecordId { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public string VehicleLabel { get; set; } = string.Empty;
}
