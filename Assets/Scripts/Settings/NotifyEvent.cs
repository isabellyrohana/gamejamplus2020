using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotifyEvent
{
    
    private static int c;

    public static class Language
    {
        public static readonly int Change = ++c;
        public static readonly int ChangeInSettings = ++c;
    }

}
