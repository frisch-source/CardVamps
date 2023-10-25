using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField] Sprite back;
    [SerializeField] Sprite front;
    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    private int ante = 1;
    
    bool faceDown = true;

    bool isVamp = false;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void OnMouseDown()
    {
        FlipCard();
    }

    public void SetFront(Sprite newFront)
    {
        front = newFront;
    }

    public void SetFaceDown()
    {
        faceDown = true;
        //Debug.Log("Card Num = " + front.name);
        spriteRenderer.sprite = back;
    }

    public void SetVamp()
    {
        isVamp = true;
    }
    public void RemoveVamp()
    {
        isVamp = false;
    }
    public void FlipCard()
    {
        if (faceDown)
        {
            spriteRenderer.sprite = front;
            faceDown = false;
            gameManager.UpdateBloodRound(ante);
            if (isVamp)
            {
                gameManager.Lose();
            }    
            gameManager.IncrementFlipped();
        }
    }
}
