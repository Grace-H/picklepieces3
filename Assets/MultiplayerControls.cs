using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerControls : MonoBehaviour {
	
	public int numPlayers;
	private TileDistributor tileDistributor;
		
	// Use this for initialization
	void Start () {
		numPlayers = 1;
		tileDistributor = TileDistributor.Instance();
		
	}
	
	void SetNumPlayers(int nnumPlayers){
		numPlayers = nnumPlayers;
	}
	
	//returns true and triggers end game behavior
	public bool CheckWin(){
		if(tileDistributor.GetBagCount() < numPlayers){
			return true;
		}
		else{
			return false;
		}
		
		//tell all players
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
