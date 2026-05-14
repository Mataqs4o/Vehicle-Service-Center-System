using AutoMapper;
using WebApplication2.Models;
using WebApplication2.Repositories;
using WebApplication2.ViewModels;

namespace WebApplication2.Services;

public class ServiceRecordService(
    IGenericRepository<ServiceRecord> serviceRecordRepository,
    IEmailNotificationService emailNotificationService,
    IMapper mapper) : IServiceRecordService
{
    public async Task BookAppointmentAsync(ServiceAppointmentViewModel model, string customerEmail)
    {
        var record = mapper.Map<ServiceRecord>(model);
        await serviceRecordRepository.AddAsync(record);

        await emailNotificationService.SendAsync(
            customerEmail,
            "Service Appointment Confirmation",
            $"Your appointment for vehicle #{model.VehicleId} is set for {model.Date:d}.");
    }
}
