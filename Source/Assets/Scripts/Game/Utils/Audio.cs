using UnityEngine;

using System;
using System.Collections;

namespace Project
{
  public class Audio
  {
    public float soundVolume;
    public float musicVolume;

    public static GameObject PlaySound(GameObject targetObject, string file, bool loop = false, Action callback = null)
    {
      GameObject soundObject = new GameObject("Sound");
      soundObject.transform.SetParent(targetObject.transform);

      AudioSource audioSource = soundObject.AddComponent<AudioSource>();
      audioSource.clip = LoadSource(file);

      AudioComponent audioComponent = soundObject.AddComponent<AudioComponent>();

      if (callback != null)
      {
        audioComponent.playingCompleted.AddListener(() =>
        {
          callback();
        });
      }

      if (!loop)
      {
        audioComponent.playingCompleted.AddListener(() =>
        {
          MonoBehaviour.Destroy(soundObject);
        });
      }

      audioComponent.StartPlaying();

      return soundObject;
    }

    private static AudioClip LoadSource(string file)
    {
      AudioClip audioClip = Resources.Load<AudioClip>(file);

      if (audioClip == null)
      {
        Funcs.Throw("Sound not found");
      }

      return audioClip;
    }

    public static void PlayMusic()
    {

    }
  }
}

