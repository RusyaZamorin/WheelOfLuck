using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneManager : MonoBehaviour {

    public void OpenScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
