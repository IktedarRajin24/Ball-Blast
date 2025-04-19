using UnityEngine;
using TMPro;
using System.Collections;

public class HighScoreNotifier : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;

    private int previousHighScore;
    private bool isShowing = false;

    private void Start()
    {
        previousHighScore = ScoreManager.instance.GetHighScore();
    }

    private void Update()
    {
        int currentScore = ScoreManager.instance.GetScore();
        int currentHighScore = ScoreManager.instance.GetHighScore();

        if (!isShowing && currentScore > previousHighScore && currentScore == currentHighScore)
        {
            ShowNewHighScoreMessage();
            previousHighScore = currentHighScore;
        }
    }

    private void ShowNewHighScoreMessage()
    {
        isShowing = true;
        string[] messages = { "Bravo!", "Well Done!", "New High Score!", "You're Amazing!" };
        messageText.text = messages[Random.Range(0, messages.Length)];
        messageText.gameObject.SetActive(true);
        Debug.Log(messageText.text);
        StartCoroutine(HideMessageAfterDelay(0.7f));
        
    }

    private IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        messageText.gameObject.SetActive(false);
    }

    
}
