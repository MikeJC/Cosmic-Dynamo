using UnityEngine;
using System.Collections;

/* This script will be used on a mesh to layout a quadrant of tiles.
 * Each quadrant will have a seperate mesh. This way each mesh not
 * being viewed by the camera will not be rendered allowing for a 
 * larger map without performance lost. */
public class mapGround : MonoBehaviour {
	// Global Variables
	// = = = = = = = = = = = = = = = = = = = = >
	private int numQuadX;		// identify which quadrant this is
	private int numQuadY;		// identify which quadrant this is
	private float QuadWidth;	// how wide each quadrant is (unity units)
	private float QuadHeight;	// how wide each quadrant is (unity units)
	private int numTilesWide;	// how many tiles wide in a quadrant
	private int numTilesHigh;	// how many tiles high in a quadrant
	private float tileWidth;	// how wide a tile is
	private float tileHeight;	// how tall a tile is

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
			this.gameObject.name = "mapQuad(" + numQuadX + "," + numQuadY + ")";
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
		int x = 0; int y = 0;
		for (int i = 0; i < (numTiles * 4); i++) {
			if ((i % 4) == 0) {
				x++;
				if (x >= numTilesWide) {
					x = 0; y++;
				}
				verts [i] = new Vector3 (x * tileWidth, y * tileHeight, 0f);
				norms [i] = Vector3.back;
				uvs [i] = new Vector2 (0f, 0f);
			} else if ((i % 4 ) == 1) {
				verts [i] = new Vector3 (x * tileWidth + tileWidth, y * tileHeight, 0f);
				norms [i] = Vector3.back;
				uvs [i] = new Vector2 (1f, 0f);
			} else if ((i % 4 ) == 2) {
				verts [i] = new Vector3 (x * tileWidth + tileWidth, y * tileHeight + tileHeight, 0f);
				norms [i] = Vector3.back;
				uvs [i] = new Vector2 (1f, 1f);
			}else if ((i % 4 ) == 3) {
				verts [i] = new Vector3 (x * tileWidth, y * tileHeight + tileHeight, 0f);
				norms [i] = Vector3.back;
				uvs [i] = new Vector2 (0f, 1f);
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
}
