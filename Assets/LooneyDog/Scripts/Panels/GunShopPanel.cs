using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace LooneyDog
{
    public class GunShopPanel : MonoBehaviour
    {


        [Header("Gun Details")]
        [SerializeField] private Image _fireRate, _damage;
        [SerializeField] private TextMeshProUGUI _gunName;


        [Header("Gun Button Unloackables")]
        [SerializeField] private Button _buyGunButton, _selectGunButton, _selectedGunButton;
        [SerializeField] private Button _nextButton, _previousButton;

        private void Awake()
        {
            _buyGunButton.onClick.AddListener(OnClickGunBuyButton);
            _selectGunButton.onClick.AddListener(OnClickGunSelectButton);
            _nextButton.onClick.AddListener(OnClickNextButton);
            _previousButton.onClick.AddListener(OnClickPreviousButton);
        }

        private void OnClickGunBuyButton() { }

        private void OnClickNextButton() {
            if (GameManager.Game.Level.GunSelectController != null)
            {
                GameManager.Game.Level.GunSelectController.OnPressedNext();
            }
            else
            {
                Debug.Log("Error : GunSelectController not assigned to LevelManager");
            }
            GameManager.Game.Sound.PlayUisound(UiClipId.Swish);

        }

        private void OnClickPreviousButton() {
            if (GameManager.Game.Level.GunSelectController != null)
            {
                GameManager.Game.Level.GunSelectController.OnPressedPrevious();
            }
            else
            {
                Debug.Log("Error :  GunSelectController not assigned to LevelManager");
            }
            GameManager.Game.Sound.PlayUisound(UiClipId.Swish);

        }

        private void OnClickGunSelectButton()
        {
            GameManager.Game.Weapon.SetActiveGun(GameManager.Game.Level.GunSelectController.ActiveGun);
            GameManager.Game.Weapon.GetActiveGun();
            SetGunStatus(GameManager.Game.Weapon.CurrentActiveGun);
        }



        public void SetGunAttributes(string GunName, float FireRate, float Damage)
        {
            _fireRate.fillAmount = FireRate / 100f;
            _damage.fillAmount = Damage / 100f;

            _gunName.text = GunName;
        }

        public void SetGunStatus(GunId Gunids)
        {
            if (GameManager.Game.Weapon.GunObjects[(int)Gunids].Owned)
            {
                if (GameManager.Game.Weapon.GunObjects[(int)Gunids].Selected)
                {
                    _buyGunButton.gameObject.SetActive(false);
                    _selectGunButton.gameObject.SetActive(false);
                    _selectedGunButton.gameObject.SetActive(true);

                    /*GameManager.Game.Screen.Home.CharacterSelectPanel.BuyButton.gameObject.SetActive(false);
                    GameManager.Game.Screen.Home.CharacterSelectPanel.SelectButton.gameObject.SetActive(false);*/
                }
                else
                {
                    _buyGunButton.gameObject.SetActive(false);
                    _selectGunButton.gameObject.SetActive(true);
                    _selectedGunButton.gameObject.SetActive(false);

                    /*GameManager.Game.Screen.Home.CharacterSelectPanel.BuyButton.gameObject.SetActive(false);
                    GameManager.Game.Screen.Home.CharacterSelectPanel.SelectButton.gameObject.SetActive(true);*/
                }
            }
            else
            {
                _buyGunButton.gameObject.SetActive(true);
                _selectGunButton.gameObject.SetActive(false);
                _selectedGunButton.gameObject.SetActive(false);

                /*GameManager.Game.Screen.Home.CharacterSelectPanel.BuyButton.gameObject.SetActive(true);
                GameManager.Game.Screen.Home.CharacterSelectPanel.SelectButton.gameObject.SetActive(false);*/
            }

        }
    }
}
