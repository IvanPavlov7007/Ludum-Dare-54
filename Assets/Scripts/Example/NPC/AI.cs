using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class AI : MonoBehaviour
{
    CharacterMovement characterMovement;
    private void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        t = movingTime;
    }

    [SerializeField]
    bool movingRight = true;
    public float movingTime = 3f;
    public bool wait;

    float t;
    void Update()
    {
        if (wait)
        {
            characterMovement.direction = Vector3.zero;
            return;
        }

        t -= Time.deltaTime;
        if(t < 0)
        {
            movingRight = !movingRight;
            t = movingTime;
        }
        characterMovement.direction = Vector3.right * (movingRight ? 1f : -1f);
    }
}
