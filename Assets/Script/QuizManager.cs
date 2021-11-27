using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
   public List<Questions> Q;
   public GameObject[] options;
   public int currentQuestion;

   public Text QuestionText;

   private void Start()
   {
        
        GenerateQuestion();
   }

   public void nextTrack() //Helps continue to next question
   {
       Q.RemoveAt(currentQuestion);
       GenerateQuestion();
   }  
   void SetAnswer()
   {
       for (int a = 0; a < options.Length; a++)
       {
           options[a].GetComponent<QuestionTrack>().AnswerTrack = false;
           
           if (Q[currentQuestion].QTrack == a+1)
           {
               options[a].GetComponent<QuestionTrack>().AnswerTrack = true;
           }
       }
   }

   

   void GenerateQuestion()
   {
       currentQuestion = Random.Range(0,Q.Count);
       QuestionText.text = Q[currentQuestion].Question + "\n\n    " + Q[currentQuestion].option1+ "\n    " + Q[currentQuestion].option2+ "\n    " + Q[currentQuestion].option3 + "\n    " + Q[currentQuestion].option4;
       
       SetAnswer();
   }
}
