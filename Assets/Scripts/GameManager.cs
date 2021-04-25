using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public Player player;
    public float depth;
    public float frequency = 1f;
    public float obstacleProbability = 5f;
    public Vector3 obstacleOffset = Vector3.zero; 
    public int maxObstacleAmmount = 10;
    public List<GameObject> prefabs = new List<GameObject>();

    private List<GameObject> obstacles = new List<GameObject>();
    private bool over = false;
    private bool readyForNext = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        StartCoroutine(Ping());
        StartCoroutine(StartGame());
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        depth = player.transform.position.y * -1;
        SetDisplay();

        if(UIManager._instance.lifes.Count <= 0 && !over)
        {
            GameOver();
        }

        if((Input.anyKey || Input.touchCount > 0) && over && readyForNext)
        {
            SceneManager.LoadScene("Start");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(obstacles.Contains(other.gameObject))
        {
            obstacles.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    private void GameOver()
    {
        player.GetComponent<Rigidbody>().isKinematic = true;
        over = true;
        StartCoroutine(ReadyUp());
        int altScore = PlayerPrefs.GetInt("Score");
        Debug.Log("Last score:" + PlayerPrefs.GetInt("Score"));
        int score = Mathf.RoundToInt(depth);
        if (score > altScore)
        {
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.Save();
            //GameObject.Find("Network").GetComponent<NetworkManager>().SendScore("Player", score);
        }
        SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
    }

    private IEnumerator Ping()
    {
        SpawnObstacle();
        yield return new WaitForSeconds(frequency);
        StartCoroutine(Ping());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(0.7f);
        Time.timeScale = 1;
    }

    private IEnumerator ReadyUp()
    {
        yield return new WaitForSeconds(1.5f);
        readyForNext = true;
    }

    private void SetDisplay()
    {
        UIManager._instance.depthDisplay.text = Mathf.RoundToInt(depth).ToString();
        UIManager._instance.depthDisplay.text += " m";
    }

    private void SpawnObstacle()
    {
        if (over) return;
        float t;
        if(depth / obstacleProbability < 100)
        {
            t = Random.Range(depth / obstacleProbability, 100);
        } else
        {
            t = 100;
        }

        if(t >= 100 && obstacles.Count < maxObstacleAmmount)
        {
            Debug.Log("Spawning");
            int prefabIndex = Random.Range(0, prefabs.Count - 1);
            float randX = Random.Range(player.xBounds.x, player.xBounds.y);
            if (player.GetVelocity().x < 1) randX = player.transform.position.x;
            GameObject obstacle = Instantiate(prefabs[prefabIndex], new Vector3(randX, (depth * -1) - 12, 0) + obstacleOffset, prefabs[prefabIndex].transform.rotation);
            obstacles.Add(obstacle);
        }
    }
}
