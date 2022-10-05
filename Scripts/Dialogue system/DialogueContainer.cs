using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Dialogue/container")]
public class DialogueContainer : ScriptableObject
{
    //https://www.youtube.com/watch?v=81kyAHy9gUE
    [SerializeField] private List<Dialogue> _dialogues = new List<Dialogue>();
    public List<Dialogue> Dialogues { get => _dialogues; set => _dialogues = value; }
    //public Dialogue[] dialogues;
    public Dialogue startDialogue;

#if UNITY_EDITOR
    [ContextMenu("Make New")]
    private void MakeNewDialogue()
    {
        Dialogue dialogue = ScriptableObject.CreateInstance<Dialogue>();
        dialogue.Initialise(this);
        _dialogues.Add(dialogue);

        AssetDatabase.AddObjectToAsset(dialogue, this);
        AssetDatabase.SaveAssets();

        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(dialogue);
    }
#endif

}
