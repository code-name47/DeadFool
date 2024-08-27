using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class SkinSelectController : MonoBehaviour
    {
        [SerializeField] private GameObject[] _characters;
        [SerializeField] private CharacterId _selectedCharacter;
        [SerializeField] private PlayerController _playerController;
        
        //[SerializeField] private 

        public void OnEnable()
        {
            GetSelectedCharacter();
            SetActiveCharacter();
            SetPlayerControllerData();
        }

        public void GetSelectedCharacter() {
            _selectedCharacter = GameManager.Game.Skin.CurrentActiveCharacter;
        }

        public void SetActiveCharacter() {
                for (int i = 0; i < _characters.Length; i++)
                {
                    if ((int)_selectedCharacter == i)
                    {
                        _characters[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        _characters[i].gameObject.SetActive(false);                
                    }
                }
        }

        public void SetPlayerControllerData() {
            CharacterPrefab characterprefab = _characters[(int)_selectedCharacter].GetComponent<CharacterPrefab>();
            if (characterprefab.CharacterId == _selectedCharacter)
            {
                _playerController.GunLeft = characterprefab.GunController;
                _playerController.Katana = characterprefab.KatanaController;
                _playerController.PlayerAnimator.avatar = characterprefab.CharacterAvatar;
            }
            else {
                Debug.Log("character id mismatch in characterPrefab");
            }
        }
    }
}
