using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Health))]
public class Hole : MonoBehaviour, Punchable
{
    TextMeshPro textMesh;
    public Health health { get; private set; }
    public int hitDamage = 1;
    public void Punch(Vector3 position, Vector3 direction, float impulse)
    {
        health.Hit(hitDamage);
    }

    void Start()
    {
        health = GetComponent<Health>();
        textMesh = GetComponentInChildren<TextMeshPro>();
    }

    void Update()
    {
        if (textMesh != null)
            textMesh.text = health.amount.ToString();
    }
}
