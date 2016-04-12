using UnityEngine;
using System.Collections;

/* This script will be used on a mesh to layout a quadrant of tiles.
 * Each quadrant will have a seperate mesh. This way each mesh not
 * being viewed by the camera will not be rendered allowing for a 
 * larger map without performance lost. */
public class mapObjects : MonoBehaviour {
	// Global Variables
	// = = = = = = = = = = = = = = = = = = = = >
	private int numQuadX;		// identify which quadrant this is
	private int numQuadY;		// identify which quadrant this is
	private float QuadWidth;	// how wide each quadrant is (unity units)
	private float QuadHeight;	// how wide each quadrant is (unity units)
	private int numTilesWide;	// how many tiles wide in a quadrant
	private int numTilesHigh;	// how many tiles high in a quadrant
	private int tileOffsetX;	// how far off the bottom left tile is from the true map origin
	private int tileOffsetY;	// how far off the bottom left tile is from the true map origin
	private float tileWidth;	// how wide a tile is
	private float tileHeight;	// how tall a tile is
	private float textureWidth;	// the width of the texture
	private float textureHeight;	// the height of the texture
	private float txtrTileWidth;	// the width of the tile texture
	private float txtrTileHeight;	// the height of the tile texture

	// Initialization
	// = = = = = = = = = = = = = = = = = = = = >
	void Start () {
		// pull the default values from the master
		if (mapMaster.Master != null) {
			QuadWidth = mapMaster.Master.QuadrantWidth;
			QuadHeight = mapMaster.Master.QuadrantHeight;
			numTilesWide = mapMaster.Master.QuadrantTileWidth;
			numTilesHigh = mapMaster.Master.QuadrantTileHeigth;
			numQuadX = Mathf.FloorToInt ((transform.position.x + (mapMaster.Master.NumberOfQuadrantsWide * 0.5f * QuadWidth)) / QuadWidth);
			numQuadY = Mathf.FloorToInt ((transform.position.y + (mapMaster.Master.NumberOfQuadrantsHigh * 0.5f * QuadHeight)) / QuadHeight);
			tileWidth = QuadWidth / numTilesWide;
			tileHeight = QuadHeight / numTilesHigh;
			tileOffsetX = numQuadX * numTilesWide;
			tileOffsetY = numQuadY * numTilesHigh;
			this.gameObject.name = "mapQuadObj(" + numQuadX + "," + numQuadY + ")";
			textureWidth = this.GetComponent<MeshRenderer> ().material.mainTexture.width; 
			textureHeight = this.GetComponent<MeshRenderer> ().material.mainTexture.height;
			txtrTileWidth = 64;
			txtrTileHeight = 64;
			BuildMesh ();
		} else {
			Debug.Log ("(Error)mapGround: Lost the master."); 
		}
	}

	// Update
	// = = = = = = = = = = = = = = = = = = = = >
	void Update () {

	}

	// Functions
	// = = = = = = = = = = = = = = = = = = = = >
	private void BuildMesh () {
		MeshFilter mf = GetComponent<MeshFilter> ();
		MeshRenderer mr = GetComponent<MeshRenderer> ();
		MeshCollider mc = GetComponent<MeshCollider> ();

		Mesh mesh = new Mesh ();

		// mesh property variables
		int numTiles = numTilesHigh * numTilesWide;
		Vector3[] verts = new Vector3[numTiles * 4];
		Vector3[] norms = new Vector3[numTiles * 4];
		Vector2[] uvs = new Vector2[numTiles * 4];
		int[] tris = new int[numTiles * 6 ];

		//create mesh property data
		int x = -1; int y = 0;
		float vertError = 0.0f /*1/96f*/;		// how much to expand a tile to create overlap
		float uvError = 0.5f / txtrTileWidth;	// how much a single tiles uv will be off
		float uvTilePercWidth = txtrTileWidth / textureWidth;
		float uvTilePercHeight = txtrTileHeight / textureHeight; 

		int tx = 2 ; int ty = 2;
		for (int i = 0; i < (numTiles * 4); i++) {
			// now work with each corner and assign the mesh stuff
			if ((i % 4) == 0) {
				x++;
				if (x >= numTilesWide) {
					x = 0; y++;
				}
				// Grab the data for the new tile
				if (mapMaster.Master.Map.Tiles [x + tileOffsetX, y + tileOffsetY] == null) {
					Debug.Log ("Tile (" + (x + tileOffsetX) + ", " + (y + tileOffsetY) + ") does not exist");
				}
				tx = mapMaster.Master.Map.Tiles[x + tileOffsetX, y + tileOffsetY].ObjectUVx;
				ty = mapMaster.Master.Map.Tiles[x + tileOffsetX, y + tileOffsetY].ObjectUVy;

				// now back to assigning mesh stuff
				verts [i] = new Vector3 (x * tileWidth - vertError, y * tileHeight - vertError, 0f);
				norms [i] = Vector3.back;
				uvs [i] = new Vector2 (uvTilePercWidth * (tx + 0) + uvError, uvTilePercHeight * (ty + 0) + uvError);
			} else if ((i % 4 ) == 1) {
				verts [i] = new Vector3 (x * tileWidth + tileWidth + vertError, y * tileHeight - vertError, 0f);
				norms [i] = Vector3.back;
				uvs [i] = new Vector2 (uvTilePercWidth * (tx + 1) - uvError , uvTilePercHeight * (ty + 0) + uvError);
			} else if ((i % 4 ) == 2) {
				verts [i] = new Vector3 (x * tileWidth + tileWidth + vertError, y * tileHeight + tileHeight + vertError, 0f);
				norms [i] = Vector3.back;
				uvs [i] = new Vector2 (uvTilePercWidth * (tx + 1) - uvError, uvTilePercHeight * (ty + 1) - uvError );
			}else if ((i % 4 ) == 3) {
				verts [i] = new Vector3 (x * tileWidth - vertError, y * tileHeight + tileHeight + vertError, 0f);
				norms [i] = Vector3.back;
				uvs [i] = new Vector2 (uvTilePercWidth * (tx + 0) + uvError, uvTilePercHeight * (ty + 1) - uvError );
			}else {
				Debug.Log ("(Error)mapGround: Unable to build mesh due to a math error."); 
			}
		}

		for (int i = 0; i < (numTiles); i++) {
			tris [i * 6 + 0] = (i * 4) + 0;
			tris[i * 6 + 1] = (i * 4) + 3;
			tris[i * 6 + 2] = (i * 4) + 1;
			tris[i * 6 + 3] = (i * 4) + 2;
			tris[i * 6 + 4] = (i * 4) + 1;
			tris[i * 6 + 5] = (i * 4) + 3;
		}

		// assign mesh property to mesh
		mesh.vertices = verts;
		mesh.normals = norms;
		mesh.triangles = tris;
		mesh.uv = uvs;

		mf.mesh = mesh;
		mc.sharedMesh = mf.mesh;
	}

	public bool IsTileInQuadrant (int _x, int _y){
		int x = _x - tileOffsetX;
		int y = _y - tileOffsetY;
		if (x < 0 || y < 0 || x >= numTilesWide || y >= numTilesHigh) {
			return false;
		} else {
			return true;
		}
	}

	public void UpdateTileTexture (int _x, int _y) {
		int tx = mapMaster.Master.Map.Tiles[_x, _y].ObjectUVx;
		int ty = mapMaster.Master.Map.Tiles[_x, _y].ObjectUVy;
		if (IsTileInQuadrant (_x, _y)) {
			int x = _x - tileOffsetX;
			int y = _y - tileOffsetY;
			Mesh mesh = GetComponent<MeshFilter> ().mesh;

			int numTiles = numTilesHigh * numTilesWide;
			float uvError = 0.5f / txtrTileWidth;	// how much a single tiles uv will be off
			float uvTilePercWidth = txtrTileWidth / textureWidth;
			float uvTilePercHeight = txtrTileHeight / textureHeight; 
			Vector2[] uvs = new Vector2[numTiles * 4];
			uvs = mesh.uv;

			uvs [(y * numTilesWide + x) * 4 + 0] = new Vector2 (uvTilePercWidth * (tx + 0) + uvError, uvTilePercHeight * (ty + 0) + uvError);
			uvs [(y * numTilesWide + x) * 4 + 1] = new Vector2 (uvTilePercWidth * (tx + 1) - uvError, uvTilePercHeight * (ty + 0) + uvError);
			uvs [(y * numTilesWide + x) * 4 + 2] = new Vector2 (uvTilePercWidth * (tx + 1) - uvError, uvTilePercHeight * (ty + 1) - uvError);
			uvs [(y * numTilesWide + x) * 4 + 3] = new Vector2 (uvTilePercWidth * (tx + 0) + uvError, uvTilePercHeight * (ty + 1) - uvError);

			mesh.uv = uvs;
			GetComponent<MeshFilter> ().mesh = mesh;
		} else {
			Debug.Log ("(Error)mapObjects: Tile found outside the quadrant."); 
		}

	}
}
