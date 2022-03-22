using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool chave = false;

    public AudioSource audios;

    void Start()
    {
        chave = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && chave == true)
        {
            StartCoroutine(ProxNivelRoutine());;
        }
    }
    public void Chave()
    {
        chave = true;
    }
    public void Resetar()
    {
        chave = false;
    }

    IEnumerator ProxNivelRoutine()
    {
        audios.Play();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
