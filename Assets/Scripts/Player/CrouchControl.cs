using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchControl : MonoBehaviour
{
    Transform playerCameraRoot;
    CharacterController characterController;

    public float lerpSpeed;

    public float crouchCameraHight;
    public float crouchOverallHight;

    float initCameraHeight, initOverallHeight;

    void Start()
    {
        playerCameraRoot = transform.Find("PlayerCameraRoot");
        characterController = GetComponent<CharacterController>();

        initCameraHeight = playerCameraRoot.localPosition.y;
        initOverallHeight = characterController.height;
        targetHeight = initCameraHeight;
        currentHeight = targetHeight;
    }

    bool activated;
    float targetHeight, currentHeight;
    private void FixedUpdate()
    {
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.fixedDeltaTime * lerpSpeed);
        characterController.height = currentHeight;
    }
    void OnCrouch(InputValue value)
    {
        if (value.isPressed)
            activated = !activated;

        Vector3 camPosition = playerCameraRoot.localPosition;
        if(activated)
        {
            //camPosition.y = crouchCameraHight;
            //playerCameraRoot.localPosition = camPosition;
            targetHeight = crouchOverallHight;
        }
        else
        {
            targetHeight = initOverallHeight;
        }
    }
}
