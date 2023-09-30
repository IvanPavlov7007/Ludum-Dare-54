using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingBag : MonoBehaviour, Punchable
{
    Rigidbody rb;

    public void Punch(Vector3 position, Vector3 direction, float impulse)
    {
        rb.AddForceAtPosition(direction * impulse, position, ForceMode.Impulse);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
}
