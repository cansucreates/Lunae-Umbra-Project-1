using UnityEngine;
using TMPro; // or TMPro if using TextMeshPro

public class ScoreChecker : MonoBehaviour
{
    public TMP_Text scoreText;    // Drag your ScoreText here
    public TMP_Text resultText;   // Drag your ResultText here

    OutfitScorer scorer;
    void Start()
    {
        scorer = new OutfitScorer();
        RollNewScore(); // Run once at start
    }

    // Called by button
    public void RollNewScore()
    {
        // Randomize choices
        bool hasTop = Random.value > 0.3f;
        bool hasBottom = Random.value > 0.3f;
        bool hasShoes = Random.value > 0.3f;
        bool hasAccessory = Random.value > 0.6f;
        bool styleMatch = Random.value > 0.5f;

        int finalScore = scorer.CalculateScore(hasTop, hasBottom, hasShoes, hasAccessory, styleMatch);
        ShowResult(finalScore);
    }

    void ShowResult(int score)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();

        if (resultText != null)
        {
            if (score >= 70)
                resultText.text = "Success!";
            else
                resultText.text = "Failure!";
        }
    }
}
