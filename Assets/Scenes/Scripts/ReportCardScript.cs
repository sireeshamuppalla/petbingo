using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using System.Linq;
using Fractions; //Nugget Package
using UnityEngine.EventSystems;
using System.Diagnostics.Tracing;

public class ReportCardScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Text playerName, EasyPercentage, MedPercentage, HardPercentage, EasyCorrect, MedCorrect, HardCorrect;
    void Start()
    {
        playerName.GetComponent<Text>().text = "Nik";
        EasyCorrect.GetComponent<Text>().text = "Answered correctly "+ FA.easyScore  +" out of "+FA.easyProblemCount +" questions";
        MedCorrect.GetComponent<Text>().text = "Answered correctly " + FA.medScore + " out of " + FA.medProblemCount + " questions";
        HardCorrect.GetComponent<Text>().text = "Answered correctly " + FA.hardScore + " out of " + FA.hardProblemCount + " questions";

        if (FA.easyProblemCount == 0 )
        {
            FA.easyProblemCount = 1;
        }
        else if (FA.medProblemCount == 0)
        {
            FA.medProblemCount = 1;
        }
        else if (FA.hardProblemCount == 0)
        {
            FA.hardProblemCount = 1;
        }

        
        EasyPercentage.GetComponent<Text>().text = (int)((FA.easyScore / (float)FA.easyProblemCount) * 100) + "%";
        MedPercentage.GetComponent<Text>().text = (int)((FA.medScore / (float)FA.medProblemCount) * 100) + "%";
        HardPercentage.GetComponent<Text>().text = (int)((FA.hardScore / (float)FA.hardProblemCount) * 100)+ "%";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
