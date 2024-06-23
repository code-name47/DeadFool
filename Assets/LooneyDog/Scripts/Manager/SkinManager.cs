using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class SkinManager : MonoBehaviour
    {
        public ShiPId CurrentActiveShip { get => _currentActiveShip; set => _currentActiveShip = value; }
        public ShipData[] ShipSkins { get => _shipSkins; set => _shipSkins = value; }

        [SerializeField] private ShipData[] _shipSkins;
        [SerializeField] private ShiPId _currentActiveShip;

        public void OnEnable()
        {
            //_shipSkins[(int)ShiPId.RedMamba].Speed = 0;
            //GameManager.Game.Data.player.SaveAllShipData();
        }

        public void SetShipDataSpeedToZeroTest() {
            _shipSkins[(int)ShiPId.RedMamba].Speed = 0;
            GameManager.Game.Data.player.SaveAllShipData();
        }
    }
}
