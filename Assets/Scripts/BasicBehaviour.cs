using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Use plugin namespace
using HybridWebSocket;

public class BasicBehaviour : MonoBehaviour
{
    public static BasicBehaviour instance;
    public bool isDebug = false;

    public string target = "score";
    [Tooltip("This will be altered for use in editor and desktop apps.")]
    public string adress = "wss://joye.dev:9000/";
    public string websocketState = "";
    public float pingFrequency = 0.5f;

    protected WebSocket ws;
    protected bool connected = false;

    // Use this for initialization
    protected void Start()
    {
        // Singelton
        if (instance != null)
            Destroy(this);

        if (instance == null)
            instance = this;

        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            adress = "ws://joye.dev:9001/";
        }
        adress = adress + target;

        // Create WebSocket instance
        Debug.Log("Connecting to " + adress);
        ws = WebSocketFactory.CreateInstance(adress);

        // Add OnError event listener
        ws.OnError += (string errMsg) =>
        {
            Debug.Log("Chat error: " + errMsg);
        };

        // Add OnClose event listener
        ws.OnClose += (WebSocketCloseCode code) =>
        {
            Debug.Log("Chat closed with code: " + code.ToString());
        };

        // Connect to the server
        ws.Connect();

        StartCoroutine(Ping());
    }

    private void Update()
    {
        string state = ws.GetState().ToString();
        if (websocketState != state) websocketState = state;
    }

    public void Send(string txt)
    {
        if (connected)
        {
            ws.Send(Encoding.UTF8.GetBytes(txt));
        }
    }
    public WebSocket GetWS()
    {
        return ws;
    }

    private IEnumerator Ping()
    {
        yield return new WaitForSeconds(pingFrequency);
        if (connected) Send("Ping");
        StartCoroutine(Ping());
    }
}
