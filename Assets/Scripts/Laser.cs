using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Change speed of the laser")]
    [Range(0f, 16f)]
    private float _laserSpeed = 8f;

    private bool isFatherEnemy = false;

    private void Start()
    {
        if (transform.parent != null && transform.parent.tag == "Enemy")
        {

            isFatherEnemy = true;
            Debug.Log(isFatherEnemy);
        }

    }

    void Update()
    {

        if (isFatherEnemy)
        {
            transform.Translate(Vector3.down * _laserSpeed * Time.deltaTime);

            
        } else
        {
            transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);
        }

        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }
       

        if (transform.position.y > 8.0f)//|| transform.position.y < -8.0f
        {

            if (transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Touch Player");
            Player player = collision.transform.GetComponent<Player>();
            player.Damage();

            Destroy(this.gameObject);
        }
    }
}
