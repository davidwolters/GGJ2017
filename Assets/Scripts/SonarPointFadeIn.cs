﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPointFadeIn : MonoBehaviour {

	public float fadeInTimeout;
	public float fadeInTimerSpeed;
	public float fadeOutSpeed;
	public float fadeInSpeed;

	float disappearChance;

	public bool fadeIn = false;
	public bool started = false;
	public float fadeInTimer;

	Vector3 rotationDirection;
	// Use this for initialization
	void Start () {
		disappearChance = Random.Range (0.02f, 0.5f);
		rotationDirection = Random.insideUnitSphere * 100f;
		fadeOutSpeed = fadeOutSpeed * (Random.value + 0.001f) + 0.1f;
		transform.localScale = new Vector3 (0, 0, 0);
		fadeInTimer = fadeInTimeout;
	}
	
	// Update is called once per frame
	void Update () 
	{		
	
		if(Random.value < 0.666f){
			return;
		}

		float scale = transform.localScale.z;

		if (!fadeIn && started)
		{
			if (scale > 0) 
			{
				scale = Mathf.Lerp (scale, 0, fadeOutSpeed * Time.deltaTime);
				if (scale < 0.01f)
				{
					scale = 0;	
				}
			} else
			{
				Sonar.RemovePoint (this.transform);
				//Destroy (this.gameObject);
			}
		} else if (started)
		{
			if (scale < 1)
			{
				scale = Mathf.Lerp (scale, 1, fadeInSpeed * Time.deltaTime);
				if (1 - scale < 0.01f)
				{
					scale = 1;
				}
			} else
			{
				fadeIn = false;
			}
		}
		transform.localScale = new Vector3 (scale, scale, scale);
		transform.Rotate (rotationDirection * Time.deltaTime);


		fadeInTimer -= fadeInTimerSpeed*Time.deltaTime;
		if (fadeInTimer <= 0f && !started) {
			this.GetComponent<MeshRenderer> ().enabled = true;
			fadeIn = true;
			started = true;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			fadeOutSpeed = 0.4f;

		}
	}
}
