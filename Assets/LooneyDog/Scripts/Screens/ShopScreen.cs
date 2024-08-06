using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
namespace LooneyDog
{
    public class ShopScreen : MonoBehaviour
    {
        public Button BackButton { get => _backButton; set => _backButton = value; }
        public float TransitionSpeed { get => _transitionSpeed; set => _transitionSpeed = value; }
        public int NormalizedInput { get => _normalizedInput; set => _normalizedInput = value; }
        public ShopPanel ShopPanel { get => _shopPanel; set => _shopPanel = value; }

        [Header("Buttons")]
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _leftRotateButton;
        [SerializeField] private Button _rightRotateButton;
        [SerializeField] private Button _shipSwitchRightButton;
        [SerializeField] private Button _shipSwitchLeftButton;

        [Header("Ui")]
        [SerializeField] private TextMeshProUGUI _CharacterName;
        [SerializeField] private TextMeshProUGUI _CharacterDiscriptionText;
        [SerializeField] private Image _speedFillMeter, _hullFillMeter, _armorFillMeter, _thrustFillMeter;
        [SerializeField] private Image _baseDamageFillMeter, _weaponCapacity_1, _weaponCapacity_2;
        [SerializeField] private ShopPanel _shopPanel;
        

        [Header("Properties")]
        [SerializeField] private float _transitionSpeed;
        [SerializeField] private int _normalizedInput;

        [Header("Scene")]
        [SerializeField] ShipSelectCameraController _cameraController;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnClickBackButton);
            _leftRotateButton.onClick.AddListener(OnClickLeftRotateButton);
            _rightRotateButton.onClick.AddListener(OnClickRightRotateButton);
            _shipSwitchLeftButton.onClick.AddListener(onClickShipSwitchLeftButton);
            _shipSwitchRightButton.onClick.AddListener(onClickShipSwitchRightButton);
        }
        private void OnEnable()
        {
            GameManager.Game.Level.PlayerSelectController.ActivateCharacter(GameManager.Game.Level.PlayerSelectController.ActiveCharacter);
        }

        private void OnClickBackButton() {
            GameManager.Game.Level.PlayerSelectController.DisableCharacters();
            GameManager.Game.Screen.ClosePopUpScreen(gameObject.transform, GameManager.Game.Screen.Home.transform, ScreenLocation.down, _transitionSpeed, true);
            GameManager.Game.Screen.OpenPopUpScreen(GameManager.Game.Screen.Home.transform, ScreenLocation.down, _transitionSpeed);
            GameManager.Game.Sound.PlayUisound(UiClipId.Click);

        }

        private void OnClickLeftRotateButton() {
            NormalizedInput = -1;
            _cameraController.OnRotateButtonPressed(NormalizedInput);
        }

        private void OnClickRightRotateButton()
        {
            NormalizedInput = 1;
            _cameraController.OnRotateButtonPressed(NormalizedInput);
        }

        private void onClickShipSwitchRightButton()
        {
            if (GameManager.Game.Level.PlayerSelectController != null)
            {
                GameManager.Game.Level.PlayerSelectController.OnPressedNext();
            }
            else
            {
                Debug.Log("Error : PlayerSelectController not assigned to LevelManager");
            }
            GameManager.Game.Sound.PlayUisound(UiClipId.Swish);

        }

        private void onClickShipSwitchLeftButton()
        {
            if (GameManager.Game.Level.PlayerSelectController != null)
            {
                GameManager.Game.Level.PlayerSelectController.OnPressedPrevious();
            }
            else
            {
                Debug.Log("Error :  PlayerSelectController not assigned to LevelManager");
            }
            GameManager.Game.Sound.PlayUisound(UiClipId.Swish);

        }

        /* public void SetShipData(string ShipName, string CharacterDiscription,float speed,float hull,float armor,float thrust
             ,float baseDamage,float weaponCapacity1,float weaponCapacity2) {
             _shipName.text = ShipName;
             _CharacterDiscriptionText.text = CharacterDiscription;
             _speedFillMeter.fillAmount = speed;
             _hullFillMeter.fillAmount = hull;
             _armorFillMeter.fillAmount = armor;
             _baseDamageFillMeter.fillAmount = baseDamage;
             _weaponCapacity_1.fillAmount = weaponCapacity1;
             _weaponCapacity_2.fillAmount = weaponCapacity2;
         }*/

        public void SetCharacter(float speed, float armor, float gunPower, float katanDamage)
        {
            _speedFillMeter.fillAmount = speed;
            _armorFillMeter.fillAmount = armor;
            _baseDamageFillMeter.fillAmount = gunPower;
        }
    }
}
