using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable] // so it can show up in the unity hud
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float TimeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float TimeBetweenWaves;
    public int WaveNumber = 0;
    private Wave CurrentWave;
    private int currentWaveIndex;
    private Transform Player;
    private bool newWave = false;
    
    public bool finishedSpawning;
    public int EnemiesLeft;
    public bool BossFight = false;
    public bool BossDead = false;

    public GameObject boss;
    public Transform bossSpawnPoint;
    private Boss Bossinfo;
    private bool LastRound;
    private bool IsBossDead;
    private Boss NewWaveinfo;
    private int NewWaveNumber;
    private enum State { Startscreen,Game, Endscreen};
    State Currentstate = State.Startscreen;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Currentstate = State.Game;
        if (Currentstate != State.Endscreen)
        {
            if (Currentstate == State.Game)
            {
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
        }
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(TimeBetweenWaves);
        StartCoroutine(SpawnWave(index));
  
    }

    IEnumerator SpawnWave (int index)
    {
        CurrentWave = waves[index];

        for (int i = 0; i < CurrentWave.count; i++) // creates a monster each time after a certain delay of time
        {
            if (Player == null)
            {
                yield break; // if player dead it will stop creating waves
            }
            Enemy randomEnemy = CurrentWave.enemies[Random.Range(0, CurrentWave.enemies.Length)]; // creates a random enemy depending on wave number so wave 1 will have one random enemy
            Transform randomSpot = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

           

            if (i == CurrentWave.count - 1)
            {
                finishedSpawning = true;

                WaveNumber++;
                Debug.Log("Wave " + WaveNumber + " Has Started");

            }
            else
            {
                finishedSpawning = false;
            }

            yield return new WaitForSeconds(CurrentWave.TimeBetweenSpawns);

        }
    }
    private void Update()
    {
        EnemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
   
            if (finishedSpawning == true && EnemiesLeft == 0)
            {
                finishedSpawning = false;
                if (currentWaveIndex + 1 < waves.Length) // if there are still waves to spawn
                {
                    currentWaveIndex++;
                    StartCoroutine(StartNextWave(currentWaveIndex));
                }
                else
                {


                    WaveNumber = 6;
                    Debug.Log("The Boss Has Arrived");
                    BossFight = true;
                    Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                    LastRound = true;

            }
            }
        if (LastRound)
        {
            NewWaveinfo = GameObject.FindGameObjectWithTag("boss").GetComponent<Boss>();
            NewWaveNumber = NewWaveinfo.WaveNumber;
            WaveNumber = NewWaveNumber;
            if(WaveNumber == 100)
            {
                BossDead = true;
            }
        }
        
    }
}
