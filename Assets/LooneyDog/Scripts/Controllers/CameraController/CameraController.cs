using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace LooneyDog {
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private ParticleSystem _speedLines;

        public ParticleSystem SpeedLines { get => _speedLines; set => _speedLines = value; }

        private void OnEnable()
        {
            GameManager.Game.Level.CameraController = this;
        }

        public void ActivateSpeedLines() {
            SpeedLines.gameObject.SetActive(true);
        }

        public void DeActivateSpeedLinnes() {
            SpeedLines.gameObject.SetActive(false);
        }
    }
}
