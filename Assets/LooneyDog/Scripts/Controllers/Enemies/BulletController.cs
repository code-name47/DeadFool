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
        [SerializeField] private float _bulletDamage;

        public float BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }
        public BulletType BulletType { get => _bulletType; set => _bulletType = value; }

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
            if (BulletType == BulletType.Enemy)
            {
                if (other.CompareTag("Player"))
                {
                    PlayerController playercontroller = other.GetComponent<PlayerController>();
                    playercontroller.GettingSmallHit();
                    Instantiate(_hitFlash, transform.position, transform.rotation);
                    if (!playercontroller.IswieldedKatana)
                    {
                        //Destroy(gameObject);
                        Debug.Log("bullet obstruction tagged as player");
                        GameManager.Game.Level.ObjectPooler.KillBullet(gameObject);
                        playercontroller.ReduceHeath(_bulletDamage);
                    }
                    else
                    {
                        transform.rotation = other.transform.rotation;
                        BulletType = BulletType.Friendly;
                    }
                }
            }
            else {
                if (other.CompareTag("Enemy"))
                {
                    Instantiate(_hitFlash, transform.position, transform.rotation);
                    if (other.GetComponent<EnemyBodyController>() != null)
                    {
                        other.GetComponent<EnemyBodyController>().GettingHit(BulletDamage);
                    }
                    else
                    {
                        Debug.Log("No Emeny Body Controller found on Enemy");
                    }
                    //Destroy(gameObject);
                    Debug.Log("bullet obstruction tagged as enemy");
                    GameManager.Game.Level.ObjectPooler.KillBullet(gameObject);
                }

            }

            if (other.CompareTag("Obstacles")) {
                GameManager.Game.Level.ObjectPooler.SpwanBulletHit(transform);
                //Instantiate(_hitFlash, transform.position, transform.rotation);
                //Destroy(gameObject);
                Debug.Log("bullet obstruction tagged as obtsacles");
                GameManager.Game.Level.ObjectPooler.KillBullet(gameObject);
            }
        }
    }
    public enum BulletType { 
        Enemy = 0,
        Friendly=1
    }
}
