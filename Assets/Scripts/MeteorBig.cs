using UnityEngine;


public class MeteorBig : Meteor
{
    [SerializeField] GameObject[] splitsPrefabs;

    protected override void Die()
    {
        SplitMeteor();
        Destroy(gameObject);
    }

    void SplitMeteor(){
        GameObject meteor;
        for (int i = 0; i < 2; i++)
        {
            splitParticle.transform.parent = null;
            destroyAudio.transform.parent = null;
            splitParticle.Play();
            destroyAudio.Play();
            meteor = Instantiate(splitsPrefabs[i], transform.position, Quaternion.identity, MeteorSpawner.Instance.transform);
            meteor.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(leftRigthBound[i], 5f);
        }
    }

}
