using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class ShipSelectController : MonoBehaviour
    {
        public ShiPId ActiveShip { get => _activeShip; set => _activeShip = value; }

        [Header("Ships and Animations")]
        [SerializeField] private GameObject[] _shipModels;
        [SerializeField] private GameObject[] _shipTimeLines;
        [SerializeField] private ShiPId _activeShip;

        private void OnEnable()
        {
            GameManager.Game.Level.ShipSelectController = this;
            ActiveShip = GameManager.Game.Skin.CurrentActiveShip;
            ActivateShip(ActiveShip);
        }

        public void OnPressedNext() {
            if ((int)ActiveShip < _shipModels.Length-1)
            {
                ActiveShip++;
            }
            if ((int)ActiveShip < _shipModels.Length) {
                ActivateShip(ActiveShip);
            }
        }

        public void OnPressedPrevious() {
            if ((int)ActiveShip > 0)
            {
                ActiveShip--;
            }

            if ((int)ActiveShip >= 0)
            {
                ActivateShip(ActiveShip);
            }
        }

        private void ActivateShip(ShiPId activeship) {
            if (_shipModels.Length == _shipTimeLines.Length)
            {
                for (int i = 0; i < _shipModels.Length; i++)
                {
                    if ((int)activeship == i)
                    {
                        _shipModels[i].gameObject.SetActive(true);
                        _shipTimeLines[i].gameObject.SetActive(true);
                        SetShipUiData();
                    }
                    else
                    {
                        _shipModels[i].gameObject.SetActive(false);
                        _shipTimeLines[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                Debug.Log("Error Ship TimeLine missing or the model of the ship is missing");
            }
        }

        private void SetShipUiData() {
            GameManager.Game.Screen.Shop.SetShipData(
                GameManager.Game.Skin.ShipSkins[(int)_activeShip].ShipName,
                GameManager.Game.Skin.ShipSkins[(int)_activeShip].ShipDiscription,
                GameManager.Game.Skin.ShipSkins[(int)_activeShip].Speed / 100,
                GameManager.Game.Skin.ShipSkins[(int)_activeShip].Health / 100,
                GameManager.Game.Skin.ShipSkins[(int)_activeShip].Shield / 100,
                GameManager.Game.Skin.ShipSkins[(int)_activeShip].Boost / 100,
                GameManager.Game.Skin.ShipSkins[(int)_activeShip].BaseDamage / 100,
                (float)GameManager.Game.Skin.ShipSkins[(int)_activeShip].Weapon1Capacity / 20,
                (float)GameManager.Game.Skin.ShipSkins[(int)_activeShip].Weapon2Capacity / 20);
        }
    }
}
