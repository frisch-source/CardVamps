using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlusPowerUp : MonoBehaviour
{
    [SerializeField] Sprite two;
    [SerializeField] Sprite three;
    [SerializeField] Sprite four;
    [SerializeField] Sprite five;
    [SerializeField] Sprite six;
    [SerializeField] Sprite seven;
    [SerializeField] Sprite eight;
    [SerializeField] Sprite nine;

    Sprite[] onCardForms = new Sprite[8];
    [SerializeField] Sprite powerDisabledSprite;
    [SerializeField] Sprite powerEnabledSprite;
    BoxCollider2D powerCollider;
    Sprite onCardSprite;
    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    GameObject board;
    bool powerEnabled = false;
    private bool clicked = false;
    bool onCard = false;
    Vector3 originalPos;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        board = GameObject.FindGameObjectWithTag("Board");
        powerCollider = GetComponent<BoxCollider2D>();
        onCardForms = new Sprite[] { two, three, four, five, six, seven, eight, nine };
        originalPos = transform.position;
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

    private void EnablePowerup()
    {
        spriteRenderer.sprite = powerEnabledSprite;
    }

    public void DisablePowerup() 
    {
        spriteRenderer.sprite = powerDisabledSprite;
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
                ChangeToOnCardSprite(cardVal);
                
                Vector3 cardPos = overlaps[1].GetComponent<Transform>().position;
                cardPos = new Vector3(cardPos.x, cardPos.y, -2);
                transform.position = cardPos;
            }
        }
    }

    private void ChangeToOnCardSprite(int value)
    {
        if (value == 10)
        {
            spriteRenderer.sprite = onCardForms[7];
        }
        else 
        {
            int ran = Random.Range(2,value);
            Sprite newSprite = onCardForms[ran-2];
            spriteRenderer.sprite = newSprite;
        }
    }
}
