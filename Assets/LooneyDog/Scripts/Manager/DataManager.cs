using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSG.iOSKeychain;
using System.IO;
namespace LooneyDog
{

    public class DataManager : MonoBehaviour
    {
        public PlayerData player { get; private set; } = new PlayerData();
        [SerializeField]
        private string master_Keyword;

        void Start()
        {
            Read(player);
            player.Check();
        }

        void OnDisable()
        {
            Write(player);
        }

        void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Write(player);
            }
        }

        void Read<T>(T obj)
        {
            string dataAsJson = Keychain.GetValue(master_Keyword);
            JsonUtility.FromJsonOverwrite(dataAsJson, obj);
        }

        void Write<T>(T obj)
        {
            string dataAsJson = JsonUtility.ToJson(obj);
            Keychain.SetValue(master_Keyword, dataAsJson);

            //string saveFilePath = Application.persistentDataPath + "/Test.json";
            //string savePlayerData = JsonUtility.ToJson(playerData);
            //File.WriteAllText(saveFilePath, dataAsJson);
        }

        public void SaveGame()
        {
            
        }
    }
}
