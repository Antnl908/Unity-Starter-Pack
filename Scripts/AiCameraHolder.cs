using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCameraHolder : MonoBehaviour
{
    public Transform holder;
    public Transform target;

    public void SetController(Transform holder)
    {
        this.holder = holder;
    }
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        /*if(holder == null)
        {
            Debug.Log("There is no holder!");
        }
        if(target == null)
        {
            Debug.Log("There is no target!");
        }*/
        if(holder != null && target != null)
        holder.position = new Vector3(target.position.x, target.position.y, target.position.z);
    }
}
