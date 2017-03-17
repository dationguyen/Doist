using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoist.Models;

namespace ToDoist.Services
{
    public interface IToDoistApiClient
    {
        Task<List<TodoistTask>> GetItemsAzync();
    }
}