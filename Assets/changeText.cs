using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeText : MonoBehaviour
{
    private int Score;

    [Header("Rise In Animation")]
    public AnimationCurve RiseInAnimationCurve;
    public float RiseInDuration;
    private float RiseInElapsed;

    [Header("Score Animation")]
    public AnimationCurve BumpAnimationCurve;
    public float BumpDuration;
    private float BumpElapsed;
    public float BumpMagnitude;
    public Color BumpColor;

    [Header("Gameplay")]
    public float TimeLeft;
    private bool GameFinished = false;

    [Header("UI")]
    public Text DisplayedText;
    public Text DisplayedScore;
    public Text DisplayedTime;
    public CameraShake CameraShake;

    private string LetterToWrite;
    private string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    private List<KeyCode> keys = new List<KeyCode>();

    private bool InputBlocked;

    void Start()
    {
        for (int i = (int)KeyCode.A; i <= (int)KeyCode.Z; i++) //init tab with A-->Z keyCodes
        {
            keys.Add((KeyCode)i);
        }
        Score = 0; //init score
        StartCoroutine(OnBeginGame()); //start countdown and game
    }

    void Update()
    {
        if (!GameFinished)
        {
            if (TimeLeft <= 0.0f)
            {
                GameFinished = true;
                StartCoroutine(OnEndGame());                
            }
            else
            {
                TimeLeft = Mathf.Clamp(TimeLeft - Time.deltaTime, 0.0f, float.MaxValue);   //time manager
                DisplayedTime.text = TimeLeft.ToString("F2");
                if (!InputBlocked && Input.anyKeyDown)  // if a key is pressed
                {
                    string pressed = GetKeyPressed();
                    if (!string.IsNullOrEmpty(pressed))
                    {
                        if (GetKeyPressed().Equals(LetterToWrite))
                        {
                            LetterToWrite = Alphabet[Random.Range(0, Alphabet.Length)];
                            StartCoroutine(DisplayMiddleScreen(LetterToWrite));
                            Score++;
                            StartCoroutine(UpdateScoreUI());
                        }
                        else
                        {
                            CameraShake.Shake();
                            //TODO add error sound
                        }
                    }
                }
            }
        }
    }

    
     private IEnumerator UpdateScoreUI()
     {
        BumpElapsed = 0.0f;
        Vector3 bumpedScale = new Vector3(BumpMagnitude, BumpMagnitude, 0.0f);
        Vector3 minScale = new Vector3(1.0f, 1.0f, 1.0f);
        DisplayedScore.transform.localScale = minScale;
        while (BumpElapsed != BumpDuration)
        {
            yield return new WaitForEndOfFrame();
            BumpElapsed = Mathf.Clamp(BumpElapsed + Time.deltaTime, 0.0f, BumpDuration);
            float value = BumpAnimationCurve.Evaluate(BumpElapsed / BumpDuration);
            if(value >= 0.5f)
                DisplayedScore.text = Score.ToString();
            DisplayedScore.transform.localScale = minScale + bumpedScale*value;
            DisplayedScore.color = Color.Lerp(Color.white, BumpColor, value);

        }
        yield return null;
     }


    private IEnumerator DisplayMiddleScreen(string letter)
    {
        InputBlocked = true;
        DisplayedText.text = letter;
        RiseInElapsed = 0.0f;
        float min = 0.3f;
        Vector3 minScale = new Vector3(min, min, 1.0f);
        DisplayedText.transform.localScale = minScale;
        while (RiseInElapsed != RiseInDuration)
        {
            yield return new WaitForEndOfFrame();
            RiseInElapsed = Mathf.Clamp(RiseInElapsed + Time.deltaTime, 0.0f, RiseInDuration);
            float scale = RiseInAnimationCurve.Evaluate(RiseInElapsed / RiseInDuration);
            DisplayedText.transform.localScale = minScale + new Vector3(1.0f - min, 1.0f - min, 0.0f) * scale;

        }
        InputBlocked = false;
    }

    private IEnumerator OnBeginGame()
    {
        //TODO add sound for 3 2 1 Go
        InputBlocked = true; //block input
        DisplayedTime.enabled = false; // hide time
        StartCoroutine(DisplayMiddleScreen("3"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(DisplayMiddleScreen("2"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(DisplayMiddleScreen("1"));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(DisplayMiddleScreen("Go!"));
        yield return new WaitForSeconds(0.7f);
        DisplayedTime.enabled = true; //display timer
        InputBlocked = false;
        LetterToWrite = Alphabet[Random.Range(0, Alphabet.Length)]; //init first letter
        StartCoroutine(DisplayMiddleScreen(LetterToWrite)); //begin game
    }

    private IEnumerator OnEndGame()
    {
        DisplayedScore.enabled = false;
        StartCoroutine(DisplayMiddleScreen("Game finished!"));
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(DisplayMiddleScreen("You scored " + Score));
    }

    private string GetKeyPressed() // return the current key pressed in string format
    {
        for (int i = 0; i < keys.Count; i++)
        { //for each letter key
            if (Input.GetKeyDown(keys[i]))
            {
                return keys[i].ToString();
            }
        }
        return "";
    }
}
