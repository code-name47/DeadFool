using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{

    public class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicPlayer;
        private void OnEnable()
        {
           //GameManager.Game.Sound.PlayMusic(_musicPlayer, 0);
        }
    }
}
