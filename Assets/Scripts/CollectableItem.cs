using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [Header("Item Settings")]
    [SerializeField] private float _xpValue = 10f;
    [SerializeField] private GameObject _collectFX;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) Collect();
    }

    void Collect()
    {
         ExperienceManager.Instance.AddXP(_xpValue);

        if(_collectFX != null) 
        {
            GameObject effect = Instantiate(_collectFX, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

        Destroy(gameObject);
    }
}
