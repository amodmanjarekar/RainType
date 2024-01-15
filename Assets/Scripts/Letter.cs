using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles the behaviour of each individual letter
public class Letter : MonoBehaviour
{
    public float speed;

    public char character;
    KeyCode charKeyCode;
    GameControllerScript gc;

    public SpriteRenderer spriteRenderer;
    string spriteName = "Letters/a_UbuntuMono-Regular"; // default/fallback sprite name
    Sprite sprite;

    void Start()
    {
        // randomly generate a letter
        character = (char)Random.Range(97, 122);
        charKeyCode = (KeyCode)character;

        gc = GameObject.Find("GameController").GetComponent(typeof(GameControllerScript)) as GameControllerScript;

        spriteName = "Letters/" + character + "_UbuntuMono-Regular";
        sprite = Resources.Load<Sprite>(spriteName);
        spriteRenderer.sprite = sprite;
        spriteRenderer.size = new Vector2(1f, 2f); // for good measure :)
    }

    void Update()
    {
        transform.Translate(new Vector3(0f, -1 * speed * Time.deltaTime, 0f)); // move letter downwards

        // destroy letter if corresponding key is pressed
        if (Input.GetKeyDown(charKeyCode))
        {
            gc.AddScore(10); // add score
            gc.EditCombo(true); // true => combo += 1
            Destroy(gameObject);
        }

        // destroy letter if it leaves valid play area
        if (transform.position.y < -4.0f)
        {
            gc.ReduceLives(); // reduce lives
            gc.EditCombo(false); // false => combo = 0
            Destroy(gameObject);
        }
    }

    public void PauseFall()
    {
        speed = 0;
    }
}
