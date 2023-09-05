using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Save
{
    public class SaveManager
    {
        protected string LoadSave(string uniqueKey)
        {
            if (!File.Exists(GetFilePath(uniqueKey)))
            {
                Debug.Log("The file does not exist.");
                return null;
            }

            try
            {
                return File.ReadAllText(GetFilePath(uniqueKey));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        protected void Delete(string uniqueKey)
        {
            if (File.Exists(GetFilePath(uniqueKey)))
            {
                File.Delete(uniqueKey);
            }
        }

        protected void Save(string uniqueKey, object saveObj)
        {
            string json = JsonUtility.ToJson(saveObj);
            try
            {
                File.WriteAllText(GetFilePath(uniqueKey), json);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        protected async Task SaveAsync(string uniqueKey, object data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            byte[] bt = (byte[])data;

            await Task.Run(() =>
            {
                using (FileStream stream = File.Open(GetFilePath(uniqueKey), FileMode.OpenOrCreate,
                           FileAccess.ReadWrite))
                {
                    bf.Serialize(stream, data);
                    File.WriteAllBytes(GetFilePath(uniqueKey), bt);
                }
            });
        }

        private string GetFilePath(string uniqueKey)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return Path.Combine(Application.persistentDataPath, uniqueKey);
#else
            return Path.Combine(Application.dataPath, uniqueKey);
#endif
        }
    }
}