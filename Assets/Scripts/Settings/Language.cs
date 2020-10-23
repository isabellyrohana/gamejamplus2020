using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public static class Language
    {

        public enum LanguageEnum
        {
            ENGLISH,
            ESPANISH,
            PORTUGUESE
        }

        public static int DefaultLanguage => (int) LanguageEnum.ENGLISH;

        public static int Length => Enum.GetNames(typeof(LanguageEnum)).Length;

        public static string GetNativeLanguage(LanguageEnum languageEnum)
        {
            switch (languageEnum)
            {
                case LanguageEnum.ESPANISH: return "Espanol";
                case LanguageEnum.PORTUGUESE: return "Português";
                default: return "English";
            }
        }

        public static string GetLanguage(LanguageEnum languageEnum)
        {
            switch (languageEnum)
            {
                case LanguageEnum.ESPANISH: return "Espanish";
                case LanguageEnum.PORTUGUESE: return "Portuguese";
                default: return "English";
            }
        }

        public static string GetShortLanguage(LanguageEnum languageEnum)
        {
            switch(languageEnum)
            {
                case LanguageEnum.ESPANISH: return "es";
                case LanguageEnum.PORTUGUESE: return "pt-br";
                default: return "en";
            }
        }

    }
}
