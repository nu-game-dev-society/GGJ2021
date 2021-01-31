using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticAlphabetWords
{
    public static List<string> words = new List<string>() { "Alfa", "Bravo", "Charlie", "Delta", "Echo", "Foxtrot", "Golf", "Hotel", "India", "Juliet", "Kilo", "Lima", "Mike", "November", "Oscar", "Papa", "Quebec", "Romeo", "Sierra", "Tango", "Uniform", "Victor", "Whiskey", "Xray", "Yankee", "Zulu" };

    public static string GetRandomWord()
    {
        return words[Random.Range(0, words.Count)];
    }
}
