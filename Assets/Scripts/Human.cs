using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour {

	public int humanCount;
	public Text count;
	public HumanType type;
	public GameObject[] enemies;
	public GameObject[] friends;
	public Vector3 target;
	public int horizonID;
	public int verticalID;
	public bool isFirst;
	int checkHorizonID;
	int checkVerticalID;

	public Material[] _material;           // 割り当てるマテリアル.


	// Use this for initialization
	void Start () {



		if (type == HumanType.ONE) {

			count.color = new Color(0, 0, 1, 1);

			transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material=_material[0];

			target = new Vector3 (horizonID + 0.5f, 0, verticalID - 0.5f);
		}
		if(type == HumanType.TWO){

			count.color = new Color(1, 0, 0, 1);

			transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material=_material[1];

			target = new Vector3 (horizonID - 1.5f, 0, verticalID - 0.5f);
		}
		isFirst = true;
		CheckTarget ();
		Invoke ("Action",2f);
	}
	
	// Update is called once per frame
	void Update () {

		if (type == HumanType.ONE) {
			enemies = GameObject.FindGameObjectsWithTag ("TWO");
			friends = GameObject.FindGameObjectsWithTag ("ONE");
		}
		if(type == HumanType.TWO){
			enemies = GameObject.FindGameObjectsWithTag ("ONE");
			friends = GameObject.FindGameObjectsWithTag ("TWO");
		}
		this.count.text = humanCount.ToString ();
	}

	public void CheckTarget(){
		CheckLeft ();
		CheckRight ();
		CheckFoward ();
	}

	public void CheckLeft(){
		if (this.type == HumanType.ONE) {
			 checkHorizonID = this.horizonID;
			 checkVerticalID = this.verticalID + 1;
			for (int i = 0; i < enemies.Length; i++) {
				if(checkHorizonID == enemies[i].GetComponent<Human>().horizonID && checkVerticalID == enemies[i].GetComponent<Human>().verticalID){
					target = new Vector3 (checkHorizonID - 0.5f,0,checkVerticalID - 0.5f);
				}
			}
		}
		if (this.type == HumanType.TWO) {
			 checkHorizonID = this.horizonID;
			 checkVerticalID = this.verticalID - 1;
			for (int i = 0; i < enemies.Length; i++) {
				if(checkHorizonID == enemies[i].GetComponent<Human>().horizonID && checkVerticalID == enemies[i].GetComponent<Human>().verticalID){
					target = new Vector3 (checkHorizonID - 0.5f,0,checkVerticalID - 0.5f);
				}
			}
		}

	}

	public void CheckRight(){
		if (this.type == HumanType.ONE) {
			 checkHorizonID = this.horizonID;
			 checkVerticalID = this.verticalID - 1;
			for (int i = 0; i < enemies.Length; i++) {
				if(checkHorizonID == enemies[i].GetComponent<Human>().horizonID && checkVerticalID == enemies[i].GetComponent<Human>().verticalID){
					target = new Vector3 (checkHorizonID - 0.5f,0,checkVerticalID - 0.5f);
				}
			}
		}
		if (this.type == HumanType.TWO) {
			 checkHorizonID = this.horizonID;
			 checkVerticalID = this.verticalID + 1;
			for (int i = 0; i < enemies.Length; i++) {
				if(checkHorizonID == enemies[i].GetComponent<Human>().horizonID && checkVerticalID == enemies[i].GetComponent<Human>().verticalID){
					target = new Vector3 (checkHorizonID - 0.5f,0,checkVerticalID - 0.5f);
				}
			}
		}
	}

	public void CheckFoward(){
		if (this.type == HumanType.ONE) {
			 checkHorizonID = this.horizonID + 1;
			 checkVerticalID = this.verticalID;
			for (int i = 0; i < enemies.Length; i++) {
				if(checkHorizonID == enemies[i].GetComponent<Human>().horizonID && checkVerticalID == enemies[i].GetComponent<Human>().verticalID){
					target = new Vector3 (checkHorizonID - 0.5f,0,checkVerticalID - 0.5f);
				}
			}
		}
		if (this.type == HumanType.TWO) {
			 checkHorizonID = this.horizonID - 1;
			 checkVerticalID = this.verticalID;
			for (int i = 0; i < enemies.Length; i++) {
				if(checkHorizonID == enemies[i].GetComponent<Human>().horizonID && checkVerticalID == enemies[i].GetComponent<Human>().verticalID){
					target = new Vector3 (checkHorizonID - 0.5f,0,checkVerticalID - 0.5f);
				}
			}
		}
	}

	public void Action(){
		//Move ();
		for(int i = 0; i < friends.Length; i++)
		{
			friends [i].GetComponent<Human> ().CheckTarget ();
			friends [i].GetComponent<Human> ().Move ();
		}
		GameManager.instance.state = GameState.CHOOSE;
		GameManager.instance.isOneTurn = !GameManager.instance.isOneTurn;
	}

	public void Move(){
		if (isFirst == true) {
			this.transform.position = target;
			this.horizonID = checkHorizonID;
			this.verticalID = checkVerticalID;
			isFirst = false;
		} else {
			if (this.type == HumanType.ONE) {
				this.transform.position += new Vector3 (1, 0, 0);
				this.horizonID++;
			} else {
				this.transform.position -= new Vector3 (1, 0, 0);
				this.horizonID--;
			}
		}


	}
}

public enum HumanType{
	ONE,
	TWO
}
