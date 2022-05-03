using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 5;
    public float Action;
    public Vector3 Target;
    public Rigidbody2D RB;
    public Animator Animator;

    private void Start() 
    {
        Target = new Vector3(Random.Range(-8, 8), -4.4f, 0);
        Action = Random.Range(1, 2);
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Target) < 1)
        {
            Target = new Vector3(Random.Range(-8, 8), -4.4f, 0);
            Action = Random.Range(0, 3);
            
            if (Action == 0) {RB.AddForce(new Vector2(0, Speed), ForceMode2D.Impulse);}
            if (Action == 1) {RB.AddForce(new Vector2(0, Speed * 2), ForceMode2D.Impulse);}
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

        //if (Health.Instance.EnemyHP <= 0) {Destroy(gameObject);}
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

        if (Col.gameObject.tag == "Special")
        {
            Health.Instance.DoDamage(10);
        }
    }
}
