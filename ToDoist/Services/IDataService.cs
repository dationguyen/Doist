using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoist.Models;

namespace ToDoist.Services
{
    public interface IDataService
    {
        void SaveData(List<TodoistTask> itemList);
        Task<List<TodoistTask>> LoadData();

    }
}
