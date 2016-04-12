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
	public GameObject quadGroundPrefab;	// the prefab for the quadrants
	public GameObject quadObjectPrefab;	// the prefab for the quadrants
	public mapData Map;

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
		numQuadWide = 3;
		numQuadHigh = 3;
		QuadWidth = 10.0f;
		QuadHeight = 10.0f;
		numTilesWide = 16;
		numTilesHigh = 16;

		// Build a new map data
		Map = new mapData(numTilesWide * numQuadWide, numTilesHigh * numQuadHigh);
		BuildDefaultShip ();

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
		GameObject curQuad = (GameObject)Instantiate(quadGroundPrefab, new Vector3 ((_x - numQuadWide / 2f) * QuadWidth, (_y - numQuadHigh / 2f) * QuadHeight, 0f) , Quaternion.identity); 
		curQuad.transform.parent = transform;
		GameObject curQuad2 = (GameObject)Instantiate(quadObjectPrefab, new Vector3 ((_x - numQuadWide / 2f) * QuadWidth, (_y - numQuadHigh / 2f) * QuadHeight, -1f) , Quaternion.identity); 
		curQuad2.transform.parent = transform;
	}

	private void BuildMap () {
		// build each quadrant
		for(int x = 0; x < numQuadWide; x++) {
			for(int y = 0; y < numQuadHigh; y++) {
				BuildQuadrant (x,y);
			}
		}

	}

	private void BuildDefaultShip () {
		// [=======][ ][ ][ ][ ][ ] 
		// [=======][X]		 	[ ][ ][ ][ ]
		// 	  [ ][ ][ ][ ][ ]	[ ]LIFSUP[ ][D][ ][ ]
		//	  [ ]REA	  [ ]	[ ]	 	 [ ]   NAV[ ]
		// 	  [ ]CTO				 	       IGA[ ]
		// 	  [ ]R--	  [ ]	[ ]	  	 [ ]   TIN[ ]
		// 	  [ ][ ][ ][ ][ ]	[ ]BEDBED[ ][D][ ][ ]
		// [=======][X]		 	[ ][ ][ ][ ]
		// [=======][ ][ ][ ][ ][ ]
		// please remove this if you got a load system and use that
		if ((numTilesHigh * numQuadHigh) > 13 && (numTilesWide * numQuadWide) > 17) {
			int anchorX = Mathf.FloorToInt ((numTilesWide * numQuadWide) / 2f - 6);
			int anchorY = Mathf.FloorToInt ((numTilesHigh * numQuadHigh) / 2f - 5);

			Map.Tiles[anchorX + 2,anchorY + 1].ObjectType = "Wall";
			Map.Tiles[anchorX + 2,anchorY + 7].ObjectType = "Wall";
			Map.Tiles[anchorX + 4,anchorY + 3].ObjectType = "Wall";
			Map.Tiles[anchorX + 4,anchorY + 5].ObjectType = "Wall";
			Map.Tiles[anchorX + 6,anchorY + 3].ObjectType = "Wall";
			Map.Tiles[anchorX + 6,anchorY + 5].ObjectType = "Wall";
			Map.Tiles[anchorX + 6,anchorY + 2].ObjectType = "Wall";
			Map.Tiles[anchorX + 6,anchorY + 6].ObjectType = "Wall";
			Map.Tiles[anchorX + 9,anchorY + 3].ObjectType = "Wall";
			Map.Tiles[anchorX + 9,anchorY + 5].ObjectType = "Wall";
			for (int b = 0; b < 3; b++) {
				Map.Tiles[anchorX + 0 ,anchorY + 3 + b].ObjectType = "Wall";
				Map.Tiles[anchorX + 12,anchorY + 3 + b].ObjectType = "Wall";
			}
			for (int b = 0; b < 5; b++) {
				Map.Tiles[anchorX + 2 + b,anchorY + 8].ObjectType = "Wall";
				Map.Tiles[anchorX + 0 + b,anchorY + 6].ObjectType = "Wall";
				Map.Tiles[anchorX + 0 + b,anchorY + 2].ObjectType = "Wall";
				Map.Tiles[anchorX + 2 + b,anchorY + 0].ObjectType = "Wall";
			}
			for (int a = 0; a < 4; a++) {
				Map.Tiles[anchorX + 6 + a,anchorY + 7].ObjectType = "Wall";
				Map.Tiles[anchorX + 9 + a,anchorY + 6].ObjectType = "Wall";
				Map.Tiles[anchorX + 9 + a,anchorY + 2].ObjectType = "Wall";
				Map.Tiles[anchorX + 6 + a,anchorY + 1].ObjectType = "Wall";
			}

			for (int a = 0; a < 2; a++) {
				for (int b = 0; b < 5; b++) {
					Map.Tiles[anchorX + a,anchorY + 2 + b].GroundType = "Red Carpet";
					Map.Tiles[anchorX + a,anchorY + 2 + b].GetObjectUVs () ;
				}
			}

			for (int a = 0; a < 5; a++) {
				for (int b = 0; b < 9; b++) {
					Map.Tiles[anchorX + 2 + a, anchorY + b].GroundType = "Red Carpet";
					Map.Tiles[anchorX + 2 + a, anchorY + b].GetObjectUVs () ;
				}
			}

			for (int a = 0; a < 3; a++) {
				for (int b = 0; b < 7; b++) {
					Map.Tiles[anchorX + 7 + a, anchorY + 1 + b].GroundType = "Red Carpet";
					Map.Tiles[anchorX + 7 + a, anchorY + 1 + b].GetObjectUVs () ;
				}
			}

			for (int a = 0; a < 3; a++) {
				for (int b = 0; b < 5; b++) {
					Map.Tiles[anchorX + 10 + a, anchorY + 2 + b].GroundType = "Red Carpet";
					Map.Tiles[anchorX + 10 + a, anchorY + 2 + b].GetObjectUVs () ;
				}
			}
		} 
	}

	private Vector2 GetQuadrant (int x, int y){
		int a = Mathf.FloorToInt (x / numTilesWide); 
		int b = Mathf.FloorToInt (y / numTilesHigh); 
		return new Vector2 (a, b);
	}

	public bool CheckForBounds (int x, int y) {
		if (x >= 3 && x <= (numQuadWide * numTilesWide - 4) && y >= 3 && y <= (numQuadHigh * numTilesHigh - 4)) {
			return true;
		} else {
			return false;
		}
	}

	public void PlaceWallOnTile (int x, int y) {
		if (Map.Tiles[x,y].ObjectType == "None") {
			Map.Tiles [x, y].ObjectType = "Wall";
			Map.Tiles [x, y].GetObjectUVs (); 
			UpdateTileObject (x, y);
			UpdateNearbyWalls (x, y);
		}
	}

	public void UpdateNearbyWalls (int x, int y ) {
		if (CheckForBounds (x, y)){
			Map.Tiles [x + 1, y].GetObjectUVs (); 
			Map.Tiles [x - 1, y].GetObjectUVs (); 
			Map.Tiles [x, y + 1].GetObjectUVs (); 
			Map.Tiles [x, y - 1].GetObjectUVs (); 
			UpdateTileObject (x + 1, y);
			UpdateTileObject (x - 1, y);
			UpdateTileObject (x, y + 1);
			UpdateTileObject (x, y - 1);
		}
	}

	public void UpdateTileObject (int x, int y) {
		int a, b;
		Vector2 fromQuad = GetQuadrant (x, y);
		a = (int)fromQuad.x; b = (int)fromQuad.y;
		GameObject.Find ("mapQuadObj(" + a + "," + b + ")").GetComponent<mapObjects> ().UpdateTileTexture(x, y);	
	}

	public Vector2 GetWallUVs (int x, int y) {
		if (CheckForBounds (x, y)) {
			bool ME, N, E, S, W;
			ME = (Map.Tiles [x, y].ObjectType == "Wall");
			if (ME) {
				int ox = 6; int oy = 0;
				N = (Map.Tiles [x + 0, y + 1].ObjectType == "Wall");
				E = (Map.Tiles [x + 1, y + 0].ObjectType == "Wall");
				S = (Map.Tiles [x + 0, y - 1].ObjectType == "Wall");
				W = (Map.Tiles [x - 1, y + 0].ObjectType == "Wall");

				if ( N &&  E && !S && !W ) {ox = 0; oy = 0;}
				if ( N &&  E && !S &&  W ) {ox = 1; oy = 0;}
				if ( N && !E && !S &&  W ) {ox = 2; oy = 0;}
				if ( N &&  E &&  S && !W ) {ox = 0; oy = 1;}
				if ( N &&  E &&  S &&  W ) {ox = 1; oy = 1;}
				if ( N && !E &&  S &&  W ) {ox = 2; oy = 1;}
				if (!N &&  E &&  S && !W ) {ox = 0; oy = 2;}
				if (!N &&  E &&  S &&  W ) {ox = 1; oy = 2;}
				if (!N && !E &&  S &&  W ) {ox = 2; oy = 2;}

				if ( N && !E && !S && !W ) {ox = 3; oy = 0;}
				if ( N && !E &&  S && !W ) {ox = 3; oy = 1;}
				if (!N && !E &&  S && !W ) {ox = 3; oy = 2;}

				if (!N &&  E && !S && !W ) {ox = 4; oy = 2;}
				if (!N &&  E && !S &&  W ) {ox = 5; oy = 2;}
				if (!N && !E && !S &&  W ) {ox = 6; oy = 2;}

				if (!N && !E && !S && !W ) {ox = 5; oy = 0;}

				return new Vector2 (ox, oy);
			} else {
				return new Vector2 (6, 0);
			}
		}
		return new Vector2 (6, 0);
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
