using UnityEngine;
using System.Collections;

/* This is the master script for all scripts involved with the 
 * map. This will control those scripts. This will be a singleton.
 * Any manipulation of the map must be made through this script
 * first.*/
public class mapMaster : MonoBehaviour {
	// Global Variables
	// = = = = = = = = = = = = = = = = = = = = >
	public static mapMaster Master;
	public GameObject quadPrefab;	// the prefab for the quadrants

	private int numQuadWide;	// how many quadrants will be across the map
	private int numQuadHigh;	// how many quadrants will be across the map
	private float QuadWidth;	// how wide each quadrant is (unity units)
	private float QuadHeight;	// how wide each quadrant is (unity units)
	private int numTilesWide;	// how many tiles wide in a quadrant
	private int numTilesHigh;	// how many tiles high in a quadrant


	// Initialization
	// = = = = = = = = = = = = = = = = = = = = >
	void Awake () {
		// Establish Singleton
		if (Master == null) {
			Master = this;
		} else {
			if (Master != this) {
				Destroy (this.gameObject); 
			}
		}

		// Setup all starter variables
		numQuadWide = 8;
		numQuadHigh = 8;
		QuadWidth = 8.0f;
		QuadHeight = 8.0f;
		numTilesWide = 16;
		numTilesHigh = 16;

		BuildMap ();
	}

	void Start () {
		// Build the Map

	}

	// Update
	// = = = = = = = = = = = = = = = = = = = = >
	void Update () {

	}

	// Functions
	// = = = = = = = = = = = = = = = = = = = = >
	private void BuildQuadrant (int _x, int _y) {
		Instantiate(quadPrefab, new Vector3 ((_x - numQuadWide / 2f) * QuadWidth, (_y - numQuadHigh / 2f) * QuadHeight, 0f) , Quaternion.identity); 
	}

	private void BuildMap () {
		// build each quadrant
		for(int x = 0; x < numQuadWide; x++) {
			for(int y = 0; y < numQuadHigh; y++) {
				BuildQuadrant (x,y);
			}
		}

	}

	public int NumberOfQuadrantsWide {
		get {return numQuadWide;}
	}

	public int NumberOfQuadrantsHigh {
		get {return numQuadHigh;}
	}

	public float QuadrantWidth {
		get {return QuadWidth;}
	}

	public float QuadrantHeight {
		get {return QuadHeight;}
	}

	public int QuadrantTileWidth{
		get {return numTilesWide;}
	}

	public int QuadrantTileHeigth {
		get {return numTilesHigh;}
	}

}
