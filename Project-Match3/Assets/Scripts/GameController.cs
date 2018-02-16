using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject GameBoardPreFab;
	public List<GameObject> GemPreFabs;

	const int NUM_COLS = 8;
	const int NUM_ROWS = 8;

	GameObject board;

	Transform SelectedTile = null;

	// Use this for initialization
	void Start () {
		board = Instantiate (GameBoardPreFab, this.transform);
		board.name = "GAMEBOARD";

		InitializeBoard ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckEmpty ();

		if (Input.GetMouseButtonUp (0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePos.x = Mathf.Floor(mousePos.x - board.transform.position.x);
			mousePos.y = Mathf.Floor(mousePos.y - board.transform.position.y);

			if (mousePos.x >= 0 && mousePos.x < NUM_COLS && mousePos.y >= 0 && mousePos.y < NUM_ROWS) {
				Transform tileParent = board.transform.GetChild ((int)mousePos.x).GetChild ((int)mousePos.y);
				if (tileParent.childCount != 0) {
					Transform tile = tileParent.GetChild (0);

				}
			}

			if (SelectedTile != null) {
				TileMovement tm = SelectedTile.GetComponent<TileMovement> ();
				tm.Deselect ();
				SelectedTile = null;
			}
		}

		if (Input.GetMouseButton(0)) {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePos.x = Mathf.Floor(mousePos.x - board.transform.position.x);
			mousePos.y = Mathf.Floor(mousePos.y - board.transform.position.y);

			if (mousePos.x >= 0 && mousePos.x < NUM_COLS && mousePos.y >= 0 && mousePos.y < NUM_ROWS) {
				Transform tileParent = board.transform.GetChild ((int)mousePos.x).GetChild ((int)mousePos.y);
				if (tileParent.childCount != 0) {
					Transform tile = tileParent.GetChild (0);
					TileMovement tm = tile.GetComponent<TileMovement> ();
					tm.Select ();
					Debug.Log ("Working");
					SelectedTile = tile;
				}
			}
		}
	}

	void InitializeBoard(){

		for (int col = 0; col < NUM_COLS; col++) {
			Transform columnTransform = board.transform.GetChild(col);

			SpawnTile (columnTransform);
		}
	}

	void SpawnTile(Transform column) {
		Transform rowTransform = column.GetChild(NUM_ROWS - 1);
		int randomSprite = Random.Range (0, GemPreFabs.Count);
		Instantiate (GemPreFabs [randomSprite],
			rowTransform.position,
			Quaternion.identity,
			rowTransform);
	}

	void CheckEmpty() {
		for (int col = 0; col < NUM_COLS; col++) {
			Transform columnTransform = board.transform.GetChild(col);
			if (columnTransform.GetChild (NUM_ROWS - 1).childCount == 0) {
				SpawnTile (columnTransform);
			}
		}
	}
}