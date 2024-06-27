using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace LooneyDog
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector2 _playerDirection;
        [SerializeField] private float _playerSpeed,_playerRotationSpeed;
        [SerializeField] private Animator _playerAnimator;


        private void FixedUpdate()
        {
            MovePlayer();
        }

        public void OnMove(InputValue input)
        {
            _playerDirection = input.Get<Vector2>();
            _playerAnimator.SetFloat("Moving", _playerDirection.magnitude);
        }



        private void MovePlayer()
        {

            /*transform.localPosition = new Vector3(transform.localPosition.x + ((_playerDirection.x * _playerSpeed) * Time.deltaTime), transform.position.y, transform.localPosition.z + ((_playerDirection.y * _playerSpeed) * Time.deltaTime));
            Quaternion LookRotation = Quaternion.LookRotation(transform.forward*_playerDirection, Vector3.up);
            transform.rotation = LookRotation;*/
            if (_playerDirection != Vector2.zero)
            {
                Vector3 moveDirection = new Vector3(_playerDirection.x, 0, _playerDirection.y).normalized;
                Vector3 newPosition = transform.localPosition + moveDirection * (_playerSpeed*_playerDirection.magnitude) * Time.deltaTime;
                transform.localPosition = new Vector3(newPosition.x, transform.localPosition.y, newPosition.z);

                // Rotate to look where it's moving
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _playerRotationSpeed); // Smooth rotation
            }

        }
    }

}
