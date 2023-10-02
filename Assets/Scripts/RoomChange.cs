using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    public MeshCollider wholeWallCollider;
    public MeshCollider brokenWallCollider;

    Hole hole;

    private void Start()
    {
        hole = GameManager.Instance.hole;
        wholeWallCollider.enabled = true;
        brokenWallCollider.enabled = false;
    }

    public void BreakRoom()
    {
        wholeWallCollider.enabled = false;
        brokenWallCollider.enabled = true;
    }
}
