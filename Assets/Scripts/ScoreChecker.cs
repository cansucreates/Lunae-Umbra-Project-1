using UnityEngine;
using TMPro;

public class ScoreChecker : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text resultText;
    public TMP_Text themeText;

    OutfitScorer scorer;
    int pendingScore = 0; // score waiting until Done is pressed
    string[] themes = { "Tema1", "Tema2", "Tema3" }; 

    void Start()
    {
        scorer = new OutfitScorer();
        RollNewScore(); // generate first random outfit
    }

    // Called by "Try Again" button
    public void RollNewScore()
    {
        string selectedTheme = themes[Random.Range(0, themes.Length)];
        if (themeText != null)
            themeText.text = "Theme: " + selectedTheme;

        // Randomize choices
        bool hasTop = Random.value > 0.3f;
        bool hasBottom = Random.value > 0.3f;
        bool hasShoes = Random.value > 0.3f;
        bool hasAccessory = Random.value > 0.6f;
        bool styleMatch = Random.value > 0.5f;

        pendingScore = scorer.CalculateScore(hasTop, hasBottom, hasShoes, hasAccessory, styleMatch);

        // Show only that an outfit is ready, but not the result yet
        if (scoreText != null)
            scoreText.text = "Outfit ready! Press Done.";
        if (resultText != null)
            resultText.text = "";
    }

    // Called by "Done" button
    public void ConfirmScore()
    {
        ShowResult(pendingScore);
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
