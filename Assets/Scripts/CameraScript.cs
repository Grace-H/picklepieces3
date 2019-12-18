using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	
	public SpriteRenderer board;

	void Start () {
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float targetRatio = board.bounds.size.x / board.bounds.size.y;

		if(screenRatio >= targetRatio){
			Camera.main.orthographicSize = board.bounds.size.y / 2;
		}else{
			float differenceInSize = targetRatio / screenRatio;
			Camera.main.orthographicSize = board.bounds.size.y / 2 * differenceInSize;
		}
	}
	

}
