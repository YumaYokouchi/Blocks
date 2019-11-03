using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour {
	
	public int gridSize = 5;
	public GameObject cellPrefab;

	// Use this for initialization
	void Start () {
		for (int x = 0; x < gridSize; x++) {  
			for (int z = 0; z < gridSize; z++) {  
				// セルを作成  
				GameObject obj = Instantiate (cellPrefab) as GameObject;  
				obj.transform.SetParent (transform);  

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
		
	}
}
