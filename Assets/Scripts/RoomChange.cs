using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    public MeshCollider wholeWallCollider;
    public MeshCollider brokenWallCollider;
    public MeshRenderer wallsMeshRenderer;
    public float closedMaterialValue;
    public float openedMaterialValue;

    int propertyId = Shader.PropertyToID("_Hole_Pos");
    Vector3 property;
    Hole hole;

    Material wallsMaterial;

    private void Start()
    {
        hole = GameManager.Instance.hole;
        hole.onPunched += HandleHolePunch;
        wholeWallCollider.enabled = true;
        brokenWallCollider.enabled = false;

        wallsMaterial = wallsMeshRenderer.sharedMaterial;
        property = wallsMaterial.GetVector(propertyId);

    }

    public void HandleHolePunch(Hole hole)
    {
        property.x = Mathf.Lerp(closedMaterialValue, openedMaterialValue, hole.progress);
        wallsMaterial.SetVector(propertyId, property);
    }

    public void BreakRoom()
    {
        wholeWallCollider.enabled = false;
        brokenWallCollider.enabled = true;
    }
}
