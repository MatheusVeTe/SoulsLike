using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public PlayerController player;
    public HearthPoints hp;
    public AudioSource audios;

    public Animator Anim;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pe"))
        {
            hp.Damage();
            StartCoroutine(ResetarRoutine());
        }
    }

    IEnumerator ResetarRoutine()
    {
        Anim.Play("Morte");
        audios.Play();
        yield return new WaitForSeconds(2.0f);
        player.Resetar();
        Anim.Play("Parado");
    }

}
