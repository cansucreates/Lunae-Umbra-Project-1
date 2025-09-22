using UnityEngine;

public class CategoryRow : MonoBehaviour
{
    public WardrobeManager manager;
    public int categoryIndex;

    public void Next()
    {
        if (manager != null)
            manager.NextOption(categoryIndex);
    }

    public void Prev()
    {
        if (manager != null)
            manager.PrevOption(categoryIndex);
    }

    public void None()
    {
        if (manager != null)
            manager.ResetAll();
    } // istersen ayrı none yap
}
