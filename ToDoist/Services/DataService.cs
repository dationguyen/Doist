using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ToDoist.Models;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ToDoist.Services
{
    public class DataService : IDataService
    {
        //Local json data file name
        private const string FILE_NAME = "DataFile.json";

        /// <summary>
        /// Load data from local storage
        /// </summary>
        /// <returns></returns>
        public async Task<List<Item>> LoadData()
        {           
            try
            {
                //Open file
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile dataFile = await localFolder.GetFileAsync(FILE_NAME);
                //read file to string
                String fileContent = await FileIO.ReadTextAsync(dataFile);
                //Desialize data
                return JsonConvert.DeserializeObject<List<Item>>(fileContent);
            }
            catch
            {
                return null;
            }              
        }

        /// <summary>
        /// Save data to local storage
        /// </summary>
        /// <param name="itemList">List of data for saving to local storage</param>
        public async void SaveData(List<Item> itemList)
        {
            //Serialize data
            string data = JsonConvert.SerializeObject(itemList);
            //Create file in LocalFolder
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile dataFile = await localFolder.CreateFileAsync(FILE_NAME, Windows.Storage.CreationCollisionOption.ReplaceExisting);
            //write data to file
            await FileIO.WriteTextAsync(dataFile, data);
        }
    }
}
