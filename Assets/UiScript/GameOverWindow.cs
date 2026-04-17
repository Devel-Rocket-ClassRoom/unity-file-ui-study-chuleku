using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System;
using Random = UnityEngine.Random;
using UnityEditor.ShaderGraph.Internal;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
public class GameOverWindow : GenericWindow
{
    public TextMeshProUGUI leftStatLabel;
    public TextMeshProUGUI leftStatValue;
    public TextMeshProUGUI rightStatLabel;
    public TextMeshProUGUI rightStatValue;
    public TextMeshProUGUI scoreValue;
    private TextMeshProUGUI[] statsLabels;
    private TextMeshProUGUI[] statsValues;
    public string scoreText;
    private const int totalStat = 6;
    private const int statsPerColumn = 3;
    private int[] statsRolls;
    private int finalScore;

    private Coroutine coroutine;

    public Button nextButton;
    public float statsDelay = 1f;
    public float scoreDuration = 2f;
    public float time;
    public float maxTimer = 3f;
    public float scoretimer = 2f;
    private bool leftValueCheck = false;
    private bool rightValueCheck = false;
    private bool scoreValueCheck = false;
    private void Awake()
    {
        statsLabels = new TextMeshProUGUI[] { leftStatLabel, rightStatLabel };
        statsValues = new TextMeshProUGUI[] { leftStatValue, rightStatValue };
        nextButton.onClick.AddListener(OnNext);
/*        leftValueCheck = false;
        rightValueCheck = false;*/
    }

    public override void Open()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        base.Open();
        ResetStats();
        coroutine = StartCoroutine(CoPlayGameOverRoutine());
   /*     StartCoroutine(LeftUpScore(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100)));*/
       
        
    }
    private void Update()
    {
/*        if(leftValueCheck)
        {
            leftValueCheck = false;
            StartCoroutine(RightUpScore(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100)));
        }

        if (rightValueCheck)
        {
            rightValueCheck = false;
            StartCoroutine(TotalUpScore(Random.Range(0, 1000000000)));
        }*/
    }
    public override void Close()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        
        base.Close();
    }

    public void OnNext()
    {
        /*       if(scoreValueCheck)   // 기존방식
               {
                   leftStatValue.text = null; 
                   rightStatValue.text = null;
                   scoreValue.text = "000000000";
                   scoreValueCheck = false;
               }*/
        windowManager.Open(0);
    }

    private void ResetStats()
    {
        statsRolls = new int[totalStat];
        for (int i = 0; i < totalStat; ++i)
        {
            statsRolls[i] = Random.Range(0, 100);
        }
        finalScore = Random.Range(0, 1000000000);

        for(int i = 0;i < statsValues.Length;i++)
        {
            statsLabels[i].text = string.Empty;
            statsValues[i].text = string.Empty;
        }

        scoreValue.text = $"{0:D9}";
    }
    private IEnumerator CoPlayGameOverRoutine()
    {
        for (int i = 0; i < totalStat; i++)
        {
            yield return new WaitForSeconds(statsDelay);
            int column = i / statsPerColumn;
            var labelText = statsLabels[column];
            var valueText = statsValues[column];
            string newline = (i%statsPerColumn == 0)? string.Empty : "\n";
            labelText.text = $"{labelText.text}{newline}Stat {(i%3)+1}";
            valueText.text = $"{valueText.text}{newline}{statsRolls[i]:D4}";

        }
        int current = 0;
        float t = 0f;
        while(t<1f)
        {
            t += Time.deltaTime / scoreDuration;
            current = Mathf.FloorToInt(Mathf.Lerp(0, finalScore, t));
            scoreValue.text = $"{current:D9}";
            yield return null;
        }
        scoreValue.text = $"{finalScore:D9}";
        coroutine = null;
    }
/*    public IEnumerator LeftUpScore(int score1,int score2,int score3)  // 기존에 하던방식
    {
        float t = 0f;
        int startnum = 0;

        while(t < maxTimer)
        {
            t += Time.deltaTime;
            float upscore = t / scoretimer;
            int s = (int)Mathf.Lerp(startnum, score1, upscore);
            leftStatValue.text = $"Stat      {s.ToString("D4")}\n" +
                                 $"   \n" +
                                 $" ";

            yield return null;
        }
        leftStatValue.text = $"Stat      {score1.ToString("D4")}\n" +
                             $"   \n" +
                             $"";
        yield return new WaitForSeconds(3f);
        t = 0f;
        while(t < maxTimer)
        {
            t += Time.deltaTime;
            float upscore = t/ scoretimer;
            int s = (int)Mathf.Lerp(startnum, score2, upscore);
            leftStatValue.text = $"Stat      {score1.ToString("D4")}\n" +
                                 $"Stat      {s.ToString("D4")}\n" +
                                 $" ";

            yield return null;
        }
        leftStatValue.text = $"Stat      {score1.ToString("D4")}\n" +
                             $"Stat      {score2.ToString("D4")}\n" +
                             $"";
        yield return new WaitForSeconds(3f);
        t = 0f;
        while( t < maxTimer)
        {
            t += Time.deltaTime;
            float upscore = t / scoretimer;
            int s = (int)Mathf.Lerp(startnum, score3, upscore);
            leftStatValue.text = $"Stat      {score1.ToString("D4")}\n" +
                                 $"Stat      {score2.ToString("D4")}\n" +
                                 $"Stat      {s.ToString("D4")}";

            yield return null;
        }
        leftStatValue.text = $"Stat      {score1.ToString("D4")}\n" +
                             $"Stat      {score2.ToString("D4")}\n" +
                             $"Stat      {score3.ToString("D4")}";
        yield return new WaitForSeconds(3f);
        leftValueCheck = true;

    }
    public IEnumerator RightUpScore(int score1, int score2, int score3)
    {
        float t = 0f;
        int startnum = 0;

        while(t < maxTimer)
        {
            t += Time.deltaTime;
            float upscore = t / scoretimer;
            int s = (int)Mathf.Lerp(startnum, score1, upscore);
            rightStatValue.text = $"Stat      {s.ToString("D4")}\n" +
                                  $"  \n" +
                                  $" ";

            yield return null;
        }
        rightStatValue.text = $"Stat      {score1.ToString("D4")}\n" +
                              $"  \n" +
                              $" ";
        yield return new WaitForSeconds(3f);
        t = 0f;
        while (t < maxTimer)
        {
            t += Time.deltaTime;
            float upscore = t / scoretimer;
            int s = (int)Mathf.Lerp(startnum, score2, upscore);
            rightStatValue.text = $"Stat      {score1.ToString("D4")}\n" +
                                 $"Stat      {s.ToString("D4")}\n" +
                                 $" ";

            yield return null;
        }
        rightStatValue.text = $"Stat      {score1.ToString("D4")}\n" +
                             $"Stat      {score2.ToString("D4")}\n" +
                             $" ";
        yield return new WaitForSeconds(3f);
        t = 0f;
        while (t < maxTimer)
        {
            t += Time.deltaTime;
            float upscore = t / scoretimer;
            int s = (int)Mathf.Lerp(startnum, score3, upscore);
            rightStatValue.text = $"Stat      {score1.ToString("D4")}\n" +
                                 $"Stat      {score2.ToString("D4")}\n" +
                                 $"Stat      {s.ToString("D4")}";

            yield return null;
        }
        rightStatValue.text = $"Stat      {score1.ToString("D4")}\n" +
                             $"Stat      {score2.ToString("D4")}\n" +
                             $"Stat      {score3.ToString("D4")}";
        yield return new WaitForSeconds(3f);
        rightValueCheck = true;
    }
    public IEnumerator TotalUpScore(int score)
    {
        float t = 0f;
        int startnum = 0;

        while(t < maxTimer)
        {
            t += Time.deltaTime;
            float upscore = t / scoretimer;
            int s = (int)Mathf.Lerp(startnum, score, upscore);
            scoreValue.text = s.ToString("D9");

            yield return null;
        }
        scoreValue.text = score.ToString("D9");
        scoreValueCheck = true;
    }*/
}
