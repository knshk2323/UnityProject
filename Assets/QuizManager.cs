using UnityEngine;
using TMPro; 
using UnityEngine.UI;  
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public Image questionImage;        
    public TextMeshProUGUI questionText;  
    public TextMeshProUGUI scoreText;     
    public GameObject gameOverPanel;      
    public TextMeshProUGUI gameOverText; 

    private int wrongAttempts = 0;
    private int maxAttempts = 3;

    private int score = 0;

    void Start()
    {
        generateQuestion();
        scoreText.text = "Score: 0";
    }

    public void correct()
    {
        score += 10;
        scoreText.text = "Score: " + score;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        wrongAttempts++;

        if (wrongAttempts >= maxAttempts)
        {
            questionText.text = "";
            questionImage.gameObject.SetActive(false);
            foreach (GameObject btn in options)
            {
                btn.SetActive(false);
            }

            gameOverPanel.SetActive(true);
            gameOverText.text = "You have used all attempts!";
        }
        else
        {
            QnA.RemoveAt(currentQuestion);
            generateQuestion();
        }
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i]; // Заменили на TextMeshProUGUI

            if (QnA[currentQuestion].CorrectAnswer == i)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);
            questionImage.sprite = QnA[currentQuestion].image;
            questionText.text = QnA[currentQuestion].questionText;
            SetAnswers();
        }
        else
        {
            questionText.text = "Quiz Over!";
            questionImage.gameObject.SetActive(false);
            foreach (GameObject btn in options)
            {
                btn.SetActive(false);
            }
        }
    }
}
