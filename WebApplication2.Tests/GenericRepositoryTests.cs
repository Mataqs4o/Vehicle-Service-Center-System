using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Tests;

public class GenericRepositoryTests
{
    [Fact]
    public async Task AddAsync_PersistsCustomer()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new ApplicationDbContext(options);
        var repo = new GenericRepository<Customer>(context);

        await repo.AddAsync(new Customer { FirstName = "A", LastName = "B", Email = "ab@example.com" });

        var all = await repo.GetAllAsync();
        Assert.Single(all);
    }
}
