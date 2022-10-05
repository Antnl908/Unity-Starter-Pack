using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public AiAgent agent;
    RaycastHit rHit;
    NavMeshHit nHit;
    Ray ray;

    public Transform marker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            //Debug.Log("Fire!");
            FireRay();
        }
    }

    void FireRay()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out rHit, 1000f))
        {
            marker.position = rHit.point;
            agent.targetT.position = rHit.point;
            agent.SetTargetPosition(nHit.position);
            Talker t = rHit.transform.GetComponent<Talker>();
            if(t != null)
            {
                //Debug.Log("Talker found!");
                agent.SetTalker(t);
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
}
