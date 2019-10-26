using UnityEngine;
using UnityEngine.Events;

using System.Collections;
using UnityEngine.Serialization;

using Bazer;

namespace Project
{
  [RequireComponent(typeof(AudioSource))]
  [RequireComponent(typeof(BoxCollider))]
  public class AudioComponent : BaseComponent
  {
    public AudioSource audioSource;

    public UnityEvent playingCompleted = new UnityEvent();

    void Awake()
    {
      audioSource = GetComponent<AudioSource>();
    }

    public void StartPlaying()
    {
      audioSource.Play();

      StartCoroutine(IsPlaying());
    }

    public IEnumerator IsPlaying()
    {
      while (audioSource.isPlaying)
      {
        yield return new WaitForSeconds(0.1f);
      }

      playingCompleted.Invoke();

      yield break;
    }
  }
}

