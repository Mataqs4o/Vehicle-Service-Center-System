using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Hubs;
using WebApplication2.Models;
using WebApplication2.Repositories;
using WebApplication2.ViewModels;

namespace WebApplication2.Services;

public class ServiceRecordService(
    IGenericRepository<ServiceRecord> serviceRecordRepository,
    IEmailNotificationService emailNotificationService,
    IMapper mapper,
    IHubContext<PublicRecordsHub> hubContext,
    ApplicationDbContext dbContext) : IServiceRecordService
{
    public async Task BookAppointmentAsync(ServiceAppointmentViewModel model, string customerEmail)
    {
        var record = mapper.Map<ServiceRecord>(model);
        record.IsPublic = true;
        await serviceRecordRepository.AddAsync(record);

        await hubContext.Clients.All.SendAsync("PublicRecordAdded");

        await emailNotificationService.SendAsync(
            customerEmail,
            "Service Appointment Confirmation",
            $"Your appointment for vehicle #{model.VehicleId} is set for {model.Date:d}.");
    }

    public async Task<IReadOnlyList<ServicePriceEstimateViewModel>> GetPriceEstimatesAsync()
    {
        return await dbContext.ServiceRecords
            .GroupBy(x => x.Description)
            .Select(g => new ServicePriceEstimateViewModel
            {
                RepairType = g.Key,
                ApproximatePrice = Math.Round(g.Average(x => x.Cost), 2)
            })
            .OrderBy(x => x.RepairType)
            .ToListAsync();
    }
}
