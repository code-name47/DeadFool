using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class KatanaSelectController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _katanaModels;
        [SerializeField] private GameObject[] _katanaTimeLines;
        [SerializeField] private KatanaId _activeKatana;
        [SerializeField] private GameObject _weaponsRoom;

        public KatanaId ActiveKatana { get => _activeKatana; set => _activeKatana = value; }
        public GameObject WeaponsRoom { get => _weaponsRoom; set => _weaponsRoom = value; }

        private void OnEnable()
        {
            //GameManager.Game.Level.KatanaSelectController = this;
            ActiveKatana = GameManager.Game.Weapon.CurrentActiveKatana;
            _weaponsRoom.SetActive(true);
            //ActivateCharacter(ActiveKatana);
        }

        private void OnDisable()
        {
            //_weaponsRoom.SetActive(false);    
        }

        public void SetActiveKatana()
        {
            ActiveKatana = GameManager.Game.Weapon.CurrentActiveKatana;
        }

        public void OnPressedNext()
        {
            if ((int)ActiveKatana < _katanaModels.Length - 1)
            {
                ActiveKatana++;
            }
            if ((int)ActiveKatana < _katanaModels.Length)
            {
                ActivateKatana(ActiveKatana);
            }
        }

        public void OnPressedPrevious()
        {
            if ((int)ActiveKatana > 0)
            {
                ActiveKatana--;
            }

            if ((int)ActiveKatana >= 0)
            {
                ActivateKatana(ActiveKatana);
            }
        }

        public void ActivateKatana(KatanaId ActiveKatana)
        {
            if (_katanaModels.Length == _katanaTimeLines.Length)
            {
                for (int i = 0; i < _katanaModels.Length; i++)
                {
                    if ((int)ActiveKatana == i)
                    {
                        _katanaModels[i].SetActive(true);
                        _katanaTimeLines[i].SetActive(true);
                        SetKatanaUiData();
                    }
                    else
                    {
                        _katanaModels[i].SetActive(false);
                        _katanaTimeLines[i].SetActive(false);
                    }
                }
            }
            else
            {
                Debug.Log("Error Katana TimeLine missing or the model of the Katana is missing");
            }

        }

        public void DisableKatanas()
        {
            if (_katanaModels.Length == _katanaTimeLines.Length)
            {
                for (int i = 0; i < _katanaModels.Length; i++)
                {
                    _katanaModels[i].SetActive(false);
                    _katanaTimeLines[i].SetActive(false);
                }
            }
            else
            {
                Debug.Log("Error Katana TimeLine missing or the model of the Katana is missing");
            }
        }

        private void SetKatanaUiData()
        {
            GameManager.Game.Screen.Shop.SetKatana(
                (float)GameManager.Game.Weapon.KatanaObjects[(int)ActiveKatana].Damage / 100f);
            GameManager.Game.Screen.Shop.KatanaShopPanel.SetKatanaStatus(ActiveKatana);
        }

    }
}