using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjEnvData {

	// Global Variables
	// = = = = = = = = = = = = = = = = = = = = >
	public List<objectData> objects;

	private int mapTileWide;		// width of the map in tiles
	private int objIdentifier;		// this will increase every creating of object to create a unique id

	// Constructor
	// = = = = = = = = = = = = = = = = = = = = >
	public ObjEnvData () {
		objIdentifier = 2;
	}

	// Functions
	// = = = = = = = = = = = = = = = = = = = = >
	public void AddNewObject (int _x, int _y, string _type, string _dir) {
		objects.Add (new objectData(_x,_y,_type,_dir, objIdentifier) );
		objIdentifier++;
	}

	public objectData FindObject (int _id) {
		for(int i = 0; i <= objects.Count; i++) {
			if (_id == objects[i].ID) {
				return objects [i];
			}
		}
		Debug.Log ("Unable to retrieve the object from the id.");
		Debug.Break (); 
		return null;
	}
}
