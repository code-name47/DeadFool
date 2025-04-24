using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LooneyDog
{
    public class KatanaShopPanel : MonoBehaviour
    {


        [Header("Katana Details")]
        [SerializeField] private Image _damage;
        [SerializeField] private TextMeshProUGUI _katanaName;


        [Header("Katana Button Unloackables")]
        [SerializeField] private Button _buyKatanaButton, _selectKatanaButton, _selectedKatanaButton;
        [SerializeField] private Button _nextButton, _previousButton;

        private void Awake()
        {
            _buyKatanaButton.onClick.AddListener(OnClickKatanaBuyButton);
            _selectKatanaButton.onClick.AddListener(OnClickKatanaSelectButton);
            _nextButton.onClick.AddListener(OnClickNextButton);
            _previousButton.onClick.AddListener(OnClickPreviousButton);
        }

        private void OnClickKatanaBuyButton() { }

        private void OnClickNextButton()
        {
            if (GameManager.Game.Level.KatanaSelectController != null)
            {
                GameManager.Game.Level.KatanaSelectController.OnPressedNext();
            }
            else
            {
                Debug.Log("Error : KatanaSelectController not assigned to LevelManager");
            }
            GameManager.Game.Sound.PlayUisound(UiClipId.Swish);

        }

        private void OnClickPreviousButton()
        {
            if (GameManager.Game.Level.KatanaSelectController != null)
            {
                GameManager.Game.Level.KatanaSelectController.OnPressedPrevious();
            }
            else
            {
                Debug.Log("Error :  KatanaSelectController not assigned to LevelManager");
            }
            GameManager.Game.Sound.PlayUisound(UiClipId.Swish);

        }

        private void OnClickKatanaSelectButton()
        {
            GameManager.Game.Weapon.SetActiveKatana(GameManager.Game.Level.KatanaSelectController.ActiveKatana);
            GameManager.Game.Weapon.GetActiveKatana();
            SetKatanaStatus(GameManager.Game.Weapon.CurrentActiveKatana);
        }



        public void SetKatanaAttributes(string KatanaName, float Damage)
        {
            _damage.fillAmount = Damage / 100f;

            _katanaName.text = KatanaName;
        }

        public void SetKatanaStatus(KatanaId Katanaids)
        {
            if (GameManager.Game.Weapon.KatanaObjects[(int)Katanaids].Owned)
            {
                if (GameManager.Game.Weapon.KatanaObjects[(int)Katanaids].Selected)
                {
                    _buyKatanaButton.gameObject.SetActive(false);
                    _selectKatanaButton.gameObject.SetActive(false);
                    _selectedKatanaButton.gameObject.SetActive(true);

                    /*GameManager.Game.Screen.Home.CharacterSelectPanel.BuyButton.gameObject.SetActive(false);
                    GameManager.Game.Screen.Home.CharacterSelectPanel.SelectButton.gameObject.SetActive(false);*/
                }
                else
                {
                    _buyKatanaButton.gameObject.SetActive(false);
                    _selectKatanaButton.gameObject.SetActive(true);
                    _selectedKatanaButton.gameObject.SetActive(false);

                    /*GameManager.Game.Screen.Home.CharacterSelectPanel.BuyButton.gameObject.SetActive(false);
                    GameManager.Game.Screen.Home.CharacterSelectPanel.SelectButton.gameObject.SetActive(true);*/
                }
            }
            else
            {
                _buyKatanaButton.gameObject.SetActive(true);
                _selectKatanaButton.gameObject.SetActive(false);
                _selectedKatanaButton.gameObject.SetActive(false);

                /*GameManager.Game.Screen.Home.CharacterSelectPanel.BuyButton.gameObject.SetActive(true);
                GameManager.Game.Screen.Home.CharacterSelectPanel.SelectButton.gameObject.SetActive(false);*/
            }

        }
    }
}