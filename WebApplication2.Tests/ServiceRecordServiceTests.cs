using AutoMapper;
using Moq;
using WebApplication2.Mapping;
using WebApplication2.Models;
using WebApplication2.Repositories;
using WebApplication2.Services;
using WebApplication2.ViewModels;

namespace WebApplication2.Tests;

public class ServiceRecordServiceTests
{
    [Fact]
    public async Task BookAppointmentAsync_AddsRecordAndSendsEmail()
    {
        var repo = new Mock<IGenericRepository<ServiceRecord>>();
        var email = new Mock<IEmailNotificationService>();
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        var mapper = mapperConfig.CreateMapper();
        var service = new ServiceRecordService(repo.Object, email.Object, mapper);

        var vm = new ServiceAppointmentViewModel
        {
            VehicleId = 1,
            Date = DateTime.UtcNow.AddDays(1),
            Description = "Oil change",
            EstimatedCost = 79.99m
        };

        await service.BookAppointmentAsync(vm, "test@example.com");

        repo.Verify(r => r.AddAsync(It.Is<ServiceRecord>(s => s.VehicleId == 1 && s.Cost == 79.99m)), Times.Once);
        email.Verify(e => e.SendAsync("test@example.com", It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}
