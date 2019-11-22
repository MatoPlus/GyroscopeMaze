using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageButton : Button
{
    public UnityEvent actions = new UnityEvent();

    public StageButton(float x, float y, GameObject prefab, string displayText, List<Action> associatedActions, GameObject canvas)
    {
        foreach (Action i in associatedActions)
        {
            actions.AddListener(delegate { i(); });
        }
        attached = UnityEngine.Object.Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        attached.transform.SetParent(canvas.transform);
        attached.GetComponent<ButtonTracking>().attachedTo = this;
        attached.GetComponentInChildren<Text>().text = displayText;
    }

    public override void Press()
    {
        actions.Invoke();
    }

    public void Deactivate()
    {
        active = false;
    }
}