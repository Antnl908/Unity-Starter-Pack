using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Dialogue/choices")]
//[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueSystem/Dialogue")]

public class Dialogue : ScriptableObject
{
    //https://www.youtube.com/watch?v=81kyAHy9gUE

    [SerializeField] private DialogueContainer dialogueContainer;
    [SerializeField] private Dialogue parentDialogue;

    [SerializeField] private List<Dialogue> _dialogues = new List<Dialogue>();
    public List<Dialogue> Dialogues { get => _dialogues; set => _dialogues = value; }


    public DialogueContainer MyDialogueContainer { get => dialogueContainer; }
    public Dialogue MyParentContainer { get => parentDialogue; }
    // Start is called before the first frame update
    //public List<dialogue> choices = new List<dialogue>();
    //public Dialogue[] choices;
    public string title;
    public string text;

    public string Title()
    {
        return title;
    }

    public string Text()
    {
        return text;
    }

    public Dialogue[] Choices()
    {
        return Dialogues.ToArray();//null; //choices;
    }

#if UNITY_EDITOR
        [ContextMenu("Make New Dialogue")]
        private void NewDialogue()
        {
            Dialogue newDialogue = ScriptableObject.CreateInstance<Dialogue>();
            newDialogue.InitialiseDialogue(this);
            _dialogues.Add(newDialogue);

            AssetDatabase.AddObjectToAsset(newDialogue, this);
            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(newDialogue);
        }
#endif
#if UNITY_EDITOR
    public void Initialise(DialogueContainer myDialogueContainer)
    {
        //this.dialogueContainer = myDialogueContainer;
        this.dialogueContainer = myDialogueContainer;
    }

    public void InitialiseDialogue(Dialogue myDialogueContainer)
    {
        this.parentDialogue = myDialogueContainer;
    }

    [ContextMenu("Rename to name")]
    private void Rename()
    {
        this.name = title;
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(this);
    }

    [ContextMenu("Delete this")]
    private void Delete()
    {
        this.name = title;
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(this);
    }

    [ContextMenu("Delete all")]
    private void DeleteAll()
    {

        for(int i = _dialogues.Count; i-- > 0; )
        {
            Dialogue tmp = _dialogues[i];
            _dialogues.Remove(tmp);
            Undo.DestroyObjectImmediate(tmp);
        }
        
        AssetDatabase.SaveAssets();
        //EditorUtility.SetDirty(this);
    }
#endif

}
