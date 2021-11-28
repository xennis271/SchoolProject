using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<Questions> Q;
    public GameObject[] options;
    public GameObject QuestionCanvas, ResultNone, ResultLow, ResultHigh;
     
    public int currentQuestion = -999;
    public bool rfSymptoms = false;
    public bool rfTravel = false;
    public Text QuestionText;

    //Try to make a bool array to track what should be done for results

    

    public void answerYes() //Helps continue to next question
    {
        
        if(Q.Count > 1)
        {
            
            rfSymptoms = true;
        }
        else 
        {
            rfTravel = true;
        } 
        GenerateQuestion();
    }  

    public void answerNo()
    {
        
        GenerateQuestion();
    }

    void quizResult()
    {
        if(rfSymptoms == false && rfTravel == false)
        {
            Debug.Log("No Risk");
            QuestionCanvas.SetActive(false);
            ResultNone.SetActive(true);
            
        }
        else if (rfSymptoms == false && rfTravel == true )
        {
            Debug.Log("Low Risk");
            QuestionCanvas.SetActive(false);
            ResultLow.SetActive(true);
        }
        else if (rfSymptoms == true)
        {
            Debug.Log("High Risk");
            QuestionCanvas.SetActive(false);
            ResultHigh.SetActive(true);
        }

        currentQuestion = 0;
    }



    void SetAnswer()//Used to assign answer options to buttons
    {
        for (int a = 0; a < options.Length; a++)
        {
            options[a].GetComponent<QuestionTrack>().AnswerTrack = false;
            options[a].transform.GetChild(0).GetComponent<Text>().text = Q[currentQuestion].Answers[a]; //Changes the button text
            
            if (Q[currentQuestion].QTrack == a+1)
            {
                options[a].GetComponent<QuestionTrack>().AnswerTrack = true;
            }
        }
    }

    

    public void GenerateQuestion()// Generates questions
    {
        if(currentQuestion < Q.Count)
        {
                Debug.Log("This is question " + currentQuestion);
                QuestionText.text = Q[currentQuestion].Question + "\n\n    " + Q[currentQuestion].option1+ "\n    " + Q[currentQuestion].option2+ "\n    " + Q[currentQuestion].option3 + "\n    " + Q[currentQuestion].option4;
                SetAnswer();
                currentQuestion++;
                
                
        }
        else
        {
            Debug.Log("End of Questionnaire");
            quizResult();
        }

        
    }
}
