using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Wiki Index", menuName = "Their Game/Wiki Index SO")]
public class WikiIndexSO : ScriptableObject
{    
    [TitleGroup("Pages")]
    public List<WikiPageSO> WikiPages;

    //TODO: needs ot be more sophisticade.. I should be able to see pages, search for words, and immediately get where they appear in other places
    [Button]
    public void RunAnalysis(int words)
    {
        foreach (var word in FindMostCommonWords(WikiPages.Select(p => p.Content).ToList(), words))
        {
            Debug.Log($"{word.Key}: {word.Value}");
        }
    }

    Dictionary<string, int> FindMostCommonWords(List<string> sentences, int topN)
    {
        Dictionary<string, int> wordCount = new Dictionary<string, int>();
        HashSet<string> stopwords = new HashSet<string> { "is", "a", "the", "and", "of", "to", "in", "this", "just", "here" };

        foreach (var sentence in sentences)
        {
            string[] words = Regex.Matches(sentence.ToLower(), @"\b\w+\b")  // Extracts words only
                                  .Select(match => match.Value)
                                  .Where(word => !stopwords.Contains(word)) // Remove stopwords
                                  .ToArray();

            foreach (var word in words)
            {
                if (wordCount.ContainsKey(word))
                    wordCount[word]++;
                else
                    wordCount[word] = 1;
            }
        }

        return wordCount.OrderByDescending(kv => kv.Value)
                        .Take(topN)
                        .ToDictionary(kv => kv.Key, kv => kv.Value);
    }   
}