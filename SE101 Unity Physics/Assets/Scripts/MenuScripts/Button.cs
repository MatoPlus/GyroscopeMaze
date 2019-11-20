using UnityEngine;

public abstract class Button
{
    public bool active;
    public GameObject attached;
    public Button()
    {
        active = false;
    }

    public abstract void Press();

    public void Remove()
    {
        Object.Destroy(attached);
    }
}