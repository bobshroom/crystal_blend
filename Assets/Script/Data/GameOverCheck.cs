using UnityEngine;

public class GameOverCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("suika") && GameManager.instance.gameState == "playing")
        {
            GameManager.instance.gameOver();
        }
    }
}
