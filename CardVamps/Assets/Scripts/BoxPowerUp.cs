using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPowerUp : MonoBehaviour
{
    Vector3[] locations = new Vector3[4];
    GameManager gameManager;
    [SerializeField] GameObject frame;
    [SerializeField] float timeToAnimate;
    int[] circleAnimateVals = new int[] { 0, 1, 3, 2 };
    private bool clicked = false;
    bool onCard = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 upperLeft = new Vector3(-.75f, 1, -.5f);
        Vector3 upperRight = new Vector3(.75f, 1, -.5f);
        Vector3 lowerLeft = new Vector3(-.75f, -1, -.5f);
        Vector3 lowerRight = new Vector3(.75f, -1, -.5f);
        locations = new Vector3[] { upperLeft, upperRight, lowerLeft, lowerRight };
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        frame.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (!clicked)
        {
            PlaceFrame();
            clicked = true;
        }
    }

    void PlaceFrame()
    {
        
        int vPos = gameManager.GetVampPosition();
        Vector3 targetLoc;

        //Corners
        if ( vPos == 0 )
        {
            targetLoc = locations[0];
        }
        if (vPos == 2)
        {
            targetLoc = locations[1];
        }
        if (vPos == 6)
        {
            targetLoc = locations[2];
        }
        if (vPos == 8)
        {
            targetLoc = locations[3];
        }

        //middle of sides
        else if (vPos == 1 )
        {
            int flip = Random.Range(0, 2);
            if ( flip == 0 )
            {
                targetLoc = locations[0];
            }
            else
            {
                targetLoc = locations[1];
            }
        }
        else if (vPos == 3)
        {
            int flip = Random.Range(0, 2);
            if (flip == 0)
            {
                targetLoc = locations[0];
            }
            else
            {
                targetLoc = locations[2];
            }
        }
        else if (vPos == 5)
        {
            int flip = Random.Range(0, 2);
            if (flip == 0)
            {
                targetLoc = locations[1];
            }
            else
            {
                targetLoc = locations[3];
            }
        }
        else if (vPos == 7)
        {
            int flip = Random.Range(0, 2);
            if (flip == 0)
            {
                targetLoc = locations[2];
            }
            else
            {
                targetLoc = locations[3];
            }
        }
        
        //middle
        else
        {
            int flip = Random.Range(0, 4);
            if (flip == 0)
            {
                targetLoc = locations[0];
            }
            else if (flip == 1)
            {
                targetLoc = locations[1];
            }
            else if (flip == 2)
            {
                targetLoc = locations[2];
            }
            else
            {
                targetLoc = locations[3];
            }
        }

        frame.gameObject.SetActive(true);
        StartCoroutine(Animate(targetLoc));
        
    }

    IEnumerator Animate( Vector3 targetLocation)
    {
        int start = 0;
        for (int i = 0; i < locations.Length; i++)
        {
            if (locations[i] == targetLocation)
            {
                start = i;
            }
        }
        for (int i=0; i < locations.Length; i++)
        {
            frame.transform.position = locations[circleAnimateVals[i]];
            yield return new WaitForSeconds(timeToAnimate);
        }
        frame.transform.position = locations[0];
        for (int i = 0; i < locations.Length; i++)
        {
            frame.transform.position = locations[circleAnimateVals[i]];
            if (locations[circleAnimateVals[i]] == targetLocation)
            {
                break;
            }
            yield return new WaitForSeconds(timeToAnimate);
        }
    }
}
