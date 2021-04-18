using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public float jumpForce;
    public bool isUp;
    public float health = 4;
    public Animator anim;
    public GameObject Effect;
    void FixedUpdate()
    {
        if (health <= 0)
        {
            Instantiate(Effect, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject); //destroi o objeto pai 
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (isUp)
            {
                anim.SetTrigger("hit");
                health--;
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
            else
            {
                anim.SetTrigger("hit");
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -jumpForce), ForceMode2D.Impulse);
            }


        }
    }
}
