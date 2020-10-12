using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journals
{
    
    private static List<Journal> journals = new List<Journal>();

    public static bool AddJournal(Journal journal)
    {
        bool contains = false;
        foreach(Journal j in journals)
        {
            if (j.text.Equals(journal.text))
            {
                contains = true;
                break;
            }
        }
        if (!contains) 
        {
            journals.Add(journal);
            return true;
        }
        return false;
    }

    public static Journal GetJournal(int index) => journals[index];

    public static List<Journal> GetJournals() => new List<Journal>(journals);

}
