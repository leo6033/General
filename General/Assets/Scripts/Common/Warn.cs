using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warn : MonoBehaviour
{
    public Transform ExclamationMarkTransform;
    public GameObject MaskSprite;

    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Warnning());
    }

    IEnumerator Warnning()
    {
        Transform MaskSpriteTransform = MaskSprite.transform;
        Vector3 scale = MaskSpriteTransform.localScale;
        Vector3 position = ExclamationMarkTransform.position;
        while(time < 2)
        {
            scale += new Vector3(5 * Time.deltaTime, 5 * Time.deltaTime, 0);
            position -= new Vector3(0, 0.3f * Time.deltaTime, 0);
            MaskSpriteTransform.localScale = scale;
            ExclamationMarkTransform.position = position;
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
