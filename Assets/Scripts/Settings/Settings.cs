using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static Settings.Fontsize;
using static Settings.Language;

namespace Settings
{
    
    [Serializable]
    public class Settings
    {

        public bool fullscreen = true;
        public int resolution = Resolution.DefaultResolution();
        public int language = Language.DefaultLanguage;
        public int fontsize = Fontsize.DefaultFontsize;

        public Settings() {}

        public Settings(Settings settings)
        {
            this.fullscreen = settings.fullscreen;
            this.resolution = settings.resolution;
            this.language = settings.language;
            this.fontsize = settings.fontsize;
        }

        public void DefaultValues()
        {
            fullscreen = true;
            resolution = Resolution.DefaultResolution();
            language = Language.DefaultLanguage;
            fontsize = Fontsize.DefaultFontsize;
        }

    }

    public static class SettingsFile
    {

        private static Settings _settings = new Settings();
        private static string _filepathSettings = Application.persistentDataPath + "/Config/Settings.dat";
        
        private static BinaryFormatter binaryFormatter = new BinaryFormatter();

        public static bool Save(Settings datas)
        {
            try
            {
                string directory = _filepathSettings.Substring(0, _filepathSettings.LastIndexOf('/'));
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                if (File.Exists(_filepathSettings)) File.Delete(_filepathSettings);
                FileStream file = File.Create(_filepathSettings);

                binaryFormatter.Serialize(file, datas);
                file.Close();

                _settings = datas;

                LoadLanguage();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public static void Load()
        {
            string directory = _filepathSettings.Substring(0, _filepathSettings.LastIndexOf('/'));
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            if (!File.Exists(_filepathSettings)) Save(_settings);
            FileStream file = File.Open(_filepathSettings, FileMode.Open);

            _settings = (Settings) binaryFormatter.Deserialize(file);
            file.Close();

            LoadLanguage();
        }

        private static void LoadLanguage()
        {
            LocalizationManager.Instance.LoadLocalizedText(Language.GetFileName((LanguageEnum) settings.language));
            Events.ObserverManager.Notify(NotifyEvent.Language.Change);
        }

        public static Settings settings => new Settings(_settings);

        public static void defaultSettings() 
        {
            _settings.DefaultValues();
            Save(_settings);
        }

    }

}