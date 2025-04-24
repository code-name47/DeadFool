using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using DG.Tweening;
namespace LooneyDog
{
    public class ShopScreen : MonoBehaviour
    {
        public Button BackButton { get => _backButton; set => _backButton = value; }
        public float TransitionSpeed { get => _transitionSpeed; set => _transitionSpeed = value; }
        public int NormalizedInput { get => _normalizedInput; set => _normalizedInput = value; }
        public ShopPanel ShopPanel { get => _shopPanel; set => _shopPanel = value; }
        public GunShopPanel GunShopPanel { get => _gunShopPanel; set => _gunShopPanel = value; }
        public KatanaShopPanel KatanaShopPanel { get => _katanaShopPanel; set => _katanaShopPanel = value; }

        [Header("Buttons")]
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _leftRotateButton;
        [SerializeField] private Button _rightRotateButton;
        [SerializeField] private Button _shipSwitchRightButton;
        [SerializeField] private Button _shipSwitchLeftButton;
        [SerializeField] private Button[] _shopPanelButtons;

        [Header("Ui")]
        [SerializeField] private TextMeshProUGUI _CharacterName;
        [SerializeField] private TextMeshProUGUI _CharacterDiscriptionText;
        [SerializeField] private Image _speedFillMeter, _hullFillMeter, _armorFillMeter, _thrustFillMeter;
        [SerializeField] private Image _gunDamage, _gunFireRate,_katanaDamage;
        [SerializeField] private Image _baseDamageFillMeter, _weaponCapacity_1, _weaponCapacity_2;
        [SerializeField] private ShopPanel _shopPanel;
        [SerializeField] private GunShopPanel _gunShopPanel;
        [SerializeField] private KatanaShopPanel _katanaShopPanel;
        [SerializeField] private PanelIds _activeShopPanel;


        [Header("Panels")]
        [SerializeField] private GameObject[] _panels;


        [Header("Properties")]
        [SerializeField] private float _transitionSpeed;
        [SerializeField] private int _normalizedInput;

        [Header("Scene")]
        [SerializeField] ShipSelectCameraController _cameraController;

        private void Awake()
        {
            _activeShopPanel = PanelIds.CharacterShopPanel;
            _backButton.onClick.AddListener(OnClickBackButton);
            _leftRotateButton.onClick.AddListener(OnClickLeftRotateButton);
            _rightRotateButton.onClick.AddListener(OnClickRightRotateButton);



            for (int i = 0; i < _shopPanelButtons.Length; i++)
            {
                var i1 = i;
                //_shopPanelButtons[i].onClick.AddListener(delegate{ OnClickShopPanelButton((PanelIds)i); });
                _shopPanelButtons[i].onClick.AddListener(() => { OnClickShopPanelButton((PanelIds)i1); });
            }
        }
        private void OnEnable()
        {
            _shopPanelButtons[(int)_activeShopPanel].Select();
            SetActivePanel();
            //_panels[(int)_activeShopPanel].SetActive(true);
            //GameManager.Game.Level.PlayerSelectController.ActivateCharacter(GameManager.Game.Level.PlayerSelectController.ActiveCharacter);
            //GameManager.Game.Level.PlayerSelectController.ActivateCharacter(GameManager.Game.Skin.CurrentActiveCharacter);
        }

        private void OnClickShopPanelButton(PanelIds panelid)
        {
            if (panelid != _activeShopPanel)
            {
                GameManager.Game.Screen.ClosePopUpScreen(_panels[(int)_activeShopPanel].transform, ScreenLocation.left, TransitionSpeed);
                _activeShopPanel = panelid;
                //SetActivePanel();
                GameManager.Game.Screen.OpenPopUpScreen(_panels[(int)panelid].transform, this.transform, ScreenLocation.right, TransitionSpeed, true);
                Debug.Log("Button Pressed is : " + (PanelIds)panelid);
            }
            _shopPanelButtons[(int)_activeShopPanel].Select();
            ActivateShopPanel();
        }

        private void ActivateShopPanel() {
            Debug.Log("Active Panel" + _activeShopPanel);
            switch (_activeShopPanel)
            {
                case PanelIds.CharacterShopPanel:
                    OnCharacterSelectView();
                    
                    break;
                case PanelIds.GunShopPanel:
                    OnGunSelectView();
                    
                    break;
                case PanelIds.KatanaShopPanel:
                    OnKatanaSelectView();
                    break;
            }
        }

        private void SetActivePanel() {
            for (int i = 0; i < _panels.Length; i++) {
                if (i == (int)_activeShopPanel)
                {
                    _panels[i].SetActive(true);
                }
                else
                {
                    _panels[i].SetActive(false);
                }
            }
            ActivateShopPanel();

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

        private void OnCharacterSelectView() {
            GameManager.Game.Level.GunSelectController.DisableGuns();
            GameManager.Game.Level.KatanaSelectController.DisableKatanas();
            Debug.Log("Character view Selected");
            GameManager.Game.Level.PlayerSelectController.gameObject.SetActive(true);
            GameManager.Game.Level.GunSelectController.gameObject.SetActive(false);
            GameManager.Game.Level.KatanaSelectController.gameObject.SetActive(false);
            GameManager.Game.Level.PlayerSelectController.ActivateCharacter(GameManager.Game.Skin.CurrentActiveCharacter);
        }

        private void OnGunSelectView() {
            GameManager.Game.Level.PlayerSelectController.DisableCharacters();
            GameManager.Game.Level.KatanaSelectController.DisableKatanas();
            Debug.Log("Gun view Selected");
            GameManager.Game.Level.PlayerSelectController.gameObject.SetActive(false);
            GameManager.Game.Level.GunSelectController.gameObject.SetActive(true);
            GameManager.Game.Level.KatanaSelectController.gameObject.SetActive(false);
            GameManager.Game.Level.GunSelectController.ActivateGun(GameManager.Game.Weapon.CurrentActiveGun);

        }

        private void OnKatanaSelectView() {
            GameManager.Game.Level.PlayerSelectController.DisableCharacters();
            GameManager.Game.Level.GunSelectController.DisableGuns();
            Debug.Log("Katana view Selected");
            GameManager.Game.Level.PlayerSelectController.gameObject.SetActive(false);
            GameManager.Game.Level.GunSelectController.gameObject.SetActive(false);
            GameManager.Game.Level.KatanaSelectController.gameObject.SetActive(true);
            GameManager.Game.Level.KatanaSelectController.ActivateKatana(GameManager.Game.Weapon.CurrentActiveKatana);
        }

        

        private void OnClickRightRotateButton()
        {
            NormalizedInput = 1;
            _cameraController.OnRotateButtonPressed(NormalizedInput);
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

        public void SetGun(float damage,float fireRate)
        {
            _gunFireRate.fillAmount = fireRate;
            _gunDamage.fillAmount = damage;
        }

        public void SetKatana(float damage) {
            _katanaDamage.fillAmount = damage;
        }
    }
    public enum PanelIds
    {
        CharacterShopPanel = 0,
        GunShopPanel = 1,
        KatanaShopPanel = 2
    }
}
