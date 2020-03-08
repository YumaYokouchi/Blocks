using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public Text oneCostText;
	public Text twoCostText;
	public int oneCost;
	public int twoCost;

	public Text oneHP;
	public Text twoHP;
	public int oneCastleHP;
	public int twoCastleHP;

	public int oneNumber;
	public int twoNumber;

	public Text oneTurnEnd;
	public Text twoTurnEnd;

	public bool isOneTurn;
	public GameState state;

	public Action endAction;

	public void Awake(){
		instance = this;
	}

	public void Start(){

		oneCost = 3;
		twoCost = 4;

		isOneTurn = true;
		state = GameState.CHOOSE;

	}

	public void Update (){


		oneCostText.text = oneCost.ToString ();
		twoCostText.text = twoCost.ToString ();

		oneHP.text = oneCastleHP.ToString ();
		twoHP.text = twoCastleHP.ToString ();
	}

	public void OnOneEndClick(){
		isOneTurn = false;

		oneTurnEnd.gameObject.SetActive(true);
		twoTurnEnd.gameObject.SetActive(false);

		endAction ();
		oneCost ++;

	}
	public void OnTwoEndClick(){
		isOneTurn = true;

		oneTurnEnd.gameObject.SetActive(false);
		twoTurnEnd.gameObject.SetActive(true);
		endAction ();
		twoCost++;
	}
}

public enum GameState{
	CHOOSE,
	ACTION
}
