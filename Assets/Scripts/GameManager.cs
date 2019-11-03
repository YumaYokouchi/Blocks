using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public int oneNumber;
	public int twoNumber;
	public bool isOneTurn;
	public GameState state;

	public void Awake(){
		instance = this;
	}

	public void Start(){
		isOneTurn = true;
		state = GameState.CHOOSE;
	}
}

public enum GameState{
	CHOOSE,
	ACTION
}
