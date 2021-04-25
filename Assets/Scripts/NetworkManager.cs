using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class NetworkManager : MonoBehaviour
{
    [Tooltip("Determines how often information is sent to the server, in seconds.")]
    public float pingFrequency = 1f;
    public bool debug = false;

    public List<Score> scores = new List<Score>();
    protected BasicBehaviour behaviour;
    protected bool readyForId = false;
    protected Vector3 lastFramePos = Vector3.zero;

    public virtual void SetPingFrequency(float input)
    {
        pingFrequency = input;
    }

    public void GetNewGuid()
    {
        behaviour.Send("Get guid");
    }

    protected virtual void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(SetUpSocket());
    }

    protected virtual IEnumerator SetUpSocket()
    {
        while (BasicBehaviour.instance == null)
        {
            Debug.Log("Waiting for instance");
            yield return new WaitForSeconds(0.5f);
        }

        behaviour = BasicBehaviour.instance;
        behaviour.GetWS().OnOpen += () =>
        {
            readyForId = true;
        };
        behaviour.GetWS().OnMessage += (byte[] msg) =>
        {
            ProcessMessage(Encoding.UTF8.GetString(msg));
        };
    }

    public void SendScore(string name, int score)
    {
        if (behaviour.GetWS().GetState() != HybridWebSocket.WebSocketState.Open) return;

        string msg = "Score:" + PlayerPrefs.GetString("Guid") + "%" + name + "%" + score.ToString();
        behaviour.Send(msg);
    }

    protected virtual void ProcessMessage(string msg)
    {
        // If the message is about the players new ID
        if (readyForId && msg.Contains("ID"))
        {
            var parts = msg.Split(":".ToCharArray());
            for (int i = parts.Length; i <= 0; i--)
            {
                Debug.Log("part " + i + ": " + parts[i - 1]);
            }
            Guid newId = Guid.Parse(parts[1]);
            if (PlayerPrefs.GetString("Guid") == null || String.IsNullOrEmpty(PlayerPrefs.GetString("Guid")))
            {
                Debug.Log("Saved guid: " + newId.ToString());
                PlayerPrefs.SetString("Guid", newId.ToString());
                PlayerPrefs.Save();
            }
        }
        else if (msg.Contains("|"))
        {
            scores.Clear();
            string[] scoreArray = msg.Split("|".ToCharArray());

            foreach(string s in scoreArray)
            {
                string[] arr = s.Split("%".ToCharArray());
                Guid guid = Guid.Parse(arr[0]);
                string playerName = arr[1];
                int score = Convert.ToInt32(arr[2]);
                Score newScore = new Score();
                newScore.guid = guid;
                newScore.playerName = playerName;
                newScore.score = score;
                scores.Add(newScore);
            }
        }
    }
}

public class Score
{
    public Guid guid;
    public string playerName;
    public int score;
}