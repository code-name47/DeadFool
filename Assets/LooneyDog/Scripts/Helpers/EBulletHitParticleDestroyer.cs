using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class EBulletHitParticleDestroyer : MonoBehaviour
    {
        float timer;
        public float deathtimer = 10;
        [SerializeField] private BulletId _bulletId;


        // Use this for initialization
        void Start()
        {

        }
        private void OnEnable()
        {
            timer = 0;
        }
        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= deathtimer)
            {

                switch (_bulletId)
                {
                    case BulletId.BasicBullet:
                        GameManager.Game.Level.ObjectPooler.KillBulletHit(this.gameObject);
                        break;
                    case BulletId.RedBullet:
                        break;
                    case BulletId.BigPurpleBullet:
                        break;
                }
            }

        }
    }
}
