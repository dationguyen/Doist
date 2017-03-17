using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToDoist.Models;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ToDoist.Services
{
    public class DataService : IDataService
    {
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private int waitingTask = 0;
        //Local json data file name
        private const string FILE_NAME = "DataFile.json";

        private bool hasFile = false;

        StorageFolder localFolder;
        StorageFile dataFile;

        public DataService()
        {
            localFolder = ApplicationData.Current.LocalFolder;
        }

        public async Task<bool> FileExistsAsync()
        {
            if (!hasFile)
                try
                {
                    dataFile = await localFolder.GetFileAsync(FILE_NAME);
                    hasFile = true;
                }
                catch (System.IO.FileNotFoundException)
                {
                    hasFile = false;
                }
                catch (Exception)
                {
                    throw;
                }
            return hasFile;
        }

        /// <summary>
        /// Load data from local storage
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoistTask>> LoadData()
        {
            if (await FileExistsAsync())
                try
                {
                    //read file to string
                    string fileContent = await FileIO.ReadTextAsync(dataFile);
                    //Desialize data                    
                    return JsonConvert.DeserializeObject<List<TodoistTask>>(fileContent);
                }
                catch
                {
                    return null;
                }

            return null;
        }

        /// <summary>
        /// Save data to local storage
        /// </summary>
        /// <param name="itemList">List of data for saving to local storage</param>
        public async void SaveData(List<TodoistTask> itemList)
        {
            waitingTask++;
            await semaphoreSlim.WaitAsync();            
            try
            {
                if (waitingTask <= 1)
                {
                    //Serialize data
                    string data = JsonConvert.SerializeObject(itemList);
                    //Create file in LocalFolder          
                    dataFile = await localFolder.CreateFileAsync(FILE_NAME, Windows.Storage.CreationCollisionOption.ReplaceExisting);

                    //write data to file
                    await FileIO.WriteTextAsync(dataFile, data);

                    Debug.WriteLine("done");
                    hasFile = true;
                }
                else
                {
                    Debug.WriteLine("cancel" + waitingTask);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                waitingTask--;
                semaphoreSlim.Release();
            }

        }


    }
}
