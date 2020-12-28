using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hero : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform cam;
    public Vector3 cameraRelative;

    public int Life = 5;
    public static int Score = 0;
    public int maxLives = 5;

    public Image[] lives;

    public Sprite heart; 
    public Sprite emptyHeart;

    public Text score;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePoint = transform.InverseTransformPoint(0,0,0);

        if (Input.GetMouseButtonDown(0) && relativePoint.y > 0.5f) {//>2.06f
            jump();
        }

        if(Life > maxLives)
        {
            Life = maxLives;
        }

        for(int i = 0; i < lives.Length; i++)
        {
            if(i < Life)
            {
                lives[i].sprite = heart;
            }
            else
            {
                lives[i].sprite = emptyHeart;
            }
            if(i < maxLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }

        score.text = "Score: " + Score;
        
    }

    void jump() {
        rb.AddForce(transform.up*75f, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Candy") {
            Life--;
            Destroy(col.gameObject);
            if(Life == 0) {
                MainLogic.UpdateScore(); 
                Score = 0;
                ReloadLevel();
            }
        }
        if(col.gameObject.tag == "Marshmallow") {
            Score++;
            Destroy(col.gameObject);
        }
    }

    void ReloadLevel() {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("PlayScene");
        //Application.LoadLevel("Restart");
    }


}
