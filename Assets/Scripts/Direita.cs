using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direita : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Golpear();
        }
    }

    public void Golpear()
    {
        
        Debug.Log("GolpeDireita");
    }
}
