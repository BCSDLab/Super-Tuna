using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUiController : MonoBehaviour
{

    public void OnClickPlayButton()
    {
        Debug.Log("���� �� �̵�");
        //SceneManager.LoadScene("GameScene");
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
