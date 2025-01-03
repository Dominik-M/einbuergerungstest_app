using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TextLoader : MonoBehaviour
{
    public string[] filelist;

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        string fulltext = "";
        foreach (string filename in filelist)
        {
            //string path = Application.dataPath + "/Resources/" + filename;

            Debug.Log("Reading " + filename);
            TextAsset textFile = Resources.Load<TextAsset>(filename);
            if (textFile)
                fulltext += textFile.text;
            else
                Debug.LogWarning("Cannot open TextAsset: " + filename);
        }
        text = gameObject.GetComponent<Text>();
        if (text)
        {
            text.text = fulltext;
        }
        else
        {
            Debug.LogWarning("Cannot find Text Component");
        }
    }

}
