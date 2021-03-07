using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f;

    private Player player;

    //handle to animator componentet

    [SerializeField]
    private GameObject _laserPrefab;

    //anim
    [SerializeField]
    private Animator _anim;

    private AudioManager _audioManager;

    //Laser Prefab
    [SerializeField]
    private GameObject _enemyLaserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        if (player == null)
        {
            Debug.LogError("Player Component not Found on Player");
        }

        //assing the componentent to anim
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator component is not Found on Enemy");
        }

        _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();

        if(_audioManager == null)
        {
            Debug.LogError("AudioManager no Found on Audio_Manager");
        }

        // StartCoroutine(enemyFire());

        StartCoroutine(enemyFireLaser());
    }

    // Update is called once per frame
    void Update()
    {


        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-10.5f, 10.5f), 8, 0);
        }
            
    }


    IEnumerator enemyFireLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Vector3 laserOffset = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
            GameObject _enemyLaserIn = Instantiate(_enemyLaserPrefab, laserOffset, Quaternion.identity);
            _enemyLaserIn.transform.parent = this.transform;
        }
    }

    IEnumerator enemyFire()
    {
        
        
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), Quaternion.identity);
        }
   
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _anim.SetTrigger("OnEmemyDeath");
            _enemySpeed = 0;
            _audioManager.playExplosionSound();
            Destroy(this.gameObject, 2.8f);
        }


        if (other.tag == "Laser")
        {
            

            Destroy(other.gameObject);
            _audioManager.playExplosionSound();
            player.addScore();

            _anim.SetTrigger("OnEmemyDeath");
            _enemySpeed = 0;

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }

    }


}
