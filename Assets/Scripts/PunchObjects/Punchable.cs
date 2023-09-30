using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Punchable
{
    public void Punch(Vector3 position, Vector3 direction, float impulse);
}
