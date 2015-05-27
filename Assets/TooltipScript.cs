using UnityEngine;
using System.Collections;

public class TooltipScript : MonoBehaviour
{
    public string Text;

    public string toolTipText = ""; // set this in the Inspector

    private string currentToolTipText = ""; 
    private GUIStyle guiStyleFore; 
    private GUIStyle guiStyleBack;

    public void Start()
    {
        guiStyleFore = new GUIStyle(); 
        guiStyleFore.normal.textColor = Color.white;
        guiStyleFore.alignment = TextAnchor.UpperCenter; 
        guiStyleFore.wordWrap = true;
        guiStyleBack = new GUIStyle();
        guiStyleBack.normal.textColor = Color.black;
        guiStyleBack.alignment = TextAnchor.UpperCenter; 
        guiStyleBack.wordWrap = true;
    }

    public void OnMouseEnter()
    {
        currentToolTipText = toolTipText;
    }

    public void OnMouseExit() { currentToolTipText = ""; }

    public void OnGUI()
    {
        if (currentToolTipText == "") return;
        var x = Event.current.mousePosition.x; var y = Event.current.mousePosition.y;
        GUI.Label(new Rect(x - 149, y + 21, 300, 60), currentToolTipText, guiStyleBack);
        GUI.Label(new Rect(x - 150, y + 20, 300, 60), currentToolTipText, guiStyleFore);
    }


}
