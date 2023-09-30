using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    CharacterMovement characterMovement;

    // Singletone pattern
    private static SphereMovement instance;
    public static SphereMovement Instance { get { return instance; } }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        characterMovement.direction = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        characterMovement.isRunning = Input.GetKey(KeyCode.LeftShift);
    }
}
