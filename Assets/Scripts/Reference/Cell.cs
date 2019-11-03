using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {  
	public bool Living { get; private set; }    // このセルが生存状態か  

	private GameObject Death;    // 死滅時  
	private GameObject Alive;    // 生存時  

	void Awake () {  
		Death = transform.FindChild ("Death").gameObject;  
		Alive = transform.FindChild ("Alive").gameObject;  

		Death.SetActive (true);  
		Alive.SetActive (false);  
		Living = false;  
	}  

	// Update is called once per frame  
	void Update () {  
		Living = Alive.activeSelf;  
	}  

	/// <summary>  
	/// 誕生  
	/// </summary>  
	public void Birth() {  
		Death.SetActive (false);  
		Alive.SetActive (true);  
	}  

	/// <summary>  
	/// 死滅  
	/// </summary>  
	public void Die() {  
		Death.SetActive (true);  
		Alive.SetActive (false);  
	}  

} 
