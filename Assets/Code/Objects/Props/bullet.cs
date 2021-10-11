using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private float bulletSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (sprite.flipX)
        {
            body.velocity = new Vector2(bulletSpeed, 0f);
        }
        else
        {
            body.velocity = new Vector2(-bulletSpeed, 0f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = gameObject.transform.position;
        if (position.y >= 11.5 || position.x >=17.5 || position.y <= -7.5 || position.x <= -10.5)
        {
            Debug.Log("Saiu!");
            Destroy(gameObject);
        }
    }
}
