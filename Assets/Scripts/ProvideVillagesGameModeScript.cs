using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class ProvideVillagesGameModeScript : MonoBehaviour
{

    public List<VillageScript> VillagesToProvide;
    public Text ScoreText;
    public RectTransform MissionCompletedUI;

    // Use this for initialization
    void Start()
    {
        MissionCompletedUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int numProvided = VillagesToProvide.Count(v => v.IsVillageProvided());

        ScoreText.text = "Villages provided with resources: " + numProvided + " of " + VillagesToProvide.Count;

        if (numProvided == VillagesToProvide.Count)
        {
            MissionCompletedUI.gameObject.SetActive(true);
        }

    }
}
