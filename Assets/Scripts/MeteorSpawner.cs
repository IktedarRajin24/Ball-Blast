using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public static MeteorSpawner Instance;

    [SerializeField] GameObject[] meteorPrefabs;
    [SerializeField] int meteorCount;
    [SerializeField] float spawnDelay;

    GameObject[] meteors;

    bool isMoving;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PrepareMeteors();
        StartCoroutine(SpawnMeteors());
        isMoving = GameManager.instance.isMoving;
    }

    IEnumerator SpawnMeteors()
    {
        while (true)
        {
            GameObject meteor = Instantiate(meteorPrefabs[Random.Range(0, meteorPrefabs.Length)], transform);
            meteor.GetComponent<Meteor>().isResultOfFission = false;
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    void PrepareMeteors()
    {
        meteors = new GameObject[meteorCount];
        int prefabCount = meteorPrefabs.Length;
        for (int i = 0; i < meteorCount; i++)
        {
            meteors[i] = Instantiate(meteorPrefabs[Random.Range(0, prefabCount)], transform);
            meteors[i].GetComponent<Meteor>().isResultOfFission = false;
            meteors[i].SetActive(false);
        }
    }
}
