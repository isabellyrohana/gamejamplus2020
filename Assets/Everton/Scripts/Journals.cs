using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Journals
{
    
    private static List<Journal> journals = new List<Journal>();

    public static void AddJournal(Journal journal) => journals.Add(journal);

    public static Journal GetJournal(int index) => journals[index];

    public static List<Journal> GetJournals() => new List<Journal>(journals);

}
