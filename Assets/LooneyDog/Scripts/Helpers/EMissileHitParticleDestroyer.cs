using UnityEngine;
namespace LooneyDog
{

    public class EMissileHitParticleDestroyer : MonoBehaviour
    {
        float timer;
        public float deathtimer = 10;


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
                GameManager.Game.Level.ObjectPooler.KillMissileHit(this.gameObject);
            }

        }
    }
}
