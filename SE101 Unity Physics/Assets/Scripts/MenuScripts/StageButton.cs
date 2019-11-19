using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageButton : Button
{
    private string targetStage;
    public StageButton(float x, float y, GameObject prefab, string displayText, string targetStage, GameObject canvas)
    {
        this.targetStage = targetStage;
        attached = Object.Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        attached.transform.SetParent(canvas.transform);
        attached.GetComponent<ButtonTracking>().attachedTo = this;
        attached.GetComponentInChildren<Text>().text = displayText;
    }

    public override void Press()
    {
        Debug.Log("TODO: Make this do something");
        //SceneManager.LoadScene(targetScene);
    }
}