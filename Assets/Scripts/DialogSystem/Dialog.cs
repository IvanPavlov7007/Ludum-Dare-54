using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog", order = 2)]
public class Dialog : MonoBehaviour
{
    [SerializeField]
    public bool disableMovement;

    [SerializeField]
    protected Phrase[] lines;

    protected int index = -1;

    public virtual void Reset()
    {
        index = -1;
    }

    public virtual DialogResult next()
    {
        index++;
        if (index < lines.Length)
            return new DialogResult(lines[index]);
        else
            return new DialogResult(DialogResult.ResultType.End);
    }
}

[System.Serializable]
public struct Phrase 
{
    public string line;
    public string name;
    public Sprite icon;
    public bool fromTheRigt;
}

public class DialogResult
{
    public readonly ResultType resultType;
    public readonly Phrase phrase;
    public enum ResultType {NextLine, Action, End}

    public DialogResult(Phrase text)
    {
        this.phrase = text;
        resultType = ResultType.NextLine;
    }

    public DialogResult(ResultType result)
    {
        this.resultType = result;
    }
}
