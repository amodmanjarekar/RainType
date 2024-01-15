using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

// This script handles the behaviour of the Menu Screen UI
public class MainMenuController : MonoBehaviour
{
    UIDocument ui;
    VisualElement root;
    Button startBtn;
    Button exitBtn;

    void OnEnable()
    {
        ui = GetComponent<UIDocument>();
        root = ui.rootVisualElement;
        startBtn = root.Q<Button>("button-start");
        exitBtn = root.Q<Button>("button-exit");

        startBtn.RegisterCallback<ClickEvent>(StartGame);
        exitBtn.RegisterCallback<ClickEvent>(ExitGame);
    }

    private void StartGame(ClickEvent e)
    {
        SceneManager.LoadScene("Game");
    }
    private void ExitGame(ClickEvent e)
    {
        Application.Quit();
    }

}
