
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadManger : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
