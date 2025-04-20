using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TMP_Text gameOverText;
    [HideInInspector] public float screenWidth;
    public bool isGameOver = false;
    public bool isMoving = false;


    void Awake()
    {
        instance = this;
        screenWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void GameOver()
    {
        if (!isGameOver) {
            Debug.Log("Game Over!");
            isGameOver = true;
            gameOverText.gameObject.SetActive(true);
            Invoke(nameof(RestartGame), 2f);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameOver = false ;

    }

    private void Update()
    {
        isMoving = Input.GetMouseButton(0);
    }
}
