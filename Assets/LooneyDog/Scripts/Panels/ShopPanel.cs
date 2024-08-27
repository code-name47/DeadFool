using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LooneyDog
{
    public class ShopPanel : MonoBehaviour
    {
        [Header("Ship Details")]
        [SerializeField] private Image _speed,_hull,_armor,_thrust;
        [SerializeField] private TextMeshProUGUI _shipDescription;
        [SerializeField] private TextMeshProUGUI _shipName;

        [Header("Button Unloackables")]
        [SerializeField] private Button _buyButton, _selectButton, _selectedButton;

        private void Awake()
        {
            _buyButton.onClick.AddListener(OnClickBuyButton);
            _selectButton.onClick.AddListener(OnClickSelectButton);
        }

        private void OnClickBuyButton() { }

        private void OnClickSelectButton() {
            GameManager.Game.Skin.SetActiveCharacter(GameManager.Game.Level.PlayerSelectController.ActiveCharacter);
            GameManager.Game.Skin.GetActiveCharacter();
            SetCharacterStatus(GameManager.Game.Skin.CurrentActiveCharacter);
        }

        

        public void SetShipAttributes(string shipName,float speed, float hull, float armor, float thrust, string description) {
            _speed.fillAmount = speed / 100f;
            _hull.fillAmount = hull / 100f;
            _armor.fillAmount = armor / 100f;
            _thrust.fillAmount = thrust / 100f;
            _shipDescription.text = description;
            _shipName.text = shipName;
        }
        public void SetCharacterStatus(CharacterId CharacterIds)
        {
            if (GameManager.Game.Skin.CharacterObject[(int)CharacterIds].Owned)
            {
                if (GameManager.Game.Skin.CharacterObject[(int)CharacterIds].Selected)
                {
                    _buyButton.gameObject.SetActive(false);
                    _selectButton.gameObject.SetActive(false);
                    _selectedButton.gameObject.SetActive(true);

                    /*GameManager.Game.Screen.Home.CharacterSelectPanel.BuyButton.gameObject.SetActive(false);
                    GameManager.Game.Screen.Home.CharacterSelectPanel.SelectButton.gameObject.SetActive(false);*/
                }
                else
                {
                    _buyButton.gameObject.SetActive(false);
                    _selectButton.gameObject.SetActive(true);
                    _selectedButton.gameObject.SetActive(false);

                    /*GameManager.Game.Screen.Home.CharacterSelectPanel.BuyButton.gameObject.SetActive(false);
                    GameManager.Game.Screen.Home.CharacterSelectPanel.SelectButton.gameObject.SetActive(true);*/
                }
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _selectButton.gameObject.SetActive(false);
                _selectedButton.gameObject.SetActive(false);

                /*GameManager.Game.Screen.Home.CharacterSelectPanel.BuyButton.gameObject.SetActive(true);
                GameManager.Game.Screen.Home.CharacterSelectPanel.SelectButton.gameObject.SetActive(false);*/
            }
        }
    }
}
