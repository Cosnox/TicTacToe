using UnityEngine;

public class StartScene : MonoBehaviour
{
    bool isListeningForInput = false;
    private BotDifficulty botDifficulty;

    public void LoadFriendMode()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Friend");
    }
    public void ListenForInput()
    {
        isListeningForInput = true;
    }

    private void Update()
    {
        if (isListeningForInput)
        {
            print("Checking");

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                botDifficulty = BotDifficulty.easyBot;
                isListeningForInput = false;
                LoadBotMode();
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                botDifficulty = BotDifficulty.hardBod;
                isListeningForInput = false;
                LoadBotMode();
            }
        }
    }

    private void LoadBotMode()
    {
        if (botDifficulty == BotDifficulty.easyBot)
            UnityEngine.SceneManagement.SceneManager.LoadScene("BotEasyMode");

        if (botDifficulty == BotDifficulty.hardBod)
            UnityEngine.SceneManagement.SceneManager.LoadScene("BotHardMode");
    }

}

public enum BotDifficulty { easyBot, hardBod }