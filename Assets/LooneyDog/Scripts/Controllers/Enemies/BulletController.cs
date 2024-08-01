using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private GameObject _hitFlash;
        [SerializeField] private BulletType _bulletType;
        
        private void OnEnable()
        {
            //transform.localPosition = transform.forward * _bulletSpeed * Time.deltaTime;
        }

        private void Update()
        {
            transform.localPosition = transform.localPosition+ transform.forward * _bulletSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_bulletType == BulletType.Enemy)
            {
                if (other.CompareTag("Player"))
                {
                    PlayerController playercontroller = other.GetComponent<PlayerController>();
                    playercontroller.GettingSmallHit();
                    Instantiate(_hitFlash, transform.position, transform.rotation);
                    if (!playercontroller.IswieldedKatana)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        transform.rotation = other.transform.rotation;
                    }
                }
            }
            else {
                if (other.CompareTag("Enemy"))
                {
                    Instantiate(_hitFlash, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
        }
    }
    public enum BulletType { 
        Enemy = 0,
        Friendly=1
    }
}
