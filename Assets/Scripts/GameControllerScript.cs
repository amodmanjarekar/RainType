using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles the behaviour of the entire game
public class GameControllerScript : MonoBehaviour
{
    public GameObject letter;
    public int lives = 5; // max lives
    public int score = 0;
    int combo = 0;
    public float delay = 2.5f; // delay between spawning

    GameObject baseline; // gameobject for the line at bottom
    UIController ui;

    void Start()
    {
        lives = 5; // initialize max lives

        baseline = GameObject.Find("Baseline");
        ui = GameObject.Find("GameUI").GetComponent(typeof(UIController)) as UIController;

        StartCoroutine(SpawnLetter());
    }

    void Update()
    {
        // progressively reduce spawn delay (until 0.2 seconds)
        if (delay > 1f)
        {
            delay -= Time.deltaTime * 0.03f;
        }
        else if (delay > 0.2f)
        {
            delay -= Time.deltaTime * 0.01f;
        }

        // once lives hit zero,
        if (lives <= 0)
        {
            StopCoroutine(SpawnLetter()); // stop spawning new letters

            // stop letter fall for each existing letter in scene
            GameObject[] letters = GameObject.FindGameObjectsWithTag("Letter");
            foreach (GameObject lobj in letters)
            {
                Letter l = lobj.GetComponent(typeof(Letter)) as Letter;
                l.PauseFall();
            }

            StartCoroutine(EndGame()); // wait for 3 seconds and return to menu
        }
    }

    IEnumerator SpawnLetter()
    {
        while (lives > 0)
        {
            Instantiate(letter, new Vector3(Random.Range(-5, 5), 6.0f, 0.0f), Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }

    public void ReduceLives()
    {
        if (lives > 0)
        {
            lives -= 1;
            ui.UpdateLivesUI(lives);
        }

        // buzz the line
        StartCoroutine(ShowFail());
        IEnumerator ShowFail()
        {
            baseline.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.3f);
            baseline.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // function to change the combo value
    // passing true as an argument will incerase combo by 1
    // passing false as an argument will revert combo back to 0
    public void EditCombo(bool isAdd)
    {
        if (lives > 0)
        {
            if (isAdd)
            {
                combo += 1;
            }
            else
            {
                combo = 0;
            }
            ui.UpdateComboUI(combo);
        }
    }

    // function to add the score
    public void AddScore(int deltaScore)
    {
        if (lives > 0)
        {
            score += deltaScore * (combo + 1);
            ui.UpdateScoreUI(score);
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("MainMenu");
    }
}
