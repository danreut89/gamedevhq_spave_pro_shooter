using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //speed rotation
    [SerializeField]
    private float _speedRotation = 12.0f;
    [SerializeField]
    private GameObject _expolisionPrefab;
    private SpawnManager _spawnManager;



    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager Component was not found ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object on z axes
        transform.Rotate(new Vector3(0, 0, _speedRotation * Time.deltaTime * -1));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Instantiate(_expolisionPrefab, this.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawing();
            Destroy(this.gameObject, 0.4f);
          
            
        }
    }

    //check for laser collision (Trigger)
    //instantiate expolision at the position of the astorid (us)

    //destryo the expolistion three free seconds 
}
