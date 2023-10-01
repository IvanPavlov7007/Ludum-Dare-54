using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public Action onDeath;

    public int maxAmount;
    public int amount { get; private set; }

    private void Awake()
    {
        amount = maxAmount;
    }

    public void Hit(int count)
    {
        amount -= count;
        if (amount <= 0)
        {
            if (onDeath != null)
                onDeath();
        }
    }
}
