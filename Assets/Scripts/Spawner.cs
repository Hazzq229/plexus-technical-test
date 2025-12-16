using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _itemPrefab; 
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _spawnRadius = 5f;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            SpawnItem();
            _timer = 0;
        }
    }

    void SpawnItem()
    {
        Vector2 randomPos = Random.insideUnitCircle * _spawnRadius;
    
        Instantiate(_itemPrefab, randomPos, Quaternion.identity);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
    }
}
