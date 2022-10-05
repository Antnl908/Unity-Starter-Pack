using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveComponent
{
    void Move(Vector2 move = default, Transform pivot = null, float speed = 1f/*, bool jump = default*/);

    void Jump(bool jump);

    void SetAgent(AiAgent agent);
}
