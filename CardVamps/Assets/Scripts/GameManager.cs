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

    [SerializeField] TextMeshProUGUI currentRoundText;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (cardsFlipped == 8 && !lost)
        {
            Win();
            
        }
    }

    public void Lose() 
    {
        //m_gameOver.Invoke();
        Debug.Log("Lost");
        cardsFlipped = 0;
        lost = true;
        bloodTotal -= bloodRound;
        bloodRound = 0;
        roundBloodText.text = "" + bloodRound + "mL";
        round++;
        bloodTotalText.text = "" + bloodTotal + " mL";
        boardSetup.SetupBoard();
    }

    void Win()
    {
        Debug.Log("Won");
        cardsFlipped = 0;
        
        UpdateBloodRound(2);
        UpdateBloodTotal();
        UpdateBloodRound(bloodRound * -1);
        round++;
        roundBloodText.text = "" + bloodRound + "mL";
        bloodTotalText.text = "" + bloodTotal + " mL";
        boardSetup.SetupBoard();

    }

    public void UpdateBloodTotal()
    {

        bloodTotal += bloodRound;
    }
    
    public void UpdateBloodRound(int addition)
    {
        bloodRound += addition;
        roundBloodText.text = "" + bloodRound + "mL";
    }
}