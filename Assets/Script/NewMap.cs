using UnityEngine;
using System.Collections;

public class NewMap : MonoBehaviour {

	public GameObject route;
	public GameObject towerspot;
	public GameObject path;
	public TextAsset csvLevel;


	private static int columns = 16;
	private static int rows = 9;

	public int[,] map = new int[rows, columns];
	public static GameObject[] pathPoints = new GameObject[11];


	// Use this for initialization
	void Start () {
		InitialMap ();

		for (int row = 0; row < rows; row++) {
			for (int column = 0; column < columns; column++) {
				if (map [row, column] == 2) {
					Vector3 position = new Vector3 ((float)(column - 15), 0.07f, -(float)(row) + 2);
					Instantiate (towerspot, position, this.transform.rotation);
				} else if (map [row, column] == 3) {
					Vector3 position = new Vector3 ((float)(column - 15), 0.07f, -(float)(row) + 2);
					Instantiate (route, position, this.transform.rotation);

				} else if (map [row, column] >= 5) {
					Vector3 position = new Vector3 ((float)(column - 15), 0.07f, -(float)(row) + 2);
					Instantiate (route, position, this.transform.rotation);
					
					GameObject pathpoint = (GameObject)Instantiate (path, position, this.transform.rotation);
					pathPoints [map [row, column] - 5] = pathpoint;
				}

			}
		}
	}

	void InitialMap () {
		string[] lines = csvLevel.text.Split ('\n');
		int i = 0;
		foreach (string line in lines) {
			string[] map_choice = line.Split (',');
			int j = 0;
			foreach (string choice in map_choice) {
				map [i, j] = int.Parse (choice);
				Debug.Log (i + "," + j + "\n");
				j++;
			}
			i++;
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
