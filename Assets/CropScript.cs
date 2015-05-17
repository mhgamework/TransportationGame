using System;
using System.Collections.Generic;
using Assets.Model;
using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class CropScript : MonoBehaviour
{
    public float GrowthPercentage = 0;
    public float GrowthSpeed = 0.2f;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var numLevels = transform.childCount;
        int currentLevel = 3;
        if (GrowthPercentage < 0.2) currentLevel = 0;
        else if (GrowthPercentage < 0.5) currentLevel = 1;
        else if (GrowthPercentage < 1) currentLevel = 2;

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(i == currentLevel);



        GrowthPercentage += GrowthSpeed*Time.deltaTime;

        GrowthPercentage = Mathf.Clamp(GrowthPercentage, 0, 1);
    }

  


}
