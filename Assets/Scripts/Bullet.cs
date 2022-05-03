using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10;
    
    private Vector2 Direction;
    private bool CanGetDirection;
    private bool Right;
    private bool Left;

    public Rigidbody2D RB;
    public SpriteRenderer SR;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 3, true);
        
        SR.enabled = true;

        if (PlayerController.Instance.X == 0)
        {
            if (PlayerController.Instance.SR.flipX == false)
            {
                transform.position = PlayerController.Instance.gameObject.transform.position + new Vector3(1,0,0);
                Direction = Vector2.right;
            }

            if (PlayerController.Instance.SR.flipX == true)
            {
                transform.position = PlayerController.Instance.gameObject.transform.position + new Vector3(-1,0,0);
                Direction = Vector2.left;
            }
        }
        if (PlayerController.Instance.X > 0)
        {
            transform.position = PlayerController.Instance.gameObject.transform.position + new Vector3(1,0,0);
            Direction = Vector2.right;
        }
        if (PlayerController.Instance.X < 0)
        {
            transform.position = PlayerController.Instance.gameObject.transform.position + new Vector3(-1,0,0);
            Direction = Vector2.left;
        }
        if (PlayerController.Instance.Y > 0)
        {
            transform.position = PlayerController.Instance.gameObject.transform.position + new Vector3(0,1,0);
            Direction = Vector2.up;
        }
        if (PlayerController.Instance.Y < 0)
        {
            transform.position = PlayerController.Instance.gameObject.transform.position + new Vector3(0,-1,0);
            Direction = Vector2.down;
        }  
    }

    private void FixedUpdate() 
    {
        if (CanGetDirection == true)
        {
            Start();
            CanGetDirection = false;
        }

        RB.velocity = Direction * Speed;
    }

    private void OnCollisionEnter2D(Collision2D Col) 
    {

        if (Col.gameObject.tag == "Enemy")
        {
            Health.Instance.DoDamage(1);
            Health.Instance.GetSpecialPoints(0.1f);
        }
        gameObject.SetActive(false);
        SR.enabled = false;
        CanGetDirection = true;
    }
}
