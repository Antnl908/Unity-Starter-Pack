using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AiLocomotion : MonoBehaviour, IMoveComponent
{
    AiAgent agent;
    float timer;
    NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAgent(AiAgent agent)
    {
        this.agent = agent;
    }

    public void Walk()
    {
        timer += Time.deltaTime;
        if (timer > 0.25f)
        {
            //targetT.position = targetPosition;
            agent.currentT.position = agent.transform.position;
            //float sqrDistance = (marker.position - navAgent.destination).sqrMagnitude;
            float sqrDistance = (agent.marker.position - agent.transform.position).sqrMagnitude;
            //float sqrDistance = (targetT.position - currentT.position).sqrMagnitude;

            if (sqrDistance > (agent.maxDist * agent.maxDist))
            {
                //agent.navAgent.destination = agent.marker.position;
                navAgent.destination = agent.marker.position;
            }
            if (sqrDistance < (agent.maxTalkDist * agent.maxTalkDist))
            {
                switch (agent.task)
                {
                    case AiAgent.Task.Talk:
                        agent.Talk();
                        break;
                        //case Task.
                        //break;
                }
            }
            /*if (targetPosition != null)
            {
                navAgent.destination = marker.position;
            }*/
            timer = 0f;
        }
        agent.anim.SetFloat("Speed", navAgent.velocity.magnitude);
    }

    public void Move(Vector2 move, Transform pivot, float sense = 1f/*, bool jump = default*/)
    {
        timer += Time.deltaTime;
        if (timer > 0.25f)
        {
            //targetT.position = targetPosition;
            agent.currentT.position = agent.transform.position;
            //float sqrDistance = (marker.position - navAgent.destination).sqrMagnitude;
            float sqrDistance = (agent.marker.position - agent.transform.position).sqrMagnitude;
            //float sqrDistance = (targetT.position - currentT.position).sqrMagnitude;

            if (sqrDistance > (agent.maxDist * agent.maxDist))
            {
                navAgent.destination = agent.marker.position;
            }
            if (sqrDistance < (agent.maxTalkDist * agent.maxTalkDist))
            {
                switch (agent.task)
                {
                    case AiAgent.Task.Talk:
                        agent.Talk();
                        break;
                        //case Task.
                        //break;
                }
            }
            /*if (targetPosition != null)
            {
                navAgent.destination = marker.position;
            }*/
            timer = 0f;
        }
    }

    public void Jump(bool jump)
    {

    }
}
