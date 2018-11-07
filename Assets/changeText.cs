using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeText : MonoBehaviour
{
    public Text DisplayedText;

    private string LetterToWrite;
    string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

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
            if (e.keyCode.ToString() == LetterToWrite) //if letter typed is correct
            {
                DisplayedText.text = "Good job";
                LetterToWrite = Alphabet[Random.Range(0, Alphabet.Length)];
                DisplayedText.text = LetterToWrite;
            } else
            {
                GetComponent<CameraShake>().ShakeCamera(20f, 1f);
                //sound
            }
        }
    }
}
