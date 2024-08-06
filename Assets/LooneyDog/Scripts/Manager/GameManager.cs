using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LooneyDog
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Game;

        public DataManager Data;
        public LevelManager Level;
        public ScreenManager Screen;
        //public AnimationManager Anime;
        public SkinManager Skin;
        //public PoolManager Pool;
        
        public SoundManager Sound;
        /*public AdManager Admob;*/
        /* 
         public SoundManager Sound;*/


        private void Awake()
        {
            if (Game == null)
            {
                DontDestroyOnLoad(gameObject);
                Game = this;
                Initialize();
            }
            else if (Game != this)
            {
                Destroy(gameObject);
            }
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            /*if (SystemInfo.deviceType != DeviceType.Desktop)
            {
                Application.targetFrameRate = 45;
            }*/
        }

        private void Initialize()
        {
            if (Data == null) { Data = gameObject.GetComponent<DataManager>(); }
            if (Level == null) { Level = gameObject.GetComponent<LevelManager>(); }
            /*if (Anime == null) { Anime = gameObject.GetComponent<AnimationManager>(); }
            if (Pool == null) { Pool = gameObject.GetComponent<PoolManager>(); }

            if (Sound == null) { Sound = gameObject.GetComponent<SoundManager>(); }
            *//*if (Admob == null) { Admob = gameObject.GetComponent<AdManager>(); }*//**/
            if (Skin == null) { Skin = gameObject.GetComponent<SkinManager>(); }
            if (Screen == null) { Screen = gameObject.GetComponent<ScreenManager>(); }


        }
    }
}
