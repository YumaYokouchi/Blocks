using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour {

	public Text count;
	public HumanType type;
	public GameObject[] enemies;
	public GameObject[] friends;
	public Vector3 target;

	public int horizonID;
	public int verticalID;
	public int humaCostID;
	public int humaMovementID;
	public int humanStrengthID;

	public GameObject attackEnemy;

	public bool isFirst;

	public FieldManager fieldmanager;
	// 敵の座標
	int checkHorizonID;
	int checkVerticalID;

	float minAngle = 0.0F;
	float maxAngle = 90.0F;

	public Material[] _material;           // 割り当てるマテリアル.

	void Awake(){
		humaCostID = 3;
	}


	// Use this for initialization
	void Start () {
		fieldmanager = FindObjectOfType<FieldManager> ();

		if (type == HumanType.ONE) {

			Debug.Log ("spawn");

			count.color = new Color(0, 0, 1, 1);

			transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material=_material[0];

			target = new Vector3 (horizonID + 1, 0, verticalID);
		}
		if(type == HumanType.TWO){

			count.color = new Color(1, 0, 0, 1);

			transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material=_material[1];

			target = new Vector3 (horizonID - 1, 0, verticalID);
		}
		isFirst = true;
		CheckTarget ();
		GameManager.instance.endAction = this.MoveEnemies();

		Invoke ("Action",1f);
	}
	
	// Update is called once per frame
	void Update () {

		//ライフがなくなったら自身を消滅する
		if (humanStrengthID <1){
			if (type == HumanType.ONE){
				fieldmanager.oneList.Remove (this.gameObject);

			}
			if (type == HumanType.TWO){
				fieldmanager.twoList.Remove (this.gameObject);
			}
			Destroy(this.gameObject,1f);

		}

		if (type == HumanType.ONE) {
			enemies = GameObject.FindGameObjectsWithTag ("TWO");
			friends = GameObject.FindGameObjectsWithTag ("ONE");
		}
		if(type == HumanType.TWO){
			enemies = GameObject.FindGameObjectsWithTag ("ONE");
			friends = GameObject.FindGameObjectsWithTag ("TWO");
		}
		this.count.text = humanStrengthID.ToString ();
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
					target = new Vector3 (checkHorizonID,0,checkVerticalID);
				}
			}
		}
		if (this.type == HumanType.TWO) {
			 checkHorizonID = this.horizonID;
			 checkVerticalID = this.verticalID - 1;
			for (int i = 0; i < enemies.Length; i++) {
				if(checkHorizonID == enemies[i].GetComponent<Human>().horizonID && checkVerticalID == enemies[i].GetComponent<Human>().verticalID){
					target = new Vector3 (checkHorizonID,0,checkVerticalID);
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
					target = new Vector3 (checkHorizonID,0,checkVerticalID);
				}
			}
		}
		if (this.type == HumanType.TWO) {
			 checkHorizonID = this.horizonID;
			 checkVerticalID = this.verticalID + 1;
			for (int i = 0; i < enemies.Length; i++) {
				if(checkHorizonID == enemies[i].GetComponent<Human>().horizonID && checkVerticalID == enemies[i].GetComponent<Human>().verticalID){
					target = new Vector3 (checkHorizonID,0,checkVerticalID);
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
					target = new Vector3 (checkHorizonID,0,checkVerticalID);
				}
			
			}
		}
		if (this.type == HumanType.TWO) {
			 checkHorizonID = this.horizonID - 1;
			 checkVerticalID = this.verticalID;
			for (int i = 0; i < enemies.Length; i++) {
				if(checkHorizonID == enemies[i].GetComponent<Human>().horizonID && checkVerticalID == enemies[i].GetComponent<Human>().verticalID){
					target = new Vector3 (checkHorizonID,0,checkVerticalID);
				}
			}
		}
	}

	public void Action(){
		Move ();

		GameManager.instance.state = GameState.CHOOSE;
		//GameManager.instance.isOneTurn = !GameManager.instance.isOneTurn;
	}

	public void MoveEnemies(){

		if (enemies.Length == 0)
			return;
		
		for(int i = 0; i < enemies.Length; i++)
		{

			enemies [i].GetComponent<Human> ().CheckTarget ();
			enemies [i].GetComponent<Human> ().Move ();
		}
	}

	public void Move(){
		if (isFirst == true) {
			this.transform.position = target;
			this.horizonID = checkHorizonID;
			this.verticalID = checkVerticalID;
			isFirst = false;
			attackEnemy = null;
			for(int i = 0; i < enemies.Length; i++)
			{
				if (enemies [i].GetComponent<Human> ().horizonID == checkHorizonID && enemies [i].GetComponent<Human> ().verticalID == checkVerticalID) {
					attackEnemy = enemies [i].gameObject;
				}
			}
			Invoke ("Attack",1f);
		} else {
			if (this.type == HumanType.ONE) {



				this.transform.position += new Vector3 (1, 0, 0);
				this.horizonID++;
				attackEnemy = null;
				for(int i = 0; i < enemies.Length; i++)
				{
					if (enemies [i].GetComponent<Human> ().horizonID == checkHorizonID && enemies [i].GetComponent<Human> ().verticalID == checkVerticalID) {
						attackEnemy = enemies [i].gameObject;
					}
				}
			} 
			else {
				

				this.transform.position -= new Vector3 (1, 0, 0);
				this.horizonID--;
				attackEnemy = null;
				for(int i = 0; i < enemies.Length; i++)
				{
					if (enemies [i].GetComponent<Human> ().horizonID == checkHorizonID && enemies [i].GetComponent<Human> ().verticalID == checkVerticalID) {
						attackEnemy = enemies [i].gameObject;
					}
				}

			}
			Invoke ("Attack",1f);
		}


	}

	public void Attack(){
		
		Debug.Log (horizonID);
		if(this.type == HumanType.ONE && horizonID >= 3){
			
			GameManager.instance.twoCastleHP -= humanStrengthID;
			fieldmanager.oneList.Remove (this.gameObject);
			Destroy (this.gameObject,1f);
			return;		
		}
		if(this.type == HumanType.TWO && horizonID <= -3){
			GameManager.instance.oneCastleHP -= humanStrengthID;
			fieldmanager.twoList.Remove (this.gameObject);
			Destroy (this.gameObject,1f);
			return;		
		}

		if (attackEnemy == null) {
			return;

		}

		int atk = attackEnemy.GetComponent<Human> ().humanStrengthID;
		attackEnemy.GetComponent<Human> ().humanStrengthID = atk - humanStrengthID;
		humanStrengthID = humanStrengthID - atk;
		//attackEnemy.GetComponent<Human> ().humanStrengthID = attackEnemy.GetComponent<Human> ().humanStrengthID - humanStrengthID;
	}

	public void Turn (){
		float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
		transform.eulerAngles = new Vector3(0, angle, 0);
	}
	public void ReceveDamage(){
		
	}
}

public enum HumanType{
	ONE,
	TWO
}
