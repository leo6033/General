using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager: MonoBehaviour
{
    public RectTransform Lines;
    [Header("起始点")]
    public List<GameObject> firstPoints;

    [Header("线宽")]
    public float LineWidth = 3.0f;

    private void Start()
    {
        foreach(GameObject point in firstPoints)
        {
            Draw(point);
        }
    }

    private void Draw(GameObject currentPoint)
    {
        PointAttr pointAttr = currentPoint.GetComponent<PointAttr>();
        foreach(GameObject nextPoint in pointAttr.points)
        {
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
        lineRT.sizeDelta = new Vector2(dir.magnitude, LineWidth);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        lineRT.localRotation = Quaternion.Euler(0, 0, angle);
    }

}
