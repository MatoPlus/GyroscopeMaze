using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MazeObjects;

public class Maze : MonoBehaviour {

	private List<Feature> features;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public Vector3 Origin { get; private set; }
    public GameObject Ball { get; private set; }
    private GameObject BallPrefab { get; set; }
    private int [,,] Map { get; set; }
    private bool [,] UniqueObjects { get; set; }
    private int StartX { get; set; }
    private int StartY { get; set; }
    private int EndX { get; set; }
    private int EndY { get; set; }

    private GameObject Platform;
    private GameObject Ceiling;
    private GameObject PlatformPrefab;
    private GameObject BlockerPrefab;

	// Use this for initialization
    public void Initialize(int width, int height)
    {
        UniqueObjects = new bool[width, height];
        features = new List<Feature>();
        Width = width;
        Height = height;
        MazeGeneration generator = new MazeGeneration(width, height);
        Map = generator.Generation();
        // Origin = transform.parent.position;
        Origin = new Vector3(-width / 2.0f, 0, -height / 2.0f);
        Debug.Log(Origin.x);
        Debug.Log(Origin.y);
        Debug.Log(Origin.z);

        BallPrefab = (GameObject) Resources.Load("Prefabs/Ball");
        BlockerPrefab = (GameObject)Resources.Load("Prefabs/Blocker");
        //TilePrefab = (GameObject)Resources.Load("Prefabs/PlatformTile");
        PlatformPrefab = (GameObject)Resources.Load("Prefabs/Platform");

        Platform = Instantiate(PlatformPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Platform.transform.localScale = new Vector3(width, (float)0.1, height);
        Ceiling = Instantiate(PlatformPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        Ceiling.transform.localScale = new Vector3(width, (float)0.1, height);
        //TopBlocker = Instantiate(BlockerPrefab, new Vector3(0, (float)(1+5), 0), Quaternion.identity);
        //BottomBlocker = Instantiate(BlockerPrefab, new Vector3(0, (float)(-5), 0), Quaternion.identity);
        Destroy(Ceiling.GetComponent<MeshRenderer>());
        Platform.transform.SetParent(transform);
        Ceiling.transform.SetParent(transform);
        //TopBlocker.transform.SetParent(Platform.transform);
        //BottomBlocker.transform.SetParent(Platform.transform);


        InitializeFeatures();
        BuildFeatures();

        Ball = Instantiate(BallPrefab, new Vector3(0, (float)0.5, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		foreach (Feature feature in features)
		{
			feature.Update();
		}
	}

    void BuildFeatures()
    {
        foreach (Feature feature in features)
        {
            feature.Build();
        }
    }
    void InitializeFeatures()
	{

        // Loop and generate coordinates with appropriate distance away.
        do {
            StartX = Random.Range(0, Width);
            StartY = Random.Range(0, Height);
            EndX = Random.Range(0, Width);
            EndY = Random.Range(0, Height);
        } while (Mathf.Sqrt(Mathf.Pow((StartX-EndX),2) + Mathf.Pow((StartY-EndY),2)) < ((Width > Height) ? Width : Height ));

        features.Add(new Walls(this, Map));
        features.Add(new SpikeBalls(this, UniqueObjects));
        features.Add(new KeyPoints(this, StartX, StartY, EndX, EndY, UniqueObjects));
        //features.Add(new Obsticles);
    }
}
