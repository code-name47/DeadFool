using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LooneyDog
{
    public class KickCaller : MonoBehaviour
    {
        [SerializeField] private ObjectActionController _actionController;
        [SerializeField] private KickId _kickId;

        public ObjectActionController ActionController { get => _actionController; set => _actionController = value; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) {
                _actionController.MainObject = this.transform;
                _actionController.KickId = _kickId;
                _actionController.EnablePowerButtonAndSetPlyerController();
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _actionController.MainObject = null;
                
                _actionController.DisablePowerButtonAndSetPlyerController();
                
            }
        }


    }
}
