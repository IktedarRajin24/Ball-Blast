using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Meteor : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected int health;

    [SerializeField] protected TMP_Text textHealth;
    [SerializeField] protected float jumpForce;
    [SerializeField] protected ParticleSystem splitParticle;
    [SerializeField] protected ParticleSystem dustParticle;
    [SerializeField] protected AudioSource destroyAudio;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected bool isShowing;
    protected float[] leftRigthBound = new float[2] {-1f, 1f};
    protected Color originalColor;

    [HideInInspector] public bool isResultOfFission = true;
    void Start()
    {
        UpdateHealthUI();
        rb = GetComponent<Rigidbody2D>();
        isShowing = true;
        rb.gravityScale = 0f;
        

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalColor = spriteRenderer.color;

        if (isResultOfFission)
        {
            FallDown();
        }
        else {
            float direction = leftRigthBound[Random.Range(0, 1)];
            float screenOffset = GameManager.instance.screenWidth * 1.3f;
            transform.position = new Vector2(screenOffset * direction, transform.position.y);
            rb.linearVelocity = new Vector2(-direction, 0f);
            Invoke("FallDown", Random.Range(screenOffset - 2.5f, screenOffset - 1f));
        }
    }

    void FallDown()
    {
        isShowing = false;
        rb.gravityScale = 1f;
        rb.AddTorque(Random.Range(-20f, 20f));
    }

    virtual protected void Die()
    {
        splitParticle.transform.parent = null;
        destroyAudio.transform.parent = null;

        splitParticle.Play();
        destroyAudio.Play();
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if(health > 1)
        {
            health -= damage;
            dustParticle.Play();
        }
        else
        {
            Die();
        }
        ScoreManager.instance.SetScore(ScoreManager.instance.GetScore() + 1);
        UpdateHealthUI();
        
    }

    protected void UpdateHealthUI()
    {
        textHealth.text = health.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("cannon"))
        {
            GameManager.instance.GameOver();
        }
        if (collision.tag.Equals("missile"))
        {
            //Haptics.Impact(ImpactFeedbackStyle.Light);
            TakeDamage(1);
            MissileSpawner.Instance.DestroyMissile(collision.gameObject);
            StartCoroutine(FlashWhite());
        }
        if (!isShowing && collision.tag.Equals("wall"))
        {
            float posX = transform.position.x;
            if (posX > 0) {
                rb.AddForce(Vector2.left * 150f);
            }
            else
            {
                rb.AddForce(Vector2.right * 150f);

            }
            rb.AddTorque(posX * 4f);
        }
         if (collision.tag.Equals("ground"))
        {
            //Haptics.Impact(ImpactFeedbackStyle.Medium);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            rb.AddTorque(-rb.angularVelocity * 4f);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.12f); 
        spriteRenderer.color = originalColor;
    }
}
