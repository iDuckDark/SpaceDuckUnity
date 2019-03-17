using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float startWait, spawnWait, waveWait;

    public Text scoreText, restartText, gameOverText;
    private int score;
    private bool gameOver; //restart;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        restartText.text = gameOverText.text ="";

        score = 0;
        //scoreText.alignment = TextAnchor.UpperLeft;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (!gameOver)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
  
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
 
        //For Desktop Players
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Obselete
            //Application.LoadLevel(Application.loadedLevel);
            //Replaced with this:
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        //Quit game
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void GameOver()
    {
        restartText.text = "Press R for Restart";
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
