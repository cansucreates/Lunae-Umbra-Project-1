using UnityEngine;

public class OutfitScorer
{
    public int CalculateScore(bool hasTop, bool hasBottom, bool hasShoes, bool hasAccessory, bool styleMatch)
    {
        int score = 0;

        if (hasTop) score += 20;
        if (hasBottom) score += 20;
        if (hasShoes) score += 20;
        if (hasAccessory) score += 10;
        if (styleMatch) score += 30;

        return Mathf.Clamp(score, 0, 100);
    }
}
