using UnityEngine;
using System.Collections;

public class buildMaster : MonoBehaviour {
	// Global Variables
	// = = = = = = = = = = = = = = = = = = = = >
	public static buildMaster Master;

	public bool BuildingMode;

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

		// setup master variables
		BuildingMode = false;

	}

	void Start () {

	}

	// Update
	// = = = = = = = = = = = = = = = = = = = = >
	void Update () {

	}

	// Functions
	// = = = = = = = = = = = = = = = = = = = = >

}
