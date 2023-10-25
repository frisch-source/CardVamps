using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BoardSetup : MonoBehaviour
{
    [SerializeField] Sprite back;

    [SerializeField] Sprite two;
    [SerializeField] Sprite three;
    [SerializeField] Sprite four;
    [SerializeField] Sprite five;
    [SerializeField] Sprite six;
    [SerializeField] Sprite seven;
    [SerializeField] Sprite eight;
    [SerializeField] Sprite nine;
    [SerializeField] Sprite vamp;
    Sprite[] cardSprites;
    List<GameObject> cards = new List<GameObject>();
    double[] cardOrder = new double[9];

    private int vampPos = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        cardSprites = new Sprite[] { two, three, four, five, six, seven, eight, nine, vamp };
        
    }
    void Start()
    {
        foreach (GameObject card in GameObject.FindGameObjectsWithTag("Card"))
        {
            cards.Add(card);
        }
        SetupBoard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetupBoard()
    {
        int toFlip = 0;
        cards[vampPos].GetComponent<Card>().RemoveVamp();
        Shuffle();
        for (int i = 0; i < cardSprites.Length; i++)
        {
            if (cardSprites[i] == vamp) 
            {
                vampPos = i;
                cards[i].GetComponent<Card>().SetVamp();
                Debug.Log("Vamp Location = " + cards[i].name);
            }
            cards[i].GetComponent<Card>().SetFront(cardSprites[i]);
            //Debug.Log("Card " + (i+1) + " = " + cardSprites[i]);
            cards[i].GetComponent<Card>().SetFaceDown();
        }
        int flip = UnityEngine.Random.Range(0, 2);
        if (vampPos == 0)
        {
            toFlip = UnityEngine.Random.Range(1, 9);
        }
        if (flip == 0)
        {
            toFlip = UnityEngine.Random.Range(0, vampPos);
        }
        else
        {
            toFlip = UnityEngine.Random.Range(vampPos + 1, 9);
        }
        cards[toFlip].GetComponent<Card>().FlipCard();
        
    }
    void Shuffle()
    {
        
        for (int i = 0; i < cardOrder.Length; i++) 
        {
            cardOrder[i] = UnityEngine.Random.value;
        }
        Array.Sort(cardOrder, cardSprites);
    }
}
