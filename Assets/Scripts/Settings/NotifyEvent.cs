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

    public static class Interactions
    {

        public static class Key
        {
            public static readonly int Show = ++c;
            public static readonly int Hide = ++c;
        }

        public static class Arrows
        {
            public static readonly int Show = ++c;
            public static readonly int Hide = ++c;
        }

        
    }

}
