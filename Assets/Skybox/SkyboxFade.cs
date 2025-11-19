using System.Collections;
using UnityEngine;

public class SkyboxFade : MonoBehaviour
{
    public Material material;

    [Header("Shader Color Properties")]
    public string sunDiscProperty = "_SunDiscColor";
	public string sunHaloProperty = "_SunHaloColor";
	public string horizonLineProperty = "_HorizonLineColor";
    public string skyTopProperty = "_SkyGradientTop";
    public string skyBottomProperty = "_SkyGradientBottom";

    [Header("Transition Duration")]
    public float duration = 5f;

    void Start()
    {
        StartCoroutine(FadeSky());
    }

    IEnumerator FadeSky()
    {
        float t = 0f;

        Color sunStart = new Color(1f, 1f, 1f);
        Color sunEnd   = new Color(0.3396226f, 0.0227379f, 0.001601997f);

		Color haloStart = new Color(1f, 1f, 1f);
        Color haloEnd   = new Color(0.3396226f, 0.0227379f, 0.001601997f);

		Color horizonStart = new Color(1f, 1f, 1f);
        Color horizonEnd   = new Color(0.3396226f, 0.0227379f, 0.001601997f);

        Color skyTopStart = new Color(1f, 0.5f, 0f);  // orange
        Color skyTopEnd   = Color.red;               // red
		
		Color skyBotStart = new Color(1f, 0.5f, 0f);  // orange
        Color skyBotEnd   = Color.red;               // red

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            // Lerp values
            Color sunDisc = Color.Lerp(sunStart, sunEnd, t);
            Color skyColTop  = Color.Lerp(skyTopStart, skyTopEnd, t);

            // Apply to material
            material.SetColor(sunDiscProperty, sunDisc);
			material.SetColor(sunHaloProperty, sunHalo);
			material.SetColor(horizonLineProperty, sunHorizon);
            material.SetColor(skyTopProperty,  skyColTop);
            material.SetColor(skyBottomProperty, skyColBot);

            yield return null;
        }
    }
}
