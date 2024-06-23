using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace LooneyDog
{

    public class ShipSelectCameraController : MonoBehaviour
    {
        [Header("Camera Properties")]
        [SerializeField] private float _cameraMovermentSpeed;
        [SerializeField] private float _cameraZoomLength;
        [SerializeField] private Transform _objectToRotateAround;
        [SerializeField] private float _objectRotationAngle;
        [SerializeField] private Vector2 _input;
        
        //[SerializeField] DefaultInputActions inputActions;

        private void Awake()
        {
            /*inputActions = new DefaultInputActions();
            inputActions.Enable();*/
            //inputActions = GameManager.Game.Screen.GameScreen._inputActions;
        }
        private void OnEnable()
        {
            
        }
        public void OnMove(InputValue input)
        {
            _input = input.Get<Vector2>();
            //Debug.Log("The vectr 2 of ship is " + _shipDirection);
        }
        private void FixedUpdate()
        {
            //transform.RotateAround(_objectToRotateAround.transform.position, Vector3.up, _objectRotationAngle* inputActions.Player.Move.ReadValue<Vector2>().x);
            transform.RotateAround(_objectToRotateAround.transform.position, Vector3.up, _objectRotationAngle * _input.x);

        }
    }
}
