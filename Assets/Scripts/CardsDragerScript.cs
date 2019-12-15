using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsDragerScript : MonoBehaviour {

	FieldManager fieldManager ;
	Vector3 originPos ;

	void Start(){
		fieldManager = FindObjectOfType<FieldManager> ();
		originPos = this.transform.position;
	}

	void OnMouseDrag(){
		
		Vector3 objectPointInScreen
		= Camera.main.WorldToScreenPoint(this.transform.position);

		Vector3 mousePointInScreen
		= new Vector3(
			Input.mousePosition.x,
			Input.mousePosition.y,
			objectPointInScreen.z);

		Vector3 mousePointInWorld = Camera.main.ScreenToWorldPoint(mousePointInScreen);
		mousePointInWorld.y = this.transform.position.y;
		this.transform.position = mousePointInWorld;
	}
	void Update (){
		this.GetComponent <BoxCollider> ().enabled = true;

		if (Input.GetMouseButtonUp (0)) { 
			
			this.gameObject.transform.position = originPos; 

			this.GetComponent <BoxCollider> ().enabled = false;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);  
			RaycastHit hit = new RaycastHit ();  
			if (Physics.Raycast (ray, out hit)) {  
				Debug.Log (hit.collider.gameObject.name);
				if (hit.collider.gameObject.tag == "DroppableField") {

					Debug.Log ("ID =" +
						hit.collider.gameObject.GetComponent<Block> ().horizonID + "," +
						hit.collider.gameObject.GetComponent<Block> ().verticalID
					);

					int h = hit.collider.gameObject.GetComponent<Block> ().horizonID;
					int v = hit.collider.gameObject.GetComponent<Block> ().verticalID;

					if (GameManager.instance.isOneTurn == true && GameManager.instance.state == GameState.CHOOSE) {
						fieldManager.MakeHuman (1, h, v);
						GameManager.instance.state = GameState.ACTION;
					} else if (GameManager.instance.isOneTurn == false && GameManager.instance.state == GameState.CHOOSE) {
						fieldManager.MakeHuman (2, h, v);
						GameManager.instance.state = GameState.ACTION;
					}

				}


			} else {
				Debug.Log ("マスを選択してください");
			}
		}
	}
}