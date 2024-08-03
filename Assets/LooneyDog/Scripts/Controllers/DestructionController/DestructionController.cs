using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class DestructionController : MonoBehaviour
    {
        [SerializeField] private GameObject _toDisable;
        [SerializeField] private float _explosionForce,_expolisionRadius,_disappearDelay;
        [SerializeField] private GameObject _particleSystem;

        private void OnEnable()
        {
            Rigidbody[] components;
            _toDisable.SetActive(false);
            transform.parent = null;
            Instantiate(_particleSystem, transform.position, Quaternion.identity);
            components = gameObject.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody parts in components) {
                parts.AddExplosionForce(_explosionForce, Vector3.up, _expolisionRadius);
            }
            StartCoroutine(DelayToDisableColliders(components));
        }

        IEnumerator DelayToDisableColliders(Rigidbody[] components) {
            yield return new WaitForSeconds(_disappearDelay);
            foreach (Rigidbody parts in components)
            {
                parts.GetComponent<BoxCollider>().enabled = false;
                parts.useGravity = false;
                parts.useGravity = true;
            }
            StartCoroutine(DelayToKill());
        }

        IEnumerator DelayToKill() {
            yield return new WaitForSeconds(_disappearDelay);
            gameObject.SetActive(false);//Destroy
        }
    }
}