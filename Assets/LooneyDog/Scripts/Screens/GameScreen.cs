using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LooneyDog
{
    public class GameScreen : MonoBehaviour
    {
        //public JoypadController JoypadController { get => _joypadController; set => _joypadController = value; }
        //public DefaultInputActions InputActions { get => _inputActions; set => _inputActions = value; }

        //[SerializeField] private JoypadController _joypadController;
        [SerializeField] public DefaultInputActions _inputActions;
        [SerializeField] private PausePanel _pauseMenu;
        [SerializeField] private float _transitionTime;
        [SerializeField] private Button _pauseButton;
        //[SerializeField] private 

        private void Awake()
        {
            _inputActions = new DefaultInputActions();
            _inputActions.Enable();

            _pauseButton.onClick.AddListener(OnClickPausePanel);
        }

        private void OnClickPausePanel() {
            GameManager.Game.Screen.OpenPopUpScreen(_pauseMenu.transform, this.transform, ScreenLocation.Pop, _transitionTime, true);
        }
    }
}
