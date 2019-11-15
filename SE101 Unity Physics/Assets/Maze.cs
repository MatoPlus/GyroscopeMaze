using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeObjects;

public class Maze : MonoBehaviour {

	List<Feature> features;

	// Use this for initialization
	void Start () {
		InitializeFeatures();
		foreach (Feature feature in collection)
		{
			feature.Build();
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Feature feature in features)
		{
			feature.Update();
		}
	}

    void InitializeFeatures()
	{
		features.Add(new Walls());
		//features.Add(new Obsticles);
	}
}
