using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedBoostCoef = 2;

    [SerializeField]
    private GameObject _laserPrefab;


    [SerializeField]
    private GameObject _tripleShootPrefab;
    [SerializeField]
    private bool _isTripleShootActive = false;
    [SerializeField]
    private bool _isShiledIsActive = false;


    [SerializeField]
    private float _laserOffset = 0.8f;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    [SerializeField]
    private GameObject _shieldObject;

    //variable reference

    [SerializeField]
    private float _score;

    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    //variable to store the audio clip
    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;

    private AudioManager _audioManager;

    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager component on Canvas was not found!");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("Audio Compnent is not attached!");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }

       _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();

        if (_audioManager == null)
        {
            Debug.Log("Audio Manager Component was not found on Audio_Manager object");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }


    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0f), 0);     

        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
            _canFire = Time.time + _fireRate;


        if (_isTripleShootActive)
        {
            Instantiate(_tripleShootPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + _laserOffset, 0), Quaternion.identity);
        }

        //play the laser audio clip
        _audioSource.Play();
    }

    public void Damage()
    {


        if (_isShiledIsActive)
        {
            _isShiledIsActive = false;
            _shieldObject.SetActive(false);
            return;
        }

        _lives -= 1;

        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }else if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }



        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            _audioManager.playExplosionSound();
            Destroy(this.gameObject);
        }
    }


    public void ShieldIsActive()
    {
        _isShiledIsActive = true;
        _shieldObject.SetActive(true);
    }

    public void TripleShootActive()
    {

        _isTripleShootActive = true;
        StartCoroutine(TripleShootReboot());

    }

    public void SpeedBoostActive()
    {
        _speed = _speed * _speedBoostCoef;
        StartCoroutine(backToNormalSpeed());
    }

    IEnumerator backToNormalSpeed()
    {
        yield return new WaitForSeconds(5.0f);
        _speed = _speed / _speedBoostCoef;
    }


    IEnumerator TripleShootReboot()
    {
        while (true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShootActive = false;
        }
    }

    //method to add to the score
    //cominicate with UI

    public void addScore()
    {
        _score += 50;
        _uiManager.updateScoreText(_score);
    }
}
