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
        public PlayerController CurrentPlayerController { get => _currentPlayerController; set => _currentPlayerController = value; }

        [Header("Level Details")]
        [SerializeField] private int _levelNumber;
        [SerializeField] private GameDifficulty _difficulty;
        [SerializeField] private LevelData[] levelDatas;
        [SerializeField] private bool _enemiesCleared, _timeCleared;
        [SerializeField] private int _currentWave=0;
        [SerializeField] private float _timeRemaning;

        [Header("Level Controllers")]
        [SerializeField] private ShipSelectController _shipSelectController;
        [SerializeField] private PlayerSelectController _playerSelectController;
        [SerializeField] private PlayerController _currentPlayerController;
        private LevelDataStruct Currentleveldata;
        private WaveDataStruct[] CurrentLevelWaveData;

        [Header("Misc")]
        [SerializeField] private float _waveStartDelay;


        private void FixedUpdate()
        {
            
        }
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
                CurrentLevelWaveData = levelDatas[levelNumber - 2].GetWaveDataStruct();
                StartLevel();
            }
            else
            {
                Debug.Log("LevelNumber Mismatch" + " The level number should be" + levelNumber + " but it is :="  + levelDatas[levelNumber - 2].LevelNumber);
            }
        }
        public void StartLevel() {
            StartCoroutine(LevelStartDelay());
        }

        IEnumerator LevelStartDelay() {
            yield return new WaitForSeconds(_waveStartDelay);
            SetNewWave();
        }
        public void SetNewWave() {
            if (_currentWave < CurrentLevelWaveData.Length) {
                GameManager.Game.Screen.GameScreen.EnableWaveText();
                FindObjectOfType<TurretManager>().SpwanTurretByFormation(CurrentLevelWaveData[_currentWave].Turretformation);
            }
        }
    }
}
