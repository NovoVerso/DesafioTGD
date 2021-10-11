using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodies : MonoBehaviour
{
    private Animator anim;
    private enum ActiveStates {idle, attacking, dying};
    private ActiveStates state = ActiveStates.idle;
    private GameObject player;
    private SpriteRenderer sprite;
    private bool canAttack = true;
    private float attackCooldown = 3;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (canAttack == true)
        {
            if (player.transform.position.y-.1 <= gameObject.transform.position.y && player.transform.position.y + .1 >= gameObject.transform.position.y)
            {
                Debug.Log("METE BALA");
                state = ActiveStates.attacking;
                canAttack = false;
            }
        }
        UpdateAction();
        UpdateAnimation();
    }

    public void hit()
    {
        state = ActiveStates.dying;
        Debug.Log("tô por baixo");
        Destroy(gameObject, .6f) ;
    }

    private void CreateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
        if (sprite.flipX)
        {
            newBullet.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void UpdateAction()
    {
        if (canAttack == false || state != ActiveStates.dying)
        {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0) {
                canAttack = true;
                attackCooldown = 3;
                Debug.Log("Atacar de novo!");
            }
        }

    }

    private void attackEnd()
    {
        CreateBullet();
        state = ActiveStates.idle;
    }

    private void UpdateAnimation()
    {
        if (state != ActiveStates.dying)
        {

        }

        anim.SetInteger("state", (int) state);
        
    }
}
