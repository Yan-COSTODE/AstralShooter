using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyWave : MonoBehaviour
{
    #region Fields & Properties
    #region Fields
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private string nextLevel;
    [SerializeField] private List<EnemyWaveEntry> enemyWaveEntries;
    private EnemyWaveEntry current;
    private int iIndex;
    private bool bWorking;
    #endregion

    #region Properties
    #endregion
    #endregion
    
    #region Methods
    private void Update()
    {
        if (!current && !bWorking)
            StartCoroutine(Spawn(5.0f));
    }

    private IEnumerator Spawn(float _delay)
    {
        bWorking = true;
        
        if (iIndex >= enemyWaveEntries.Count)
            yield return GoToNextLevel(_delay);

        waveText.gameObject.SetActive(true);
        waveText.text = $"The {enemyWaveEntries[iIndex].WaveName} wave will start soon";
        yield return new WaitForSeconds(_delay);
        waveText.gameObject.SetActive(false);
        current = Instantiate(enemyWaveEntries[iIndex]);
        iIndex++;
        bWorking = false;
    }

    private IEnumerator GoToNextLevel(float _delay)
    {
        waveText.gameObject.SetActive(true);
        waveText.text = $"You completed all the waves here";
        yield return new WaitForSeconds(_delay);
        waveText.gameObject.SetActive(false);
        SceneManager.LoadScene(nextLevel);
        bWorking = false;
    }
    #endregion
}
