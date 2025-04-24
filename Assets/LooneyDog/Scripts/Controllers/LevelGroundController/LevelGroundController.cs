using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class LevelGroundController : MonoBehaviour
    {
        [SerializeField] private Transform _levelGround;

        private void Start()
        {
            GameManager.Game.Level.SetLevelSelectScene(_levelGround);

        }
    }
}
