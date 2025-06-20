using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] panels;

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void GameStart()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
    }

    public void BackMain()
    {
        panels[0].SetActive(true);
    }
    public void SetSpownPoint(int num)
    {
        DataManager data = FindAnyObjectByType<DataManager>();
        data.spownpointNum = num;
        SceneManager.LoadScene(1);
    }
    public void GameTitle()
    {
        SceneManager.LoadScene(0);
    }
}
