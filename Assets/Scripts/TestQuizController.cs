using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestQuizController : MonoBehaviour
{
    private const int MAX_TRIES = 500000;

    private Question[] testQuests;
    private int[] testAnswers;
    public int numQuestions = 33, minCorrectAnswers = 17, currentQuestionIdx = -1;
    public GameObject resultPanel;
    public Text titleText, answer1, answer2, answer3, answer4, pageNumText, nextButtonText, resultText;
    public Question question;
    public Color selectColor = new Color(0.0f, 0.0f, 1.0f, 1f);
    public Color normalColor = new Color(0.0f, 0.0f, 0.0f, 1f);

    bool containsQuestion(Question q)
    {
        for (int i = 0; i < testQuests.Length; i++)
        {
            if (testQuests[i] != null && testQuests[i].Equals(q))
                return true;
        }
        return false;
    }
    void Start()
    {
        testQuests = new Question[numQuestions];
        testAnswers = new int[numQuestions];
        for (int i = 0; i < numQuestions; i++)
        {
            Question q; int count = 0;
            do
            {
                q = Question.getRandomQuestion();
                count++;
            } while (containsQuestion(q) && count < MAX_TRIES);

            testQuests[i] = q;
            testAnswers[i] = -1;
        }
        resultPanel.SetActive(false);
        ShowQuestion(0);
    }

    public bool CheckResult()
    {
        int correctAnswers = 0;
        return minCorrectAnswers <= correctAnswers;
    }

    public void EndTest()
    {
        Debug.Log("Test Ende");
        int correctAnswers = 0;

        for (int i = 0; i < testQuests.Length; i++)
        {
            if (testQuests[i] != null &&
                testQuests[i].rightAnswerIdx == testAnswers[i])
                correctAnswers++;
        }
        bool result = minCorrectAnswers <= correctAnswers;
        resultPanel.SetActive(true);
        resultText.text = "Test beendet\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            "\n" +
                            (result ? "SIE HABEN  BESTANDEN!\n": "SIE HABEN LEIDER NICHT BESTANDEN.\n") +
                            "\n" +
                            "\n" +
                            "\n" +
                            correctAnswers+" von " +numQuestions+" \n" +
                            "Fragen korrekt beantwortet.\n" +
                            "\n" +
                            "\n";
    }
    public void Back()
    {
        int idx = currentQuestionIdx - 1;
        if (idx >= 0)
        {
            ShowQuestion(idx);
        }
    }
    public void Next()
    {
        int idx = currentQuestionIdx + 1;
        if (idx >= numQuestions)
        {
            EndTest();
        }
        else
        {
            ShowQuestion(idx);
        }
    }
    public void ShowQuestion(int idx)
    {
        if (idx < numQuestions && idx >= 0)
        {
            currentQuestionIdx = idx;
            question = testQuests[idx];
            titleText.text = question.titleText;
            answer1.text = question.answers[0];
            answer2.text = question.answers[1];
            answer3.text = question.answers[2];
            answer4.text = question.answers[3];
            answer1.color = testAnswers[currentQuestionIdx] == 0 ? selectColor : normalColor;
            answer2.color = testAnswers[currentQuestionIdx] == 1 ? selectColor : normalColor;
            answer3.color = testAnswers[currentQuestionIdx] == 2 ? selectColor : normalColor;
            answer4.color = testAnswers[currentQuestionIdx] == 3 ? selectColor : normalColor;
            pageNumText.text = 1 + idx + " / " + numQuestions;

            if (idx >= numQuestions)
            {
                nextButtonText.text = "Test Beenden";
            }
            else
            {
                nextButtonText.text = "Nächste Frage";
            }
        }
        else
            Debug.LogWarning("ShowQuestion() index out of range: " + idx);
    }

    public void SelectAnswer(int idx)
    {
        testAnswers[currentQuestionIdx] = idx;
        ShowQuestion(currentQuestionIdx);
    }
}
