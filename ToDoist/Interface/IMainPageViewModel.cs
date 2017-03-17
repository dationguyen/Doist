using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoist.Models;

namespace ToDoist.Interface
{
    interface IMainPageViewModel
    {
        List<TodoistTask> ItemListResource { get; set; }
        bool ProgressBarIsActive { get; set; }
        string ErrorMessage { get; set; }
    }
}
