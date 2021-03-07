using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawing = false;


    public void StartSpawing()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawing == false)
        {
           
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3 (Random.Range(-10.5f, 10.5f), 8, 0), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }        
    }

    IEnumerator SpawnPowerRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawing == false)
        {

            yield return new WaitForSeconds(Random.Range(3,8));
            int randomPowerUp = Random.Range(0, 3);
            // Instantiate(_tripleShootPowerPrefab, new Vector3(Random.Range(-10.5f, 10.5f), 8, 0), Quaternion.identity);
            Instantiate(powerups[randomPowerUp], new Vector3(Random.Range(-10.5f, 10.5f), 8, 0), Quaternion.identity);

        }

    }

    public void OnPlayerDeath()
    {
        _stopSpawing = true;
    }
}
