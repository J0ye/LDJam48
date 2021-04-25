using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public float depth;
    public float frequency = 1f;
    public float obstacleProbability = 5f;
    public int maxObstacleAmmount = 10;
    public List<GameObject> prefabs = new List<GameObject>();

    private List<GameObject> obstacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Ping());
    }

    // Update is called once per frame
    void Update()
    {
        depth = player.transform.position.y * -1;
    }

    private void OnTriggerExit(Collider other)
    {
        if(obstacles.Contains(other.gameObject))
        {
            obstacles.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Ping()
    {
        SpawnObstacle();
        yield return new WaitForSeconds(frequency);
        StartCoroutine(Ping());
    }

    private void SpawnObstacle()
    {
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
            GameObject obstacle = Instantiate(prefabs[prefabIndex], new Vector3(randX, (depth * -1) - 12, 0), prefabs[prefabIndex].transform.rotation);
            obstacles.Add(obstacle);
        }
    }
}
