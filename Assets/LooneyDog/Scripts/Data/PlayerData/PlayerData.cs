using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace LooneyDog
{
    [Serializable]
    public class PlayerData
    {
        public int HighScore { get { return HIGHSCORE; } set { HIGHSCORE = value; } }
        public float CameraSensitivity { get { return CAMERASENSITIVITY; } set { CAMERASENSITIVITY = value; } }
        public bool ShowTutorial { get { return SHOWTUTORIAL; } set { SHOWTUTORIAL = value; } }

        public List<ShipData> ShipData { get => SHIPDATA; set => SHIPDATA = value; }
        public ShiPId CurrentShipId { get => CURRENTSHIPID; set => CURRENTSHIPID = value; }
        public int Duckpoint { get => DUCKPOINTS; set => DUCKPOINTS = value; }

        [SerializeField] private int HIGHSCORE;
        [SerializeField] private float CAMERASENSITIVITY = 0.5f;
        [SerializeField] private bool SHOWTUTORIAL = true;
        [SerializeField] private int COINS;
        [SerializeField] private int DUCKPOINTS;

        [SerializeField] List<ShipData> SHIPDATA = new List<ShipData>();
        [SerializeField] ShiPId CURRENTSHIPID;

        public void Check()
        {
            if (ShipData.Count== 0)
            {
                Debug.Log("in the if function empty funciton ");
                SaveNewAllShipData1stTIMEONLY();
                
            }
            else
            {
                LoadShipData();
                Debug.Log("in the if function load funciton ");
            }
            SaveAllShipData();
            //dataTester();
        }
        


        private void dataTester() {
            for (int i = 0; i < GameManager.Game.Skin.ShipSkins.Length; i++) {
                Debug.Log("data stored in dictory is : " + ShipData[i].ShipName);
            }
        }

        private void SaveNewAllShipData1stTIMEONLY() {
            for (int i = 0; i < GameManager.Game.Skin.ShipSkins.Length; i++)
            {
                ShipData.Add(GameManager.Game.Skin.ShipSkins[i]);
            }
        }

        public void SaveAllShipData() {
            for (int i = 0; i < GameManager.Game.Skin.ShipSkins.Length; i++)
            {
                if (ShipData.Contains(GameManager.Game.Skin.ShipSkins[i]))
                {
                    ShipData[i]=GameManager.Game.Skin.ShipSkins[i];
                    //Debug.Log("The speed is this == "+ShipData[(int)ShiPId.RedMamba].Speed);
                }
                else
                {
                    ShipData.Add(GameManager.Game.Skin.ShipSkins[i]);
                }
            }
        }

        public void SaveShipDataAt(ShiPId shipId,ShipData data) {
            if (ShipData.Contains(GameManager.Game.Skin.ShipSkins[(int)shipId])) {
                ShipData[(int)shipId] = data;
                Debug.Log("Data Saved Succesfully for the ship =" + shipId);
            }
        }

        public void LoadShipData() {
            for (int i = 0; i < GameManager.Game.Skin.ShipSkins.Length; i++)
            {
                if (ShipData.Contains(GameManager.Game.Skin.ShipSkins[i]))
                {
                    GameManager.Game.Skin.ShipSkins[i] = ShipData[i];
                }
            }
            GameManager.Game.Skin.CurrentActiveShip = CurrentShipId;
        }

        

        public void AddRewardCoins(int coins)
        {
            COINS = COINS + coins;
        }

        public void SubstractRewardCoins(int coins)
        {
            if ((COINS - coins) > 0)
            {
                COINS = COINS - coins;
            }
            else
            {
                COINS = 0;
            }
        }

        public bool CheckIfEnoughCoins(int value)
        {
            if (value > COINS)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetCoinData()
        {
            return (Int32)COINS;
        }
        public void SetNormalizedCameraSensitivity(float value)
        {
            CAMERASENSITIVITY = value / 100;
        }

        public void AddDuckPoints(int DuckPoints) {
            DUCKPOINTS += DuckPoints;
        }

        public int UpdateDuckPoints() {
            return DUCKPOINTS;
        }

    }

    [Serializable]
    public struct LevelDataJson
    {
        public bool LevelCompleted;
        public int StarsObtained;
    }

    public struct VehicleDataJson
    {
        public bool Unlocked;
        public int currentSkin;
    }

}
