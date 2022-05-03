using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : MonoBehaviour
{
    public float Speed = 5;
    public float Action;
    public Vector3 Target;
    public Rigidbody2D RB;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public bool delay = false;

    public GameObject arrow;

    void Start()
    {
        Target = new Vector3(Random.Range(-8, 8), -3.7f, 0);
        Action = Random.Range(1, 2);
        StartCoroutine(TiroRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (delay == true && timeBtwShots <= 0)
        {
            Instantiate(arrow, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Target) < 1)
        {
            Target = new Vector3(Random.Range(-8, 8), -3.7f, 0);
            Action = Random.Range(0, 3);

            if (Action == 0) { RB.AddForce(new Vector2(0, Speed), ForceMode2D.Impulse); }
            if (Action == 1) { RB.AddForce(new Vector2(0, Speed * 2), ForceMode2D.Impulse); }
            if (Action == 2)
            {
                if (Target.x > transform.position.x)
                {
                    RB.AddForce(new Vector2(Speed, 0), ForceMode2D.Impulse);
                }
                if (Target.x < transform.position.x)
                {
                    RB.AddForce(new Vector2(-Speed, 0), ForceMode2D.Impulse);
                }
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
    }

    IEnumerator TiroRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        delay = true;
    }

    private void OnCollisionEnter2D(Collision2D Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            if (transform.position.x >= Col.gameObject.transform.position.x)
            {
                RB.AddForce(new Vector2(Speed, 0), ForceMode2D.Impulse);
            }
            if (transform.position.x < Col.gameObject.transform.position.x)
            {
                RB.AddForce(new Vector2(-Speed, 0), ForceMode2D.Impulse);
            }
        }
    }
}

