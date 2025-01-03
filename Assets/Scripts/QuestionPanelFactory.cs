using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanelFactory : MonoBehaviour
{
    public GameObject questPanelPrefab;

    void Start()
    {
        foreach (Question q in Question.getAllQuestions())
        {
            GameObject instance = Instantiate(questPanelPrefab);
            instance.transform.SetParent(transform);
            Text text = instance.transform.Find("Text").GetComponent<Text>();
            text.text = q.titleText;
        }
    }

}
