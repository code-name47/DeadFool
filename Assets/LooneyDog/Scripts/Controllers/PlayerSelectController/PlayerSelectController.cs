using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class PlayerSelectController : MonoBehaviour
    {
        public CharacterId ActiveCharacter { get => _activeCharacter; set => _activeCharacter = value; }

        [Header("Character and Animations")]
        [SerializeField] private GameObject[] _characterModels;
        [SerializeField] private GameObject[] _characterTimeLines;
        [SerializeField] private CharacterId _activeCharacter;

        private void OnEnable()
        {
            GameManager.Game.Level.PlayerSelectController = this;
            ActiveCharacter = GameManager.Game.Skin.CurrentActiveCharacter;
            ActivateCharacter(ActiveCharacter);
        }

        public void OnPressedNext()
        {
            if ((int)ActiveCharacter < _characterModels.Length - 1)
            {
                ActiveCharacter++;
            }
            if ((int)ActiveCharacter < _characterModels.Length)
            {
                ActivateCharacter(ActiveCharacter);
            }
        }

        public void OnPressedPrevious()
        {
            if ((int)ActiveCharacter > 0)
            {
                ActiveCharacter--;
            }

            if ((int)ActiveCharacter >= 0)
            {
                ActivateCharacter(ActiveCharacter);
            }
        }

        private void ActivateCharacter(CharacterId ActiveCharacter)
        {
            if (_characterModels.Length == _characterTimeLines.Length)
            {
                for (int i = 0; i < _characterModels.Length; i++)
                {
                    if ((int)ActiveCharacter == i)
                    {
                        _characterModels[i].gameObject.SetActive(true);
                        _characterTimeLines[i].gameObject.SetActive(true);
                        SetCharacterUiData();
                    }
                    else
                    {
                        _characterModels[i].gameObject.SetActive(false);
                        _characterTimeLines[i].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                Debug.Log("Error Character TimeLine missing or the model of the character is missing");
            }
        }

        private void SetCharacterUiData()
        {
            GameManager.Game.Screen.Shop.SetCharacter(
                GameManager.Game.Skin.CharacterObject[(int)_activeCharacter].Health / 100,
                GameManager.Game.Skin.CharacterObject[(int)_activeCharacter].Armor / 100,
                GameManager.Game.Skin.CharacterObject[(int)_activeCharacter].GunPower / 100,
                GameManager.Game.Skin.CharacterObject[(int)_activeCharacter].KatanaDamage / 100);
        }
    }
}
