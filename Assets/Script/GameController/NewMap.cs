﻿using UnityEngine;
using System.Collections;
using System.IO;

public class NewMap : MonoBehaviour {

	public GameObject route;
	public GameObject towerspot;
	public GameObject path;
	public TextAsset csvLevel;
	public GameObject barrel;
	public GameObject lid;
	public GameObject mushroom;
	public GameObject rock;


	private static int columns;
	private static int rows;
	private static int totalPathPoint;

	public int[,] map;
	public static GameObject[] pathPoints;


	// Use this for initialization
	void Start () {
		InitialMap ();

		for (int row = 0; row < rows; row++) {
			for (int column = 0; column < columns; column++) {
				if (map [row, column] == 2) {
					Vector3 position = new Vector3 ((float)(column - 15), 0.07f, -(float)(row) + 2);
					GameObject temp = (GameObject)Instantiate (towerspot, position, this.transform.rotation);
					temp.name += ("_" + row + "_" + column);
				} else if (map [row, column] == 3) {
					Vector3 position = new Vector3 ((float)(column - 15), 0.07f, -(float)(row) + 2);
					Instantiate (route, position, this.transform.rotation);
				} else if (map [row, column] == 5) {
					Vector3 position = new Vector3 ((float)(column - 15), 0.37f, -(float)(row) + 2);
					Instantiate (barrel, position, this.transform.rotation);	
				} else if (map [row, column] == 6) {
					Vector3 position = new Vector3 ((float)(column - 15) , 0.37f, -(float)(row) + 2);
					Instantiate (lid, position, this.transform.rotation);
				} else if (map [row, column] == 7) {
					Vector3 position = new Vector3 ((float)(column - 15), 0.37f, -(float)(row) + 2);
					Instantiate (mushroom, position, this.transform.rotation);
				} else if (map [row, column] == 8) {
					Vector3 position = new Vector3 ((float)(column - 15), 0.37f, -(float)(row) + 2);
					Instantiate (rock, position, this.transform.rotation);
				} else if (map [row, column] >= 9) {
					Vector3 position = new Vector3 ((float)(column - 15), 0.07f, -(float)(row) + 2);
					Instantiate (route, position, this.transform.rotation);
					GameObject pathpoint = (GameObject)Instantiate (path, position, this.transform.rotation);
					pathPoints [map [row, column] - 9] = pathpoint;
				} 
			}
		}
	}

	void InitialMap () {
		string[] lines = csvLevel.text.Split ('\n');
		string line1 = lines [0];
		// get the map count data;
		string[] map_count = line1.Split (',');

		totalPathPoint = int.Parse (map_count [1]);
		columns = int.Parse (map_count [3]);
		rows = int.Parse (map_count [5]);
		pathPoints = new GameObject[totalPathPoint];
		map = new int[rows, columns];

		for(int i = 1; i < lines.GetLength(0); i++) {
			string[] map_choice = lines[i].Split (',');
			int j = 0;
			foreach (string choice in map_choice) {
				map [i-1, j] = int.Parse (choice);
				//Debug.Log (i + "," + j + "\n");
				j++;
			}
		}



	}

	// Update is called once per frame
	void Update () {
	
	}
}
