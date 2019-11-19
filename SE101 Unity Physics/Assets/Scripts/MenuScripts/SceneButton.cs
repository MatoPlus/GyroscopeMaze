using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : Button
{
    private string targetScene;
    public SceneButton(float x, float y, GameObject prefab, string targetScene, GameObject canvas)
    {
        this.targetScene = targetScene;
        attached = UnityEngine.Object.Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        attached.transform.SetParent(canvas.transform);
        attached.GetComponent<ButtonTracking>().attachedTo = this;
    }

    public override void Press()
    {
        Debug.Log("Pressed");
        //SceneManager.LoadScene(targetScene);
    }
}