using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoist.Interface;
using ToDoist.Models;

namespace ToDoist.DesignViewModels
{
    public class MainPageViewModel : IMainPageViewModel
    {
        public List<TodoistTask> ItemListResource { get; set; }
        public bool ProgressBarIsActive { get ; set; }
        public string ErrorMessage { get; set; }

        public MainPageViewModel()
        {
            //initialize data in design mode
            string data = "[{\"day_order\":0,\"assigned_by_uid\":null,\"due_date_utc\":\"Thu 09 Mar 2017 12:59:59 + 0000\",\"is_archived\":0,\"labels\":[],\"sync_id\":null,\"all_day\":true,\"in_history\":0,\"date_added\":\"Wed 08 Mar 2017 12:59:12 + 0000\",\"_checked\":0,\"date_lang\":\"en\",\"id\":132554257,\"content\":\"Create Project\",\"indent\":1,\"user_id\":12343615,\"is_deleted\":0,\"priority\":1,\"parent_id\":null,\"item_order\":1,\"responsible_uid\":null,\"project_id\":198501720,\"collapsed\":0,\"date_string\":\"Mar 9\"},{\"day_order\":-1,\"assigned_by_uid\":null,\"due_date_utc\":null,\"is_archived\":0,\"labels\":[],\"sync_id\":null,\"all_day\":false,\"in_history\":0,\"date_added\":\"Wed 08 Mar 2017 14:21:36 + 0000\",\"_checked\":0,\"date_lang\":\"en\",\"id\":132570593,\"content\":\"Implement Prism and alocate the project\",\"indent\":1,\"user_id\":12343615,\"is_deleted\":0,\"priority\":1,\"parent_id\":null,\"item_order\":2,\"responsible_uid\":null,\"project_id\":198501720,\"collapsed\":0,\"date_string\":\"\"},{\"day_order\":-1,\"assigned_by_uid\":null,\"due_date_utc\":null,\"is_archived\":0,\"labels\":[],\"sync_id\":null,\"all_day\":false,\"in_history\":0,\"date_added\":\"Wed 08 Mar 2017 14:21:46 + 0000\",\"_checked\":0,\"date_lang\":\"en\",\"id\":132570633,\"content\":\"implement rest api\",\"indent\":1,\"user_id\":12343615,\"is_deleted\":0,\"priority\":1,\"parent_id\":null,\"item_order\":3,\"responsible_uid\":null,\"project_id\":198501720,\"collapsed\":0,\"date_string\":\"\"},{\"day_order\":-1,\"assigned_by_uid\":null,\"due_date_utc\":null,\"is_archived\":0,\"labels\":[],\"sync_id\":null,\"all_day\":false,\"in_history\":0,\"date_added\":\"Wed 08 Mar 2017 14:22:08 + 0000\",\"_checked\":0,\"date_lang\":\"en\",\"id\":132570690,\"content\":\"Implement viewModel\",\"indent\":1,\"user_id\":12343615,\"is_deleted\":0,\"priority\":1,\"parent_id\":null,\"item_order\":4,\"responsible_uid\":null,\"project_id\":198501720,\"collapsed\":0,\"date_string\":\"\"}]";
            ItemListResource = JsonConvert.DeserializeObject<List<TodoistTask>>(data);
            ProgressBarIsActive = true;
            ErrorMessage = "You don't have any task. Please go to Todoist website to add more task";
        }
    }
}
