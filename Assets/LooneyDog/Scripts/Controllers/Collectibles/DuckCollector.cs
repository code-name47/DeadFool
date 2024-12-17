using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class DuckCollector : MonoBehaviour
    {
        [SerializeField] private float _pullPower=5f,_pullSize=5f,_minimumTimeDuration,_collectionDistance;
        [SerializeField] private SphereCollider _pullAreaSphere;
        [SerializeField] bool _startAttraction;

        private void OnEnable()
        {
            _pullAreaSphere.radius = _pullSize;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Duck")) {
                PullDuck(other.gameObject);
                if (Vector3.Distance(this.transform.position, other.transform.position) < _collectionDistance) {
                    CollectDuck(other.gameObject);
                }
            }
        }

        private void PullDuck(GameObject Duck) {
            float DuckCollectPower = ((_pullPower / 100) / _minimumTimeDuration) / Vector3.Distance(Duck.transform.position, this.transform.position);
            Duck.transform.position = Vector3.Lerp(Duck.transform.position, this.transform.position, DuckCollectPower * Time.deltaTime);
        }

        private void CollectDuck(GameObject Duck) {
            Destroy(Duck);
            GameManager.Game.Screen.GameScreen.AddDuckPoint();
        }

    }
}
