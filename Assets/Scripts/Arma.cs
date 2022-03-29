using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public Transform tiroPonto;
    public GameObject bulletPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, tiroPonto.position, tiroPonto.rotation);
    }
}
