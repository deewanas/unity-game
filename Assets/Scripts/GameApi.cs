using UnityEngine;
using System.Collections;
using System;

using SimpleJSON;

public class GameApi : MonoBehaviour
{
    public enum Method { GET, POST }

    [SerializeField]
    private string BASE_URL = "https://gentle-reaches-23324.herokuapp.com/";
    [SerializeField]
    private float WAIT_TIMEOUT = 10.0f;
    [SerializeField]
    private Method _selectedMethod = Method.GET;

    //public static string token;

    public delegate void ResultCallback(bool error, string data);

    public void Register(string username, string password, ResultCallback callback)
    {
        var form = new WWWForm();

        form.AddField("username", username);
        form.AddField("password", password);

        var www = new WWW(BASE_URL + "register", form);

        SendRequest(www, callback);
    }

    public void Logout(string token, ResultCallback callback)
    {
        _selectedMethod = Method.POST;
        var form = new WWWForm();

        form.AddField("token", token);

        var www = new WWW(BASE_URL + "logout", form);

        SendRequest(www, callback);
    }

    public void Login(string username, string password, ResultCallback callback)
    {
        var form = new WWWForm();

        form.AddField("username", username);
        form.AddField("password", password);

        var www = new WWW(BASE_URL + "login", form);

        SendRequest(www, callback);
    }

    public void UpdateHighscore(int score, string token, ResultCallback callback)
    {
        var form = new WWWForm();

        form.AddField("highscore", score);
        form.AddField("token", token);

        var www = new WWW(BASE_URL + "update", form);

        SendRequest(www, callback);
    }


    public void HighscoresTable( ResultCallback callback)
    {
        _selectedMethod = Method.GET;
        var form = new WWWForm();

        var www = new WWW(BASE_URL + "highscores");

        SendRequest(www, callback);
    }

    private void SendRequest(WWW www, ResultCallback callback)
    {
        Debug.Log("GameApi: send request to " + www.url);

        StartCoroutine(ExecuteRequest(www, callback));
    }

    private IEnumerator ExecuteRequest(WWW www, ResultCallback callback)
    {
        float elapsedTime = 0.0f;

        while (!www.isDone)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= WAIT_TIMEOUT)
            {
                if (callback != null)
                    callback(true, "{\"status\":400,\"message\":\"local timeout!\"}");

                yield break;
            }

            yield return null;
        }

        if (!www.isDone || !string.IsNullOrEmpty(www.error) || string.IsNullOrEmpty(www.text))
        {
            if (callback != null)
                callback(true, "{\"status\":400,\"message\":\"" + www.error + "\"}");

            yield break;
        }

        var response = www.text;

        try
        {
            var json = SimpleJSON.JSON.Parse(response);

            if (json["status"] != null && json["status"].AsInt != 200)
            {
                if (callback != null)
                    callback(true, response);

                yield break;
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }


        if (callback != null)
            callback(false, response);
    }
}