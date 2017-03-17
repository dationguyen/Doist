using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoist.Models
{
    public class ResponseMessage
    {
        public string sync_token { get; set; }
        public bool full_sync { get; set; }
        public List<TodoistTask> items { get; set; }
    }
}
