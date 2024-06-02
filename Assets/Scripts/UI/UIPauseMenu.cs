using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    #region Fields & Properties
    #region Fields
    [SerializeField] private TMP_Text countdown;
    [SerializeField] private float fDelay = 3.0f;
    [SerializeField] private GameObject menu;
    [SerializeField] private string sceneName;
    [SerializeField] private Button resumeButton;
    [SerializeField] private UITweening resumeButtonTween;
    private bool bIsDoing = false;
    #endregion

    #region Properties
    #endregion
    #endregion
    
    #region Methods
    public void Open(bool _status = true)
    {
        if (bIsDoing || menu.activeSelf)
            return;
        
        foreach (UITweening _tween in gameObject.GetComponentsInChildren<UITweening>())
            _tween.Reset();
        
        menu.SetActive(true);
        resumeButton.interactable = _status;
        resumeButtonTween.SetActive(_status);
        Time.timeScale = 0.0f;
    }

    public void Close()
    {
        menu.SetActive(false);
        StartCoroutine(SetTimeScale(fDelay, 1.0f));
    }

    public void Quit()
    {
        SoundManager.Instance.Stop();
        Player.Instance.Die(false);
        menu.SetActive(false);
        UIManager.Instance.SetUI(false);
        SceneManager.LoadScene(sceneName);
    }
    
    private IEnumerator SetTimeScale(float _delay, float _timeScale)
    {
        bIsDoing = true;
        float _start = Time.unscaledTime;
        countdown.gameObject.SetActive(true);

        while (Time.unscaledTime - _start < _delay)
        {
            countdown.text = (_delay - (Time.unscaledTime - _start)).ToString("F1");
            yield return null;
        }

        Time.timeScale = _timeScale;
        countdown.gameObject.SetActive(false);
        bIsDoing = false;
    }
    #endregion
}
