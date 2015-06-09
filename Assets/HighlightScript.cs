using UnityEngine;
using System.Collections;

public class HighlightScript : MonoBehaviour
{

    public GameObject HighlightObject;

    // Use this for initialization
    void Start()
    {
        OnMouseExit();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        HighlightObject.SetActive(true);
    }
    void OnMouseExit()
    {
        HighlightObject.SetActive(false);
    }

    public void HideHighlight()
    {
        OnMouseExit();
    }

}
