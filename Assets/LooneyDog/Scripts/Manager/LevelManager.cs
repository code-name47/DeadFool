using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class LevelManager : MonoBehaviour
    {

        public int LevelNumber { get => _levelNumber; set => _levelNumber = value; }
        public GameDifficulty Difficulty { get => _difficulty; set => _difficulty = value; }
        public ShipSelectController ShipSelectController { get => _shipSelectController; set => _shipSelectController = value; }
        public PlayerSelectController PlayerSelectController { get => _playerSelectController; set => _playerSelectController = value; }

        [Header("Level Details")]
        [SerializeField] private int _levelNumber;
        [SerializeField] private GameDifficulty _difficulty;
        [SerializeField] private LevelData[] levelDatas;

        [Header("Level Controllers")]
        [SerializeField] private ShipSelectController _shipSelectController;
        [SerializeField] private PlayerSelectController _playerSelectController;
        private LevelDataStruct Currentleveldata;

        public void SetCurrentLevelDetails(int levelnumber, GameDifficulty difficulty) //Called From LoadingScreen 
        {
            LevelNumber = levelnumber;
            Difficulty = difficulty;
        }

        public void GetLevelData(int levelNumber, GameDifficulty gamedifficulty)
        {
            if ((levelNumber - 1) == levelDatas[levelNumber - 2].LevelNumber)//-2 coz sciptable object array starts from 0
            {
                Currentleveldata = levelDatas[levelNumber - 2].GetLevelData(gamedifficulty);//-2 coz sciptable object array starts from 0
            }
            else
            {
                Debug.Log("LevelNumber Mismatch" + " The level number should be" + levelNumber + " but it is :="  + levelDatas[levelNumber - 2].LevelNumber);
            }
        }
    }
}
