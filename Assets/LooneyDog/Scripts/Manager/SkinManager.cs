using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class SkinManager : MonoBehaviour
    {
        public CharacterId CurrentActiveCharacter { get => _currentActiveCharacter; set => _currentActiveCharacter = value; }
        public CharacterData[] CharacterObject { get => _characterObject; set => _characterObject = value; }
        public ShipData[] ShipSkins { get => _shipSkins; set => _shipSkins = value; }
        public ShiPId CurrentActiveShip { get => _currentActiveShip; set => _currentActiveShip = value; }

        [SerializeField] private CharacterData[] _characterObject;
        [SerializeField] private CharacterId _currentActiveCharacter;

        [SerializeField] private ShipData[] _shipSkins;
        [SerializeField] private ShiPId _currentActiveShip;


        public void OnEnable()
        {
            //_shipSkins[(int)ShiPId.RedMamba].Speed = 0;
            //GameManager.Game.Data.player.SaveAllShipData();
            GetActiveCharacter();
        }

        public void SetShipDataSpeedToZeroTest()
        {
            _shipSkins[(int)ShiPId.RedMamba].Speed = 0;
            GameManager.Game.Data.player.SaveAllShipData();
        }

        public void GetActiveCharacter() {
            foreach (CharacterData characterData in CharacterObject) {
                if (characterData.Selected)
                {
                    _currentActiveCharacter = characterData.CharacterId;
                }
            }
        }
    }
}
