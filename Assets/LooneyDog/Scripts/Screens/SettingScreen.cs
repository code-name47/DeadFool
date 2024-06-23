using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{
    public class SettingScreen : MonoBehaviour
    {
        public Button BackButton { get => _backButton; set => _backButton = value; }
        [Header("Buttons")]
        [SerializeField] private Button _backButton;
        [Header("Properties")]
        [SerializeField] private float _transitionSpeed;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnClickBackButton);
        }

        private void OnClickBackButton() {
            //Save Settings and move back
            GameManager.Game.Screen.ClosePopUpScreen(gameObject.transform,GameManager.Game.Screen.Home.transform, ScreenLocation.right, _transitionSpeed,true);
            GameManager.Game.Screen.OpenPopUpScreen(GameManager.Game.Screen.Home.transform, ScreenLocation.right, _transitionSpeed);
        }

    }
}
