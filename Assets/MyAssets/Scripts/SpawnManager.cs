using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab = default;
    [SerializeField] private GameObject _enemyContainer = default;
    [SerializeField] private GameObject _bonusPrefab = default;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnBonusCoroutine());
    }

    // Coroutine pour l'apparition des PowerUps
    IEnumerator SpawnBonusCoroutine()
    {
        yield return new WaitForSeconds(5f);
        while (!_stopSpawning)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            Instantiate(_bonusPrefab, positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6f, 10f));
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while(!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void FinJeu()
    {
        _stopSpawning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
