using Newtonsoft.Json;
using PayloadModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public class Listener : MonoBehaviour
{
    [SerializeField] private GameObject _drawerGameObject = default;
    private Drawer _drawer;
    private HttpListener _listener;
    private string _payloadProxy;

    public event EventHandler<NewPayloadEventArgs> NewPayload;

    public class NewPayloadEventArgs : EventArgs
    {
        public Payload Payload { get; set; }
    }

    private void Start()
    {
        _drawer = _drawerGameObject.GetComponent<Drawer>();

        if (!HttpListener.IsSupported)
        {
            Debug.Log("HttpListener not supported!");
            Application.Quit();
        }

        _listener = new HttpListener();
        _listener.Prefixes.Add("http://*:8888/");
        _listener.Start();
        _listener.BeginGetContext(new AsyncCallback(ContextCallback), null);

        Debug.Log("HTTP Listening...");
    }

    private void ContextCallback(IAsyncResult result)
    {
        HttpListenerContext context = _listener.EndGetContext(result);

        ProcessRequest(context);

        if (_listener.IsListening)
        {
            _listener.BeginGetContext(new AsyncCallback(ContextCallback), null);
        }
    }

    private void ProcessRequest(HttpListenerContext context)
    {
        _payloadProxy = new StreamReader(context.Request.InputStream).ReadToEnd();

        byte[] b = Encoding.UTF8.GetBytes("OK");
        context.Response.StatusCode = 200;
        context.Response.KeepAlive = false;
        context.Response.ContentLength64 = b.Length;

        Stream output = context.Response.OutputStream;
        output.Write(b, 0, b.Length);
        context.Response.Close();
    }

    private void Update()
    {
        if (_payloadProxy == null)
        {
            return;
        }

        Payload p = JsonConvert.DeserializeObject<Payload>(_payloadProxy);
        _payloadProxy = null;

        NewPayload?.Invoke(this, new NewPayloadEventArgs
        {
            Payload = p,
        });
    }
}
