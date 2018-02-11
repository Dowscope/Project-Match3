using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject GameBoardPreFab;
	public List<GameObject> GemPreFabs;

	const int NUM_COLS = 8;
	const int NUM_ROWS = 8;

	GameObject board;

	// Use this for initialization
	void Start () {
		board = Instantiate (GameBoardPreFab, this.transform);
		board.name = "GAMEBOARD";

		InitializeBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitializeBoard(){

		for (int col = 0; col < NUM_COLS; col++) {
			Transform columnTransform = board.transform.GetChild(col);
			for (int row = 0; row < NUM_ROWS; row++) {
				Transform rowTransform = columnTransform.GetChild(row);
				int randomSprite = Random.Range (0, GemPreFabs.Count);
				Instantiate (GemPreFabs [randomSprite],
					rowTransform.position,
					Quaternion.identity,
					rowTransform);
			}
		}
	}
}
