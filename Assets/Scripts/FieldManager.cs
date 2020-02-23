using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldManager : MonoBehaviour {
	
	public int gridSize = 5;
	public GameObject cam1;
	public GameObject cam2;
	public GameObject cellPrefab;
	public GameObject oneBoder;
	public GameObject twoBoder;
	public GameObject human;
	public int oneTroopsNum;
	public int twoTroopsNum;
	public Transform one;
	public Transform two;
	public Text oneCostText;
	public Text twoCostText;
	public int oneCost;
	public int twoCost;

	//ヒューマンの位置を確認する
	public List<GameObject> oneList;
	public List<GameObject> twoList;

	// Use this for initialization
	void Start () {
		oneCostText.text = oneCost.ToString ();
		twoCostText.text = twoCost.ToString ();

		MakeField ();


	}

	void MakeField () {
		
		for (int x = 0; x < gridSize; x++) {  
			for (int z = 0; z < gridSize; z++) {  
				// セルを作成  
				GameObject obj = Instantiate (cellPrefab) as GameObject;  
				obj.transform.SetParent (transform);  
				obj.GetComponent<Block> ().horizonID = x - 2;
				obj.GetComponent<Block> ().verticalID = z - 2;
				// 位置を設定  
				float xPos = (x - gridSize * 0.5f);  
				float zPos = (z - gridSize * 0.5f);  
				obj.transform.localPosition = new Vector3 (xPos+0.5f, -0.5f, zPos+0.5f);   

				// Cellをセット  
			}  
		}
		oneBoder.transform.localPosition = new Vector3 (-1.5f,0.05f,0);
		twoBoder.transform.localPosition = new Vector3 (1.5f,0.05f,0);
	}


		
	// Update is called once per frame
	void Update () {



//		if (Input.GetMouseButtonDown(0)) {  
//			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
//			RaycastHit hit = new RaycastHit();  
//
//			if (Physics.Raycast (ray, out hit)) {  
//				
//				Debug.Log ("ID =" +
//				hit.collider.gameObject.GetComponent<Block> ().horizonID + "," +
//				hit.collider.gameObject.GetComponent<Block> ().verticalID);
//
//				int h = hit.collider.gameObject.GetComponent<Block> ().horizonID;
//				int v = hit.collider.gameObject.GetComponent<Block> ().verticalID;
//
//				if (GameManager.instance.isOneTurn == true && GameManager.instance.state == GameState.CHOOSE) {
//					MakeHuman (1, h, v);
//					GameManager.instance.state = GameState.ACTION;
//				} else if (GameManager.instance.isOneTurn == false && GameManager.instance.state == GameState.CHOOSE) {
//					MakeHuman (2, h, v);
//					GameManager.instance.state = GameState.ACTION;
//				}
//			} else {
//				Debug.Log ("マスを選択してください");
//			}  
//		} 
	}

	public void MakeHuman(int id, int horizonID, int verticalID){
		if (id == 1) {

			Vector3 pos = new Vector3 (horizonID,0,verticalID);
		
			GameObject hum = Instantiate (human,pos,Quaternion.Euler(0f, 90f, 0f),one);
			(hum).GetComponent<Renderer> ().material.color = Color.blue;
			(cam1).GetComponent<Camera> ().backgroundColor = Color.blue;
			(cam2).GetComponent<Camera> ().backgroundColor = Color.blue;
			hum.GetComponent<Human> ().type = HumanType.ONE;
			hum.GetComponent<Human> ().horizonID = horizonID;
			hum.GetComponent<Human> ().verticalID = verticalID;
			hum.GetComponent<Human> ().humanStrengthID = Random.Range (3,10);

			oneList.Add (hum);
			twoList.Add (hum);

			hum.tag = "ONE";
		
			//プレイヤー１を生成する
		}
		if (id == 2) {
			//プレイヤー２を生成する
			Vector3 pos = new Vector3 (horizonID,0,verticalID);
			GameObject hum = Instantiate (human,pos,Quaternion.Euler(0f, -90f, 0f),two);
			(hum).GetComponent<Renderer> ().material.color = Color.red;
			(cam1).GetComponent<Camera> ().backgroundColor = Color.red;
			(cam2).GetComponent<Camera> ().backgroundColor = Color.red;
			hum.GetComponent<Human> ().type = HumanType.TWO;
			hum.GetComponent<Human> ().horizonID = horizonID;
			hum.GetComponent<Human> ().verticalID = verticalID;
			hum.GetComponent<Human> ().humanStrengthID = Random.Range (3,10);
			hum.tag = "TWO";
		}
	}
}
