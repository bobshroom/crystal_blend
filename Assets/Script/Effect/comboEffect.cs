using System.Collections;
using TMPro;
using UnityEngine;

public class comboEffect : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    [SerializeField] private float muki;
    [SerializeField] private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float angle = Random.Range(-muki, muki);
        Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.up;

        // Rigidbody2Dがあれば速度を与える
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = dir * speed;
        }
        GetComponent<TMP_Text>().text = "Combo x" + (ComboManager.instance.currentCombo - 1);
        GetComponent<TMP_Text>().fontSize = 50 + 10 * (ComboManager.instance.currentCombo - 1);
        StartCoroutine(comboChangeColor());
        Invoke("destroy", 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void destroy()
    {
        Destroy(this.gameObject);
    }

    IEnumerator comboChangeColor()
    {
        TMP_Text text = GetComponent<TMP_Text>();
        while (true)
        {
            text.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            text.color = Color.yellow;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
