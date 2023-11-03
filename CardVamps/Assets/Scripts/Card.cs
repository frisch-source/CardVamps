using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Card : MonoBehaviour
{
    [SerializeField] Sprite back;
    [SerializeField] Sprite front;
    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    private int ante = 1;
    [SerializeField] int value;
    Vector2[] locations = new Vector2[9] { new Vector2(-1.5f, 2), new Vector2(0, 2), new Vector2(1.5f, 2), new Vector2(-1.5f, 0), new Vector2(0, 0), new Vector2(1.5f, 0), new Vector2(-1.5f, -2), new Vector2(0, -2), new Vector2(1.5f, -2) };
    
    bool faceDown = true;
    bool hasNumPowerUp = false;

    [SerializeField] bool isVamp;

    
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
        hasNumPowerUp = false;
    }

    public void SetVamp()
    {
        isVamp = true;
        //value = 10;
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
            int[] rc = GetRowAndColumn(transform.position);
            gameManager.UpdateBoardRow(rc[0]);
            gameManager.UpdateBoardColumn(rc[1]);
            if (isVamp)
            {
                gameManager.Lose();
            }    
            if (hasNumPowerUp)
            {
                Vector3 powerPos = new Vector3(transform.position.x, transform.position.y, transform.position.z-1);
                Collider2D[] overlaps = Physics2D.OverlapPointAll(powerPos);
                PlusPowerUp plusPower = overlaps[0].GetComponent<PlusPowerUp>();
                plusPower.DisablePowerup();
                //Debug.Log("Attempt To Disable");
            }
            gameManager.IncrementFlipped();
        }
    }

    public void SetValue(int value)
    {
        this.value = value;
    }    
    public int GetValue()
    {
        return value;
    }

    public bool IsVamp()
    { 
        return isVamp; 
    }

    public void SetHasNumPowerup()
    {
        hasNumPowerUp = true;
        //Debug.Log(value + "Has Power On");
    }

    public bool GetFaceDown()
    {
        return faceDown;
    }

    private int[] GetRowAndColumn(Vector2 location)
    {
        int col = 0;
        int row = 0;
        if (location == locations[1])
        {
            col = 1;
            row = 0;
        }
        else if (location == locations[2])
        {
            col = 2;
            row = 0;
        }
        else if (location == locations[3])
        {
            col = 0;
            row = 1;
        }
        else if (location == locations[4])
        {
            col = 1;
            row = 1;
        }
        else if (location == locations[5])
        {
            col = 2;
            row = 1;
        }
        else if (location == locations[6])
        {
            col = 0;
            row = 2;
        }
        else if (location == locations[7])
        {
            col = 1;
            row = 2;
        }
        else if (location == locations[8])
        {
            col = 2;
            row = 2;
        }
        else
        {
            col = 0;
            row = 0;
        }
        //Debug.Log(row);
        //Debug.Log(col);
        return new int[] { row, col };
    }
}
