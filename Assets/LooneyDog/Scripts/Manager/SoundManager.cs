using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LooneyDog
{


    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _musicClips;
        [SerializeField] private AudioClip[] _uiClips;
        [SerializeField] private AudioClip[] _sfxClips;
        [SerializeField] private MusicController _musicPlayer;
        [SerializeField] private AudioSource _uiSoundPlayer;
        



        public AudioClip[] MusicClips { get => _musicClips; set => _musicClips = value; }
        public AudioClip[] UiClips { get => _uiClips; set => _uiClips = value; }

        public void PlayMusic(MusicClipId id)    
        {
            AudioSource source = _musicPlayer.GetComponent<AudioSource>();
            source.clip = _musicClips[(int)id];
            source.Play();
            

        }
        public void PlayUisound(UiClipId id)
        {
            AudioSource source = _uiSoundPlayer.GetComponent<AudioSource>();
            source.PlayOneShot(_uiClips[(int)id]);


        }
        public void PlaySfxsound(SfxClipId id, AudioSource source)
        {
            source.PlayOneShot(_sfxClips[(int)id]);


        }

    }
    public enum UiClipId
    {
        Click=0,
        Swish=1 
    }
    public enum MusicClipId
    {
        Action = 0,
        ingamephonk = 1
    }
    public enum SfxClipId
    {
       Footsteps= 0,
       Bodyfall=1,
       Pistolload=2,
       Pistolfire=3,
       Katanadraw=4,
       Katanaslash=5,
       deadfoolhurt1=6,
       deadfoolhurt2=7,
       missilesound=8,
       missileblast=9,
       turretfiring=10,
       turretblast=11,
       nextwavesound=12




    }


}