using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class SelectionControllerAssigner : MonoBehaviour
    {
        [SerializeField] private GunSelectController _gunSelectController;
        [SerializeField] private PlayerSelectController _playerSelectController;
        [SerializeField] private KatanaSelectController _katanaSelectController;

        private void OnEnable()
        {
            if (_playerSelectController != null)
            {
                GameManager.Game.Level.PlayerSelectController = _playerSelectController;
            }
            if (_gunSelectController != null)
            {
                GameManager.Game.Level.GunSelectController = _gunSelectController;
            }
            if (_katanaSelectController != null)
            {
                GameManager.Game.Level.KatanaSelectController = _katanaSelectController;    
            }

        }

    }
}
