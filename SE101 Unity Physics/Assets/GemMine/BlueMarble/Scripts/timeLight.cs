using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class timeLight : MonoBehaviour {

	float angle;
	float declination;

	public float updateInterval = 1f;
	float updateTimer = 0f;

	float secondsPerDay = 86400;
	float secondsElapsed = 0;

	public GameObject earthGO;

	// Use this for initialization
	void Start () {
		// declination does not change over the day
	}
		


	// Update is called once per frame
	void Update () {
		// set forth timer
		updateTimer += Time.deltaTime;
		if (updateTimer >= updateInterval) {
			// decrement timer
			updateTimer -= updateInterval;
			// get actual time
			DateTime now = DateTime.Now;
			// calculate elapsed seconds since midnight
			secondsElapsed = now.Hour * 60f * 60f + now.Minute * 60f + now.Second;
			// calculate the current angle
			angle = secondsElapsed / secondsPerDay * 360f+90;
			declination = 23.45f * (Mathf.PI / 180f) * Mathf.Sin(2f * Mathf.PI * ((284f + now.DayOfYear)/365.25f)); 
			declination = declination * 180f / Mathf.PI;
			// set the light's position / rotation
		}

		transform.rotation = earthGO.transform.rotation * Quaternion.Euler (declination, angle, 0);
	}
}
