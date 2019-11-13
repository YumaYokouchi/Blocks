using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierColorScript : MonoBehaviour {

	public Material[] _material;           // 割り当てるマテリアル.
	private int i;

	// Use this for initialization
	void Start () {

		i = 0; //マテリアル変更用の棚数.

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.Space)) {
			i++;
			if (i == 2) {
				i = 0;
			}

			this.GetComponent<Renderer>().material=_material[i];
		} 


	}
}
