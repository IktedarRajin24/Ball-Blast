using UnityEngine;

public class Boundary : MonoBehaviour
{
    [SerializeField] BoxCollider2D leftBoundary;
    [SerializeField] BoxCollider2D rightBoundary;

    void Awake()
    {
        float screenWidth = GameManager.instance.screenWidth;

        leftBoundary.transform.position = new Vector3(-screenWidth - leftBoundary.size.x/2f, 0, 0);
        rightBoundary.transform.position = new Vector3(screenWidth + rightBoundary.size.x/2f, 0, 0);
    }
}
