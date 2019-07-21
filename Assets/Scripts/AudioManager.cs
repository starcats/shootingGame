using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	AudioSource audioSource;
	// Use this for initialization
	void Start () {
		
		audioSource = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BulletExplosionSE () {
		AudioClip se = Resources.Load<AudioClip> ("Sounds/BulletExplosionSE");
		audioSource.PlayOneShot (se);
	}
}
