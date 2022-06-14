using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance; 

    public float X;
    public float Y;
    public float Speed = 5;
    public bool Pause;
    public bool Rolled;
    public bool SpecialUsed;
    
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    public Animator Animator;
    public AudioSource AudioSource;
    public AudioClip Hit;
    public Transform Boat;

    private void Awake() 
    {
        if (Instance == null) {Instance = this;}
    }

    private void FixedUpdate() 
    {
        //Movimento
        X = Input.GetAxis("Horizontal");
        Y = Input.GetAxis("Vertical");
        transform.position += new Vector3(X, 0, 0) * Speed * Time.deltaTime;
        Animator.SetFloat("IsWalking", Mathf.Abs(X));

        if(X > 0){SR.flipX = false;}
        if(X < 0){SR.flipX = true;}
    }

    void Update()
    {
        if (Pause == false)
        {
            //Pulo
            if (Input.GetButtonDown("Jump") && Mathf.Abs(RB.velocity.y) < 0.001f)
            {
                RB.AddForce(new Vector2(0, Speed), ForceMode2D.Impulse);
                Animator.SetBool("IsJumping", true);
                AudioSource.PlayOneShot(AudioSource.clip);
            }
            if (Mathf.Abs(RB.velocity.y) < 0.001f) {Animator.SetBool("IsJumping", false);}

            //rolamento
            if (Input.GetButtonDown("Roll") && Animator.GetBool("IsRolling") == false && Health.Instance.StaminaPoints > 0)
            {
                if (X > 0) {RB.AddForce(new Vector2(Speed, 0), ForceMode2D.Impulse);}
                if (X < 0) {RB.AddForce(new Vector2(-Speed, 0), ForceMode2D.Impulse);}
                Animator.SetBool("IsRolling", true);
                AudioSource.PlayOneShot(AudioSource.clip);
                Health.Instance.UseStamina(1);
            } 
            if (Animator.GetBool("IsRolling") == true)
            {
                Physics2D.IgnoreLayerCollision(7, 6, true);
                Physics2D.IgnoreLayerCollision(7, 3, true);
            }
            if (Animator.GetBool("IsRolling") == false)
            {
                Physics2D.IgnoreLayerCollision(7, 6, false);
                Physics2D.IgnoreLayerCollision(7, 3, false);
            }

            if (Rolled == true)
            {
                Animator.SetBool("IsRolling", false); 
                Animator.SetBool("IsTakingHit", false);
            }

            //atirar
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject Bullet = ObjectPool.Instance.GetPooledObject();
                if (Bullet != null) {Bullet.SetActive(true);}
            }

            //Usar ataque especial
            if (Input.GetButtonDown("Fire2") && Health.Instance.SpecialPoints >= 1 
                && Animator.GetBool("SpecialRight") == false && Animator.GetBool("SpecialLeft") == false
                && Animator.GetBool("SpecialUp") == false && Animator.GetBool("SpecialDown") == false)
            {
                if (SR.flipX == false && Y == 0)
                {
                    Animator.SetBool("SpecialRight", true);
                    Health.Instance.UseSpecial(1);
                }
                if (SR.flipX == true && Y == 0)
                {
                    Animator.SetBool("SpecialLeft", true);
                    Health.Instance.UseSpecial(1);
                }
                if (Y > 0)
                {
                    RB.AddForce(new Vector2(0, Speed * 2), ForceMode2D.Impulse);
                    Animator.SetBool("SpecialUp", true);
                    Health.Instance.UseSpecial(1);
                }
                if (Y < 0)
                {
                    Animator.SetBool("SpecialDown", true);
                    Health.Instance.UseSpecial(1);
                }
            }
            if (SpecialUsed == true) 
            {
                Animator.SetBool("SpecialRight", false); 
                Animator.SetBool("SpecialLeft", false);
                Animator.SetBool("SpecialUp", false);
                Animator.SetBool("SpecialDown", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D Col) 
    {
        if (Col.gameObject.tag == "Enemy" || Col.gameObject.tag == "Arrow" || Col.gameObject.tag == "Spell")
        {
            Health.Instance.TakeDamage(1);
            Animator.SetBool("IsTakingHit", true);
            AudioSource.PlayOneShot(Hit);

            if (Health.Instance.Dead == true) 
            {
                Animator.SetBool("IsDead", true); 
                Pause = true;
            }

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

    private void OnTriggerEnter2D(Collider2D Col) 
    {
        if (Col.gameObject.name == "River")
        {
            transform.position = Boat.position;
            AudioSource.PlayOneShot(Hit);
        }
        if (Col.gameObject.name == "Boss")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}