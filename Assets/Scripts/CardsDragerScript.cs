using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsDragerScript : MonoBehaviour {

	FieldManager fieldManager ;
	Vector3 originPos ;
	public Camera cam1, cam2;
	public	Camera cam;
	public int id;

	void Start(){
		fieldManager = FindObjectOfType<FieldManager> ();
		originPos = this.transform.position;
	}

	void OnMouseDrag(){

		if (GameManager.instance.isOneTurn == true) {
			if (id == 2) {
				return;
			}
		}

		if (GameManager.instance.isOneTurn == false) {
			if (id == 1) {
				return;
			}
		}
		
		if (GameManager.instance.isOneTurn == true) {
			cam = cam1;
		} else {
			cam = cam2;
		}



		Vector3 objectPointInScreen
		= cam.WorldToScreenPoint(this.transform.position);

		Vector3 mousePointInScreen
		= new Vector3(
			Input.mousePosition.x,
			Input.mousePosition.y,
			objectPointInScreen.z);

		Vector3 mousePointInWorld = cam.ScreenToWorldPoint(mousePointInScreen);
		mousePointInWorld.y = this.transform.position.y;
		this.transform.position = mousePointInWorld;
	}
	void Update (){
		this.GetComponent <BoxCollider> ().enabled = true;

		if (Input.GetMouseButtonUp (0)) { 
			
			this.gameObject.transform.position = originPos; 

			this.GetComponent <BoxCollider> ().enabled = false;
		
			if (GameManager.instance.isOneTurn == true) {
				cam = cam1;
			} else {
				cam = cam2;
			}
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);  
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

	
					for(int i = 0; i < fieldManager.oneList.Count; i++){
						if(fieldManager.oneList[i].GetComponent<Human>().horizonID == h && fieldManager.oneList[i].GetComponent<Human>().verticalID == v){
							return;
						}
					}
					for(int j = 0; j < fieldManager.twoList.Count; j++){
						if(fieldManager.twoList[j].GetComponent<Human>().horizonID == h && fieldManager.twoList[j].GetComponent<Human>().verticalID == v){
							return;
						}
					}

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