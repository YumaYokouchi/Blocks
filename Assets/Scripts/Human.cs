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

	// Use this for initialization
	void Start () {
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

	public void Action(){
		//Move ();
		for(int i = 0; i < friends.Length; i++)
		{
			friends [i].GetComponent<Human> ().Move ();
		}
		GameManager.instance.state = GameState.CHOOSE;
		GameManager.instance.isOneTurn = !GameManager.instance.isOneTurn;
	}

	public void Move(){
		if (this.type == HumanType.ONE) {
			this.transform.position += new Vector3 (1, 0, 0);
		}else{
			this.transform.position -= new Vector3 (1, 0, 0);
		}



	}
}

public enum HumanType{
	ONE,
	TWO
}
