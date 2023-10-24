using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetStoreApp
{
    interface IPetService
    {
        Task<List<Pet>> GetAvailablePetsAsync();
    }
}
