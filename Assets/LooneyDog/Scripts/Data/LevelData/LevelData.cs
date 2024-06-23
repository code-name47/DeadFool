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

        [SerializeField] private int _levelNumber;
        [SerializeField] private bool _levelCompleted;
        [SerializeField] private int _starsObtained;

        public LevelDataStruct[] datas = new LevelDataStruct[3];

        public LevelDataStruct GetLevelData(GameDifficulty difficulty)
        {
            return datas[(int)difficulty];
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
}
