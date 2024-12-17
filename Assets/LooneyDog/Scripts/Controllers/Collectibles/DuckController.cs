using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class DuckController : MonoBehaviour
    {
        [SerializeField] private float _explosionForce, _explosionRadius, _disappearDelay,_offset;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] float _initialImpulseForce = 5f,_upwardModifier=20f; // A small initial force to move the duck outward
        [SerializeField] int _aliveDuration;
        // SummonDuck method to spawn the rubber duck at the hit position and scatter outward

        private void OnEnable()
        {
            StartCoroutine(DestroyDelay());
        }

        public void SummonDuck(Vector3 hitPosition)
        {
            // Set the position of the rubber duck to the exact hit point (center)
            transform.position = hitPosition;

            // Apply an initial random impulse to avoid ducks piling up
            Vector3 randomDirection = Random.insideUnitSphere.normalized;  // Random direction from the center
            _rb.AddForce(randomDirection * _initialImpulseForce, ForceMode.Impulse);

            // Apply explosion force to scatter the ducks outward
            _rb.AddExplosionForce(_explosionForce, hitPosition, _explosionRadius, _upwardModifier);
        }

        public void DestroyDuck() {
            Destroy(gameObject);
        }
        IEnumerator DestroyDelay() {
            yield return new WaitForSeconds(_aliveDuration);
            DestroyDuck();
        }
    }
}
