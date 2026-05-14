using WebApplication2.ViewModels;

namespace WebApplication2.Services;

public interface IServiceRecordService
{
    Task BookAppointmentAsync(ServiceAppointmentViewModel model, string customerEmail);
    Task<IReadOnlyList<ServicePriceEstimateViewModel>> GetPriceEstimatesAsync();
}
