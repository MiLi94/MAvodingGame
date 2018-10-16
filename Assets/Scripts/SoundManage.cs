using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManage {
    public static AudioSource _as_music = null;
    public const string MusicPath = "Sounds/";
    public static void PlayMusic(string name, string cameraName)
    {
        if (_as_music == null)
        {
            _as_music = GameObject.Find(cameraName).GetComponent<AudioSource>();
        }

        AudioClip ac = Resources.Load<AudioClip>(MusicPath + name);

            _as_music.clip = ac;
            _as_music.loop = true;
            _as_music.Play();

    }

    public static List<AudioSource> ASList = new List<AudioSource>();
    public static void PlaySound(string name, string cameraName) {
        AudioSource _as = null; 
        //for (int i = 0; i < ASList.Count; i++)
        //{
        //    if(ASList[i].isPlaying == false)
        //    {
        //        _as = ASList[i];
        //        break;
        //    }
        //}

        if(_as == null)
        {
            //_as = Camera.main.gameObject.AddComponent<AudioSource>();
            _as = GameObject.Find(cameraName).GetComponent<AudioSource>();
            ASList.Add(_as);
        }

   
            _as.clip = Resources.Load<AudioClip>(MusicPath + name);
            _as.loop = false;
            _as.Play();
     
       

    }

    public static void mute(string cameraName) {
     AudioSource[] audioSocures= GameObject.Find(cameraName).GetComponents<AudioSource>();
        for (int i = 0; i < audioSocures.Length; i++) {
            audioSocures[i].mute = true;
        }
    }
    public static void recover(string cameraName)
    {
        AudioSource[] audioSocures = GameObject.Find(cameraName).GetComponents<AudioSource>();
        for (int i = 0; i < audioSocures.Length; i++)
        {
            audioSocures[i].mute = false;
        }
    }


}
