using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestionTrack : MonoBehaviour
{
    public bool AnswerTrack = false;
    public QuizManager quizManager;

    
    public void Answer()
    {
        if (AnswerTrack)
        {
            Debug.Log("Yes");
            quizManager.answerYes();
        }
        else
        {
            Debug.Log("No");
            quizManager.answerNo();
        }
    }

}
