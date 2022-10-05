using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour, IBridge
{
    public enum State { Active, Inactive, Walk, Task}
    public State state = State.Walk;
    public enum Task { Active, Idle, Talk }
    public Task task;
    //public NavMeshAgent navAgent;
    public Rigidbody rb;
    Vector3 targetPosition;
    float timer;
    public Transform marker;
    public Animator anim;
    public Speaker speaker;
    public Talker talker;

    public Transform targetT;
    public Transform currentT;

    public Dialogue[] CharacterDialogues;

    public float maxDist = 1.0f;
    public float maxTalkDist = 5.0f;
    bool taskRunning;

    Ray ray;
    RaycastHit rHit;

    AiLocomotion aiLocomotion;
    IMoveComponent mover;
    ILookComponent looker;
    public CharacterController characterController;

    Vector2 move;
    bool jump;
    Vector2 look;
    [HideInInspector]public Transform pivot;
    [HideInInspector]public Transform camera;
    public Transform viewPosition;

    Quaternion movementDirection;

    public Transform animatedTransform;

    void Awake()
    {
        mover = GetComponent<IMoveComponent>();
        mover.SetAgent(this);
        looker = GetComponent<ILookComponent>();
        looker.SetAgent(this);
        //navAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        //navAgent.enabled = false;
        state = State.Walk;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mover.Move(move, pivot);
        switch (state)
        {
            case State.Active:
                break;
            case State.Inactive:
                break;
            case State.Walk: mover.Move(move, pivot, 10f/*, jump*/); mover.Jump(jump); looker.Look(look, pivot, camera, viewPosition, 0.25f); //animatedTransform.rotation = looker.YRotation();
                break;
            case State.Task:
                break;
        }

        //movementDirection = Quaternion.LookRotation(new Vector3(transform.rotation.x + move.x, 0f, transform.rotation.z + move.y), Vector3.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation, movementDirection, 0.01f);
        //anim.SetFloat("Speed", navAgent.velocity.magnitude);

    }

    /*void FixedUpdate()
    {
        if(pivot != null)
        {
            characterController.Move(pivot.forward * move.y + pivot.right * move.x);
        }
    }*/

    public void SetTargetPosition(Vector3 position)
    {
        //navAgent.enabled = true;
        Debug.Log("Recieve position");
        //targetPosition = position;
        //navAgent.SetDestination(position);
        state = State.Walk;
        targetPosition = position;
        //targetT.position = position;
        //navAgent.isStopped = false;
        speaker.Quit();
        task = Task.Idle;
        talker = null;
        taskRunning = false;
    }

    /*void Walk()
    {
        timer += Time.deltaTime;
        if (timer > 0.25f)
        {
            //targetT.position = targetPosition;
            currentT.position = transform.position;
            //float sqrDistance = (marker.position - navAgent.destination).sqrMagnitude;
            float sqrDistance = (marker.position - transform.position).sqrMagnitude;
            //float sqrDistance = (targetT.position - currentT.position).sqrMagnitude;
            
            if (sqrDistance > (maxDist * maxDist))
            {
                navAgent.destination = marker.position;
            }
            if(sqrDistance < (maxTalkDist * maxTalkDist))
            {
                switch (task)
                {
                    case Task.Talk:
                        Talk();
                        break;
                    //case Task.
                        //break;
                }
            }
            //if (targetPosition != null)
            //{
            //    navAgent.destination = marker.position;
            //}
            timer = 0f;
        }
        //anim.SetFloat("Speed", navAgent.velocity.magnitude);
    }*/

    public void Talk()
    {
        Debug.Log("Talk!");
        //navAgent.destination = transform.position;
        //navAgent.isStopped = true;
        if(taskRunning)
        {
            return;
        }
        if(talker != null)
        {
            speaker.SetDialogue(talker.GetDialogue());
            if(speaker.Check())
            {
                speaker.Speak();
                taskRunning = true;
                //navAgent.destination = transform.position;
                //navAgent.isStopped = true;
            }
        }
    }

    public void SetTalker(Talker talker)
    {
        this.talker = talker;
        if(talker != null)
        {
            Debug.Log("Talker recieved!");
        }
        task = Task.Talk;
    }

    public void Move(Vector2 move, Transform pivot/*, bool jump*/)
    {
        //navAgent.enabled = false;
        //characterController.enabled = true;
        ///characterController.Move(pivot.forward * move.y + pivot.right * move.x);
        this.move = move;
        this.jump = jump;
        this.pivot = pivot;
        if(move != Vector2.zero)
        {
            Vector3 newRot = new Vector3(transform.rotation.x + move.x, 0f, transform.rotation.z + move.y);
            movementDirection = Quaternion.LookRotation(newRot, Vector3.up);
        }
        
    }

    public void Jump(bool jump)
    {
        this.jump = jump;
    }

    public void Look(Vector2 look, Transform pivot, Transform camera)
    {
        this.look = look;
        this.pivot = pivot;
        this.camera = camera;
    }

    public void Click()
    {
        FireRay();
    }

    void FireRay()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rHit, 1000f))
        {
            marker.position = rHit.point;
            targetT.position = rHit.point;
            //SetTargetPosition(nHit.position);
            SetTargetPosition(rHit.point);
            Talker t = rHit.transform.GetComponent<Talker>();
            if (t != null)
            {
                //Debug.Log("Talker found!");
                SetTalker(t);
            }
            /*
            if (NavMesh.Raycast(Camera.main.transform.position, rHit.point, out nHit, NavMesh.AllAreas))
            {
                Debug.Log("Send position");
                agent.SetTargetPosition(nHit.position);
            }
            */
        }

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Transform GetViewPosition()
    {
        return viewPosition;
    }
}
