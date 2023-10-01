using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField]
    private AudioSource leftFootSound, rightFootSound;

    [SerializeField]
    private float stepDistance = 1f;

    [SerializeField]
    private StarterAssets.FirstPersonController firstPersonController;

    private bool isStanding = true;

    private int stepCount = 0;

    private Vector3 lastFootstepPosition;

    // Start is called before the first frame update
    void Start()
    {
        lastFootstepPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstPersonController.IsWalking)
        {
            if (isStanding)
            {
                PlayFootstepSound();
            }
            isStanding = false;
        }
        else
        {
            isStanding = true;
        }

        if (Vector3.Distance(lastFootstepPosition, transform.position) >= stepDistance)
        {
            PlayFootstepSound();
        }
    }

    void PlayFootstepSound()
    {
        if (stepCount % 2 == 0)
        {
            leftFootSound.Play();
        }
        else
        {
            rightFootSound.Play();
        }
        stepCount++;
        lastFootstepPosition = transform.position;
    }
}
