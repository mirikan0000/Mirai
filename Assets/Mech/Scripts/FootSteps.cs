using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour {

	public AudioClip audioFootStep;

	AudioSource ASFootStep;

	void Start () {
		ASFootStep = GetComponent<AudioSource> ();
		ASFootStep.clip = audioFootStep;

	}
	
	public void FootStep() {
		ASFootStep.Play ();
	}
}
