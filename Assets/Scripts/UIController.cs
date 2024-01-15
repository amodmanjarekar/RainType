using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// This script handles the behaviour of the Game UI
public class UIController : MonoBehaviour
{
    UIDocument ui;
    VisualElement root;
    Label comboValue;
    Label scoreValue;
    Label livesValue;

    void OnEnable()
    {
        ui = GetComponent<UIDocument>();
        root = ui.rootVisualElement;
        comboValue = root.Q<Label>("combo-value");
        scoreValue = root.Q<Label>("score-value");
        livesValue = root.Q<Label>("lives-value");
    }

    public void UpdateComboUI(int value)
    {
        comboValue.text = value.ToString();
    }
    public void UpdateScoreUI(int value)
    {
        scoreValue.text = value.ToString();
    }
    public void UpdateLivesUI(int value)
    {
        livesValue.text = value.ToString();
    }

}
