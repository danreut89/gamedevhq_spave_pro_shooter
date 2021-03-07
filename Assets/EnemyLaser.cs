using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{

    private float _enemyLaserSpeed = 8f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemyLaserSpeed * Time.deltaTime);

        if (transform.position.y < -8.0f)
        {
            Destroy(gameObject);
        }
    }
}
