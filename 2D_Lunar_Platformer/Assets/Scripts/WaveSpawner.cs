using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class WaveSpawner : MonoBehaviour
{


    [System.Serializable]
    public class Wave
    {
        public string Name;
        public List<Enemy> enemies;
        public float Rate;
    }
    public Wave[] Waves;
    private int _currentWave = 0;


  
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    private SpawnState _state = SpawnState.COUNTING;

    //Time between Waves to be decided 
    private float _timeBetweenWaves = 5f;
    private float _waveCountDown;
    private float _searchCountDown = 1f;

    //List of Transforms where enemies will be spawning 
    public Transform[] SpawnPoints;

    void Start()
    {
        _waveCountDown = _timeBetweenWaves;
    }

  
    void Update()
    {
       
        if (_state == SpawnState.WAITING)
        {
                
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                  
                }
                else
                {
                    return;
                }

        }
        if (_waveCountDown <= 0)
        {
            
            if (_state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(Waves[_currentWave]));
            }
           
        }
        else
        {
            _waveCountDown -= Time.deltaTime;

        }


    }

    void WaveCompleted()
    {
        _state = SpawnState.COUNTING;
        _waveCountDown = _timeBetweenWaves;

        if (_currentWave + 1 > Waves.Length - 1)
        {
            _currentWave = 0;

        }
        else
        {
            _currentWave++;
        }

    }
    bool EnemyIsAlive()
    {

        //Checking if there are enemies alive
        _searchCountDown -= Time.deltaTime;
        if (_searchCountDown <= 0)
        {
            _searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnWave(Wave _wave)
    {

        Debug.Log("Spawning wave " + _wave.Name);
        _state = SpawnState.SPAWNING;
     
        // Create Enemy class to instantiate enemies

        foreach (Enemy enemy in _wave.enemies)
        {
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(1f / _wave.Rate);
        }
        _state = SpawnState.WAITING;

        yield break;

    }

    void SpawnEnemy(Enemy _enemy)
    {
        Transform _sp = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
       
    }
}
