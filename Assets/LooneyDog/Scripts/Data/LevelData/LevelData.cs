using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LooneyDog
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelDataScriptableObject", order = 1)]
    public class LevelData : ScriptableObject
    {
        public int LevelNumber { get { return _levelNumber; } set { _levelNumber = value; } }
        public bool LevelCompleted { get { return _levelCompleted; } set { _levelCompleted = value; } }

        public int StarsObtained { get { return _starsObtained; } set { _starsObtained = value; } }

        public string LevelTitle { get => _levelTitle; set => _levelTitle = value; }
        public string LevelStory { get => _levelStory; set => _levelStory = value; }

        [SerializeField] private int _levelNumber;
        [SerializeField] private bool _levelCompleted;
        [SerializeField] private bool _levelLocked;
        [SerializeField] private int _starsObtained;
        [SerializeField] private String _levelTitle;
        [SerializeField][TextArea(5,10)] private String _levelStory;



        public LevelDataStruct[] datas = new LevelDataStruct[3];

        public WaveDataStruct[] waves = new WaveDataStruct[3];

        public LevelDataStruct GetLevelData(GameDifficulty difficulty)
        {
            return datas[(int)difficulty];
        }

        public WaveDataStruct[] GetWaveDataStruct()
        {
            return waves;
        }

        public void SetLevelData(bool LevelCompleted, int starsObtained)
        {
            _levelCompleted = LevelCompleted;
            _starsObtained = starsObtained;
        }

    }

    [Serializable]
    public struct LevelDataStruct
    {
        [SerializeField] public float timeRequriedFor_1Star;
        [SerializeField] public float timeRequriedFor_2Star;
        [SerializeField] public float timeRequriedFor_3Star;
        [SerializeField] public int rewardfor1Star;
        [SerializeField] public int rewardfor2Star;
        [SerializeField] public int rewardfor3Star;
    }

    [Serializable]
    public struct WaveDataStruct
    {
        [Header("WaveName")]
        [SerializeField] public String waveName;
        [Header("Turret")]
        [SerializeField] public TurretType[] Turretformation;
        [SerializeField] public TurretBaseRotation[] baseRotation;
        [SerializeField] public TurretHeadRotation[] turretHeadRotation;
        [Header("Missile")]
        [SerializeField] public float[] missileDelays;
        [SerializeField] public MissileType[] missileType;
        [SerializeField] public int[] missilePosition;  
        [Header("Timer(Seconds)")]
        [SerializeField] public float WaveTime;
/*        [Header("Missile")]
        [SerializeField] public*/
    }

}
