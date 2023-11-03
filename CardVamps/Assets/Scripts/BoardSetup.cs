using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BoardSetup : MonoBehaviour
{
    [SerializeField] Sprite back;

    /*
    [SerializeField] Sprite two;
    [SerializeField] Sprite three;
    [SerializeField] Sprite four;
    [SerializeField] Sprite five;
    [SerializeField] Sprite six;
    [SerializeField] Sprite seven;
    [SerializeField] Sprite eight;
    [SerializeField] Sprite nine;
    [SerializeField] Sprite vamp;
    */

    [SerializeField] GameObject arrowPower1;
    [SerializeField] GameObject arrowPower2;
    [SerializeField] GameObject plusPower1;
    [SerializeField] GameObject plusPower2;
    [SerializeField] GameObject plusPower3;
    [SerializeField] GameObject squarePower;

    [SerializeField] GameObject two;
    [SerializeField] GameObject three;
    [SerializeField] GameObject four;
    [SerializeField] GameObject five;
    [SerializeField] GameObject six;
    [SerializeField] GameObject seven;
    [SerializeField] GameObject eight;
    [SerializeField] GameObject nine;
    [SerializeField] GameObject vamp;

    Vector3 twoPos = new Vector3();
    Vector3 threePos = new Vector3();
    Vector3 fourPos = new Vector3();
    Vector3 fivePos = new Vector3();   
    Vector3 sixPos = new Vector3();
    Vector3 sevenPos = new Vector3();
    Vector3 eightPos = new Vector3();
    Vector3 ninePos = new Vector3();
    Vector3 vampPos = new Vector3();

    Sprite[] cardSprites;
    Vector3[] cardLocations = new Vector3[9];
    Vector3[] cardLocationsStatic = new Vector3[9];
    GameObject[] cards = new GameObject[9];
    int[] cardOrder = new int[9];
    int[] cardVals = new int[9];

    int column1 = 0;
    int column2 = 0;
    int column3 = 0;
    int row1 = 0;
    int row2 = 0;
    int row3 = 0;

    private int vampPosInDeck = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        cards = new GameObject[] { two, three, four, five, six, seven, eight, nine, vamp };
        cardOrder = new int[] {2,3,4,5,6,7,8,9,10 };
        cardLocations = new Vector3[] { twoPos, threePos, fourPos, fivePos, sixPos, sevenPos, eightPos, ninePos, vampPos};
        cardLocationsStatic = new Vector3[] { twoPos, threePos, fourPos, fivePos, sixPos, sevenPos, eightPos, ninePos, vampPos };

    }
    void Start()
    {
        /*
        foreach (GameObject card in GameObject.FindGameObjectsWithTag("Card"))
        {
            cards.Add(card);
        }
        */
        for (int i = 0; i < cards.Length; i++)
        {
            //Debug.Log(cards[i].GetComponent<SpriteRenderer>().sprite.name);
            cardLocations[i] = cards[i].GetComponent<Transform>().transform.position;
            cardLocationsStatic[i] = cards[i].GetComponent<Transform>().transform.position;
            //cardOrder[i] = cards[i].GetComponent<Card>().GetValue();

        }
        

        SetupBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (row1 == 3)
        {
            plusPower2.GetComponent<PlusPowerUp>().EnablePowerup();
            row1 = 0;
        }
        if (row2 == 3)
        {
            arrowPower2.GetComponent<ArrowsPowerUp>().EnablePowerUp();
            row2 = 0;
        }
        if (row3 == 3)
        {
            plusPower3.GetComponent<PlusPowerUp>().EnablePowerup();
            row3 = 0;
        }
        if (column1 == 3)
        {
            plusPower1.GetComponent<PlusPowerUp>().EnablePowerup();
            column1 = 0;
        }
        if (column2 == 3)
        {
            squarePower.GetComponent<BoxPowerUp>().EnablePowerUp();
            column2 = 0;
        }
        if (column3 == 3)
        {
            arrowPower1.GetComponent<ArrowsPowerUp>().EnablePowerUp();
            column3 = 0;
        }
    }

    public void SetupBoard()
    {
        row1 = 0;
        row2 = 0;
        row3 = 0;
        column1 = 0;
        column2 = 0;
        column3 = 0;

        arrowPower1.GetComponent<ArrowsPowerUp>().ResetPowerUp();
        arrowPower2.GetComponent<ArrowsPowerUp>().ResetPowerUp();
        plusPower1.GetComponent<PlusPowerUp>().ResetPowerUp();
        plusPower2.GetComponent<PlusPowerUp>().ResetPowerUp();
        plusPower3.GetComponent<PlusPowerUp>().ResetPowerUp();
        squarePower.GetComponent<BoxPowerUp>().ResetPowerUp();

        int toFlip = 0;
        //cards[vampPosInDeck].GetComponent<Card>().RemoveVamp();
        Shuffle();

        for (int i = 0; i < cards.Length; i++)
        {
            
            
            //cards[i].GetComponent<Card>().SetFront(cardSprites[i]);
            cards[i].GetComponent<Transform>().transform.position = cardLocations[i];
            
            
            if (cardLocations[8] == cardLocationsStatic[i])
            {
                vampPosInDeck = i;
                //cards[i].GetComponent<Card>().SetVamp();
                Debug.Log("Vamp Location = " + (vampPosInDeck + 2));
            }
            
            //Debug.Log("Card " + (i+1) + " = " + cardSprites[i]);
            cards[i].GetComponent<Card>().SetFaceDown();
        }
        int flip = UnityEngine.Random.Range(0, 2);
        //vamp location is always at 4 card array
        
        toFlip = UnityEngine.Random.Range(0, 8);
        
        cards[toFlip].GetComponent<Card>().FlipCard();
        
        

        

    }
    void Shuffle()
    {
        Array.Copy(cardLocationsStatic, cardLocations, 9);
        //cardLocations = cardLocationsStatic;
        for (int i = 0; i < cardOrder.Length; i++) 
        {
            
            Vector3 tempLoc = cardLocations[i];
            int ran = UnityEngine.Random.Range(i, cardOrder.Length);
            cardLocations[i] = cardLocations[ran];
            cardLocations[ran] = tempLoc;

            //Debug.Log(cards[8].GetComponent<Transform>().transform.position);
            //Debug.Log(cardLocations[ran]);
            
            //int ran = UnityEngine.Random.Range(i, cardOrder.Length);
            //cardOrder[i] = cards[i].GetComponent<Card>().GetValue();
            //int cardOrderTemp = cardOrder[i];
            //cardOrder[i] = cardOrder[ran];
            //cardOrder[ran] = cardOrderTemp;

            


            //cardOrder[i] = UnityEngine.Random.value;
        }
        /*
        Vector3[] cardLocationsTemp = new Vector3[9];

        for (int i = 0;i < cards.Length; i++)
        {
            Debug.Log(cardOrder[i]);
            cardLocationsTemp[i] = cardLocationsStatic[cardOrder[i] - 2];
        }
        cardLocations = cardLocationsTemp;
        */
        //Array.Sort(cardOrder, cardSprites);
        //Array.Sort(cardOrder, cardLocations);
    }
    
    void ShufflePowerUps()
    {

    }
    public int GetVampPosition()
    {
        return vampPosInDeck;
    }

    public void UpdateRow(int row)
    {
        if (row == 0)
        {
            row1++;
        }
        else if (row == 1)
        {
            row2++;
        }
        else
        {
            row3++;
        }
    }
    public void UpdateColumn(int col)
    {
        if (col == 0)
        {
            column1++;
        }
        else if (col == 1)
        {
            column2++;
        }
        else
        {
            column3++;
        }
    }
}
