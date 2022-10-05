using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speaker : MonoBehaviour
{
    // Start is called before the first frame update
    public Dialogue dialogue;
    private Dialogue[] choices;
    public TextMeshProUGUI text;
    public TextMeshProUGUI choiceText;
    int index;
    void Start()
    {
        Clear();
        if (dialogue != null)
        {
            text.text = dialogue.Text();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
            
                if (dialogue.Dialogues.Count > 0)
                {
                    if (dialogue.Dialogues[index] != null)
                    {
                        dialogue = dialogue.Dialogues[index];
                        Speak();
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    Quit();
                    return;
                }
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                index++;
                if(index > dialogue.Dialogues.Count-1)
                {
                    index = 0;
                }
                if (dialogue.Dialogues.Count >= 1)
                {
                    if (dialogue.Dialogues[index] != null)
                    {
                        //dialogue = dialogue.Dialogues[0];
                        //Speak();
                        DisplayChoice(dialogue.Dialogues[index].Title());
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    Quit();
                    return;
                }
            }
            
        }
    }

    public void SetDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
    }

    public void GetChoices()
    {
        this.choices = dialogue.Choices();
        if(choices.Length > 0)
        {

        }
    }

    public void Speak()
    {
        text.text = dialogue.Text();
        index = 0;
        if(dialogue.Dialogues.Count > 0)
        {
            if (dialogue.Dialogues[0] != null)
            {
                DisplayChoice(dialogue.Dialogues[0].Title());
            }
            else
            {
                NullChoice();
            }
        }
        else
        {
            NullChoice();
        }

    }

    void DisplayChoice(string title)
    {
        choiceText.text = title;
    }

    public void Quit()
    {
        text.text = "";
        choiceText.text = "";
        dialogue = null;
    }

    public void Clear()
    {
        text.text = "";
        choiceText.text = "";
    }

    public void NullChoice()
    {
        choiceText.text = "";
    }

    public bool Check()
    {
        if(dialogue != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
