using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageButton : Button
{
    public UnityEvent actions = new UnityEvent();

    public StageButton(float x, float y, GameObject prefab, string displayText, Action associatedAction, GameObject canvas)
    {
        actions.AddListener(delegate { associatedAction(); });
        attached = UnityEngine.Object.Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        attached.transform.SetParent(canvas.transform);
        attached.GetComponent<ButtonTracking>().attachedTo = this;
        attached.GetComponentInChildren<Text>().text = displayText;
    }

    public override void Press()
    {
        actions.Invoke();
        //SceneManager.LoadScene(targetScene);
    }
}