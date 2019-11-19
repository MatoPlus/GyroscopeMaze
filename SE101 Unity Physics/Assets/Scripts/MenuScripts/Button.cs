using UnityEngine;

public abstract class Button
{
    bool active;
    public GameObject attached;
    public Button(GameObject attached)
    {
        active = false;
        this.attached = attached;
    }

    public abstract void Press();
}