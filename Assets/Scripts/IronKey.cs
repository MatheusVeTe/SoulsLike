using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronKey : MonoBehaviour
{
    public GameObject keyobj;
    public GameObject keyspr;
    public Door door;

    public AudioSource audios;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            keyobj.SetActive(false);
            keyspr.SetActive(true);
            audios.Play();
            door.Chave();
        }
    }
    public void Resetar() 
    {
        keyobj.SetActive(true);
        keyspr.SetActive(false);
    }
}
