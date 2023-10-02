using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Player : Singleton<Player>
{
    PunchController punchController;
    CrouchControl crouchControl;
    StarterAssets.FirstPersonController firstPersonController;
    public Transform cameraContainer;

    void Start()
    {
        punchController = GetComponent<PunchController>();
        crouchControl = GetComponent<CrouchControl>();
        firstPersonController = GetComponent<StarterAssets.FirstPersonController>();
    }

    public void TurnOff()
    {
        punchController.enabled = false;
        crouchControl.enabled = false;
        firstPersonController.enabled = false;
    }

    public void TurnOn()
    {
        punchController.enabled = true;
        crouchControl.enabled = true;
        firstPersonController.enabled = true;
    }

    public void BlackOut(float time)
    {
        TurnOff();
        Tween.LocalPosition(cameraContainer, cameraContainer.localPosition + Vector3.down, time, 0f, Tween.EaseIn);
        Tween.Rotation(cameraContainer, Quaternion.Euler(-90f,0f,0f), time, 0f, Tween.EaseIn);
    }
}
