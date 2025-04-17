using TMPro;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int health;

    [SerializeField] TMP_Text textHealth;
    [SerializeField] float jumpForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        if(health > 1)
        {
            health -= damage;
        }
        else
        {
            Die();
        }
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        textHealth.text = health.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("cannon"))
        {
            Debug.Log("Game over");
        }else if (collision.tag.Equals("missile"))
        {
            TakeDamage(1);
        }else if (collision.tag.Equals("wall"))
        {
            float posX = transform.position.x;
            if (posX > 0) {

            }
            else
            {

            }
        }else if (collision.tag.Equals("ground"))
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
