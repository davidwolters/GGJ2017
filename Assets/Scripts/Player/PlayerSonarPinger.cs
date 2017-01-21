﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSonarPinger : MonoBehaviour {

	public GameObject sonarPointPrefab;

	public int pointCount;

	Microphone_Input mic;

	void sonar(int count){
		var me = Camera.main.transform;

        //Debug.Log (count);

        float volume = count;


		for (var i = 0; i < count; i++) {
			var ray = new Ray(me.position, me.forward*0.1f + (Random.insideUnitSphere*0.13f));
			Sonar.ShootRay (ray, sonarPointPrefab, volume);
		}

        //var noise = new Noise(me.position, volume);
    }


	// Use this for initialization
	void Start () {
		mic = gameObject.transform.parent.GetComponentInChildren<Microphone_Input> ();
	}
	
	// Update is called once per frame
	void Update () {
		var volume = mic.GetAveragedVolume ();
		if (volume < 0.2f)
			volume = 0f;
		sonar ((int)(volume * 1000f));


		if (Input.GetMouseButton (0)) {
			sonar (500);
		}
	}
}
