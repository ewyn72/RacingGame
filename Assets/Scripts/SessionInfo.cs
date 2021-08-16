using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionInfo : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;
    public Transform theoryBest;
    public Transform fastLap;
    private List<List<float>> bestLaps;
    private List<Transform> lapEntryTransformList;

    // Start is called before the first frame update
    void Awake()
    {
        entryTemplate.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSession()
    {
        // Remove all current entries
        bool keepTemplate = true;
        foreach (Transform child in entryContainer)
        {
            if (keepTemplate)
            {
                keepTemplate = false;
            }
            else { Destroy(child.gameObject); }
            
        }

        bestLaps = TimerController.instance.GetBestSessions();
        lapEntryTransformList = new List<Transform>();
        foreach (List<float> lapEntry in bestLaps)
        {
            CreateLapEntryTransform(lapEntry, entryContainer, lapEntryTransformList);
        }

        // Fastest lap
        fastLap.Find("timeText (1)").GetComponent<Text>().text = TimeSpan.FromSeconds(bestLaps[0][1]).ToString("mm':'ss'.'ff");
        fastLap.Find("timeText (2)").GetComponent<Text>().text = TimeSpan.FromSeconds(bestLaps[0][2]).ToString("mm':'ss'.'ff");
        fastLap.Find("timeText (3)").GetComponent<Text>().text = TimeSpan.FromSeconds(bestLaps[0][3]).ToString("mm':'ss'.'ff");
        fastLap.Find("timeText (4)").GetComponent<Text>().text = TimeSpan.FromSeconds(bestLaps[0][4]).ToString("mm':'ss'.'ff");

        // Theory best
        float t1 = 0;
        float t2 = 0;
        float t3 = 0;
        for (int i = 0; i < bestLaps.Count; i++)
        {
            if (t1 == 0 || t1 > bestLaps[i][2])
            {
                t1 = bestLaps[i][2];
            }
            if (t2 == 0 || t2 > bestLaps[i][3])
            {
                t2 = bestLaps[i][3];
            }
            if (t3 == 0 || t3 > bestLaps[i][4])
            {
                t3 = bestLaps[i][4];
            }
        }
        theoryBest.Find("timeText (1)").GetComponent<Text>().text = TimeSpan.FromSeconds(t1 + t2 + t3).ToString("mm':'ss'.'ff");
        theoryBest.Find("timeText (2)").GetComponent<Text>().text = TimeSpan.FromSeconds(t1).ToString("mm':'ss'.'ff");
        theoryBest.Find("timeText (3)").GetComponent<Text>().text = TimeSpan.FromSeconds(t2).ToString("mm':'ss'.'ff");
        theoryBest.Find("timeText (4)").GetComponent<Text>().text = TimeSpan.FromSeconds(t3).ToString("mm':'ss'.'ff");

    }

    private void CreateLapEntryTransform(List<float> Lap, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int lapNumber = (int)Lap[0];

        entryTransform.Find("lapNumText").GetComponent<Text>().text = lapNumber.ToString();
        string time = TimeSpan.FromSeconds(Lap[1]).ToString("mm':'ss'.'ff");
        entryTransform.Find("timeText").GetComponent<Text>().text = time;
        entryTransform.Find("s1Text").GetComponent<Text>().text = TimeSpan.FromSeconds(Lap[2]).ToString("mm':'ss'.'ff");
        entryTransform.Find("s2Text").GetComponent<Text>().text = TimeSpan.FromSeconds(Lap[3]).ToString("mm':'ss'.'ff"); 
        entryTransform.Find("s3Text").GetComponent<Text>().text = TimeSpan.FromSeconds(Lap[4]).ToString("mm':'ss'.'ff");
        entryTransform.Find("gapText").GetComponent<Text>().text = "+" + TimeSpan.FromSeconds(Lap[5]).ToString("mm':'ss'.'ff");

        transformList.Add(entryTransform);
    }
}
