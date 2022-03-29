using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthPoints : MonoBehaviour
{
    public GameObject[] hearts;
    public int h = 3;
    public Door door;
    //public IronKey key;
    public PlayerController player;
    public Animator Anim;

    public AudioSource audios;

    public AudioClip dano1;
    public AudioClip dano2;
    public AudioClip dano3;


    void Start()
    {
        h = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (h <= 0)
        {
            player.life = false;
            StartCoroutine(ResetarRoutine());
        }
    }

    public void Damage()
    {
        h--;
        switch (h)
        {
            case 2:
                audios.PlayOneShot(dano1);
                break;
            case 1:
                audios.PlayOneShot(dano2);
                break;
            case 0:
                audios.PlayOneShot(dano3);
                break;
        }
        hearts[h].SetActive(false);
    }

    IEnumerator ResetarRoutine()
    {
        Anim.Play("Morte");
        yield return new WaitForSeconds(2.0f);
        if(door.chave == true) 
        {
            door.Resetar();
            //key.Resetar();
        }
        player.Resetar();
        hearts[0].SetActive(true);
        hearts[1].SetActive(true);
        hearts[2].SetActive(true);
        h = 3;
        player.life = true;
        Anim.Play("Parado");
    }
    
}
