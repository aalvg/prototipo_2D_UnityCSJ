using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;

    public float speed;
    public Transform rightColl;
    public Transform leftColl;
    public Transform headPoint;
    public LayerMask layer;
    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;
    private bool colliding;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y); //diferente do godot velocity na unity é uma propriedade do rigidbody
        colliding = Physics2D.Linecast(rightColl.position, leftColl.position);
        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y); //faz a rotacao do objeto
            speed *= -1f; //toda vez que o colliding acima for chamado o speed vai inverter  negativo e positivo

        }
    }
    bool playerDestroyed;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            float height = col.contacts[0].point.y - headPoint.position.y; //verifica o ponto y que o player bateu
            if (height > 0 && !playerDestroyed)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse); //faz o player da um pulinho ao pular na cabeca do inimigo
                speed = 0;
                anim.SetTrigger("die");
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                rig.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject, 0.33f);
            }
            else
            {
                playerDestroyed = true;
                GameController.instance.showGameOver();
                Destroy(col.gameObject);
            }

        }
    }
}
