using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : MonoBehaviour
{
    Collider col;
    AiAgent ai;
    public Speaker speaker;
    public Dialogue myDialogue;
    public Dialogue[] myDialogues;
    void Awake()
    {
        col = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnTriggerEnter(Collider other)
    {
        ai = null;
        ai = other.transform.GetComponent<AiAgent>();
        if(ai != null)
        {
            speaker.SetDialogue(myDialogue);
            if(speaker.Check())
            {
                speaker.Speak();
                ai.state = AiAgent.State.Talk;
            }
        }
    }*/

    public Dialogue GetDialogue()
    {
        return myDialogue;
    }
}
