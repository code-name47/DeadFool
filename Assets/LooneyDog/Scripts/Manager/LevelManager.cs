using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public TurretManager TurretManager { get => _turretManager; set => _turretManager = value; }
        public MissileManager MissileManager { get => _missileManager; set => _missileManager = value; }
        public Transform LevelGrounds { get => _levelGrounds; set => _levelGrounds = value; }
        public GunSelectController GunSelectController { get => _gunSelectController; set => _gunSelectController = value; }
        public KatanaSelectController KatanaSelectController { get => _katanaSelectController; set => _katanaSelectController = value; }
        public CameraController CameraController { get => _cameraController; set => _cameraController = value; }
        public ObjectPoolController ObjectPooler { get => _objectPooler; set => _objectPooler = value; }

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
        [SerializeField] private GunSelectController _gunSelectController;
        [SerializeField] private KatanaSelectController _katanaSelectController;
        [SerializeField] private PlayerController _currentPlayerController;
        [SerializeField] private CameraController _cameraController;
        private LevelDataStruct Currentleveldata;
        private WaveDataStruct[] CurrentLevelWaveData;

        [Header("In Game Managers(RunTime)")]
        [SerializeField] private TurretManager _turretManager;
        [SerializeField] private MissileManager _missileManager;

        [Header("Misc")]
        [SerializeField] private float _waveStartDelay,_timerCount;
        [SerializeField] private bool _startTimer=false;
        [SerializeField] private Stack<float> _missileStack;
        [SerializeField] private Stack<int> _missilePosition;
        [SerializeField] private Stack<MissileType> _missileType;

        [Header("LevelSelectSCene")]
        [SerializeField] private Transform _levelGrounds;

        [Header("ObjectPool")]
        [SerializeField] private ObjectPoolController _objectPooler;


        private void Update()
        {
            if (_timerCount > 0 && _startTimer == true)
            {
                _timerCount -= Time.deltaTime;
                //_timerText.text = Mathf.Ceil(_timerCount).ToString();
                GameManager.Game.Screen.GameScreen.SetTimerText(Mathf.Ceil(_timerCount).ToString());
                //Debug.Log("missile stack = :"+ _missileStack.Count);
                if (_missileStack != null && _missileStack.Count > 0)
                {
                    //Debug.Log("inside stack check");
                    CheckMissile();
                }
            }
            else
            {
                if (_startTimer == true)
                {
                    _startTimer = false;
                    GameManager.Game.Level.FinishWave();
                }
            }
        }

        public void SetTimer(float timeCount, bool startTimer)
        {
            _timerCount = timeCount;
            _startTimer = startTimer;
        }

        private void CheckMissile() {
            //Debug.Log("timer count =" + _timerCount + " || missilepeek" + _missileStack.Peek());
            if ((int)_timerCount == (int)_missileStack.Peek() && (int)_timerCount>0)
            {
                int i = (int)_missileStack.Pop();

                if (_missilePosition.TryPeek(out int position) && _missilePosition.TryPeek(out int missiletype))
                {
                    _missileManager.LaunchMissile(_missilePosition.Pop(),_missileType.Pop());
                }
                else {
                    Debug.Log("Error in stackdata fetching for missile");
                }

                Debug.Log("Missile Delay =" + i);
                if (_missileStack.TryPeek(out float result ))
                {
                    if (i == _missileStack.Peek())
                    {
                        CheckMissile();
                    };
                }
            }
        }

        public void SetLevelSelectScene(Transform levelGround) {
            _levelGrounds = levelGround;
        }

        public void SetCurrentLevelDetails(int levelnumber, GameDifficulty difficulty) //Called From LoadingScreen 
        {
            LevelNumber = levelnumber;
            Difficulty = difficulty;
        }

        public void GetLevelData(int levelNumber, GameDifficulty gamedifficulty)
        {
            if ((levelNumber - 2) == levelDatas[levelNumber - 3].LevelNumber)//-2 coz sciptable object array starts from 0
            {
                Currentleveldata = levelDatas[levelNumber - 3].GetLevelData(gamedifficulty);//-2 coz sciptable object array starts from 0
                CurrentLevelWaveData = levelDatas[levelNumber - 3].GetWaveDataStruct();
                StartLevel();
            }
            else
            {
                Debug.Log("LevelNumber Mismatch" + " The level number should be" + levelNumber + " but it is :="  + levelDatas[levelNumber - 3].LevelNumber);
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
                //Set Wave Text
                GameManager.Game.Screen.GameScreen.EnableWaveText(CurrentLevelWaveData[_currentWave].waveName);
                //Turret
                FindObjectOfType<TurretManager>().SpwanTurretByFormation(CurrentLevelWaveData[_currentWave].Turretformation, CurrentLevelWaveData[_currentWave].baseRotation, CurrentLevelWaveData[_currentWave].turretHeadRotation);
                //Timer
                //GameManager.Game.Screen.GameScreen.SetTimer(CurrentLevelWaveData[_currentWave].WaveTime , true);
                SetTimer(CurrentLevelWaveData[_currentWave].WaveTime, true);
                //Missiles
                _missileStack = new Stack<float>( CurrentLevelWaveData[_currentWave].missileDelays.Reverse());
                _missilePosition = new Stack<int>(CurrentLevelWaveData[_currentWave].missilePosition.Reverse());
                _missileType = new Stack<MissileType>(CurrentLevelWaveData[_currentWave].missileType.Reverse());
                //FindObjectOfType<MissileManager>().StartMissile(CurrentLevelWaveData[_currentWave].missileDelays);
            }
        }

        public void FinishWave() {
            _currentWave++;
            if (_currentWave < CurrentLevelWaveData.Length)
            {
                FindObjectOfType<TurretManager>().DisableAllTurrets(); //Destroy all turrets
                //Destroy All missiles
                //Destroy all Mines
                SetNewWave();
            }
            else
            {
                LevelWin();
            }
        }

        public void CheckAllEnimiesDestoryed() { 
        
        }

        public int GetLevelTitleAndStory(int levelnumber,out string title, out string story) {
            title = levelDatas[levelnumber].LevelTitle;
            story =  levelDatas[levelnumber].LevelStory;
            return levelDatas.Count();
        }


        public void LevelWin() {
            Debug.Log("Level Won");
            //Disable Slow Motion Power Of character
            //_currentPlayerController.EnableSlowMotion = false;
        }

        public void GameOver() {
            //Disable Slow Motion Power Of character
            //_currentPlayerController.EnableSlowMotion = false;
        }
    }
}
