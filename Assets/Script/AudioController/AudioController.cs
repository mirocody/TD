﻿using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
    public AudioClip clip1;
    public AudioClip clip2;
	// Use this for initialization
	void Start () {
        this.transform.GetComponent<AudioSource>().clip = clip1;
        this.transform.GetComponent<AudioSource>().Play();

    }
    public void changeToBGM2()
    {
        this.transform.GetComponent<AudioSource>().clip = clip2;
        this.transform.GetComponent<AudioSource>().Play();
    }
}
