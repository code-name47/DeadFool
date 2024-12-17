using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class KatanaCollectible : MonoBehaviour
    {
        [SerializeField] private GameObject _portal;
        [SerializeField] private Animator _ani;
        [SerializeField] private float _destroyObjectTimer;

        private void OnEnable()
        {
            _portal.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")){
                other.gameObject.GetComponent<PlayerController>().SetKatanaWeilder();
                _ani.SetTrigger("KatanaRemoved");
                StartCoroutine(DestroyAfter());
            }
        }

        private IEnumerator DestroyAfter() {
            yield return new WaitForSeconds(_destroyObjectTimer);
            Destroy(gameObject);
        }
    }
}