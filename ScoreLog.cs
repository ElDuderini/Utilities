using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
//using System.IO;
//using System.Net;

public class ScoreLog : MonoBehaviour
{
    //establish variables
    private string sceneName;

    private float throwTime;

    private float catchTime;

    private bool resultsShown;

    public GameObject win;

    public GameObject lose;

    public GameObject draw;

    private TextMeshProUGUI resultsTxt;

    public TextMeshProUGUI enteredName;

    public TMP_InputField username;


    //code used to access the website to store the information from the match
    const string privateCode = "KD4BAWAwOE6iwJSA8M7oign70cmejRFUeeBF4ADx4HoA";
    const string webURL = "http://dreamlo.com/lb/";

    private void Awake()
    {
      //Hide the menus that show the player the result of the match
        resultsShown = false;
        DontDestroyOnLoad(this.gameObject);
        win.SetActive(false);
        lose.SetActive(false);
        draw.SetActive(false);
        resultsShown = false;

        //if the player has already entered a username in a previous win, then use that same username and prevent them from making a new one.
        if (PlayerPrefs.HasKey("username"))
        {
            username.text = PlayerPrefs.GetString("username");
            username.interactable = false;
        }

    }

    void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;
        SceneCheck();
    }

//preform different actions in other scenes, like logging information every second to keep track of score, then present the informaion
    private void SceneCheck()
    {
        if(sceneName == "ArenaThrow")
        {
            throwTime += Time.deltaTime;
        }
        else if(sceneName == "ArenaCatch")
        {
            catchTime += Time.deltaTime;
        }
        //let the player see the score and let them use the mouse to navaigate the menu
        else if(sceneName == "Results" && resultsShown == false)
        {
            ShowResults();
            //UploadScore();
            resultsShown = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(sceneName == "Menu")
        {
            print("ScoreLogger destroyed");
            Destroy(this.gameObject);
        }
    }

    //method used to determine what type of result the player will see regarding if they win, lose or tie with the bot.
    private void ShowResults()
    {

        if (catchTime > throwTime)
        {
            print("Player wins!");
            win.SetActive(true);

           GameObject Results = win.transform.Find("Canvas").gameObject.transform.Find("Results").gameObject;

            resultsTxt = Results.GetComponent<TextMeshProUGUI>();

            resultsTxt.text = "You juggled " + (catchTime - throwTime) + " seconds longer";
        }
        else if (throwTime > catchTime)
        {
            print("Player looses");
            lose.SetActive(true);

            GameObject Results = lose.transform.Find("Canvas").gameObject.transform.Find("Results").gameObject;

            resultsTxt = Results.GetComponent<TextMeshProUGUI>();

            resultsTxt.text = "The bot juggled " + (throwTime - catchTime) + " seconds longer";
        }
        else if(throwTime == catchTime)
        {
            print("Draw");
            draw = transform.Find("Draw").gameObject;
            draw.SetActive(true);
        }
    }

//when the player presses the button the upload their score, then save the username they use plus upload the score
    public void UploadButton()
    {
        string name = enteredName.text;

        int score = Mathf.CeilToInt(catchTime - throwTime);

        PlayerPrefs.SetString("username", name);

        StartCoroutine(UploadScore(name, score));
    }

//access the website to create a new entry in the leaderboard
    IEnumerator UploadScore(string name, int score)
    {
        UnityWebRequest request = UnityWebRequest.Get(webURL + privateCode + "/add/" + name + "/" + score);
        yield return request.SendWebRequest();

        //if upload fails, then throw and error message
        if (request.isNetworkError)
        {
            print("Unable to upload score");
        }
        //if it works, then prevent the user from chaning their username
        else
        {
            print("Uploaded Score");
            username.interactable = false;
        }
    }
}
