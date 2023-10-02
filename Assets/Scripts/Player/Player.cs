using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Player : Singleton<Player>
{
    PunchController punchController;
    CrouchControl crouchControl;
    Transform cameraContainer;
}
