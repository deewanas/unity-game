using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Linq.Enumerable;

public class HighscoresTable : MonoBehaviour
{
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, int> table = new Dictionary<string, int>();
        for(int i = 0; i < MainLogic.usernames.Length; i++)
        {
            table.Add(MainLogic.usernames[i], MainLogic.highscores[i]);
        }
        var myList = table.ToList();
        myList.Sort((pair1,pair2) => pair2.Value.CompareTo(pair1.Value));

        var x = 1;
        foreach (KeyValuePair<string,int> item in myList)
        {
            if(item.Key.Equals(MainLogic.username))
            {
                score.text += "Your place in highscore table - " + x + '\n';
                score.text += x + ")   " + item.Key + ":   " + item.Value + '\n';
            }
            else
            {
                score.text += x + ")   " + item.Key + ":   " + item.Value + '\n';
            }
            x++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
