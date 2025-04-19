using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public static MissileSpawner Instance;
    Queue<GameObject> missileQueue;

    [SerializeField] GameObject missilePrefab;
    [SerializeField] int missileCount;
    [SerializeField] ParticleSystem cannonParticleSystem;
    [SerializeField] AudioSource firingAudio;

    [Space]
    [SerializeField] float missileSpeed = 0.3f;
    [SerializeField] float delay = 0.3f;

    GameObject missile;
    float time = 0f;
    bool isMoving;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Debug.Log(transform.parent.position.x);
        CreateMissile();

    }

    // Update is called once per frame
    void Update()
    {
        isMoving = GameManager.instance.isMoving;
        if (GameManager.instance != null && GameManager.instance.isGameOver)
        {
            return; 
        }
        time += Time.deltaTime;
        if (time >= delay && isMoving) { 
            time = 0f;
            missile = SpawnMissile(transform.position);
            missile.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * missileSpeed;
            cannonParticleSystem.Play();
            firingAudio.Play();
        }
    }

    void CreateMissile()
    {
        missileQueue = new Queue<GameObject>();
        for (int i = 0; i < missileCount; i++) {
            missile = Instantiate(missilePrefab, transform.position, Quaternion.identity, transform);
            missile.SetActive(false);
            missileQueue.Enqueue(missile);
        }
    }

    public GameObject SpawnMissile(Vector2 position)
    {
        if (missileQueue.Count > 0)
        {
            missile = missileQueue.Dequeue();
            missile.transform.position = position;
            missile.SetActive(true);
            return missile;
        }
        return null;

    }
    public void DestroyMissile(GameObject missileObj) {
        missileQueue.Enqueue(missileObj);
        missileObj.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("missile"))
        {
            DestroyMissile(collision.gameObject);
        }
    }
}
