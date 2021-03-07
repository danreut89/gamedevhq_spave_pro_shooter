using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speedPower = 3.0f;

    [SerializeField]  //0 = Triple Shot //1 = Speed //2 = Shield
    private int powerID;

    private AudioManager _am;

    [SerializeField]
    private AudioClip _audioClip;

    private void Start()
    {
        _am = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
        if (_am == null)
        {
            Debug.LogError("Audio Manager Component was not found on Audio_Manager Game Component");
        }
    }


    // Update is called once per frame
    void Update()
    {


        transform.Translate(Vector3.down * _speedPower * Time.deltaTime);

        if (transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }

        //move down at a peed of 3 (adjecy in the inspector)
        //when we leave the screen, diestryo us
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                switch (powerID)
                {
                    case 0:
                        player.TripleShootActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldIsActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }

            AudioSource.PlayClipAtPoint(_audioClip, transform.position);
            //comunicate with the player script 
           // _am.playCollectableSound();
            Destroy(this.gameObject);
        }
    }
}
