﻿using UnityEngine;
using System.Collections;
//code adapted from https://unity.grogansoft.com/word-game-drag-and-snap/
public class InputManager : MonoBehaviour
{
    private bool draggingItem = false;
    private GameObject draggedObject;
    private Vector3 touchOffset;
	private GameObject selectedObject;
	
    void Update()
    {
        if (HasInput)
        {
            DragOrPickUp();
        }
        else
        {
            if (draggingItem)
                DropItem();
        }
    }
    Vector3 CurrentTouchPosition
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;
        if (draggingItem)
        {
            draggedObject.transform.position = inputPosition + touchOffset;
        }
        else
        {
            var layerMask = 1 << 0;
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f, layerMask);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null && hit.transform.tag == "Tile")
                {
                    draggingItem = true;
                    draggedObject = hit.transform.gameObject;
                    touchOffset = (Vector3)hit.transform.position - inputPosition;
                    hit.transform.GetComponent<Tile>().PickUp();
					if(selectedObject)
						selectedObject.transform.GetComponent<Tile>().Deselect();
					selectedObject = draggedObject;
					selectedObject.transform.GetComponent<Tile>().Select();
					
                }
            }
        }
    }
    private bool HasInput
    {
        get
        {
            // returns true if either the mouse button is down or at least one touch is felt on the screen
            return Input.GetMouseButton(0);
        }
    }
    void DropItem()
    {
        draggingItem = false;
        draggedObject.transform.localScale = new Vector3(1, 1, 1);
        draggedObject.GetComponent<Tile>().Drop();
    }
}
