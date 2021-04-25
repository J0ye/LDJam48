using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surroundings : MonoBehaviour
{
    public List<GameObject> entitys = new List<GameObject>();
    public float indicatorLength = 2f;
    public float frequency = 1f;

    private LineRenderer line;
    private Vector2 playerBounds;
    private Vector3 leftWall;
    private Vector3 rightWall;
    private float randomY;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        playerBounds = transform.parent.GetComponent<Player>().xBounds;
        randomY = Random.Range(-1f, 1f);
        SetWallPoints();
        StartCoroutine(NewRandom());
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        if(DirectionToNearestObject().magnitude < indicatorLength)
        {
            if(DirectionToNearestObject().y == 0)
            {
                line.SetPosition(1, transform.position - DirectionToNearestObject() + new Vector3(0, 0, 0));
            } else
            {
                line.SetPosition(1, transform.position - DirectionToNearestObject());
            }
        }else
        {
            line.SetPosition(1, transform.position);
        }
    }

    public Vector3 DirectionToNearestObject()
    {
        if(transform.position.y < leftWall.y - indicatorLength)
        {
            SetWallPoints();
        }

        Vector3 leftWallDir = transform.position - leftWall;
        Vector3 rightWallDir = transform.position - rightWall;

        Vector3 dir = leftWallDir;
        if (rightWallDir.magnitude < dir.magnitude) dir = rightWallDir;

        foreach (GameObject go in entitys)
        {
            Vector3 toObject = transform.position - new Vector3(go.transform.position.x, go.transform.position.y, 0);
            if (dir == Vector3.zero)
            {
                dir = toObject;
            } else if(toObject.magnitude < dir.magnitude)
            {
                dir = toObject;
            }
        }
        return dir;
    }

    private void SetWallPoints()
    {
        float aheadY = transform.position.y - indicatorLength;
        leftWall = new Vector3(playerBounds.x - 1, aheadY, transform.position.z);
        rightWall = new Vector3(playerBounds.y + 1, aheadY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            entitys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (entitys.Contains(other.gameObject))
        {
            entitys.Remove(other.gameObject);            
        }
    }

    private IEnumerator NewRandom()
    {
        yield return new WaitForSeconds(frequency);
        randomY = Random.Range(-1f, 1f);
        StartCoroutine(NewRandom());
    }
}
