using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour {

	Vector3 endPosition;
	float speed = 5f;
	bool moving = false;

	// Use this for initialization
	void Start () {
		speed = Random.Range (3f, 8f);
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
		}

		checkTileBelow ();
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
}
