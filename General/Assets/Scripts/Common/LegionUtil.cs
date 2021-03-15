using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionUtil : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void setColor(Color color)
    {
        if(skinnedMeshRenderer != null)
            skinnedMeshRenderer.material.color = color;
        if(isActiveAndEnabled)
            StartCoroutine(ColorReset());
    }

    IEnumerator ColorReset()
    {
        yield return new WaitForSeconds(0.2f);
        if (skinnedMeshRenderer.material.color != Color.white)
            skinnedMeshRenderer.material.color = Color.black;
    }

    public Color getColor()
    {
        return skinnedMeshRenderer.material.color;
    }
}
