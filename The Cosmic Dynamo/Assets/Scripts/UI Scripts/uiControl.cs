using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiControl : MonoBehaviour {
	// Global Variables
	// = = = = = = = = = = = = = = = = = = = = >
	private GameObject pnlBuild;
	private GameObject pnlStructure;
	private GameObject pnlFurniture;
	private GameObject pnlDecor;
	private GameObject pnlHull;
	private GameObject pnlMisc;

	private Toggle tglStructure;
	private Toggle tglFurniture;
	private Toggle tglDecor;
	private Toggle tglHull;
	private Toggle tglMisc;

	// Initialization
	// = = = = = = = = = = = = = = = = = = = = >
	void Awake () {
		pnlBuild = GameObject.Find ("uiPanelBuildMenus");

		pnlStructure = GameObject.Find ("uiPanelStructure"); 
		pnlFurniture = GameObject.Find ("uiPanelFurniture"); 
		pnlDecor = GameObject.Find ("uiPanelDecor"); 
		pnlHull = GameObject.Find ("uiPanelHull"); 
		pnlMisc = GameObject.Find ("uiPanelMisc"); 

		tglStructure = GameObject.Find ("uiTglStructure").GetComponent<Toggle> (); 
		tglFurniture = GameObject.Find ("uiTglFurniture").GetComponent<Toggle> (); 
		tglDecor = GameObject.Find ("uiTglDecor").GetComponent<Toggle> (); 
		tglHull = GameObject.Find ("uiTglHull").GetComponent<Toggle> (); 
		tglMisc = GameObject.Find ("uiTglMisc").GetComponent<Toggle> (); 
	}

	void Start () {
		pnlStructure.SetActive (false); 
		pnlFurniture.SetActive (false);
		pnlDecor.SetActive (false);
		pnlHull.SetActive (false);
		pnlMisc.SetActive (false);
		pnlBuild.SetActive (false); 
	}


	// Update
	// = = = = = = = = = = = = = = = = = = = = >
	void Update () {
	
	}


	// Functions
	// = = = = = = = = = = = = = = = = = = = = >
	public void OpenBuildMenu(string menu) {
		if (menu == "Structure") { if (tglStructure.isOn) 	{ pnlBuild.SetActive (true); } else { pnlBuild.SetActive (false); } } 
		if (menu == "Furniture") { if (tglFurniture.isOn) 	{ pnlBuild.SetActive (true); } else { pnlBuild.SetActive (false); } } 
		if (menu == "Decor") 	 { if (tglDecor.isOn) 		{ pnlBuild.SetActive (true); } else { pnlBuild.SetActive (false); } } 
		if (menu == "Hull") 	 { if (tglHull.isOn) 		{ pnlBuild.SetActive (true); } else { pnlBuild.SetActive (false); } } 
		if (menu == "Misc") 	 { if (tglMisc.isOn) 		{ pnlBuild.SetActive (true); } else { pnlBuild.SetActive (false); } } 

		if (menu == "Structure" && !pnlStructure.activeSelf)	{ pnlStructure.SetActive (true); } else { pnlStructure.SetActive (false); }
		if (menu == "Furniture" && !pnlFurniture.activeSelf) 	{ pnlFurniture.SetActive (true); } else { pnlFurniture.SetActive (false); }
		if (menu == "Decor" && !pnlDecor.activeSelf) 			{ pnlDecor.SetActive (true); } else { pnlDecor.SetActive (false); }
		if (menu == "Hull" && !pnlHull.activeSelf) 				{ pnlHull.SetActive (true); } else { pnlHull.SetActive (false); }
		if (menu == "Misc" && !pnlMisc.activeSelf) 				{ pnlMisc.SetActive (true); } else { pnlMisc.SetActive (false); }


	}
}
