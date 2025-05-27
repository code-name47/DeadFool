using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace LooneyDog
{
    public class ObjectPoolController : MonoBehaviour
    {
        [Header("ObjectPool")]
        private ObjectPool<GameObject> _bulletBasicPool;
        private ObjectPool<GameObject> _basicMuzzleFlashPool;
        private ObjectPool<GameObject> _basicBulletHitPool;
        private ObjectPool<GameObject> _missilePool;
        private ObjectPool<GameObject> _missileHitPool;


        [Header("Bool For Use Of Pools")]
        [SerializeField] private bool _useBulletBasicPool;
        [SerializeField] private bool _useBasicMuzzleFlashPool;
        [SerializeField] private bool _useBasicBulletHitPool;
        [SerializeField] private bool _useMissilePool;
        [SerializeField] private bool _useMissileHitPool;


        [Header("PreFabs")]
        [SerializeField] private BulletController[] _bulletBasicPrefab;
        [SerializeField] private GameObject[] _basicMuzzleFlashPrefab;
        [SerializeField] private GameObject[] _basicBulletHitPrefab;
        [SerializeField] private MissileController[] _missilePrefab;
        [SerializeField] private GameObject _missileHitPrefab;

        [Header("SpwanCollection")]
        [SerializeField] private List<GameObject> _spwanedBullets;
        [SerializeField] private List<GameObject> _spwanedBasicMuzzleFlash;
        [SerializeField] private List<GameObject> _spwanedBasicBulletHits;
        [SerializeField] private List<GameObject> _spwanedMissiles;
        [SerializeField] private List<GameObject> _spwanedMissileHits;
 
        private void OnEnable()
        {
            GameManager.Game.Level.ObjectPooler = this;
        }

        private void Start()
        {

            //-----------------------------------   BulletsBasic -------------------------------------
            _bulletBasicPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(_bulletBasicPrefab[(int)BulletId.BasicBullet].gameObject);
            }, bulletBasic =>
            {
                bulletBasic.gameObject.transform.SetParent(null);
                bulletBasic.gameObject.GetComponent<BulletController>().BulletType = BulletType.Enemy;
                bulletBasic.gameObject.SetActive(true);
            }, bulletBasic =>
            {
                bulletBasic.gameObject.SetActive(false);
                bulletBasic.gameObject.transform.SetParent(this.transform);
            }, bulletBasic =>
            {
                Destroy(bulletBasic.gameObject);
            },true,100,1000);
            //-------------------------------------Basic Muzzle Flash -----------------------
            _basicMuzzleFlashPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(_basicMuzzleFlashPrefab[(int)BulletId.BasicBullet].gameObject);
            }, muzzleFlashBasic =>
            {
                muzzleFlashBasic.gameObject.transform.SetParent(null);
                muzzleFlashBasic.gameObject.SetActive(true);
            }, muzzleFlashBasic =>
            {
                muzzleFlashBasic.gameObject.SetActive(false);
                muzzleFlashBasic.gameObject.transform.SetParent(this.transform);
            }, muzzleFlashBasic =>
            {
                Destroy(muzzleFlashBasic.gameObject);
            }, false, 100, 1000);
            //-------------------------------------- Basic Bullet Hit ------------------------
            _basicBulletHitPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(_basicBulletHitPrefab[(int)BulletId.BasicBullet].gameObject);
            }, bulletHit =>
            {
                bulletHit.gameObject.transform.SetParent(null);
                bulletHit.gameObject.SetActive(true);
            }, bulletHit =>
            {
                bulletHit.gameObject.SetActive(false);
                bulletHit.gameObject.transform.SetParent(this.transform);
            }, bulletHit =>
            {
                Destroy(bulletHit.gameObject);
            }, false, 100, 1000);
            //-------------------------------------- Missile ------------------------
            _missilePool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(_missilePrefab[(int)MissileType.HomingMissileSlow].gameObject);
            }, missile =>
            {
                missile.gameObject.transform.SetParent(null);
                missile.gameObject.SetActive(true);
            }, missile =>
            {
                missile.gameObject.SetActive(false);
                missile.gameObject.transform.SetParent(this.transform);
            }, missile =>
            {
                Destroy(missile.gameObject);
            }, false, 100, 1000);

            //-------------------------------------   MissileExpolision ---------------------

            _missileHitPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(_missileHitPrefab);
            }, missileHit =>
            {
                missileHit.gameObject.transform.SetParent(null);
                missileHit.gameObject.SetActive(true);
            }, missileHit =>
            {
                missileHit.gameObject.SetActive(false);
                missileHit.gameObject.transform.SetParent(this.transform);
            }, missileHit =>
            {
                Destroy(missileHit.gameObject);
            }, false, 100, 1000);

        }




        //-----------------------------------  Spwaning Section -----------------------------------------
        public void SpwanBasicBullet(Transform spwanPoint,float damage,BulletType bulletType) {

            _bulletBasicPrefab[(int)BulletId.BasicBullet].BulletDamage = damage;
            _bulletBasicPrefab[(int)BulletId.BasicBullet].BulletType = bulletType;
            _spwanedBullets.Add(_useBulletBasicPool ? _bulletBasicPool.Get() : Instantiate(_bulletBasicPrefab[(int)BulletId.BasicBullet].gameObject));
            _spwanedBullets[_spwanedBullets.Count - 1].transform.position = spwanPoint.transform.position;
            _spwanedBullets[_spwanedBullets.Count - 1].transform.rotation = Quaternion.LookRotation(spwanPoint.transform.forward);
        }

        public void SpwanMuzzleFlash(Transform spwanPoint)
        {
            _spwanedBasicMuzzleFlash.Add(_useBasicMuzzleFlashPool ? _basicMuzzleFlashPool.Get() : Instantiate(_basicMuzzleFlashPrefab[(int)BulletId.BasicBullet].gameObject));
            _spwanedBasicMuzzleFlash[_spwanedBasicMuzzleFlash.Count - 1].transform.position = spwanPoint.transform.position;
            _spwanedBasicMuzzleFlash[_spwanedBasicMuzzleFlash.Count - 1].transform.rotation = Quaternion.LookRotation(spwanPoint.transform.forward);
        }

        public void SpwanBulletHit(Transform spwanPoint)
        {
            _spwanedBasicBulletHits.Add(_useBasicBulletHitPool? _basicBulletHitPool.Get() : Instantiate(_basicBulletHitPrefab[(int)BulletId.BasicBullet].gameObject));
            _spwanedBasicBulletHits[_spwanedBasicBulletHits.Count - 1].transform.position = spwanPoint.transform.position;
            _spwanedBasicBulletHits[_spwanedBasicBulletHits.Count - 1].transform.rotation = spwanPoint.transform.rotation;
        }

        public GameObject SpwanMissile(Transform spawnPoint,MissileType missileType) {
            _spwanedMissiles.Add(_useMissilePool ? _missilePool.Get() : Instantiate(_missilePrefab[(int)missileType].gameObject));
            _spwanedMissiles[_spwanedMissiles.Count - 1].transform.position = spawnPoint.transform.position;
            _spwanedMissiles[_spwanedMissiles.Count - 1].transform.rotation = spawnPoint.transform.rotation;
            MissileController spwanedmissile = _spwanedMissiles[_spwanedMissiles.Count - 1].GetComponent<MissileController>();
            spwanedmissile.Speed = _missilePrefab[(int)missileType].Speed;
            spwanedmissile.RotationSpeed = _missilePrefab[(int)missileType].RotationSpeed;
            spwanedmissile.Damage = _missilePrefab[(int)missileType].Damage;
            return spwanedmissile.gameObject;
        }

        public void SpwanMissileHit(Transform spwanPoint) {
            _spwanedMissileHits.Add(_useMissileHitPool ? _missileHitPool.Get() : Instantiate(_missileHitPrefab));
            _spwanedMissileHits[_spwanedMissileHits.Count - 1].transform.position = spwanPoint.transform.position;
            _spwanedMissileHits[_spwanedMissileHits.Count - 1].transform.rotation = Quaternion.identity;
        }
        //--------------------------------   End Of Spwaning Section --------------------------------
        public void KillBullet(GameObject bulletBasic)
        {
            if (bulletBasic == null) return;  // Null safety
            if (!bulletBasic.activeInHierarchy) return;  // Already deactivated, don't try releasing again
            if (_useBulletBasicPool)
            {
               
                _bulletBasicPool.Release(bulletBasic);
            }
            else
            {
                Destroy(bulletBasic);
            }
            if (_spwanedBullets.Contains(bulletBasic))
            {
                _spwanedBullets.Remove(bulletBasic);
            }
        }

        public void KillMuzzleFlash(GameObject muzzleFlash)
        {
            if (_useBasicMuzzleFlashPool)
            {
                _basicMuzzleFlashPool.Release(muzzleFlash);
            }
            else
            {
                Destroy(muzzleFlash);
            }
            if (_spwanedBasicMuzzleFlash.Contains(muzzleFlash))
            {
                _spwanedBasicMuzzleFlash.Remove(muzzleFlash);
            } 
        }

        public void KillBulletHit(GameObject bulletHit)
        {
            if (_useBasicBulletHitPool)
            {
               _basicBulletHitPool.Release(bulletHit);
            }
            else
            {
                Destroy(bulletHit);
            }
            if (_spwanedBasicBulletHits.Contains(bulletHit))
            {
                _spwanedBasicBulletHits.Remove(bulletHit);
            }
        }

        public void KillMissile(GameObject missile)
        {
            if (_useMissilePool)
            {
                _missilePool.Release(missile);
            }
            else
            {
                Destroy(missile);
            }
            if (_spwanedMissiles.Contains(missile))
            {
                _spwanedMissiles.Remove(missile);
            }
        }

        public void KillMissileHit(GameObject missileHit)
        {
            if (_useMissileHitPool)
            {
                _missileHitPool.Release(missileHit);
            }
            else
            {
                Destroy(missileHit);
            }
            if (_spwanedMissileHits.Contains(missileHit))
            {
                _spwanedMissileHits.Remove(missileHit);
            }
        }

    }
}
