using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSettingsController : UiGenericMenu
{

    #region resolution

    [Header("Resolution Components")]
    [SerializeField] private Text resolutionsText = null;
    [SerializeField] private Button buttonPreviousResolution = null;
    [SerializeField] private Button buttonNextResolution = null;

    private float maxWidth = Screen.width;
    private float maxHeight = Screen.height;

    private Vector2[] screenSizes =
    {
        new Vector2(800, 600), new Vector2(1024, 768), new Vector2(1280, 720), new Vector2(1366, 768),
        new Vector2(1440, 900), new Vector2(1600, 900), new Vector2(1680, 1050), new Vector2(1920, 1080),
        new Vector2(2560, 1080), new Vector2(2560, 1440)
    };

    private Vector2[] listScreenSizes
    {
        get
        {
            List<Vector2> listScreenSizes = new List<Vector2>();
            foreach (Vector2 vector2 in screenSizes) if (vector2.x <= maxWidth && vector2.y <= maxHeight) listScreenSizes.Add(vector2);
            return listScreenSizes.ToArray();
        }
    }

    private Vector2[] resolutions = null;

    private int indexResolution = -1;
    private Vector2 actualResolution => resolutions[indexResolution];

    #endregion

    #region language

    [Header("Language Components")]
    [SerializeField] private Text languageText;
    [SerializeField] private Button buttonPreviousLanguage;
    [SerializeField] private Button buttonNextLanguage;

    private string[] languages = {"English", "Espanol", "Português"};
    private string actualLanguage => languages[indexLanguage];

    private int indexLanguage = 0;

    #endregion

    #region fontsize

    [Header("Fontsize Components")]
    [SerializeField] private Text fontsizeText;
    [SerializeField] private Button buttonPreviousFontSize;
    [SerializeField] private Button buttonNextFontSize;

    private string[] fontSizes = {"SMALL", "NORMAL", "BIG"};
    private string actualFontsize => fontSizes[indexFontsize];

    private int indexFontsize = 0;

    #endregion

    protected new virtual void Awake()
    {
        base.Awake();

        resolutions = listScreenSizes;
        FindCloserResolution();

        // Setting resolution buttons
        buttonPreviousResolution?.onClick.RemoveAllListeners();
        buttonPreviousResolution?.onClick.AddListener(PreviousResolution);
        buttonNextResolution?.onClick.RemoveAllListeners();
        buttonNextResolution?.onClick.AddListener(NextResolution);

        // Setting language buttons
        buttonPreviousLanguage?.onClick.RemoveAllListeners();
        buttonPreviousLanguage?.onClick.AddListener(PreviousLanguage);
        buttonNextLanguage?.onClick.RemoveAllListeners();
        buttonNextLanguage?.onClick.AddListener(NextLanguage);

        // Setting fontsize buttons
        buttonPreviousFontSize?.onClick.RemoveAllListeners();
        buttonPreviousFontSize?.onClick.AddListener(PreviousFontsize);
        buttonNextFontSize?.onClick.RemoveAllListeners();
        buttonNextFontSize?.onClick.AddListener(NextFontsize);
    }

    #region resolutionMethods
    private void FindCloserResolution()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            Vector2 resolution = resolutions[i];
            if (maxWidth == resolution.x && maxHeight == resolution.y)
            {
                indexResolution = i;
                break;
            }
        }
        if (indexResolution == -1) indexResolution = 3;
    }

    private void NextResolution()
    {
        indexResolution++;
        if (indexResolution >= resolutions.Length) indexResolution = resolutions.Length - 1;
        UpdateResolution();
    }

    private void PreviousResolution()
    {
        indexResolution--;
        if (indexResolution < 0) indexResolution = 0;
        UpdateResolution();
    }

    private void UpdateResolution()
    {
        Vector2 resolution = actualResolution;
        if (resolutionsText != null) resolutionsText.text = resolution.x + "x" + resolution.y;
    }
    #endregion

    #region languageMethods
    private void PreviousLanguage()
    {
        indexLanguage--;
        if (indexLanguage < 0) indexLanguage = 0;
        UpdateLanguage();
    }

    private void NextLanguage()
    {
        indexLanguage++;
        if (indexLanguage >= languages.Length) indexLanguage = languages.Length - 1;
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        string language = actualLanguage;
        languageText.text = language;
    }
    #endregion

    #region fontsizeMethods
    private void PreviousFontsize()
    {
        indexFontsize--;
        if (indexFontsize < 0) indexFontsize = 0;
        UpdateFontsize();
    }

    private void NextFontsize()
    {
        indexFontsize++;
        if (indexFontsize >= fontSizes.Length) indexFontsize = fontSizes.Length - 1;
        UpdateFontsize();
    }

    private void UpdateFontsize()
    {
        string fontsize = actualFontsize;
        fontsizeText.text = fontsize;
    }
    #endregion

}
