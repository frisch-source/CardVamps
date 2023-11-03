using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent m_gameOver;
    private int cardsFlipped = 0;
    private bool lost = false;
    private int bloodTotal = 50;
    private int bloodRound = 0;
    private int round = 1;
    private string currentRoundPipsS = ".    .    .    .    .";
    private string currentRoundPipsOnlyResults = "";
    private GameObject[] topPowerUps = new GameObject[3];

    [SerializeField] TextMeshProUGUI currentRoundText;
    [SerializeField] TextMeshProUGUI currentRoundPips;
    [SerializeField] TextMeshProUGUI roundBloodText;
    [SerializeField] TextMeshProUGUI bloodTotalText;
    [SerializeField] BoardSetup boardSetup;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //if (m_gameOver != null) 
        //{
        //    m_gameOver = new UnityEvent();
        //}
        
    }

    public void IncrementFlipped()
    {
        cardsFlipped ++;
        //Debug.Log(cardsFlipped);
    }
    
    public int GetVampPosition()
    {
        return boardSetup.GetVampPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (cardsFlipped == 8 && !lost)
        {
            Win();
            
        }
    }

    public void Pass()
    {
        Debug.Log("Passed");
        cardsFlipped = 0;
        //subtract 2 from earnings and eventually scale with ante
        bloodTotal += bloodRound-2;
        bloodRound = 0;
        roundBloodText.text = "" + bloodRound + " mL";
        round++;
        bloodTotalText.text = "" + bloodTotal + " mL";
        boardSetup.SetupBoard();

        currentRoundPipsOnlyResults += "P ";
        currentRoundText.text = "Round " + round;
        currentRoundPips.text = ConstructPipString();
    }
    public void Lose() 
    {
        //m_gameOver.Invoke();
        Debug.Log("Lost");
        cardsFlipped = 0;
        lost = true;
        bloodTotal -= bloodRound;
        bloodRound = 0;
        roundBloodText.text = "" + bloodRound + " mL";
        round++;
        bloodTotalText.text = "" + bloodTotal + " mL";
        boardSetup.SetupBoard();

        currentRoundPipsOnlyResults += "L ";
        currentRoundText.text = "Round " + round;
        currentRoundPips.text = ConstructPipString();
        lost = false;
    }

    void Win()
    {
        Debug.Log("Won");
        cardsFlipped = 0;
        
        UpdateBloodRound(2);
        UpdateBloodTotal();
        UpdateBloodRound(bloodRound * -1);
        round++;
        roundBloodText.text = "" + bloodRound + " mL";
        bloodTotalText.text = "" + bloodTotal + " mL";
        boardSetup.SetupBoard();

        currentRoundPipsOnlyResults += "W ";
        currentRoundText.text = "Round " + round;
        currentRoundPips.text = ConstructPipString();
    }

    public void UpdateBloodTotal()
    {

        bloodTotal += bloodRound;
    }
    
    public void UpdateBloodRound(int addition)
    {
        bloodRound += addition;
        roundBloodText.text = "" + bloodRound + " mL";
    }

    private string ConstructPipString()
    {
        string toReturn = currentRoundPipsOnlyResults;
         
        for (int i = 0; i < 5-round + 1; i++)
        {
            toReturn += "   .";
        }
        return toReturn;
    }

    public void UpdateBoardRow(int row)
    {
        boardSetup.UpdateRow(row);
    }
    public void UpdateBoardColumn(int column)
    {
        boardSetup.UpdateColumn(column);
    }
}
