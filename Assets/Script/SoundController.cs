using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    private static AudioSource audioSource;

    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void ChangeBGM(AudioClip clip, bool canLoop)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = canLoop;
        audioSource.Play();
    }
}
