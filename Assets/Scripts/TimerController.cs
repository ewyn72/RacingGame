using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public Text timeCounter;
    public Text bestTimeText;
    public Text lapCount;
    public Text deltaText;
    public Image deltaBackground;

    private TimeSpan timePlaying;
    private bool timerGoing;
    private int lapNum = 1;
    private bool start = true;

    private float elapsedTime;
    private float bestTime = 0;

    // current sector timing
    private float cs1 = 0;
    private float cs2 = 0;
    private float cs3 = 0;

    // Best sector timing
    private float s1 = 0;
    private float s2 = 0;
    private float s3 = 0;

    // Laps
    List<List<float>> best5laps = new List<List<float>>(); // lap, time, s1, s2, s3, gap


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeCounter.text = "00:00.00";
        timerGoing = false;
    }

    public void Lap()
    {
        if(!start) // Update best time
        {
            UpdateBestSessions(lapNum, cs1, cs2, cs3);

            // Update lap count
            lapNum++;
            lapCount.text = "Lap " + lapNum;

            if (bestTime == 0)
            {
                bestTime = elapsedTime;
            }
            else
            {
                bestTime = Math.Min(bestTime, elapsedTime);
            }

            string timeStr = TimeSpan.FromSeconds(bestTime).ToString("mm':'ss'.'ff");
            bestTimeText.text = timeStr;
        }

        timerGoing = true;
        elapsedTime = 0f;

        if(start) {
            StartCoroutine(UpdateTimer());
            start = false;
        }
  
        
    }

    public void EndTimer()
    {
        timerGoing = false;
        //HighScoreTable.instance.AddHighscoreEntry(elapsedTime, name);
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timeStr = timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timeStr;

            yield return null;
        }
    }

    public void updateS1()
    {
        cs1 = elapsedTime;

        if (s1 == 0)
        {
            s1 = cs1;
        }
        else 
        {
            float delta = cs1 - s1;
            updateDelta(delta);
            if (cs1 < s1)
                s1 = cs1;
        }

        
    }

    public void updateS2()
    {
        cs2 = elapsedTime - cs1;
        if (s2 == 0)
        {
            s2 = cs2;
        }
        else
        {
            float delta = cs2 - s2;
            updateDelta(delta);
            if (cs2 < s2)
                s2 = cs2;

        }

        
    }

    public void updateS3()
    {
        cs3 = elapsedTime - (cs1 + cs2);
        if (s3 == 0)
        {
            s3 = cs3;
        }
        else 
        {
            float delta = cs3 - s3;
            updateDelta(delta);
            if (cs3 < s3)
            {
                s3 = cs3;
            }
        }
    }

    private void updateDelta(float delta)
    {
        string timeStr = TimeSpan.FromSeconds(delta).ToString("mm':'ss'.'ff");

        if (delta < 0)
        {
            deltaBackground.color = new Color32(0, 255, 0, 162);
            deltaText.text = "-" + timeStr;
        }
        else if (delta > 0)
        {
            deltaBackground.color = new Color32(255, 0, 0, 162);
            deltaText.text = "+" + timeStr;
        }
        else
        {
            deltaBackground.color = new Color32(0,0,0,162);
            deltaText.text = timeStr;
        }
    }

    private void UpdateBestSessions(float lapNum, float s1, float s2, float s3)
    {
        List<float> lap = new List<float>();
        float totalTime = s1 + s2 + s3;
        lap.Add(lapNum);
        lap.Add(totalTime);
        lap.Add(s1);
        lap.Add(s2);
        lap.Add(s3);
        lap.Add(0);

        // Calculate order if laps
        if (best5laps.Count < 5)
        {
            best5laps.Add(lap);
        }
        else
        {
            // check to see if current lap faster than slowest best lap
            if (best5laps[4][1] > lap[1])
            {
                best5laps.RemoveAt(4);
                best5laps.Add(lap);
            }
        }

        // Sort List
        for (int i = 0; i < best5laps.Count; i++)
        {
            for (int j = i + 1; j < best5laps.Count; j++)
            {
                if (best5laps[i][1] > best5laps[j][1])
                {
                    List<float> temp = best5laps[i];
                    best5laps[i] = best5laps[j];
                    best5laps[j] = temp;
                }
            }
        }

        // Update gaps
        for (int i = 1; i < best5laps.Count; i++)
        {
            best5laps[i][5] = best5laps[i][1] - best5laps[0][1];
        }
    }

    public List<List<float>> GetBestSessions()
    {
        return best5laps;
    }
}
