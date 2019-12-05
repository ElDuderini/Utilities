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

    //http://dreamlo.com/lb/KD4BAWAwOE6iwJSA8M7oign70cmejRFUeeBF4ADx4HoA
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



    const string privateCode = "KD4BAWAwOE6iwJSA8M7oign70cmejRFUeeBF4ADx4HoA";
    const string webURL = "http://dreamlo.com/lb/";

    private void Awake()
    {
        resultsShown = false;
        DontDestroyOnLoad(this.gameObject);
        win.SetActive(false);
        lose.SetActive(false);
        draw.SetActive(false);
        resultsShown = false;

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

    public void UploadButton()
    {
        string name = enteredName.text;

        int score = Mathf.CeilToInt(catchTime - throwTime);

        PlayerPrefs.SetString("username", name);

        StartCoroutine(UploadScore(name, score));
    }

    IEnumerator UploadScore(string name, int score)
    {
        UnityWebRequest request = UnityWebRequest.Get(webURL + privateCode + "/add/" + name + "/" + score);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            print("Unable to upload score");
        }
        else
        {
            print("Uploaded Score");
            username.interactable = false;
        }
    }
}
