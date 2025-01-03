using UnityEngine;

public class BackButtonHandler : MonoBehaviour
{
    public bool isMain = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleBackButtonPress();
        }
    }

    void HandleBackButtonPress()
    {
        Debug.Log("Back Button gedrückt");

        if (isMain)
            Application.Quit();
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");

    }
}
