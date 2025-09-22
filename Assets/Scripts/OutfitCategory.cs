using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Make the class serializable so it can be shown in the Inspector
public class OutfitCategory
{
    public string categoryName;
    public UnityEngine.SpriteRenderer target;
    public List<StyledItem> options; // List of StyledItem objects

    [HideInInspector]
    public int currentIndex = -1; // To track the current selected item
}
