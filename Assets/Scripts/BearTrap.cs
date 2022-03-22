using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    private Animator Anim;
    bool armado = true;

    public HearthPoints hp;

    public AudioSource audios;

    public AudioClip ativado;
    public AudioClip reativando;

    void Start()
    {
        Anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pe") && armado == true) 
        {
            hp.Damage();
            armado = false;
            Anim.Play("BearTrigger");
            audios.PlayOneShot(ativado);
            StartCoroutine(TrapRoutine());
        }
    }

    IEnumerator TrapRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        Anim.Play("BearReset");
        audios.PlayOneShot(reativando);
        armado = true;
    }
}
