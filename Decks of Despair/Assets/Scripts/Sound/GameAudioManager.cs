using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource ghostAudio;
    [SerializeField] private AudioSource wolfAudio;
    [SerializeField] private AudioSource slimeAudio;
    public void GhostPlay()
    {
        ghostAudio.Play();
    }

    public void WolfPlay()
    {
        wolfAudio.Play();
    }

    public void SlimePlay()
    {
         slimeAudio.Play();
    }
}
