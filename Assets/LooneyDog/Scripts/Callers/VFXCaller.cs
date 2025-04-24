using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class VFXCaller : MonoBehaviour
    {
        [SerializeField] ParticleSystem _particleSystem;
        [SerializeField] Transform _instantiatorPosition;

        public void CallVfx() {
            _particleSystem.gameObject.SetActive(true);
        }

        public void InstantiateVfx() {
            Instantiate(_particleSystem,_instantiatorPosition.position,Quaternion.identity);
        }


    }
}
