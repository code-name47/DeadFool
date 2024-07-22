using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class MissileController : MonoBehaviour
    {
        /*[SerializeField] private float _speed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _target;
        [SerializeField] private Rigidbody _rb;
        private void FixedUpdate()
        {
            // Calculate the direction to the target
            Vector3 direction = (_target.position - transform.position).normalized;

            // Apply force in the direction of the target
            
            //_rb.AddTorque(direction * _speed, ForceMode.Force);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - _speed * Time.deltaTime);
            // Rotate the missile to face the target, constrained to the Y-axis
            Vector3 flatTargetDirection = new Vector3(direction.x, 0, direction.z);
            if (flatTargetDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(flatTargetDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
            }
            _rb.AddForce(transform.forward * _speed, ForceMode.VelocityChange);
        }*/

        public Transform target; // Reference to the player or target
        public float speed = 10f; // Speed of the missile
        public float rotationSpeed = 5f; // Speed at which the missile rotates

        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Calculate the direction to the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Apply force in the direction of the target
            //rb.AddForce(direction * speed, ForceMode.Force);

            // Rotate the missile to face the target, constrained to the Y-axis
            Vector3 flatTargetDirection = new Vector3(direction.x, 0, direction.z);
            if (flatTargetDirection != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(flatTargetDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
            rb.AddForce(transform.forward * speed, ForceMode.Force);
        }

    }
}
