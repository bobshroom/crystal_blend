using System.Collections.Generic;
using UnityEngine;

public class stoneTipsEffect : MonoBehaviour
{
    [SerializeField] private List<Sprite> stoneSprites;
    private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>();
    public float upPower = 150;
    public float sidePower = 50;
    public float rotatePower = 200;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.sprite = stoneSprites[Random.Range(0, stoneSprites.Count)];
        Destroy(gameObject, 3.0f);
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-sidePower, sidePower), Random.Range(upPower, upPower * 1.2f)));
        gameObject.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-rotatePower, rotatePower));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
