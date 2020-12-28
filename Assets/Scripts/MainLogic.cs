using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainLogic : MonoBehaviour
{
    [SerializeField]
    public static GameApi _api;

    [SerializeField]
    private string _dataForSave = string.Empty;

    [SerializeField]
    private InputField _usernameField, _passwordField;

    public static string token;
    public static string username;

    public static string[] usernames;
    public static int[] highscores;

    // Use this for initialization
    void Awake()
    {
        if (_api == null)
            _api = FindObjectOfType<GameApi>();

        //if (_api == null)
            //Debug.LogError("'Api' field must be set!");
    }

    public void OnRegisterButtonClick()
    {
        _api.Register(_usernameField.text, _passwordField.text, (bool error, string data) =>
        {
            if (error)
                Debug.LogError("Error:" + data);
            else {
                Debug.Log("Response:" + data);
                SceneManager.LoadScene("LoginScene");
            }
                
        });
        

    }

    public void OnLoginButtonClick()
    {
        _api.Login(_usernameField.text, _passwordField.text, (bool error, string data) =>
        {
            if (error)
                Debug.LogError("Error:" + data);
            else {               
                var json = SimpleJSON.JSON.Parse(data);
                token = json["token"];
                username = _usernameField.text;
                Debug.Log("Response:" + username);
                SceneManager.LoadScene("PlayScene");
            }
                
        });
        

    }

    public void OnLogoutButtonClick()
    {
        _api.Logout(token, (bool error, string data) =>
        {
            if (error)
                Debug.LogError("Error:" + data);
            else {
                
                //var json = SimpleJSON.JSON.Parse(data);
                //string tok = json["token"];
                Debug.Log("Response:" + data);
                token = "";
                SceneManager.LoadScene("MainScene");
            }
                
        });       

    }

    public static void UpdateScore()
    {
        int score = hero.Score;
        _api.UpdateHighscore(score, token, (bool error, string data) =>
        {
            if (error)
                Debug.LogError("Error:" + data);
            else {
                Debug.Log("Response:" + data);
                //SceneManager.LoadScene("LoginScene"); 
            }
                
        });
        

    }

    public void OnHighscoresButtonClick()
    {
        _api.HighscoresTable( (bool error, string data) =>
        {
            if (error)
                Debug.Log("Response:" + data);
            else {             
                var json = SimpleJSON.JSON.Parse(data);
                usernames = new string[json["names"].Count];
                highscores = new int[json["highscores"].Count];
                for(int i = 0; i < json["names"].Count; i++) 
                {
                    usernames[i] = json["names"][i];
                    highscores[i] = json["highscores"][i].AsInt;
                }
                Debug.Log("Response:" + data);
                SceneManager.LoadScene("HighscoresScene");
            }
                
        });       
    }

    public void OnToRegisterButtonClick()
    {
        SceneManager.LoadScene("RegisterScene");

    }

    public void OnToLoginButtonClick()
    {
        SceneManager.LoadScene("LoginScene");

    }

    public void OnBack1ButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnBack2ButtonClick()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("GameScene");
    }

    
  

}
