using UnityEngine;
using System.Collections;

public class mapData {
	// Global Variables
	// = = = = = = = = = = = = = = = = = = = = >
	public tileData[,] Tiles;

	private int mapTileWide;		// width of the map in tiles
	private int mapTileHigh;		// height of the map in tiles

	// Constructor
	// = = = = = = = = = = = = = = = = = = = = >
	public mapData (int _tw, int _th) {
		mapTileWide = _tw;
		mapTileHigh = _th;

		Tiles = new tileData[mapTileWide, mapTileHigh];
		BuildNewMap ();
	}

	// Functions
	// = = = = = = = = = = = = = = = = = = = = >
	private void BuildNewMap () {
		for (int x = 0; x < mapTileWide; x++) {
			for (int y = 0; y < mapTileHigh; y++) {
				Tiles [x, y] = new tileData (x, y);
				Tiles [x, y].GroundType = "Space";
				Tiles [x, y].ObjectType = "None";
			}
		}
	}

}
