using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour {
	
	Vector3 endPosition;
	float speed = 5f;
	bool moving = false;

	// Use this for initialization
	void Start () {
		speed = Random.Range (5f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (moving) {

			Vector3 newPos = transform.position;
			newPos.y = newPos.y - speed * Time.deltaTime;

			if (newPos.y <= endPosition.y) {
				moving = false;
				transform.position = endPosition;
			} else {
				transform.position = newPos;
			}
		} else {
			checkTileBelow ();
			CheckMatching ();
		}

	}

	public void Move(Vector3 pos){
		endPosition = pos;
		moving = true;
	}

	void checkTileBelow(){
		Transform row = transform.parent;
		Transform column = row.parent;

		Vector3 row_pos = row.position;

		if (column.childCount > 1) {
			int siblingIndex = row.GetSiblingIndex ();
			if (siblingIndex <= 0) {
				return;
			}
			if (column.GetChild(siblingIndex - 1).childCount == 0) {
				transform.parent = column.GetChild (siblingIndex - 1);
				Move (transform.parent.position);
			}
		}
	}

	void CheckMatching() {
		Transform row = transform.parent;
		Transform column = row.parent;

		List<Transform> listOfTilesRow = CheckNeighbourAbove (transform);
		List<Transform> listOfTilesColRight = CheckNeighbourRight (transform);
		List<Transform> listOfTilesColLeft = CheckNeighbourRight (transform);

		if (listOfTilesRow.Count >= 3) {
			foreach (Transform i in listOfTilesRow) {
				Destroy (i.gameObject);
			}
		}

		if (listOfTilesColRight.Count >= 3) {
			foreach (Transform i in listOfTilesColRight) {
				Destroy (i.gameObject);
			}
		}

		if (listOfTilesColLeft.Count >= 3) {
			foreach (Transform i in listOfTilesColLeft) {
				Destroy (i.gameObject);
			}
		}
	}

	List<Transform> CheckNeighbourAbove(Transform t) {
		List<Transform> tiles = new List<Transform>();
		tiles.Add (t);

		Transform row = t.parent;
		Transform column = row.parent;

		int siblingIndex = row.GetSiblingIndex ();
		if (siblingIndex < column.childCount - 1) {
			Transform rowAbove = column.GetChild (siblingIndex + 1);
			if (rowAbove.childCount != 0) {
				Transform tileAbove = rowAbove.GetChild (0);
				if (t.gameObject.name == tileAbove.gameObject.name) {
					List<Transform> moreTiles = CheckNeighbourAbove (tileAbove);

					foreach (Transform i in moreTiles) {
						tiles.Add (i);
					}
				}
			}
		}

		return tiles;
	}

	List<Transform> CheckNeighbourRight(Transform t) {
		List<Transform> tiles = new List<Transform>();
		tiles.Add (t);

		Transform row = t.parent;
		Transform column = row.parent;
		Transform gameBoard = column.parent;

		int siblingIndexRow = row.GetSiblingIndex ();
		int siblingIndexCol = column.GetSiblingIndex ();
		if (siblingIndexCol < gameBoard.childCount - 1) {
			Transform colRight = gameBoard.GetChild (siblingIndexCol + 1);
			Transform rowRight = colRight.GetChild (siblingIndexRow);
			if (rowRight.childCount != 0) {
				Transform tileRight = rowRight.GetChild (0);
				if (tileRight.gameObject.name == t.gameObject.name) {
					List<Transform> moreTiles = CheckNeighbourRight (tileRight);
					foreach (Transform i in moreTiles) {
						tiles.Add (i);
					}
				}
			}
		}
		return tiles;
	}

	List<Transform> CheckNeighbourLeft(Transform t) {
		List<Transform> tiles = new List<Transform>();
		tiles.Add (t);

		Transform row = t.parent;
		Transform column = row.parent;
		Transform gameBoard = column.parent;

		int siblingIndexRow = row.GetSiblingIndex ();
		int siblingIndexCol = column.GetSiblingIndex ();
		if (siblingIndexCol > 0) {
			Transform colLeft = gameBoard.GetChild (siblingIndexCol - 1);
			Transform rowLeft = colLeft.GetChild (siblingIndexRow);
			if (rowLeft.childCount != 0) {
				Transform tileLeft = rowLeft.GetChild (0);
				if (tileLeft.gameObject.name == t.gameObject.name) {
					List<Transform> moreTiles = CheckNeighbourRight (tileLeft);
					foreach (Transform i in moreTiles) {
						tiles.Add (i);
					}
				}
			}
		}
		return tiles;
	}
}
