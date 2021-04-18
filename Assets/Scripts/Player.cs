using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variaveis
    public float vel;
    public float forceJump;
    public bool isJumping;
    public bool doubleJump;
    private Rigidbody2D rig;
    private Animator anim;
    bool isBlowing; //desativa o pulo quando usa o ventilador

    //metodos
    void Start()
    {
        rig = GetComponent<Rigidbody2D>(); //aqui minha variavel esta pegando um componente do meu game object player
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        move();
        Jump();

    }

    void move()
    {
        //*********
        //tranform.position move sem usar fisica é uma forma mais basica
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        //transform.position += movimento * Time.deltaTime * vel;
        //*******

        float movement = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(movement * vel, rig.velocity.y);
        //esta e a propriedade da animacao
        if (movement > 0.5f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (movement < 0.5f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f); //rotacao do objeto eixo x
        }

        if (movement == 0f)
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isBlowing)
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, forceJump), ForceMode2D.Impulse); //forcemode e uma propriedade do addforce
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if (doubleJump)
                {
                    rig.AddForce(new Vector2(0f, forceJump), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }

        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 3)
        {
            isJumping = false;
            anim.SetBool("jump", false); //aqui verifico se ele ta tocando o chao para a anim de jump ser false
        }
        if (col.gameObject.tag == "spike")
        {
            GameController.instance.showGameOver();
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "saw")
        {
            GameController.instance.showGameOver();
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == 3)
        {
            isJumping = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
        {
            isBlowing = true;
        }
    }
    void OnTriggerExitr2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
        {
            isBlowing = false;
        }
    }
}
