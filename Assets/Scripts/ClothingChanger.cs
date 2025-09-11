using UnityEngine;
using UnityEngine.UI;
public class ClothingChanger : MonoBehaviour
{
    public Sprite[] options;
    public SpriteRenderer target;
    private int currentIndex = 0;

    public void NextOption()
    {
        currentIndex = (currentIndex + 1) % options.Length;
        target.sprite = options[currentIndex];
    }

    public void PrevOption()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = options.Length - 1;
        target.sprite = options[currentIndex];
    }

}
