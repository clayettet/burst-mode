using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Menus
{
    MainMenu,
    GameScene
}

public class MenuManager : MonoBehaviour {

    public static MenuManager Instance;

    public changeText GameManager;
   
    public Button PlayButton, ExitButton;
    public GameObject Menu;
    public GameObject TimerGame;

    public Text BestScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Only one Menu Manager can be instanciated at a time");
            DestroyImmediate(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        BestScore.text = PlayerPrefs.GetInt("Best Score", 0).ToString();
    }

    public void DisplayMenu(Menus m)
    {
        Menu.SetActive(m == Menus.MainMenu);
        TimerGame.SetActive(m == Menus.GameScene);
    }

    public void OnPlayButtonClicked()
    {
        GameManager.LaunchGame();
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
