using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esquerda : MonoBehaviour
{
    public AnimationClip golpeE;
    Animation anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            anim.clip = golpeE;
            anim.Play();
            Debug.Log("GolpeEsquerda");
        }
    }
}
