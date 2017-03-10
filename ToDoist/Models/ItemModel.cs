using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoist.Models
{  
    public class Item
    {
        public int day_order { get; set; }
        public object assigned_by_uid { get; set; }
        public string due_date_utc { get; set; }
        public int is_archived { get; set; }
        public object[] labels { get; set; }
        public object sync_id { get; set; }
        public bool all_day { get; set; }
        public int in_history { get; set; }
        public string date_added { get; set; }
        public int _checked { get; set; }
        public string date_lang { get; set; }
        public int id { get; set; }
        public string content { get; set; }
        public int indent { get; set; }
        public int user_id { get; set; }
        public int is_deleted { get; set; }
        public int priority { get; set; }
        public object parent_id { get; set; }
        public int item_order { get; set; }
        public object responsible_uid { get; set; }
        public int project_id { get; set; }
        public int collapsed { get; set; }
        public string date_string { get; set; }
    }

}
