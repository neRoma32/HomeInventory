using HomeInventory.Infrastructure.Persistence.Data;
using Microsoft.Extensions.DependencyInjection;

namespace HomeInventory.Api.Tests.Common;

[Collection("IntegrationTests")]
public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebFactory>
{
    protected readonly HttpClient Client;
    protected readonly ApplicationDbContext Context;
    private readonly IServiceScope _scope;

    protected BaseIntegrationTest(IntegrationTestWebFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Client = factory.CreateClient();
        Context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    protected async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }
}