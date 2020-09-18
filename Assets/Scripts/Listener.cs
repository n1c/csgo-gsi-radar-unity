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

    private event EventHandler<NewPayloadEventArgs> NewPayload;

    private void Start()
    {
        _drawer = _drawerGameObject.GetComponent<Drawer>();
        NewPayload += HandlePayload;

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
        string body = new StreamReader(context.Request.InputStream).ReadToEnd();

        NewPayload?.Invoke(this, new NewPayloadEventArgs
        {
            Body = body,
        });

        byte[] b = Encoding.UTF8.GetBytes("OK");
        context.Response.StatusCode = 200;
        context.Response.KeepAlive = false;
        context.Response.ContentLength64 = b.Length;

        Stream output = context.Response.OutputStream;
        output.Write(b, 0, b.Length);
        context.Response.Close();
    }

    private void HandlePayload(object _, NewPayloadEventArgs e)
    {
        Payload p = JsonConvert.DeserializeObject<Payload>(e.Body);
        Debug.Log("Payload: " + e.Body);

        try
        {
            _drawer.NewPayload(p);
        }
        catch (Exception ex)
        {
            Debug.Log("NewPayload Got Exception: " + ex.Message);
        }
    }

    private class NewPayloadEventArgs : EventArgs
    {
        public string Body { get; set; }
    }
}
