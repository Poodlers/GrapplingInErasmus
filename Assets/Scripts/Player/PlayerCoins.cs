using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    public int totalCoins = 0;
    void Start()
    {

    }

    public void GainCoins(int coinValue)
    {
        totalCoins += coinValue;
    }
}
