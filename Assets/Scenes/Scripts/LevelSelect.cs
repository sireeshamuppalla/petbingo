using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public void getText()
    {
        TextMeshProUGUI txt = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        FA.levelSelect = txt.text;
    }  
}
