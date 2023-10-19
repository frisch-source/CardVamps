using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent m_gameOver;
    private int cardsFlipped = 0;
    private bool lost = false;
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
        lost = true;
    }

    void Win()
    {
        Debug.Log("Won");
    }
}
