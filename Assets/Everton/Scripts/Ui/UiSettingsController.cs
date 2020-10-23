﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSettingsController : UiGenericMenu
{

    #region fullscreen

    [Header("Fullscreen Components")]
    [SerializeField] private Text fullscreenText;
    [SerializeField] private Button buttonPreviousFullscreen;
    [SerializeField] private Button buttonNextFullscreen;

    private bool fullscreen = true;

    #endregion

    #region resolution

    [Header("Resolution Components")]
    [SerializeField] private Text resolutionsText = null;
    [SerializeField] private Button buttonPreviousResolution = null;
    [SerializeField] private Button buttonNextResolution = null;

    private Vector2[] resolutions = null;
    private int indexResolution = -1;

    #endregion

    #region language

    [Header("Language Components")]
    [SerializeField] private Text languageText;
    [SerializeField] private Button buttonPreviousLanguage;
    [SerializeField] private Button buttonNextLanguage;

    private int indexLanguage = 0;

    #endregion

    #region fontsize

    [Header("Fontsize Components")]
    [SerializeField] private Text fontsizeText;
    [SerializeField] private Button buttonPreviousFontSize;
    [SerializeField] private Button buttonNextFontSize;

    private int indexFontsize = 0;

    #endregion

    #region generalButtons

    [Header("General Buttons")]
    [SerializeField] private Button buttonDefault;
    [SerializeField] private Button buttonApply;

    #endregion

    protected virtual new void Awake()
    {
        base.Awake();

        resolutions = Settings.Resolution.listScreenSizes;

        // Setting fontsize buttons
        buttonPreviousFullscreen?.onClick.RemoveAllListeners();
        buttonPreviousFullscreen?.onClick.AddListener(FullscreenDisable);
        buttonNextFullscreen?.onClick.RemoveAllListeners();
        buttonNextFullscreen?.onClick.AddListener(FullscreenEnable);

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

        // Setting fontsize buttons
        buttonDefault?.onClick.RemoveAllListeners();
        buttonDefault?.onClick.AddListener(Default);
        buttonApply?.onClick.RemoveAllListeners();
        buttonApply?.onClick.AddListener(Apply);
    }

    public override void Setup()
    {
        Settings.SettingsFile.Load();
        DefaultSettings();
    }

    #region fullscreenMethods

    private void DefaultFullscreen(Settings.Settings settings)
    {
        fullscreen = settings.fullscreen;
        UpdateFullscreen();
    }

    private void FullscreenDisable()
    {
        fullscreen = false;
        UpdateFullscreen();
    }

    private void FullscreenEnable()
    {
        fullscreen = true;
        UpdateFullscreen();
    }

    private void UpdateFullscreen()
    {
        buttonPreviousFullscreen.interactable = fullscreen;
        buttonNextFullscreen.interactable = !fullscreen;
        UpdateFullscreenLabel();
    }

    private void UpdateFullscreenLabel()
    {
        string fullscreenYesNo = LocalizationManager.Instance.GetLocalizationValue(fullscreen ? "yes" : "no");
        fullscreenText.text = fullscreenYesNo;
    }

    #endregion

    #region resolutionMethods

    private void DefaultResolution(Settings.Settings settings)
    {
        indexResolution = settings.resolution;
        UpdateResolution();
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
        Vector2[] settingsResolution = Settings.Resolution.listScreenSizes;

        buttonPreviousResolution.interactable = !(indexResolution <= 0);
        buttonNextResolution.interactable = !(indexResolution >= resolutions.Length - 1);
        Vector2 resolution = Settings.Resolution.GetResolution(indexResolution);
        if (resolutionsText != null) resolutionsText.text = resolution.x + "x" + resolution.y;
    }

    #endregion

    #region languageMethods

    private void DefaultLanguage(Settings.Settings settings)
    {
        indexLanguage = settings.language;
        UpdateLanguage();
    }

    private void PreviousLanguage()
    {
        indexLanguage--;
        if (indexLanguage < 0) indexLanguage = 0;
        UpdateLanguage();
    }

    private void NextLanguage()
    {
        indexLanguage++;
        int length = Settings.Language.Length;
        if (indexLanguage >= length) indexLanguage = length - 1;
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        int length = Settings.Language.Length;

        buttonPreviousLanguage.interactable = !(indexLanguage <= 0);
        buttonNextLanguage.interactable = !(indexLanguage >= length - 1);
        string language = Settings.Language.GetNativeLanguage((Settings.Language.LanguageEnum) indexLanguage);
        languageText.text = language;

        LocalizationManager.Instance.LoadLocalizedText(Settings.Language.GetFileName((Settings.Language.LanguageEnum) indexLanguage));
        Events.ObserverManager.Notify(NotifyEvent.Language.ChangeInSettings);

        UpdateFullscreenLabel();
        UpdateFontsizeLabel();
    }

    #endregion

    #region fontsizeMethods

    private void DefaultFontsize(Settings.Settings settings)
    {
        indexFontsize = settings.fontsize;
        UpdateFontsize();
    }

    private void PreviousFontsize()
    {
        indexFontsize--;
        if (indexFontsize <= 0) indexFontsize = 0;
        UpdateFontsize();
    }

    private void NextFontsize()
    {
        indexFontsize++;
        int length = Settings.Fontsize.Length;
        if (indexFontsize >= length - 1) indexFontsize = length - 1;
        UpdateFontsize();
    }

    private void UpdateFontsize()
    {
        int length = Settings.Fontsize.Length;

        buttonPreviousFontSize.interactable = !(indexFontsize <= 0);
        buttonNextFontSize.interactable = !(indexFontsize >= length - 1);

        UpdateFontsizeLabel();
    }

    private void UpdateFontsizeLabel()
    {
        string fontsizeKey = Settings.Fontsize.GetFontsize((Settings.Fontsize.FontsizeEnum) indexFontsize);
        string fontsizeValue = LocalizationManager.Instance.GetLocalizationValue(fontsizeKey);
        fontsizeText.text = fontsizeValue;
    }

    #endregion

    #region settings

    private void DefaultSettings()
    {
        Settings.Settings settings = Settings.SettingsFile.settings;
        DefaultFullscreen(settings);
        DefaultResolution(settings);
        DefaultLanguage(settings);
        DefaultFontsize(settings);
    }

    private void Apply()
    {
        Settings.Settings settings = new Settings.Settings();
        settings.fullscreen = fullscreen;
        settings.resolution = indexResolution;
        settings.language = indexLanguage;
        settings.fontsize = indexFontsize;
        Settings.SettingsFile.Save(settings);

        ApplySettings();
    }

    private void Default()
    {
        Settings.SettingsFile.defaultSettings();
        DefaultSettings();
        ApplySettings();
    }

    private void ApplySettings()
    {
        Vector2 resolution = resolutions[indexResolution];
        FullScreenMode fullscreenMode = fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;

        Screen.SetResolution((int) resolution.x, (int) resolution.y, fullscreenMode);
        base.PlaySfxSound();
    }

    #endregion

}
