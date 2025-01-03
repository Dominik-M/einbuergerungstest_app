using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeQuizController : MonoBehaviour
{
    public enum State
    {
        AWAIT_ANSWER, WRONG_ANSWER, RIGHT_ANSWER
    }

    private State state;

    public Text titleText, answer1, answer2, answer3, answer4, probText, resultText;
    public Question question;
    public Color rightColor = new Color(0.0f, 1.0f, 0.0f, 1f);
    public Color wrongColor = new Color(1.0f, 0.0f, 0.0f, 1f);
    public Color normalColor = new Color(0.0f, 0.0f, 0.0f, 1f);
    void Start()
    {
        NextQuestion();
    }


    public void NextQuestion()
    {
        int idx = (int)(Random.Range(0, Question.getAllQuestions().Length));
        question = Question.getAllQuestions()[idx];
        titleText.text = question.titleText;
        answer1.text = question.answers[0];
        answer2.text = question.answers[1];
        answer3.text = question.answers[2];
        answer4.text = question.answers[3];
        answer1.color = normalColor;
        answer2.color = normalColor;
        answer3.color = normalColor;
        answer4.color = normalColor;
        probText.text = "\r\nAuftrittswahrscheinlichkeit: "+question.probability+"%\n"+
            "xác suất trúng";
        resultText.text = "";
        state = State.AWAIT_ANSWER;
    }

    public void SelectAnswer(int idx)
    {
        if (question != null && state == State.AWAIT_ANSWER)
        {
            if (question.rightAnswerIdx == idx)
            {
                Debug.Log("Richtige Antwort");
                state = State.RIGHT_ANSWER;
                resultText.text = "RICHTIG\n"+
                    "ĐÚNG";
                resultText.color = rightColor;
            }
            else
            {
                Debug.Log("Falsche Antwort");
                state = State.WRONG_ANSWER;
                resultText.text = "FALSCH\n"+
                    "SAI";
                resultText.color = wrongColor;
            }
            answer1.color = question.rightAnswerIdx == 0 ? rightColor : wrongColor;
            answer2.color = question.rightAnswerIdx == 1 ? rightColor : wrongColor;
            answer3.color = question.rightAnswerIdx == 2 ? rightColor : wrongColor;
            answer4.color = question.rightAnswerIdx == 3 ? rightColor : wrongColor;
        }
    }
}
