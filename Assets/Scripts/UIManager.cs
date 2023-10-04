using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singelton<UIManager>
{
    public Action OnPointDown;
    public Action OnPointUp;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _loosePanel;
    private void OnEnable()
    {
        PlayerHandler.Instance.OnFinishGame += ShowFinishPanel;
    }
    private void OnDisable()
    {
        PlayerHandler.Instance.OnFinishGame -= ShowFinishPanel;
    }
    public void PointDown()
    {
        OnPointDown?.Invoke();
    }
    public void PointUp()
    {
        OnPointUp?.Invoke();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void ShowFinishPanel(bool isWin)
    {
        if(isWin)
        {
            _winPanel.gameObject.SetActive(true);
        }
        else
        {
            _loosePanel.gameObject.SetActive(true);
        }
    }
}
