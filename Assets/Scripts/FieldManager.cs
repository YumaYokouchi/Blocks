using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour {
	
	public int gridSize = 5;
	public GameObject cellPrefab;
	public GameObject human;
	public Transform one;
	public Transform two;

	// Use this for initialization
	void Start () {
		

		for (int x = 0; x < gridSize; x++) {  
			for (int z = 0; z < gridSize; z++) {  
				// セルを作成  
				GameObject obj = Instantiate (cellPrefab) as GameObject;  
				obj.transform.SetParent (transform);  
				obj.GetComponent<Block> ().horizonID = x-2;
				obj.GetComponent<Block> ().verticalID = z-2;
				// 位置を設定  
				float xPos = (x - gridSize * 0.5f);  
				float zPos = (z - gridSize * 0.5f);  
				obj.transform.localPosition = new Vector3 (xPos, 0f, zPos);   

				// Cellをセット  
			}  
		}  

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {  
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
			RaycastHit hit = new RaycastHit();  

			if (Physics.Raycast(ray, out hit)){  
				
				Debug.Log("ID ="+
					hit.collider.gameObject.GetComponent<Block>().horizonID +","+
					hit.collider.gameObject.GetComponent<Block>().verticalID	);

				int h = hit.collider.gameObject.GetComponent<Block> ().horizonID;
				int v = hit.collider.gameObject.GetComponent<Block> ().verticalID;

				if (GameManager.instance.isOneTurn == true && GameManager.instance.state == GameState.CHOOSE) {
					MakeHuman (1,h,v);
					GameManager.instance.state = GameState.ACTION;
				}
				else if(GameManager.instance.isOneTurn == false && GameManager.instance.state == GameState.CHOOSE){
					MakeHuman (2,h,v);
					GameManager.instance.state = GameState.ACTION;
				}
			}  
		} 
	}

	void MakeHuman(int id, int horizonID, int verticalID){
		if (id == 1) {
			Vector3 pos = new Vector3 (horizonID-0.5f,0,verticalID-0.5f);
			GameObject hum = Instantiate (human,pos,Quaternion.identity,one);
			hum.GetComponent<Human> ().type = HumanType.ONE;
			hum.GetComponent<Human> ().horizonID = horizonID;
			hum.GetComponent<Human> ().verticalID = verticalID;
			hum.tag = "ONE";
		
			//プレイヤー１を生成する
		}
		if (id == 2) {
			//プレイヤー２を生成する
			Vector3 pos = new Vector3 (horizonID-0.5f,0,verticalID-0.5f);
			GameObject hum = Instantiate (human,pos,Quaternion.identity);
			hum.GetComponent<Human> ().type = HumanType.TWO;
			hum.GetComponent<Human> ().horizonID = horizonID;
			hum.GetComponent<Human> ().verticalID = verticalID;
			hum.tag = "TWO";
		}
	}
}
