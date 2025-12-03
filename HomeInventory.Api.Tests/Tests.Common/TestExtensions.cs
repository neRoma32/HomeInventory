using System.Net.Http.Json;
using System.Text.Json;

namespace HomeInventory.Api.Tests.Common;

public static class TestExtensions
{
    public static async Task<T> ToResponseModel<T>(this HttpResponseMessage response)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true 
        };

        return await response.Content.ReadFromJsonAsync<T>(jsonOptions)
               ?? throw new InvalidOperationException("Failed to deserialize response");
    }
}