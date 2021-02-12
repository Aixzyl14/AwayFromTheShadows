using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDisplayer : MonoBehaviour
{
    [SerializeField] GameObject[] WaveText;
    private WaveSpawner Waveinfo;
    private int CurrentWave;
    private bool NotCurrentlyInWave = true;
    private bool BossFight;
    private bool BossEnd;
    void Start()
    {
      
    }

    void Update()
    {
        Waveinfo = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        CurrentWave = Waveinfo.WaveNumber;
        BossFight = Waveinfo.BossFight;
        BossEnd = Waveinfo.BossDead;
        if(Waveinfo.finishedSpawning)
        {
            NotCurrentlyInWave = true;
        }
        if(NotCurrentlyInWave || BossFight || BossEnd)
        {
            
            switch(CurrentWave)
            {
                case 1:
                    ResetWaveText();
                    WaveText[0].SetActive(true);
                    break;
                case 2:
                    ResetWaveText();
                    WaveText[1].SetActive(true);
                    break;
                case 3:
                    ResetWaveText();
                    WaveText[2].SetActive(true);
                    break;
                case 4:
                    ResetWaveText();
                    WaveText[3].SetActive(true);
                    break;
                case 5:
                    ResetWaveText();
                    WaveText[4].SetActive(true);
                    break;
                case 6:
                    ResetWaveText();
                    WaveText[5].SetActive(true);
                    break;
                case 100:
                    ResetWaveText();
                    WaveText[6].SetActive(true);
                    break;
                default:
                    break;
            }
            NotCurrentlyInWave = false;
            
        }
    }

    void ResetWaveText()
    {
        foreach (GameObject WaveText in WaveText)
        {
            WaveText.SetActive(false);
        }
    }
}
