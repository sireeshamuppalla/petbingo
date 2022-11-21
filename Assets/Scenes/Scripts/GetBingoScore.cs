using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetBingoScore : MonoBehaviour
{
    public string bingoCount;

    // Start is called before the first frame update
    void Start()
    {
        bingoCount = FA.incrementBingo.ToString();
        GetComponent<TMP_Text>().text = bingoCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
