using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public PointEffector2D LeftHole, RightHole;
    public GameObject BHL, BHR;
    
    
    private float Force = 50;
    protected int rng;
    public bool IsOpen =false;
    
    void Start()
    {
        
    }

    private int Rng()
    {
        return Random.Range(0, 2);
    }

    public void turnOnBH()
    {

        rng = Rng();
        Debug.Log(rng);
        if (rng == 0)
        {
            StartCoroutine(RunBlackHoleL());
        }
        else
        {
            StartCoroutine(RunBlackHoleR());
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if(IsOpen ==true)
        {
            turnOnBH();
        }
    }

    IEnumerator  RunBlackHoleL()
    {
        Debug.Log(rng+"L");
        BHL.SetActive(true);
        LeftHole.forceMagnitude = -Force;
        yield return new WaitForSeconds(10f);
        LeftHole.forceMagnitude = Force;
        BHL.SetActive(false);
    }

    IEnumerator RunBlackHoleR()
    {
        Debug.Log(rng+"R");
        BHR.SetActive(true);
        RightHole.forceMagnitude = -Force;
        yield return new WaitForSeconds(10f);
        RightHole.forceMagnitude = Force;
        BHR.SetActive(false);
    }

    
}
