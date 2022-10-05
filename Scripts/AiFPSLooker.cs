using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFPSLooker : MonoBehaviour, ILookComponent
{
    public enum Mode { Update, LateUpdate}
    public Mode mode = Mode.Update;
    AiAgent agent;
    AiCameraHolder holder;
    Vector2 look;
    Transform pivot;
    Transform camera;
    Transform viewPosition;

    Quaternion xRot;
    Quaternion yRot;

    float xAmount;
    float yAmount;

    float sense;

    public void Look(Vector2 look = default, Transform pivot = null, Transform camera = null, Transform viewPosition = null, float sense = 0.5f)
    {
        this.look = look;
        this.pivot = pivot;
        this.camera = camera;
        this.sense = sense;

        this.xAmount += look.x * sense;
        this.yAmount -= look.y * sense;
        //Debug.Log("Looker recieving");
        if(pivot != null)
        {
            holder.SetController(pivot);
        }

        if(viewPosition != null)
        {
            holder.SetTarget(viewPosition);
        }
        
        
    }

    public void SetAgent(AiAgent agent)
    {
        this.agent = agent;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject g = new GameObject();
        g.AddComponent<AiCameraHolder>();
        holder = g.GetComponent<AiCameraHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        yAmount = Mathf.Clamp(yAmount, -45f, 45);
        xRot = Quaternion.Euler(yAmount, 0f, 0f);
        yRot = Quaternion.Euler(0f, xAmount, 0f);

        switch(mode)
        {
            case Mode.Update: SetRotationPivot(); SetRotationCamera();
                break;
            case Mode.LateUpdate:
                break;
        }
    }

    void LateUpdate()
    {
        switch (mode)
        {
            case Mode.Update:
                break;
            case Mode.LateUpdate: SetRotationPivot(); SetRotationCamera();
                break;
        }
    }

    public Quaternion XRotation()
    {
        return xRot;
    }
    
    public Quaternion YRotation()
    {
        return yRot;
    }

    void SetRotationCamera()
    {
        if (camera != null)
        //camera.localRotation = xRot;
        camera.localRotation = Quaternion.Slerp(camera.localRotation, xRot, 0.5f);
        /*if (agent.camera != null)
        agent.camera.localRotation = xRot;*/
    }
    
    void SetRotationPivot()
    {
        if (pivot != null)
        //pivot.rotation = yRot;
        pivot.rotation = Quaternion.Slerp(pivot.rotation, yRot, 0.5f);
        /*if (agent.pivot != null)
        agent.pivot.rotation = yRot;*/
    }

    
}
