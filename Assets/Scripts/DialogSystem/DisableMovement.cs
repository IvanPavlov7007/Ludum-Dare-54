using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovement : MonoBehaviour
{
    SphereMovement movement;
    void Start()
    {
        movement = SphereMovement.Instance;
    }

    public void Disable(float time)
    {
        StartCoroutine(wait(time));
    }

    IEnumerator wait(float time)
    {
        movement.enabled = false;
        movement.GetComponent<CharacterMovement>().direction = Vector3.zero;
        yield return new WaitForSeconds(time);
        movement.enabled = true;
    }
}
