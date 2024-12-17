using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class MissileController : MonoBehaviour
    {

        [SerializeField] private GameObject _hitParticle;
        [SerializeField] private Transform target; // Reference to the player or target
        [SerializeField] private float speed = 10f; // Speed of the missile
        [SerializeField] private float rotationSpeed = 5f; // Speed at which the missile rotates

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
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")){
                if (other.GetComponent<PlayerController>() != null)
                {
                    other.GetComponent<PlayerController>().GettingHit();
                }
                else
                {
                    //Do Nothing
                    Debug.Log("Error : Player Controller Not Found When Missile Hit");
                }
                
            }

            if (other.CompareTag("Enemy"))
            {
                DestroyGameObject();
               /* Instantiate(_hitParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);*/
            }
        }

        public void DestroyGameObject() {
            Instantiate(_hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
