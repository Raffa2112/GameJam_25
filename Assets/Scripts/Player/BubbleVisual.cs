using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BubbleVisual : MonoBehaviour
{
    [SerializeField] private float[] _growthScales;
    // [SerializeField] private float _constantGrowthScale = 1.2f;

    public void SetScale(int level)
    {
        if (level >= _growthScales.Length) level = _growthScales.Length;
        if (level < 0) level = 0;

        StartCoroutine(GrowToScale(level));
    }

    private IEnumerator GrowToScale(int level)
    {
        Vector3 targetScale = Vector3.one * _growthScales[level];
        Vector3 startScale = transform.localScale;

        float timer = 0f;
        float duration = 0.5f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, targetScale, timer / duration);
            yield return null;
        }
    }

    // private IEnumerator ConstantGrowAndShrink()
    // {
    //     while (true)
    //     {
    //         Vector3 startScale = transform.localScale;
    //         Vector3 targetScale = startScale * _constantGrowthScale;

    //         transform.localScale = Vector3.Lerp(startScale, _constantGrowthScale, timer / duration);
    //         yield return null;
    //     }
    // }
}