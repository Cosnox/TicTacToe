using UnityEngine;

public class HomeRuler : MonoBehaviour
{
    public void LoadFreindMode()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FriendMode");
    }
    public void LoadNormalBot()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BotNormalMode");
    }
    public void LoadHardBot()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BotHardMode");
    }
}