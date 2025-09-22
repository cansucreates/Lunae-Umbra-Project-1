using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class WardrobeManager : MonoBehaviour
{
    public List<OutfitCategory> categories = new List<OutfitCategory>();
    public Style targetStyle = Style.Victorian; // default style

    [Range(0f, 100f)]
    public float successThreshold = 70f; // success threshold for style matching

    public TMP_Text scoreText;
    public TMP_Text dialogueText;
    public TMP_Text targetStyleText;

    public string[] successLines;
    public string[] failLines;

    void Start()
    {
        UpdateTargetStyleLabel(); // initialize the target style label at the start
        ResetAll();
        RefreshAllTargets();
        if (scoreText)
        {
            scoreText.text = "Score: -"; // initial score display
        }
    }

    void Update() { }

    public void NextOption(int categoryIndex)
    {
        if (categoryIndex < 0 || categoryIndex >= categories.Count)
            return;
        var c = categories[categoryIndex];
        if (c.options == null || c.options.Count == 0)
            return;

        c.currentIndex = (c.currentIndex + 1) % c.options.Count;

        HandleExclusivity(c);
        ApplyCategorySprite(c);
    }

    public void PrevOption(int categoryIndex)
    {
        if (categoryIndex < 0 || categoryIndex >= categories.Count)
            return;
        var c = categories[categoryIndex];
        if (c.options == null || c.options.Count == 0)
            return;

        c.currentIndex--;
        if (c.currentIndex < 0)
            c.currentIndex = c.options.Count - 1;

        HandleExclusivity(c);
        ApplyCategorySprite(c);
    }

    void HandleExclusivity(OutfitCategory chosen)
    {
        var dressCat = categories.Find(c => c.categoryName == "Dress");
        var shirtCat = categories.Find(c => c.categoryName == "Shirt");
        var pantsCat = categories.Find(c => c.categoryName == "Pants");

        if (chosen == null)
            return;

        // If Dress is chosen → clear Shirt & Pants
        if (chosen == dressCat && dressCat.currentIndex >= 0)
        {
            if (shirtCat != null)
            {
                shirtCat.currentIndex = -1;
                shirtCat.target.sprite = null;
            }
            if (pantsCat != null)
            {
                pantsCat.currentIndex = -1;
                pantsCat.target.sprite = null;
            }
        }

        // If Shirt or Pants are chosen → clear Dress
        if ((chosen == shirtCat || chosen == pantsCat) && chosen.currentIndex >= 0)
        {
            if (dressCat != null)
            {
                dressCat.currentIndex = -1;
                dressCat.target.sprite = null;
            }
        }
    }

    void ApplyCategorySprite(OutfitCategory cat)
    {
        if (cat == null || cat.target == null)
            return;

        if (cat.currentIndex >= 0 && cat.currentIndex < cat.options.Count)
        {
            cat.target.sprite = cat.options[cat.currentIndex].sprite;
        }
        else
        {
            cat.target.sprite = null; // show nothing (naked for that slot)
        }
    }

    void RefreshAllTargets()
    {
        foreach (var c in categories)
        {
            ApplyCategorySprite(c);
        }
    }

    public void Evaluate()
    {
        int catCount = categories.Count;
        if (catCount == 0)
            return;

        float totalScore = 0f;
        int validCategories = 0; // only count categories that are relevant

        // Check if Dress is chosen
        var dressCat = categories.Find(c => c.categoryName == "Dress");
        bool usingDress = (dressCat != null && dressCat.currentIndex >= 0);

        foreach (var c in categories)
        {
            // Skip Shirt & Pants if Dress is chosen
            if (usingDress && (c.categoryName == "Shirt" || c.categoryName == "Pants"))
                continue;

            validCategories++; // count this category toward scoring

            if (c.currentIndex >= 0 && c.currentIndex < c.options.Count)
            {
                var chosen = c.options[c.currentIndex];
                if (chosen != null && chosen.style == targetStyle)
                    totalScore += 1f; // give 1 point for correct match
            }
        }

        // Convert to percentage
        float rounded =
            (validCategories > 0) ? Mathf.Round((totalScore / validCategories) * 1000f) / 10f : 0f;

        if (scoreText)
            scoreText.text = $"Skor: {rounded}%";
        bool success = rounded >= successThreshold;
        if (dialogueText)
            dialogueText.text = success ? Pick(successLines) : Pick(failLines);
    }

    string Pick(string[] arr)
    {
        if (arr == null || arr.Length == 0)
            return "";
        return arr[Random.Range(0, arr.Length)];
    }

    public void RandomizeTargetStyle()
    {
        var vals = (Style[])System.Enum.GetValues(typeof(Style));
        targetStyle = vals[Random.Range(0, vals.Length)];
        UpdateTargetStyleLabel();
    }

    void UpdateTargetStyleLabel()
    {
        if (targetStyleText != null)
            targetStyleText.text = $"Hedef Stil: {targetStyle}";
    }

    public void ResetAll()
    {
        foreach (var c in categories)
            c.currentIndex = -1;
        RefreshAllTargets();
        if (scoreText)
            scoreText.text = "Skor: -";
        if (dialogueText)
            dialogueText.text = "";
    }
}
