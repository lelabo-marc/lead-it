﻿using UnityEngine;
using System.Collections;

public class DaysLabel : MonoBehaviour
{
    public LastScoreCompo scores;

    void Start()
    {
        this.gameObject.GetComponent<UILabel>().text = "You successfully governed during\n" + scores.GetDay().ToString() + " Days";
    }
}
