using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {
	public GameObject effect;
	
	public void PlayNew(float time)
	{
		GameObject obj = GameObject.Instantiate(effect, transform);
		AudioSource audio = obj.GetComponent<AudioSource>();
		audio.pitch = audio.clip.length / time;
		audio.Play();
	}

	private void Start()
	{
		SFXManager.SetPlayer(this);
	}
}
