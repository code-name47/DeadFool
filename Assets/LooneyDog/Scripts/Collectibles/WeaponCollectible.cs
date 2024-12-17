using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class WeaponCollectible : MonoBehaviour
    {
        [SerializeField] private GameObject _portal,_originalCrate;
        [SerializeField] private Animator _ani;
        [SerializeField] private float _destoryTimer,_breakForce,_breakRadius;
        [SerializeField] private GameObject _brokenCase;

        private void OnEnable()
        {
            _portal.gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) {
                other.gameObject.GetComponent<PlayerController>().SetPistolWeilder();
                _originalCrate.GetComponent<MeshRenderer>().enabled = false;
                _brokenCase.SetActive(true);
                Rigidbody[] brokenCase = _brokenCase.GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody piece in brokenCase) {
                    piece.AddExplosionForce(_breakForce, transform.position, _breakRadius);
                }
                StartCoroutine(WaitForDestory());
            }
        }

        IEnumerator WaitForDestory() {
            yield return new WaitForSeconds(_destoryTimer);
            MeshCollider[] meshcollider = _brokenCase.GetComponentsInChildren<MeshCollider>();
            foreach (MeshCollider piece in meshcollider)
            {
                piece.enabled = false;
            }
            yield return new WaitForSeconds(_destoryTimer);
            Destroy(gameObject);
        }
    }
}
