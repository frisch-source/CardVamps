using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsPowerUp : MonoBehaviour
{
    [SerializeField] GameObject upArrow;
    [SerializeField] GameObject downArrow;
    [SerializeField] GameObject rightArrow;
    [SerializeField] GameObject leftArrow;

    private bool clicked = false;
    bool onCard = false;
    int[] neighbors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;
            Vector3 newPos = new Vector3(mousePos.x, mousePos.y, -2);
            transform.position = newPos;
        }
    }

    private void OnMouseDown()
    {
        if (!clicked && !onCard)
        {
            clicked = true;
        }
        else if (clicked && !onCard)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 newPos;
            //newPos = board.transform.InverseTransformVector(mousePos);
            newPos = new Vector3(mousePos.x, mousePos.y, -1);
            //Debug.Log(newPos);
            //Debug.Log(GameObject.FindGameObjectWithTag("Card").transform.position);
            //Vector2 testPos = new Vector2(0,0);
            //Debug.Log(Physics2D.OverlapPointAll(newPos)[1]);
            Collider2D[] overlaps = Physics2D.OverlapPointAll(newPos);
            Card overlapCard;
            if (overlaps.Length > 1)
            {
                overlapCard = overlaps[1].GetComponent<Card>();
                int cardVal = overlapCard.GetValue();
                Debug.Log(cardVal);
                clicked = false;
                onCard = true;
                

                Vector3 cardPos = overlaps[1].GetComponent<Transform>().position;
                cardPos = new Vector3(cardPos.x, cardPos.y, -2);
                //xdist = 1.5   ydist = 2
                Debug.Log(cardPos);
                neighbors = GetNeighbors(cardPos, cardVal);

                for (int i = 0; i < neighbors.Length; i++)
                {
                    if (neighbors[i] != 0)
                    {

                    }
                }
                

                transform.position = cardPos;
            }
        }
    }

    private int[] GetNeighbors(Vector3 cardPos, int cardVal)
    {
        int upVal = 0;
        int downVal = 0;
        int leftVal = 0;
        int rightVal = 0;
        

        //Up
        Vector3 upPos = new Vector3(cardPos.x, cardPos.y + 2, cardPos.z);
        Collider2D[] overlaps = Physics2D.OverlapPointAll(upPos);
        Card upCard;
        if (overlaps.Length > 0)
        {
            upCard = overlaps[0].GetComponent<Card>();
            upVal = upCard.GetValue();
            upArrow.transform.position = new Vector3(cardPos.x, cardPos.y + 1, cardPos.z);
            if (upVal > cardVal)
            {
                upArrow.transform.rotation = downArrow.transform.rotation;
            }
        }

        //Left
        Vector3 leftPos = new Vector3(cardPos.x-1.5f, cardPos.y, cardPos.z);
        overlaps = Physics2D.OverlapPointAll(leftPos);
        Card leftCard;
        if (overlaps.Length > 0)
        {
            leftCard = overlaps[0].GetComponent<Card>();
            leftVal = leftCard.GetValue();
            leftArrow.transform.position = new Vector3(cardPos.x - 0.75f, cardPos.y, cardPos.z);
            if (leftVal > cardVal)
            {
                leftArrow.transform.rotation = rightArrow.transform.rotation;
            }

        }

        //Right
        Vector3 rightPos = new Vector3(cardPos.x+1.5f, cardPos.y, cardPos.z);
        overlaps = Physics2D.OverlapPointAll(rightPos);
        Card rightCard;
        if (overlaps.Length > 0)
        {
            rightCard = overlaps[0].GetComponent<Card>();
            rightVal = rightCard.GetValue();
            rightArrow.transform.position = new Vector3(cardPos.x + 0.75f, cardPos.y, cardPos.z);
            if (rightVal > cardVal)
            {
                rightArrow.transform.rotation = leftArrow.transform.rotation;
            }
        }

        //Down
        Vector3 downPos = new Vector3(cardPos.x, cardPos.y-2, cardPos.z);
        
        overlaps = Physics2D.OverlapPointAll(downPos);
        //Debug.Log(overlaps[0]);
        Card downCard;
        if (overlaps.Length > 0)
        {
            downCard = overlaps[0].GetComponent<Card>();
            downVal = downCard.GetValue();
            downArrow.transform.position = new Vector3(cardPos.x, cardPos.y - 1, cardPos.z);
            if (downVal > cardVal)
            {
                downArrow.transform.rotation = upArrow.transform.rotation;
            }
        }

        int[] neighborVals = new int[] { upVal, leftVal, rightVal, downVal };

        return neighborVals;
    }
}
