using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestQuizController : MonoBehaviour
{
    public static readonly string IMAGE_MARKER = "[IMAGE]";

    private const int MAX_TRIES = 500000;

    private Question[] testQuests;
    private int[] testAnswers;
    public int numQuestions = 33, minCorrectAnswers = 17, currentQuestionIdx = -1;
    public GameObject resultPanel;
    public Text titleText, pageNumText, nextButtonText, resultText;
    public Text[] answerTexts;
    public Question question;
    public ImageLoader titleImageLoader;
    public Color rightColor = new Color(0.0f, 1.0f, 0.0f, 1f);
    public Color wrongColor = new Color(1.0f, 0.0f, 0.0f, 1f);
    public Color selectColor = new Color(0.0f, 0.0f, 1.0f, 1f);
    public Color normalColor = new Color(0.0f, 0.0f, 0.0f, 1f);
    public Question.Category questionCategory = Question.Category.GENERAL;

    private bool testEnd = false;
    bool containsQuestion(Question q)
    {
        for (int i = 0; i < testQuests.Length; i++)
        {
            if (testQuests[i] != null && testQuests[i].Equals(q))
                return true;
        }
        return false;
    }

    bool inSelectedCategory(Question q)
    {
        Question.Category c = q.GetCategory();
        return c == questionCategory || c == Question.Category.GENERAL;
    }
    void Start()
    {
        testEnd = false;
        testQuests = new Question[numQuestions];
        testAnswers = new int[numQuestions];
        for (int i = 0; i < numQuestions; i++)
        {
            Question q; int count = 0;
            do
            {
                q = Question.getRandomQuestion();
                count++;
            } while ((!inSelectedCategory(q) || containsQuestion(q)) && count < MAX_TRIES);

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
        testEnd = true;
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
                            (result ? "SIE HABEN  BESTANDEN!\n" : "SIE HABEN LEIDER NICHT BESTANDEN.\n") +
                            "\n" +
                            "\n" +
                            "\n" +
                            correctAnswers + " von " + numQuestions + " \n" +
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
            string questiontitleText = question.titleText;
            // Check if Title contains an image and try to load it
            if (questiontitleText.Contains(IMAGE_MARKER))
            {
                questiontitleText = questiontitleText.Replace(IMAGE_MARKER, "");
                titleImageLoader.LoadImage(question.ID);
            }
            else
            {
                titleImageLoader.gameObject.SetActive(false);
            }
            titleText.text = questiontitleText;
            for (int i = 0; i < answerTexts.Length; i++)
            {
                answerTexts[i].text = question.answers[i];
                answerTexts[i].color = testAnswers[currentQuestionIdx] == i ? selectColor : normalColor;
                if (testEnd)
                {
                    if(i == testAnswers[currentQuestionIdx])
                    {
                        answerTexts[i].text += " - Ihre Antwort";
                        answerTexts[i].color = wrongColor;
                    }
                    if(i == question.rightAnswerIdx)
                    {
                        answerTexts[i].text += " - Richtige Antwort";
                        answerTexts[i].color = rightColor;
                    }
                }
            }
            pageNumText.text = 1 + idx + " / " + numQuestions;

            if (idx + 1 >= numQuestions)
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
        if (!testEnd)
        {
            testAnswers[currentQuestionIdx] = idx;
            ShowQuestion(currentQuestionIdx);
        }
    }

    public void CloseResultPanel()
    {
        resultPanel.SetActive(false);
    }
}
