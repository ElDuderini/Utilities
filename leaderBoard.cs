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

        boardText.text = "";

        StartCoroutine(DownLoadScores());
    }

    IEnumerator DownLoadScores()
    {
        UnityWebRequest download = UnityWebRequest.Get("http://dreamlo.com/lb/5dcde5b2b5622e683c16f8c5/json/10");

        yield return download.SendWebRequest();

        if (download.isNetworkError)
        {
            print("Unable to download scores");
        }
        else
        {
            print("downloaded Scores");
            jsonSerialize = JsonUtility.FromJson<RootObject>(download.downloadHandler.text);

            print(download.downloadHandler.text);

            DisplayScore();
        }
    }

    private void DisplayScore()
    {
        var path = jsonSerialize.dreamlo.leaderboard.entry;
        for (int i = 0; i <= path.Count; i++)
        {
            boardText.text += (i + 1) + ": " + path[i].name + " - " + path[i].score + "\n";
        }
    }

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