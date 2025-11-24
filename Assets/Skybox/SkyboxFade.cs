using System.Collections;
using UnityEngine;

public class SkyboxFade : MonoBehaviour
{
    [Header("Skybox Material")]
    public Material material;

    [Header("Boss Phase Toggle")]
    public bool bossPhase = false;
    private bool previousBossPhase = false;
    private Coroutine fadeRoutine;

    [Header("Shader Color Properties")]
    public string sunDiscProperty = "_SunDiscColor";
    public string sunHaloProperty = "_SunHaloColor";
    public string horizonLineProperty = "_HorizonLineColor";
    public string skyTopProperty = "_SkyGradientTop";
    public string skyBottomProperty = "_SkyGradientBottom";

    [Header("Transition Duration")]
    public float duration = 1f;

    [Header("Color Sets")]
    public Color sunStart = new Color(0.990566f, 0.9795048f, 0.873754f);
    public Color sunEnd   = new Color(0.3396226f, 0.0227379f, 0.001601997f);

    public Color haloStart = new Color(0.9333334f, 0.8627452f, 0.8156863f);
    public Color haloEnd   = new Color(1f, 0.5481836f, 0f);

    public Color horizonStart = new Color(0.854902f, 0.8267749f, 0.7764707f);
    public Color horizonEnd   = new Color(0.8676471f, 0f, 0.1256586f);

    public Color skyTopStart = new Color(0.9433962f, 0.5971702f, 0.2536489f);
    public Color skyTopEnd   = new Color(0.1226415f, 0.1213383f, 0.06074223f);

    public Color skyBotStart = new Color(0.08454963f, 0.6709107f, 0.7169812f);
    public Color skyBotEnd   = new Color(0.7647059f, 0.5226884f, 0.3921568f);

    void Start()
    {
        // Clone material to avoid editing the asset
        material = new Material(material);
        RenderSettings.skybox = material;

        // Initialize starting colors
        material.SetColor(sunDiscProperty, sunStart);
        material.SetColor(sunHaloProperty, haloStart);
        material.SetColor(horizonLineProperty, horizonStart);
        material.SetColor(skyTopProperty, skyTopStart);
        material.SetColor(skyBottomProperty, skyBotStart);

        previousBossPhase = bossPhase;
    }

    void Update()
    {
        // Detect change in bossPhase state
        if (bossPhase != previousBossPhase)
        {
            previousBossPhase = bossPhase;
            SetBossPhase(bossPhase);
        }
    }

    public void SetBossPhase(bool active)
    {
        // Stop previous fade if needed
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        // Start correct fade direction
        fadeRoutine = StartCoroutine(FadeSky(active));
    }

    IEnumerator FadeSky(bool toBossPhase)
    {
        float t = 0f;

        // Choose direction
        Color s_sunDisc = toBossPhase ? sunStart : sunEnd;
        Color e_sunDisc = toBossPhase ? sunEnd   : sunStart;

        Color s_sunHalo = toBossPhase ? haloStart : haloEnd;
        Color e_sunHalo = toBossPhase ? haloEnd   : haloStart;

        Color s_horizon = toBossPhase ? horizonStart : horizonEnd;
        Color e_horizon = toBossPhase ? horizonEnd   : horizonStart;

        Color s_skyTop  = toBossPhase ? skyTopStart : skyTopEnd;
        Color e_skyTop  = toBossPhase ? skyTopEnd   : skyTopStart;

        Color s_skyBot  = toBossPhase ? skyBotStart : skyBotEnd;
        Color e_skyBot  = toBossPhase ? skyBotEnd   : skyBotStart;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            material.SetColor(sunDiscProperty,  Color.Lerp(s_sunDisc, e_sunDisc, t));
            material.SetColor(sunHaloProperty,  Color.Lerp(s_sunHalo, e_sunHalo, t));
            material.SetColor(horizonLineProperty, Color.Lerp(s_horizon, e_horizon, t));
            material.SetColor(skyTopProperty,   Color.Lerp(s_skyTop, e_skyTop, t));
            material.SetColor(skyBottomProperty,Color.Lerp(s_skyBot, e_skyBot, t));

            yield return null;
        }
    }
}