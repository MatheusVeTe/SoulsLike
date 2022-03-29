using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float Velocidade;
    bool ViradoDireita = true;
    private Rigidbody2D rb;
    public float ForcaPulo;
    public int ContaPulos;
    private Animator Anim;
    public bool Ataque = false;
    public AudioSource audios;

    public bool life = true;

    public int ataqueN = 1;
    public int pulo = 1;

    public bool rollCond = true;

    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;
    public AudioClip sword1;
    public AudioClip sword2;
    public AudioClip sword3;
    public AudioClip jump1;
    public AudioClip jump2;
    public AudioClip jump3;


    void Start()
    {
        audios = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life == true) 
        {
            float PosX = Input.GetAxis("Horizontal") * Time.deltaTime * Velocidade;
            transform.Translate(new Vector3(PosX, 0, 0));



            if (PosX != 0 && Anim.GetBool("Pulo") == false)
            {
                Anim.SetBool("Corre", true);
            }
            else
            {
                Anim.SetBool("Corre", false);
            }

            // Anim.SetBool("Corre", PosX!=0);

            if (PosX < 0 && ViradoDireita)
            {
                Gira();
                ViradoDireita = false;
            }
            if (PosX > 0 && !ViradoDireita)
            {
                Gira();
                ViradoDireita = true;
            }

            /*
             if(PosX<0 && ViradoDireita)||(PosX>0 && !ViradoDireita)
            {
                Gira();
                ViradoDireita = !ViradoDireita;
            }
            */

            if (Input.GetKeyDown(KeyCode.LeftShift) && rollCond == true)
            {
                {
                    if (ViradoDireita == true)
                    {
                        StartCoroutine(coroutineRollD());
                    }
                    else if (ViradoDireita == false)
                    {
                        StartCoroutine(coroutineRollE());
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && ContaPulos <= 0)
            {
                rb.AddForce(new Vector2(0, 1f) * ForcaPulo);
                ContaPulos++;
                Anim.SetBool("Pulo", true);
                switch (pulo)
                {
                    case 3:
                        audios.PlayOneShot(jump3);
                        pulo = 1;
                        break;
                    case 2:
                        audios.PlayOneShot(jump2);
                        pulo = 3;
                        break;
                    case 1:
                        audios.PlayOneShot(jump1);
                        pulo = 2;
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && Ataque == false)
            {
                Anim.Play("Ataque1");
                Ataque = true;
                switch (ataqueN)
                {
                    case 3:
                        audios.PlayOneShot(attack3);
                        audios.PlayOneShot(sword3);
                        ataqueN = 1;
                        break;
                    case 2:
                        audios.PlayOneShot(attack2);
                        audios.PlayOneShot(sword2);
                        ataqueN = 3;
                        break;
                    case 1:
                        audios.PlayOneShot(attack1);
                        audios.PlayOneShot(sword1);
                        ataqueN = 2;
                        break;
                }

            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && Ataque == true)
            {
                Anim.Play("Ataque2");
                Ataque = false;
                switch (ataqueN)
                {
                    case 3:
                        audios.PlayOneShot(attack3);
                        audios.PlayOneShot(sword3);
                        ataqueN = 1;
                        break;
                    case 2:
                        audios.PlayOneShot(attack2);
                        audios.PlayOneShot(sword2);
                        ataqueN = 3;
                        break;
                    case 1:
                        audios.PlayOneShot(attack1);
                        audios.PlayOneShot(sword1);
                        ataqueN = 2;
                        break;
                }
            }
        }
    }

    IEnumerator coroutineRollD()
    {
        rollCond = false;
        rb.AddForce(new Vector2(150f, 0));
        Anim.Play("Roll");
        yield return new WaitForSeconds(1.0f);
        rollCond = true;
    }

    IEnumerator coroutineRollE()
    {
        rollCond = false;
        rb.AddForce(new Vector2(-150f, 0));
        Anim.Play("Roll");
        yield return new WaitForSeconds(1.0f);
        rollCond = true;
    }

    void Gira()
    {
        Vector3 GuardaEscala;
        GuardaEscala = transform.localScale;
        GuardaEscala.x *= -1; // GuardaEscala.x = GuardaEscala.x * (-1);
        transform.localScale = GuardaEscala;
    }

      private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("plataforma"))
        {
            ContaPulos = 0;
            Anim.SetBool("Pulo", false);
        }

    }
    public void Resetar()
    {
        transform.position = new Vector3(-27, -2, 0);
    }
}
