using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class objectData {
	// Global Variables
	// = = = = = = = = = = = = = = = = = = = = >
	private int x, y;			// coordinates of the object source tile
	private int ObjectID;		// 
	private string objType;		// what type of object
	private bool interactable;	// whether this object can be interacted
	private bool interactSpot;	// whether this object has a specific interactible spot or in general
	private int intrX, intrY;	// coordinates of the interactive spot
	private string direction;	// which direction this object is facing
	private List<Vector2> tiles;// location of all tiles this object occupies
	private int health;			// how much damage this object can take
	private int hp;				// how much current health does this object have
	private int armor;			// how defensive this object is

	// Constructor
	// = = = = = = = = = = = = = = = = = = = = >
	public objectData (int _x, int _y, string _type, string _dir, int _id) {
		x = _x; y = _y;
		ObjectID = _id;
		objType = _type;
		direction = _dir;
		tiles = new List<Vector2> ();
		GetTilesSet (); 
		GetObjectStats ();
	}

	// Functions
	// = = = = = = = = = = = = = = = = = = = = >
	public int ID {
		get {return ID; }
	}

	public bool Interactable {
		get {return interactable; }
		set {interactable = value; }
	}

	public List<Vector2> Tiles {
		get {return tiles;}
	}

	public bool MustInteractInSpot {
		get { return interactSpot; }
	}

	public Vector2 InteractPosition {
		get {return new Vector2 (intrX, intrY);}
	}

	public int Armor {
		get {return armor;}
	}

	public int Health {
		get {return hp; }
		set {hp = value; }
	}

	private void GetTilesSet () {
		switch (direction) {
		case "Front":
			GetTilesFront ();
			break;
		case "Back":
			GetTilesBack ();
			break;
		case "Left":
			GetTilesLeft ();
			break;
		case "Right":
			GetTilesRight ();
			break;
		default:
			Debug.Log ("Unknown Variable Passed for Direction of Object");
			Debug.Break ();
			break;
		}
	}

	private void GetObjectStats () {
		switch (objType) {
		case "Wall":
			health = 100;
			armor = 30;
			break;
		case "Door":
			health = 80;
			armor = 20;
			break;
		case "Reactor":
			health = 300;
			armor = 50;
			break;
		case "Bed":
			health = 80;
			armor = 20;
			break;
		default:
			Debug.Log ("Unknown Variable Passed for Object Type of Object");
			Debug.Break ();
			break;
		}

		hp = health;
	}

	private void GetTilesFront () {
		tiles.Clear (); 
		switch (objType) {
		case "Wall":
			tiles.Add (new Vector2 (x, y));
			intrX = x; intrY = y;
			interactable = false;
			interactSpot = false;
			break;
		case "Door":
			tiles.Add (new Vector2 (x, y));
			intrX = x; intrY = y;
			interactable = true;
			interactSpot = false;
			break;
		case "Reactor":
			tiles.Add (new Vector2 (x - 1, y));
			tiles.Add (new Vector2 (x, y));
			tiles.Add (new Vector2 (x + 1, y));
			intrX = x; intrY = y - 1;
			interactable = true;
			interactSpot = true;
			break;
		case "Bed":
			tiles.Add (new Vector2 (x, y));
			tiles.Add (new Vector2 (x + 1, y));
			intrX = x; intrY = y;
			interactable = true;
			interactSpot = false;
			break;
		default:
			Debug.Log ("Unknown Variable Passed for Object Type of Object");
			Debug.Break ();
			break;
		}
	}

	private void GetTilesBack() {
		tiles.Clear (); 
		switch (objType) {
		case "Wall":
			tiles.Add (new Vector2 (x, y));
			intrX = x; intrY = y;
			interactable = false;
			interactSpot = false;
			break;
		case "Door":
			tiles.Add (new Vector2 (x, y));
			intrX = x; intrY = y;
			interactable = true;
			interactSpot = false;
			break;
		case "Reactor":
			tiles.Add (new Vector2 (x - 1, y));
			tiles.Add (new Vector2 (x, y));
			tiles.Add (new Vector2 (x + 1, y));
			intrX = x; intrY = y + 1;
			interactable = true;
			interactSpot = true;
			break;
		case "Bed":
			tiles.Add (new Vector2 (x, y));
			tiles.Add (new Vector2 (x - 1, y));
			intrX = x; intrY = y;
			interactable = true;
			interactSpot = false;
			break;
		default:
			Debug.Log ("Unknown Variable Passed for Object Type of Object");
			Debug.Break ();
			break;
		}
	}


	private void GetTilesLeft() {
		tiles.Clear (); 
		switch (objType) {
		case "Wall":
			tiles.Add (new Vector2 (x, y));
			intrX = x; intrY = y;
			interactable = false;
			interactSpot = false;
			break;
		case "Door":
			tiles.Add (new Vector2 (x, y));
			intrX = x; intrY = y;
			interactable = true;
			interactSpot = false;
			break;
		case "Reactor":
			tiles.Add (new Vector2 (x, y - 1));
			tiles.Add (new Vector2 (x, y));
			tiles.Add (new Vector2 (x, y + 1));
			intrX = x - 1; intrY = y;
			interactable = true;
			interactSpot = true;
			break;
		case "Bed":
			tiles.Add (new Vector2 (x, y));
			tiles.Add (new Vector2 (x, y + 1));
			intrX = x; intrY = y;
			interactable = true;
			interactSpot = false;
			break;
		default:
			Debug.Log ("Unknown Variable Passed for Object Type of Object");
			Debug.Break ();
			break;
		}
	}



	private void GetTilesRight() {
		tiles.Clear (); 
		switch (objType) {
		case "Wall":
			tiles.Add (new Vector2 (x, y));
			intrX = x; intrY = y;
			interactable = false;
			interactSpot = false;
			break;
		case "Door":
			tiles.Add (new Vector2 (x, y));
			intrX = x; intrY = y;
			interactable = true;
			interactSpot = false;
			break;
		case "Reactor":
			tiles.Add (new Vector2 (x, y - 1));
			tiles.Add (new Vector2 (x, y));
			tiles.Add (new Vector2 (x, y + 1));
			intrX = x + 1; intrY = y;
			interactable = true;
			interactSpot = true;
			break;
		case "Bed":
			tiles.Add (new Vector2 (x, y));
			tiles.Add (new Vector2 (x, y - 1));
			intrX = x; intrY = y;
			interactable = true;
			interactSpot = false;
			break;
		default:
			Debug.Log ("Unknown Variable Passed for Object Type of Object");
			Debug.Break ();
			break;
		}
	}
}
