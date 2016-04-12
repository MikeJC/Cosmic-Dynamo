using UnityEngine;
using System.Collections;

public class tileData {
	// Global Variables
	// = = = = = = = = = = = = = = = = = = = = >
	private int x, y;			// coordinates of the tile
	private int uvGx, uvGy;		// the coordinates of the uv texture for the ground
	private int uvOx, uvOy;		// the coordinates of the uv texture for the ground
	private string groundType;	// what type of ground tile
	private string objectType;	// what type of object is on this
	private int objID;			// id for object that spans multiple tiles
	private bool walkable;		// is this space walkable

	// Constructor
	// = = = = = = = = = = = = = = = = = = = = >
	public tileData (int _x, int _y) {
		x = _x; y = _y;
		groundType = "Space";
		objectType = "None";
	}

	public tileData (int _x, int _y, string _gt, string _ot) {
		x = _x; y = _y;
		groundType = _gt;	
		objectType = _ot;
		GetGroundUVs ();
		GetObjectUVs (); 
	}

	// Functions
	// = = = = = = = = = = = = = = = = = = = = >
	public bool Walkable {
		get {return walkable; }
		set {walkable = value; }
	}

	public string GroundType {
		get {return groundType;}
		set {groundType = value; GetGroundUVs (); }
	}

	public string ObjectType {
		get {return objectType;}
		set {objectType = value; GetObjectUVs (); }
	}

	public Vector2 GroundUV {
		get {return new Vector2 (uvGx, uvGy); }
		set {uvGx = Mathf.RoundToInt (value.x); uvGy = Mathf.RoundToInt (value.y);}
	}

	public int GroundUVx {
		get {return uvGx; }
		set {uvGx = value; }
	}

	public int GroundUVy {
		get {return uvGy; }
		set {uvGy = value; }
	}

	public Vector2 ObjectUV {
		get {return new Vector2 (uvOx, uvOy); }
		set {uvOx = Mathf.RoundToInt (value.x); uvOy = Mathf.RoundToInt (value.y);}
	}

	public int ObjectUVx {
		get {return uvOx; }
		set {uvOx = value; }
	}

	public int ObjectUVy {
		get {return uvOy; }
		set {uvOy = value; }
	}

	private void GetGroundUVs () {
		switch (groundType) {
		case "White Tile":
			uvGx = 0; uvGy = 0; break;
		case "White Panel":
			uvGx = 0; uvGy = 1; break;
		case "White Carpet":
			uvGx = 0; uvGy = 2; break;
		case "Metal Tile":
			uvGx = 1; uvGy = 0; break;
		case "Metal Panel":
			uvGx = 1; uvGy = 1; break;
		case "Metal Carpet":
			uvGx = 1; uvGy = 2; break;
		case "Red Tile":
			uvGx = 2; uvGy = 0; break;
		case "Red Panel":
			uvGx = 2; uvGy = 1; break;
		case "Red Carpet":
			uvGx = 2; uvGy = 2; break;
		case "Blue Tile":
			uvGx = 3; uvGy = 0; break;
		case "Blue Panel":
			uvGx = 3; uvGy = 1; break;
		case "Blue Carpet":
			uvGx = 3; uvGy = 2; break;
		case "Brown Tile":
			uvGx = 4; uvGy = 0; break;
		case "Brown Panel":
			uvGx = 4; uvGy = 1; break;
		case "Brown Carpet":
			uvGx = 4; uvGy = 2; break;
		case "Dark Tile":
			uvGx = 5; uvGy = 0; break;
		case "Dark Panel":
			uvGx = 5; uvGy = 1; break;
		case "Dark Carpet":
			uvGx = 5; uvGy = 2; break;
		case "Space":
			uvGx = Mathf.RoundToInt (Random.Range(-0.4f, 5.4f)); uvGy = 4; break;
		default:
			Debug.Log ("(Warning)tileData: Found no ground type for tile (" + x + ", " + y + ")"); 
			uvGx = 0; uvGy = 2; break;
		}
	}

	public void GetObjectUVs () {
		switch (objectType) {
		case "Wall":
			Vector2 uvO = mapMaster.Master.GetWallUVs(x, y);
			uvOx = (int)uvO.x ; uvOy = (int)uvO.y ; break;
		case "None":
			uvOx = 6; uvOy = 0; break;
		default:
			uvOx = 0; uvOy = 0; break;
		}
	}
}