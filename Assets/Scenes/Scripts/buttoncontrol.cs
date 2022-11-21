 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class buttoncontrol : MonoBehaviour
{
    public TextMeshProUGUI user_name;
    public TMP_InputField user_inputField;

    //public void Awake()
    //{
    //    PlayerPrefs.SetString("Name", "Player1");
    //    PlayerPrefs.Save();
    //}

     void Start()
    {
        user_name.text = PlayerPrefs.GetString("Name");
    }

    public void setName()
    {
        
        user_name.text = user_inputField.text;

        PlayerPrefs.SetString("Name",user_name.text);
        PlayerPrefs.Save();
    }

    //public void SetString(string KeyName, string Value)
    //{
    //    PlayerPrefs.SetString(KeyName, Value);
    //}

    //public string GetString(string KeyName)
    //{
    //    return PlayerPrefs.GetString(KeyName);
    //}

}
