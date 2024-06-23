using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
namespace LooneyDog
{
    public class JoypadController : MonoBehaviour
    {

        public Vector2 NormalizedInput { get => _normalizedInput; set => _normalizedInput = value; }

        [SerializeField] private Transform _joyPadCenter, _joyPadBg;
        [SerializeField] private float _touchDistance, _joypadCenterSpeed;
        [SerializeField] private bool _istouched;
        [SerializeField] private Vector2 _normalizedInput;
        [SerializeField] private PlayerInput _playerinputActions;

        private void Update()
        {
            
            if (Input.touchCount > 0)
            {
                if (Vector2.Distance(_joyPadBg.position, Input.GetTouch(0).position) < _touchDistance)
                {
                    _istouched = true;
                }

            }
            else
            {
                _istouched = false;

            }

            if (_istouched)
            {
                Vector2 position = new Vector2(
                    Mathf.Clamp(Input.GetTouch(0).position.x,
                    _joyPadBg.position.x - _touchDistance, _joyPadBg.position.x + _touchDistance)
                    , Mathf.Clamp(Input.GetTouch(0).position.y, _joyPadBg.position.y - _touchDistance, _joyPadBg.position.y + _touchDistance));
                _joyPadCenter.position = position;


            }
            else
            {

                GoToCenter();
            }
            _normalizedInput.x = _joyPadCenter.localPosition.x / _touchDistance;
            _normalizedInput.y = _joyPadCenter.localPosition.y / _touchDistance;
        }

        private void GoToCenter()
        {
            _joyPadCenter.localPosition = Vector2.Lerp(_joyPadCenter.localPosition, Vector2.zero, _joypadCenterSpeed * Time.deltaTime);

        }

        private void AddToInputActions() {
            //_playerinputActions.actions.bindings(ho)
        }

    }
}
