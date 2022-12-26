using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
   public AudioSource Source1;
    private void Awake()
    {
        instance = this;
        Source1 = GetComponent<AudioSource>();
    }
    private void Start()
    {
       
    }

    public void Source(AudioClip m_clip)
    {
        Source1.PlayOneShot(m_clip);
    }

}
