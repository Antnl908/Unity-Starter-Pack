using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBridge
{
    void Move(Vector2 move, Transform pivot/*, bool jump*/);
    void Look(Vector2 move, Transform pivot, Transform camera);

    void Jump(bool jump);

    void Click();

    Vector3 GetPosition();

    Transform GetViewPosition();
}
