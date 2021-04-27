using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager: MonoBehaviour
{
    public int level;
    public RectTransform Lines;
    [Header("起始点")]
    public List<GameObject> firstPoints;

    [Header("线宽")]
    public float LineWidth = 3.0f;

    private int round;
    private Dictionary<int, PointAttr> pointDict;
    private int count = 0;

    private void Start()
    {
        pointDict = new Dictionary<int, PointAttr>();
        round = 1;
        foreach(GameObject point in firstPoints)
        {
            PointAttr next = point.GetComponent<PointAttr>();
            next.level = level;
            next.round = round;
            next.number = count++;
            pointDict[next.number] = next;
            Draw(point);
        }

        PlayerData player = SaveSystem.LoadPlayer();
        for(int i = 1; i < player.CurrentLevelPoint.Count; i++)
        {
            Debug.Log(pointDict[player.CurrentLevelPoint[i - 1]] + " " + pointDict[player.CurrentLevelPoint[i]]);
            DrawLine(pointDict[player.CurrentLevelPoint[i - 1]].GetComponent<RectTransform>(), pointDict[player.CurrentLevelPoint[i]].GetComponent<RectTransform>(), Color.black);
        }

        for(int i = 0; i < player.CurrentLevelPoint.Count - 1; i++)
        {
            Slider slider = pointDict[player.CurrentLevelPoint[i]].GetComponent<Slider>();
            slider.handleRect.GetComponent<Image>().color = Color.gray;
        }

        if(player.CurrentLevelPoint.Count > 0)
            pointDict[player.CurrentLevelPoint[player.CurrentLevelPoint.Count - 1]].GetComponent<Slider>().value = 1;
    }

    private void Draw(GameObject currentPoint)
    {
        PointAttr pointAttr = currentPoint.GetComponent<PointAttr>();
        foreach(GameObject nextPoint in pointAttr.points)
        {
            PointAttr next = nextPoint.GetComponent<PointAttr>();
            next.level = level;
            next.round = pointAttr.round + 1;
            next.number = count++;
            pointDict[next.number] = next;
            DrawLine(currentPoint.GetComponent<RectTransform>(), nextPoint.GetComponent<RectTransform>(), Color.red);
            Draw(nextPoint);
        }
        
    }

    public void DrawLine(RectTransform start, RectTransform target, Color color)
    {
        GameObject line = new GameObject("line");
        line.SetActive(false);
        RectTransform  lineRT = line.AddComponent<RectTransform>();
        lineRT.pivot = new Vector2(0, 0.5f);
        lineRT.localScale = Vector3.one;
        Image lineImg = line.AddComponent<Image>();
        lineImg.color = color;
        lineImg.raycastTarget = false;
        lineRT.SetParent(Lines);
        lineRT.position = start.position;
        lineRT.sizeDelta = Vector2.zero;
        line.SetActive(true);

        Vector3 targetPos = target.position;
        Vector3 curPos = start.position;
        Vector3 dir = targetPos - curPos;
        lineRT.position += dir / 4;
        lineRT.sizeDelta = new Vector2(dir.magnitude / 2, LineWidth);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lineRT.localRotation = Quaternion.Euler(0, 0, angle);
    }

}
