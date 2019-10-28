using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefinePrefabs : MonoBehaviour {
    public GameObject BallPrefab;
    public GameObject Ball;

    void Awake()
    {
        BallPrefab = (GameObject)Resources.Load("Prefabs/Ball");
        BallPrefab.AddComponent<Rigidbody>();
        BallPrefab.GetComponent<Rigidbody>().useGravity = true;
        BallPrefab.GetComponent<Rigidbody>().mass = 8;
        BallPrefab.AddComponent<SphereCollider>();
    }
    // Use this for initialization
    void Start () {
        Ball = Instantiate(BallPrefab, new Vector3(0, 1, 0), Quaternion.identity);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
