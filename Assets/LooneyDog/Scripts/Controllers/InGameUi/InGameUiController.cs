using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace LooneyDog
{

    public class InGameUiController : MonoBehaviour
    {
        [SerializeField] private Image _healthMeter;

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }

       
    }
}
