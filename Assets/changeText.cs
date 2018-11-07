using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeText : MonoBehaviour
{
    public Text DisplayedText;

    private string LetterToWrite;

    void Start()
    {
        //Text sets your text to say this message
        LetterToWrite = "A";
        DisplayedText.text = LetterToWrite;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //PAUSE HERE
        }
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (e.keyCode.ToString() == LetterToWrite)
            {
                DisplayedText.text = "Good job";
            } else
            {
                //camera shake
                //sound
            }
        }
    }
}
