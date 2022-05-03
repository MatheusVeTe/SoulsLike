using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance; 

    public float X;
    public float Y;
    public float Speed = 5;
    public bool Rolled;
    public bool SpecialUsed;
    
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    public Animator Animator;
    public AudioSource AudioSource;
    public AudioClip Hit;

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
        //Não Pausado
        if (Time.timeScale == 1)
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
                Health.Instance.UseStamina(1.5f);
            } 
            if (Animator.GetBool("IsRolling") == true) {Physics2D.IgnoreLayerCollision(7, 6, true);}
            if (Animator.GetBool("IsRolling") == false) {Physics2D.IgnoreLayerCollision(7, 6, false);}
            
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
                && Animator.GetBool("SpecialRight") == false && Animator.GetBool("SpecialLeft") == false)
            {
                if (SR.flipX == false)
                {
                    Animator.SetBool("SpecialRight", true);
                    Health.Instance.UseSpecial(1);
                }
                if (SR.flipX == true)
                {
                    Animator.SetBool("SpecialLeft", true);
                    Health.Instance.UseSpecial(1);
                }
            }
            if (SpecialUsed == true) 
            {
                Animator.SetBool("SpecialRight", false); 
                Animator.SetBool("SpecialLeft", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D Col) 
    {
        if (Col.gameObject.tag == "Enemy")
        {
            Health.Instance.TakeDamage(1);
            Animator.SetBool("IsTakingHit", true);
            AudioSource.PlayOneShot(Hit);

            if (Health.Instance.Dead == true) {Animator.SetBool("IsDead", true);}

            if (transform.position.x >= Col.gameObject.transform.position.x)
            {
                RB.AddForce(new Vector2(Speed, 0), ForceMode2D.Impulse);
            }
            if (transform.position.x < Col.gameObject.transform.position.x)
            {
                RB.AddForce(new Vector2(-Speed, 0), ForceMode2D.Impulse);
            }
        }

        if (Col.gameObject.tag == "Arrow")
        {
            Debug.Log("Hit");
            Health.Instance.TakeDamage(1);
            Animator.SetBool("IsTakingHit", true);
            AudioSource.PlayOneShot(Hit);

            if (Health.Instance.Dead == true) { Animator.SetBool("IsDead", true); }

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