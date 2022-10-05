using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class AiFPSMover : MonoBehaviour, IMoveComponent
{
    public enum Mode { CharacterController, PhysicsBased}
    public Mode mode = Mode.CharacterController;
    AiAgent agent;
    CharacterController characterController;
    Rigidbody rb;
    Collider col;
    Vector2 move;
    bool jump;
    Transform pivot;
    float speed;
    float gravity;

    void Start()
    {
        this.characterController = GetComponent<CharacterController>();
        this.rb = GetComponent<Rigidbody>();
        this.col = GetComponent<Collider>();
    }

    public void Move(Vector2 move = default, Transform pivot = null, float speed = 5.5f/*, bool jump = default*/)
    {
        this.move = move;
        this.pivot = pivot;
        this.speed = speed;
        //Debug.Log("Mover");
    }

    public void Jump(bool jump)
    {
        this.jump = jump;
    }

    public void SetAgent(AiAgent agent)
    {
        this.agent = agent;
        //Debug.Log("Set");
    }

    void Update()
    {
        switch(mode)
        {
            case Mode.CharacterController: MoveCharacterController();
                break;
            case Mode.PhysicsBased: 
                break;

        }
        
    }
    
    void FixedUpdate()
    {
        switch(mode)
        {
            case Mode.CharacterController:
                break;
            case Mode.PhysicsBased: MoveCharacterPhysics();
                break;

        }
        
    }

    public bool Grounded(Vector3 oriVec, /*Vector3 dirVec,*/ float dist)
    {
        Ray ray = new Ray(oriVec, oriVec + -Vector3.up);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, dist))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void MoveCharacterController()
    {
        if (pivot != null)
        {
            
            //Debug.Log(move, pivot);
            if (!Grounded(agent.transform.position, 0.1f))
            {
                //gravity = Mathf.Lerp(gravity, 7f, 0.1f);
                gravity -= 0.5f;
                gravity = Mathf.Clamp(gravity, -7f, 16f);
                //agent.characterController.Move(pivot.forward * move.y * speed * Time.deltaTime + pivot.right * move.x * speed * Time.deltaTime + -Vector3.up * 0.5f * Time.deltaTime);
                //agent.characterController.Move((pivot.forward * move.y * speed + pivot.right * move.x * speed + -Vector3.up * 0.5f) * Time.deltaTime);
                //characterController.Move((pivot.forward * move.y * speed + pivot.right * move.x * speed + -Vector3.up * 0.5f) * Time.deltaTime);
                characterController.Move((pivot.forward * move.y * speed + pivot.right * move.x * speed + Vector3.up * gravity) * Time.deltaTime);
            }
            else
            {
                if(jump)
                {
                    gravity = 16f;
                }
                else
                {
                    gravity = -0.15f;
                }
                //gravity = 0.15f;
                //agent.characterController.Move(pivot.forward * move.y * speed * Time.deltaTime + pivot.right * move.x * speed * Time.deltaTime + -Vector3.up * 0.15f * Time.deltaTime);
                //agent.characterController.Move((pivot.forward * move.y * speed + pivot.right * move.x * speed + -Vector3.up * 0.15f) * Time.deltaTime);
                //characterController.Move((pivot.forward * move.y * speed + pivot.right * move.x * speed + -Vector3.up * 0.15f) * Time.deltaTime);
                characterController.Move((pivot.forward * move.y * speed + pivot.right * move.x * speed + Vector3.up * gravity) * Time.deltaTime);
                //agent.characterController.Move(pivot.forward * move.y + pivot.right * move.x);
            }

        }
    }
    
    void MoveCharacterPhysics()
    {
        if (pivot != null)
        {
            //Debug.Log(move, pivot);
            if (!Grounded(agent.transform.position, 0.1f))
            {
                //agent.characterController.Move(pivot.forward * move.y * speed + pivot.right * move.x * speed + -Vector3.up * 0.5f);
            }
            else
            {
                //agent.characterController.Move(pivot.forward * move.y * speed + pivot.right * move.x * speed + -Vector3.up * 0.15f);
                //agent.characterController.Move(pivot.forward * move.y + pivot.right * move.x);
            }

        }
    }
}
