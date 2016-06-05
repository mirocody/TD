using UnityEngine;
using System.Collections;

public class TowerSelectionPanel : MonoBehaviour {

	[HideInInspector]
	public GameObject selectedTower;

	public float normalizedPosX;
	public float normalizedPosY;

	Transform image;

	// Use this for initialization
	void Start () {
		image = transform.Find("TSCanvas/TSImage");
		image.GetComponent<RectTransform>().anchoredPosition =  new Vector2(
			normalizedPosX * Screen.width, 
			-normalizedPosY * Screen.height);
	}
		
	public void SelectTowerType(GameObject prefab) {

		// Pass the tower selected by user to TowerSpot.cs
		selectedTower = prefab;
		Debug.Log ("Tower Selected!");
	}

	public void CancelSelectTower()
	{
		gameObject.SetActive (false);
	}
}
