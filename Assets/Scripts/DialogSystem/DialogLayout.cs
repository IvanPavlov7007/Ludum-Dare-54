using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class DialogLayout : MonoBehaviour
{
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI text;
    public Image face;

    public void setText(string txt, bool fromRight)
    {
        if (text != null)
        {
            text.SetText(txt);
            text.alignment = fromRight ? TextAlignmentOptions.Right : TextAlignmentOptions.Left;
        }
    }

    public void setName(string txt, bool fromRight)
    {
        if (characterName != null)
        {
            characterName.SetText(txt);
            characterName.alignment = fromRight ? TextAlignmentOptions.Right : TextAlignmentOptions.Left;
        }
    }

    public void setIcon(Sprite s)
    {
        if (face != null)
            face.sprite = s;
    }
}
