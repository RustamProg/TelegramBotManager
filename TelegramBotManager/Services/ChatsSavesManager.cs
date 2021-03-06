﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Configuration;

namespace TelegramBotManager.Services
{    
    public static class ChatsSavesManager
    {
        // private static string _defaultFilePath = "D:\\C#Utils\\TelegramChatIDList\\chatlist.json"; - перенесён в конфиги

        /* Здесь старое  */


        /// <summary>
        /// Saves dictinaries with usernames and chatids to default file path
        /// </summary>
        /// <param name="chatsDict">Default file path</param>
        public static void SaveChatsToJson(Dictionary<string, int> chatsDict)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Dictionary<string, int>));
            using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["ChatSaves"], FileMode.Append))
            {
                jsonSerializer.WriteObject(fs, chatsDict);
            }
        }

        /// <summary>
        /// Return dictionary with usernames and chatids from default file path
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> OpenChatsToJson()
        {            
            Dictionary<string, int> chatsList = new Dictionary<string, int>();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Dictionary<string, int>));
            using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["ChatSaves"], FileMode.OpenOrCreate))
            {
                chatsList = jsonSerializer.ReadObject(fs) as Dictionary<string, int>;
            }

            return chatsList;
        }






        /* Здесь новое, через extensions methods */

        public static void SaveChatsDictionaryToJson(this Dictionary<string, int> chatsDict)
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Dictionary<string, int>));
            using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["ChatSaves"], FileMode.Append))
            {
                jsonSerializer.WriteObject(fs, chatsDict);
            }
        }

        public static Dictionary<string, int> LoadChatsFromJsonToDictionary(this Dictionary<string, int> chatsDict)
        {
            Dictionary<string, int> chatsList = new Dictionary<string, int>();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Dictionary<string, int>));
            using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["ChatSaves"], FileMode.OpenOrCreate))
            {
                chatsList = jsonSerializer.ReadObject(fs) as Dictionary<string, int>;
            }

            return chatsList;
        }
    }
}
