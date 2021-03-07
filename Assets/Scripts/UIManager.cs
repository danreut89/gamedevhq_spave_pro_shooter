using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{

    //handle to text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livespites;
    [SerializeField]
    private GameObject _gameOverText;
    [SerializeField]
    private Text _pressRkeyText;
    [SerializeField]
    private float _flickeringUnmber = 0.5f;


    private GameManager _gm;


    // Start is called before the first frame update
    void Start()
    {
        
        //assign that component to the handle
        _scoreText.text = "Score: " + 0;
        _gameOverText.SetActive(false);
        _pressRkeyText.gameObject.SetActive(false);

        _gm = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gm == null)
        {
            Debug.Log("Game Manager is NULL");
        }
    }


    public void updateScoreText(float score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _livespites[currentLives];
        if (currentLives == 0)
        {
            GameOverScene();
            
        }
    }

    void GameOverScene()
    {

        _gm.GameOver();
        _pressRkeyText.gameObject.SetActive(true);
        StartCoroutine(flickeringText());
    }

    IEnumerator flickeringText()
    {
        while (true)
        {
            Debug.Log("StartCoroutine");
            _gameOverText.SetActive(true);
            yield return new WaitForSeconds(_flickeringUnmber);
            _gameOverText.SetActive(false);
            yield return new WaitForSeconds(_flickeringUnmber);
        }
    }

}
