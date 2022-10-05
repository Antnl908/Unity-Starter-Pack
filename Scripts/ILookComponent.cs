using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILookComponent
{
    void Look(Vector2 look = default, Transform pivot = null, Transform camera = null, Transform viewPosition = null, float sense = 1f);

    void SetAgent(AiAgent agent);

    Quaternion YRotation();

    Quaternion XRotation();
}
