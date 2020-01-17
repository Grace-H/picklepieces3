using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHand : MonoBehaviour {

	private GameObject[] hand;
	private GameObject[,] cells;
	private BoardChecker boardChecker;
	private TileDistributor tileDistributor;
	
	private int[,] xyz = new int[80, 3]{
		{-74, 40, 1},
		{-68, 40, 1},
		{-62, 40, 1},
		{-56, 40, 1},
		{-50, 40, 1},
		
		{-74, 34, 1},
		{-68, 34, 1},
		{-62, 34, 1},
		{-56, 34, 1},
		{-50, 34, 1},
		
		{-74, 28, 1},
		{-68, 28, 1},
		{-62, 28, 1},
		{-56, 28, 1},
		{-50, 28, 1},
		
		{-74, 22, 1},
		{-68, 22, 1},
		{-62, 22, 1},
		{-56, 22, 1},
		{-50, 22, 1},

		{-74, 16, 1},
		{-68, 16, 1},
		{-62, 16, 1},
		{-56, 16, 1},
		{-50, 16, 1},
		
		{-74, 10, 1},
		{-68, 10, 1},
		{-62, 10, 1},
		{-56, 10, 1},
		{-50, 10, 1},
		
		{-74, 4, 1},
		{-68, 4, 1},
		{-62, 4, 1},
		{-56, 4, 1},
		{-50, 4, 1},
		
		{-74, -2, 1},
		{-68, -2, 1},
		{-62, -2, 1},
		{-56, -2, 1},
		{-50, -2, 1},
		
		{-74, -8, 1},
		{-68, -8, 1},
		{-62, -8, 1},
		{-56, -8, 1},
		{-50, -8, 1},
		
		{-74, -14, 1},
		{-68, -14, 1},
		{-62, -14, 1},
		{-56, -14, 1},
		{-50, -14, 1},
		
		{-74, -20, 1},
		{-68, -20, 1},
		{-62, -20, 1},
		{-56, -20, 1},
		{-50, -20, 1},
		
		{-74, -26, 1},
		{-68, -26, 1},
		{-62, -26, 1},
		{-56, -26, 1},
		{-50, -26, 1},
		
		{-74, -32, 1},
		{-68, -32, 1},
		{-62, -32, 1},
		{-56, -32, 1},
		{-50, -32, 1},
		
		{-74, -38, 1},
		{-68, -38, 1},
		{-62, -38, 1},
		{-56, -38, 1},
		{-50, -38, 1},
		
		{-74, -44, 1},
		{-68, -44, 1},
		{-62, -44, 1},
		{-56, -44, 1},
		{-50, -44, 1},
		
		{-74, -50, 1},
		{-68, -50, 1},
		{-62, -50, 1},
		{-56, -50, 1},
		{-50, -50, 1},
	};
	
	//modal window
	private ModalPanel modalPanel;
	private UnityAction okayErrorAction;
	private UnityAction dealOneTileAction;
	private UnityAction returnTilesAction;
	
	void Start () {
		hand = new GameObject[18];  //change back to 18
		tileDistributor = TileDistributor.Instance();
		
		//modal planel and options
		modalPanel = ModalPanel.Instance();
		okayErrorAction = new UnityAction(ModalPanelErrorOkayAction);
		dealOneTileAction = new UnityAction(TakeOneTile);
		returnTilesAction = new UnityAction(ReturnTilesAction);
		
		//fill hand randomly
		for(int i = 0; i < hand.Length; i++){
			hand[i] = tileDistributor.DealTile();
		}
		
		/*use the following to deal a specific hand
		hand[0] = tileDistributor.DealSpecificTile('M');
		hand[1] = tileDistributor.DealSpecificTile('E');
		hand[2] = tileDistributor.DealSpecificTile('L');
		hand[3] = tileDistributor.DealSpecificTile('T');
		hand[4] = tileDistributor.DealSpecificTile('S');
		hand[5] = tileDistributor.DealSpecificTile('P');
		hand[6] = tileDistributor.DealSpecificTile('U');
		hand[7] = tileDistributor.DealSpecificTile('N');
		hand[8] = tileDistributor.DealSpecificTile('Q');
		hand[9] = tileDistributor.DealSpecificTile('I');
		hand[10] = tileDistributor.DealSpecificTile('E');
		hand[11] = tileDistributor.DealSpecificTile('T');
		hand[12] = tileDistributor.DealSpecificTile('E');
		hand[13] = tileDistributor.DealSpecificTile('R');
		hand[14] = tileDistributor.DealSpecificTile('B');
		hand[15] = tileDistributor.DealSpecificTile('A');
		hand[16] = tileDistributor.DealSpecificTile('R');
		hand[17] = tileDistributor.DealSpecificTile('L');
		hand[18] = tileDistributor.DealSpecificTile('A');
		hand[19] = tileDistributor.DealSpecificTile('C');
		hand[20] = tileDistributor.DealSpecificTile('K');
		hand[21] = tileDistributor.DealSpecificTile('D');
		hand[22] = tileDistributor.DealSpecificTile('N');
		hand[23] = tileDistributor.DealSpecificTile('O');
		hand[24] = tileDistributor.DealSpecificTile('D');
		hand[25] = tileDistributor.DealSpecificTile('S');
		hand[26] = tileDistributor.DealSpecificTile('O');
		hand[27] = tileDistributor.DealSpecificTile('W');
		
		*/
		
		
		//get boardchecker component
		boardChecker = gameObject.GetComponent(typeof(BoardChecker)) as BoardChecker;
		PlaceHand();
		GetCells();
	}
	
	/*Loads parent cells into array "cells"
	called from Start()
	*/
	void GetCells(){
		cells = new GameObject[16,25];
		GameObject[] rows = GameObject.FindGameObjectsWithTag("Row");
		rows = SortArray(rows);
		for(int i = 0; i < rows.Length; i++){
			//Debug.Log("Childcount: " + rows[i].transform.childCount);
			GameObject[] children = new GameObject[rows[i].transform.childCount];
			for(int j = 0; j < children.Length; j++){
				children[j] = rows[i].transform.GetChild(j).gameObject;
			}
			//Debug.Log(children.Length);
			children = SortArray(children);
			Print1DArray(children);
			for(int j = 0; j < children.Length; j++){
				cells[15 - i,j] = children[j];
			}
		}
	}
	
	/*uses QuickSort to sort array of game objects by their order in the heirarchy
	This is a modified version of the method from Sort.java, written by Grace Hunter
	*/
	private GameObject[] SortArray(GameObject[] narray) {
		
		//if longer than one number
		if (narray.Length > 1) {
			GameObject pivot = narray[narray.Length - 1];
			int wall = 0; //where the divider is in the array
			//main loop which moves all objects with indeces greater than pivot to left and all numbers less to right
			for (int i = 0; i < narray.Length; i++) { //for each number in the string...
				if (narray[i].transform.GetSiblingIndex() <= pivot.transform.GetSiblingIndex()) { //if the gameobject index is less than or equal to the pivot,
					//swap it with the number next to the wall
					GameObject original = narray[i];
					narray[i] = narray[wall];
					narray[wall] = original;
					wall++; //move the wall over one
				}
			}
			
			//sort half before wall
			GameObject[] firstHalf = new GameObject[wall - 1];
			System.Array.Copy(narray, 0, firstHalf, 0, wall - 1);
			firstHalf = SortArray(firstHalf);
			
			//make firstHalf match the first half of numbers
			for (int i = 0; i < firstHalf.Length; i++) {
				narray[i] = firstHalf[i];
			}
			
			//sort half after wall
			GameObject[] secondHalf = new GameObject[narray.Length - wall];
			System.Array.Copy(narray, wall, secondHalf, 0, narray.Length - wall);
			secondHalf = SortArray(secondHalf);
			//make secondHalf match half after wall in numbers
			for (int i = 0; i < secondHalf.Length; i++) {
				narray[i + wall] = secondHalf[i];
			}
		}
		return narray;
	}
	
	/*used for checking 'GetCells()' function, prints o for occupied cell, i for unoccupied"
	*/
	void PrintCells(){
		string output = "";
		for(int i = 0; i < 16; i++){
			for(int j = 0; j < 25; j++){
				if(cells[i,j] != null){
					output += 'o';
				}
				else{
					output += "i";
				}
			}
			output += "\n";
		}
		//Debug.Log(output);
	}
	
	/*Prints letters in player's hand to console*/
	void PrintHand(){
		string output = "PlayerHand: ";
		for(int i = 0; i < hand.Length; i++){
			LetterValue letter = hand[i].GetComponent(typeof(LetterValue)) as LetterValue;
			if(letter != null){
				output += letter.GetLetter();
			}
			else{
				output += ',';
			}
		}
		//Debug.Log(output);
	}
	
	/*places tiles in locations set in coordinates array*/
	void PlaceHand(){
		for(int i = 0; i < hand.Length; i++){
			//hand[i].Scale()
			hand[i].transform.position = new Vector3(xyz[i,0], xyz[i,1], xyz[i,2]);
			Tile tile = hand[i].GetComponent(typeof(Tile)) as Tile;
			tile.SetStartPosition(new Vector3(xyz[i,0], xyz[i,1], xyz[i,2]));

		}
	}
	
	//not sure what this does
	public void LoadTileValues(){
		for(int i = 0; i < hand.Length; i++){
			Vector3 position = hand[i].transform.position;
			//Debug.Log(position);
		}
	}
	
	/*Checks board that player is using*/
	public void CheckBoard(){
		char[,] boardLetters = new char[16,25];
		int totalLetters = 0;
		
		//ask each cell if it has a child, if so get the letter
		for(int i = 0; i < 16; i++){
			for(int j = 0; j < 25; j++){
				//Debug.Log("Index: " + cells[i,j].transform.GetSiblingIndex());
				//if the cell has a child
				if(cells[i,j].transform.childCount > 0){
					GameObject child = cells[i,j].transform.GetChild(0).gameObject;
					//get letter
					LetterValue letterObj = child.GetComponent(typeof(LetterValue)) as LetterValue;
					if(letterObj != null){
						boardLetters[i,j] = letterObj.GetLetter();
						totalLetters++;
					}
					else{
						boardLetters[i,j] = ' ';
					}
				}
				else{
					boardLetters[i,j] = ' ';
				}
			}
		}
		
		//if not all letters were played
		if(totalLetters < hand.Length){
			Debug.Log("Not all tiles were played.");
			modalPanel.Choice("You must play all tiles.", okayErrorAction);
		}
		else{
			//check board
			int error = boardChecker.CheckBoard(boardLetters, 16, 25);
			Debug.Log("BoardChecker returned: " + error);
			//if board is all good
			if(error == 0){
				Debug.Log("Bag count: " + tileDistributor.GetBagCount());
				if(tileDistributor.GetBagCount() <= 1){
					Debug.Log("You win!");
					modalPanel.Choice("You win!\nYou have used all tiles.", okayErrorAction);
				}
				else{
					modalPanel.Choice("Good job! Here's another tile!", dealOneTileAction);
				}
			}
			//disconnected tile
			else if(error == 1){
				modalPanel.Choice("It looks like you have a disconnected word!", okayErrorAction);
			}
			//wrong word
			else if(error == 2){
				modalPanel.Choice("Oops...you misspelled a word.", okayErrorAction);
			}
		}
		Print2DArray(boardLetters);
	}
	
	/*called when modal panel "okay" clicked for user errors on board*/
	void ModalPanelErrorOkayAction(){
		//Debug.Log("Okay");
	}
	
	/*prints out 2D board to console*/
	void Print2DArray(char[,] board){
		string output = "BOARD: \n";
		Debug.Log("PlayerHand.cs: board length = " + board.Length);
		for(int i = 0; i < 16; i ++){
			for(int j = 0; j < 25; j ++){
				if( board[i, j] == ' '){
					output += " ,";
				}
				else{					
					output += board[i, j];
					output += ",";
				}
			}
			output += "\n";
		}
		//Debug.Log(output);
	}
	
	//prints child indexes of long array of GameObject
	void Print1DArray(GameObject[] narray){
		string output = "Here is the array this is a long line of text: ";
		for(int i = 0; i < narray.Length; i++){
			output += narray[i].transform.GetSiblingIndex();
			output += ",";
		}
		//Debug.Log(output);
	}
	
	//draw one tile from the bag
	public void TakeOneTile(){
		Debug.Log("taking one tile");
		
		if(hand.Length < xyz.Length){

			//creates new hand object
			GameObject[] nhand = new GameObject[hand.Length + 1];
			//copies each tile into the new hand
			for(int i = 0; i < hand.Length; i++){
				nhand[i] = hand[i];
			}
			//puts a new tile into new empty spot in the new hand
			nhand[nhand.Length - 1] = tileDistributor.DealTile(); //has problem when there is only one tile in the bag
			//places the new tile
			nhand[nhand.Length - 1].transform.position = new Vector3(xyz[nhand.Length - 1,0], xyz[nhand.Length - 1,1], xyz[nhand.Length - 1,2]);
			//sets starting position of the new tile
			Tile tile = nhand[nhand.Length - 1].GetComponent(typeof(Tile)) as Tile;
			tile.SetStartPosition(new Vector3(xyz[nhand.Length - 1,0], xyz[nhand.Length - 1,1], xyz[nhand.Length - 1,2]));
			
			//replace previous hand with new hand
			hand = nhand;
		}
		else{
			Debug.LogError("Hand is too large. Cannot draw tile");
		}
	}
	
	//replace selected tile with three new ones
	public void Dump()
	{
		/*if(tileDistributor.GetBagCount() > 2){
			int foundTile = 0;
			//creates new hand for dump
			GameObject[] dumphand = new GameObject[hand.Length + 2];
			//copies each tile from hand into the new hand
			for(int i = 0; i < hand.Length; i++)
			{
				dumphand[i] = hand[i];
			}
		*/
			//hand = dumphand;
		
			for(int i = 0; i < hand.Length; i++)
			{
				if(hand[i] != null)
				{
					Tile t = hand[i].GetComponent(typeof(Tile)) as Tile;
					if(t.selected == true)
					{//there is a selected tile
						if(tileDistributor.GetBagCount() > 2)
						{
							t.Deselect();
							int foundTile = 0;
							//creates new hand for dump
							GameObject[] dumphand = new GameObject[hand.Length + 2];
							//copies each tile from hand into the new hand
							for(int j = 0; j < hand.Length; j++)
							{
								dumphand[j] = hand[j];
							}

							hand = dumphand;


							foundTile = i;
							hand[i].transform.position = new Vector3(100,100,0);
							tileDistributor.AddTile(hand[i]);
							hand[i] = null;
							//first new tile
		
							//places tile into spot in hand where removed tile was
							hand[foundTile] = tileDistributor.DealTile();
							//places the new tile
							hand[foundTile].transform.position = new Vector3(xyz[foundTile,0], xyz[foundTile,1], xyz[foundTile,2]);
							//sets starting position of new tile
							Tile tile = hand[foundTile].GetComponent(typeof(Tile)) as Tile;
							tile.SetStartPosition(new Vector3(xyz[foundTile,0], xyz[foundTile,1], xyz[foundTile,2]));
		
							//second new tile

							//puts a new tile into new empty spot in the new hand
							hand[hand.Length - 2] = tileDistributor.DealTile();
							//places the new tile
							hand[hand.Length - 2].transform.position = new Vector3(xyz[hand.Length - 2,0], xyz[hand.Length - 2,1], xyz[hand.Length - 2,2]);
							//sets starting position of the new tile
							tile = hand[hand.Length - 2].GetComponent(typeof(Tile)) as Tile;
							tile.SetStartPosition(new Vector3(xyz[hand.Length - 2,0], xyz[hand.Length - 2,1], xyz[hand.Length - 2,2]));

							//third new tile
		
							//puts a new tile into new empty spot in the new hand
							hand[hand.Length - 1] = tileDistributor.DealTile();
							//places the new tile
							hand[hand.Length - 1].transform.position = new Vector3(xyz[hand.Length - 1,0], xyz[hand.Length - 1,1], xyz[hand.Length - 1,2]);
							//sets starting position of the new tile
							tile = hand[hand.Length - 1].GetComponent(typeof(Tile)) as Tile;
							tile.SetStartPosition(new Vector3(xyz[hand.Length - 1,0], xyz[hand.Length - 1,1], xyz[hand.Length - 1,2]));
						}
						else
						{
							modalPanel.Choice("Not enough tiles to dump!", okayErrorAction);
						}
					}
				}
		}
		//else
		//{
			//modalPanel.Choice("Not enough tiles to dump!", okayErrorAction);
		//}
	}

	
	//Returns tiles from the board into the hand area
	public void ReturnFromBoard() 
	{
		modalPanel.Choice("Are you sure you want to return all tiles to your hand?", returnTilesAction, okayErrorAction);
	}

	private void ReturnTilesAction(){
		for(int i = 0; i < hand.Length; i++)
		{
			Tile tile = hand[i].GetComponent(typeof(Tile)) as Tile;
			hand[i].transform.position = tile.GetStartPosition();
		}	
	}

	// Update is called once per frame
	void Update () {
	}
}
