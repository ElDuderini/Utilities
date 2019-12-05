using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class leaderBoard : MonoBehaviour
{

    public TextMeshProUGUI boardText;

    private RootObject jsonSerialize;

    private void OnEnable()
    {
        print("Gameobject enabled!");

        //reset the text for the leaderboard
        boardText.text = "";

        StartCoroutine(DownLoadScores());
    }

    IEnumerator DownLoadScores()
    {
      //connectec to the link to download the information
        UnityWebRequest download = UnityWebRequest.Get("http://dreamlo.com/lb/5dcde5b2b5622e683c16f8c5/json/10");

        yield return download.SendWebRequest();

        //if it is unable to download the information, throw an error message
        if (download.isNetworkError)
        {
            print("Unable to download scores");
        }
        //if it is able to download the data, place the data in a class to deserialize it
        else
        {
            print("downloaded Scores");
            jsonSerialize = JsonUtility.FromJson<RootObject>(download.downloadHandler.text);

            print(download.downloadHandler.text);

            DisplayScore();
        }
    }

    //Use a for loop to display the information on the leaderbaord
    private void DisplayScore()
    {
        var path = jsonSerialize.dreamlo.leaderboard.entry;
        for (int i = 0; i <= path.Count; i++)
        {
            boardText.text += (i + 1) + ": " + path[i].name + " - " + path[i].score + "\n";
        }
    }

    //Classes that are used to store/organize information from the downloaded json string
    [System.Serializable]
    public class Entry
    {
        public string name;
        public string score;
        public string seconds;
        public string text;
        public string date;
    }
    [System.Serializable]
    public class Leaderboard
    {
        public List<Entry> entry;
    }
    [System.Serializable]
    public class Dreamlo
    {
        public Leaderboard leaderboard;
    }
    [System.Serializable]
    public class RootObject
    {
        public Dreamlo dreamlo;
    }
}
