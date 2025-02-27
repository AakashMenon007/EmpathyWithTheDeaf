using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;


public class AirtableSceneController : MonoBehaviour
{
    [Header("Scripts")]
    public AirtableManager airtableManager;

    [Header("Record ID")]
    public TMP_Text recordIDTMP;

    [Header("Questions")]
    // Define all questions with Slider, Text, and the string to store the answer
    [Header("question1")]
    public Slider question1Slider;
    public TMP_Text question1Level;
    public string question1;

    [Header("question2")]
    public Slider question2Slider;
    public TMP_Text question2Level;
    public string question2;

    [Header("question3")]
    public Slider question3Slider;
    public TMP_Text question3Level;
    public string question3;

    [Header("question4")]
    public Slider question4Slider;
    public TMP_Text question4Level;
    public string question4;

    [Header("question5")]
    public Slider question5Slider;
    public TMP_Text question5Level;
    public string question5;

    [Header("question6")]
    public Slider question6Slider;
    public TMP_Text question6Level;
    public string question6;

    [Header("question7")]
    public Slider question7Slider;
    public TMP_Text question7Level;
    public string question7;

    [Header("question8")]
    public Slider question8Slider;
    public TMP_Text question8Level;
    public string question8;

    [Header("question9")]
    public Slider question9Slider;
    public TMP_Text question9Level;
    public string question9;

    // Ensure you capture the input values for questions before calling SaveAllData
    public void SaveAllData()
    {
        // Set the values for the questions, including the slider values
        airtableManager.Question1 = question1Slider.value.ToString(); // Slider value
        airtableManager.Question2 = question2Slider.value.ToString();
        airtableManager.Question3 = question3Slider.value.ToString();
        airtableManager.Question4 = question4Slider.value.ToString();
        airtableManager.Question5 = question5Slider.value.ToString();
        airtableManager.Question6 = question6Slider.value.ToString();
        airtableManager.Question7 = question7Slider.value.ToString();
        airtableManager.Question8 = question8Slider.value.ToString();
        airtableManager.Question9 = question9Slider.value.ToString();

        airtableManager.CreateRecord();
    }

    // Update method to update the question levels dynamically based on slider values
    void Update()
    {
        // Update text fields dynamically for each question's slider
        question1Level.text = "Question 1 Level: " + question1Slider.value.ToString("0");
        question2Level.text = "Question 2 Level: " + question2Slider.value.ToString("0");
        question3Level.text = "Question 3 Level: " + question3Slider.value.ToString("0");
        question4Level.text = "Question 4 Level: " + question4Slider.value.ToString("0");
        question5Level.text = "Question 5 Level: " + question5Slider.value.ToString("0");
        question6Level.text = "Question 6 Level: " + question6Slider.value.ToString("0");
        question7Level.text = "Question 7 Level: " + question7Slider.value.ToString("0");
        question8Level.text = "Question 8 Level: " + question8Slider.value.ToString("0");
        question9Level.text = "Question 9 Level: " + question9Slider.value.ToString("0");
    }
}
