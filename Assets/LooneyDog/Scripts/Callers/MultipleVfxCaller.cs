using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{
    public class MultipleVfxCaller : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _vfx1, _vfx2;
        [SerializeField] private Transform _vfx1Position, _vfx2Position;
        public void Vfx1Caller() {
            Instantiate(_vfx1, _vfx1Position.position, _vfx1Position.rotation);
        }

        public void Vfx2Caller() {
            Instantiate(_vfx2, _vfx2Position.position, _vfx2Position.rotation);
        }
    }
}