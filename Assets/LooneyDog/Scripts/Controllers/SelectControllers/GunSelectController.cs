using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class GunSelectController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gunModels;
        [SerializeField] private GameObject[] _gunTimeLines;
        [SerializeField] private GunId _activeGun;
        [SerializeField] private GameObject _weaponsRoom;

        public GunId ActiveGun { get => _activeGun; set => _activeGun = value; }
        public GameObject WeaponsRoom { get => _weaponsRoom; set => _weaponsRoom = value; }

        private void OnEnable()
        {
            //GameManager.Game.Level.GunSelectController = this;
            ActiveGun = GameManager.Game.Weapon.CurrentActiveGun;
            _weaponsRoom.SetActive(true);
            //ActivateCharacter(ActiveGun);
        }

        private void OnDisable()
        {
           // _weaponsRoom.SetActive(false);
        }

        public void SetActiveGun()
        {
            ActiveGun = GameManager.Game.Weapon.CurrentActiveGun;
        }

        public void OnPressedNext()
        {
            if ((int)ActiveGun < _gunModels.Length - 1)
            {
                ActiveGun++;
            }
            if ((int)ActiveGun < _gunModels.Length)
            {
                ActivateGun(ActiveGun);
            }
        }

        public void OnPressedPrevious()
        {
            if ((int)ActiveGun > 0)
            {
                ActiveGun--;
            }

            if ((int)ActiveGun >= 0)
            {
                ActivateGun(ActiveGun);
            }
        }

        public void ActivateGun(GunId ActiveGun)
        {
            if (_gunModels.Length == _gunTimeLines.Length)
            {
                for (int i = 0; i < _gunModels.Length; i++)
                {
                    if ((int)ActiveGun == i)
                    {
                        _gunModels[i].SetActive(true);
                        _gunTimeLines[i].SetActive(true);
                        SetGunUiData();
                    }
                    else
                    {
                        _gunModels[i].SetActive(false);
                        _gunTimeLines[i].SetActive(false);
                    }
                }
            }
            else
            {
                Debug.Log("Error Gun TimeLine missing or the model of the Gun is missing");
            }

        }

        public void DisableGuns()
        {
            if (_gunModels.Length == _gunTimeLines.Length)
            {
                for (int i = 0; i < _gunModels.Length; i++)
                {
                    _gunModels[i].SetActive(false);
                    _gunTimeLines[i].SetActive(false);
                }
            }
            else
            {
                Debug.Log("Error Gun TimeLine missing or the model of the Gun is missing");
            }
        }
        
        private void SetGunUiData()
        {
            GameManager.Game.Screen.Shop.SetGun(
                (float)GameManager.Game.Weapon.GunObjects[(int)ActiveGun].Damage / 100f,
                (float)GameManager.Game.Weapon.GunObjects[(int)ActiveGun].FireRate / 100f
                );
            GameManager.Game.Screen.Shop.GunShopPanel.SetGunStatus(ActiveGun);
        }


    }
}
