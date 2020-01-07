using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    private Vector3 startingPosition;
    private List<Transform> touchingTiles;
    private Transform myParent;
    private AudioSource audSource;
	private SpriteRenderer m_SpriteRenderer;
	private Color highlight;
	private Color normal;
	public bool selected;

    private void Awake()
    {
        startingPosition = transform.position;
		Debug.Log("I AM AWAKE!");
        touchingTiles = new List<Transform>();
        myParent = transform.parent;
        audSource = gameObject.GetComponent<AudioSource>();
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		highlight = new Color(255, 0, 255);
		normal = new Color(255, 255, 255);
		selected = false;
    }

	public void Select(){
		//change color
		m_SpriteRenderer.color = highlight;
		Debug.Log("Changed color");
		selected = true;
        
	}
	
	public void Deselect(){
		//change color
		m_SpriteRenderer.color = normal;
		Debug.Log("Changed color");
        selected = false;

	}
	
    public void PickUp()
    {
        transform.localScale = new Vector3(1.1f,1.1f,1.1f);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
	
    public void Drop()
    {
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

        Vector3 newPosition;
        if (touchingTiles.Count == 0)
        {
            transform.position = startingPosition;
            transform.parent = myParent;
            return;
        }

        var currentCell = touchingTiles[0];
        if (touchingTiles.Count == 1)
        {
            newPosition = currentCell.position;
        }
        else
        {
            var distance = Vector3.Distance(transform.position, touchingTiles[0].position);
            
            foreach (Transform cell in touchingTiles)
            {
                if (Vector3.Distance(transform.position, cell.position) < distance)
                {
                    currentCell = cell;
                    distance = Vector3.Distance(transform.position, cell.position);
                }
            }
            newPosition = currentCell.position;
        }
        if (currentCell.childCount != 0)
        {
            transform.position = startingPosition;
            transform.parent = myParent;
            return;
        }
        else
        {
            transform.parent = currentCell;
            StartCoroutine(SlotIntoPlace(transform.position, newPosition));
        }
		
		
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Cell") return;
        if (!touchingTiles.Contains(other.transform))
        {
            touchingTiles.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "Cell") return;
        if (touchingTiles.Contains(other.transform))
        {
            touchingTiles.Remove(other.transform);
        }
    }

    IEnumerator SlotIntoPlace(Vector3 startingPos, Vector3 endingPos)
    {
        float duration = 0.1f;
        float elapsedTime = 0;
        audSource.Play();
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPos, endingPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = endingPos;
    }
	
	public void SetStartPosition(Vector3 startingPos){
		startingPosition = startingPos;
	}

	public Vector3 GetStartPosition(){
		return startingPosition;
	}
}
