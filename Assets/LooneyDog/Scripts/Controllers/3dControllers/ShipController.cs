using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LooneyDog
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField] private Vector2 _shipDirection;
        [SerializeField] private float _shipTurnSpeed;
        [SerializeField] private float _shipForwardSpeed;
        [SerializeField] private Animator _shipAnimator;


        private void FixedUpdate()
        {
            //_shipDirection = GameManager.Game.Screen.GameScreen.JoypadController.NormalizedInput;
            //_shipDirection = Input.
            MoveShip();
        }

        public void OnMove(InputValue input)
        {
            _shipDirection = input.Get<Vector2>();
            //Debug.Log("The vectr 2 of ship is " + _shipDirection);
        }

        private void RotateShip(Vector2 Direction) {
            // Get horizontal and vertical input from the input vector
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate rotation angles based on input
            float horizontalRotation = horizontalInput * _shipTurnSpeed * Time.deltaTime;
            float verticalRotation = verticalInput * _shipTurnSpeed * Time.deltaTime;

            // Apply rotation around the ship's local axes
            transform.Rotate(Vector3.up, horizontalRotation, Space.Self); // Rotate around the ship's up axis (yaw)
            transform.Rotate(Vector3.left, verticalRotation, Space.Self); // Rotate around the ship's left axis (pitch)
        }

        private void MoveShip() {
            //_shipDirection = Input.get
            //_shipDirection = inputActions.Player.Move.ReadValue<Vector2>();
            //if (_shipDirection != Vector2.zero) {
                transform.localPosition = new Vector3(transform.localPosition.x + ((_shipDirection.x * _shipForwardSpeed) * Time.deltaTime), transform.position.y, transform.localPosition.z + ((_shipDirection.y * _shipForwardSpeed) * Time.deltaTime));
            //}
        }
    }
}
