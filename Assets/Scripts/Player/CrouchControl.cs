using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchControl : MonoBehaviour
{
    Transform playerCameraRoot;
    CharacterController characterController;

    public float crouchCameraHight;
    public float crouchOverallHight;

    float initCameraHeight, initOverallHeight;

    void Start()
    {
        playerCameraRoot = transform.Find("PlayerCameraRoot");
        characterController = GetComponent<CharacterController>();

        initCameraHeight = playerCameraRoot.localPosition.y;
        initOverallHeight = characterController.height;
    }

    void OnCrouch(InputValue value)
    {
        Vector3 camPosition = playerCameraRoot.localPosition;
        if(value.isPressed)
        {
            //camPosition.y = crouchCameraHight;
            //playerCameraRoot.localPosition = camPosition;
            characterController.height = crouchOverallHight;
        }
        else
        {
            characterController.height = initOverallHeight;
        }
    }
}
