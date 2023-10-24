using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PetStoreApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IPetService, PetService>()
                .AddSingleton<IConfiguration>(configuration)
                .AddMvc(options =>
                {
                    options.Filters.Add(new ApiKeyAuthorizationAttribute());
                })
                .BuildServiceProvider();

            var petService = serviceProvider.GetRequiredService<IPetService>();

            List<Pet> availablePets = await petService.GetAvailablePetsAsync();

            // Sort pets by category and then by name in reverse order
            availablePets.Sort((x, y) =>
            {
                int categoryComparison = y.Category.Name.CompareTo(x.Category.Name);
                if (categoryComparison == 0)
                {
                    return y.Name.CompareTo(x.Name);
                }
                return categoryComparison;
            });

            // Print the sorted list of pets
            foreach (var pet in availablePets)
            {
                Console.WriteLine($"Category: {pet.Category.Name}");
                Console.WriteLine($"Name: {pet.Name}");
                Console.WriteLine("------");
            }

            Console.ReadLine();
        }
    }
}
