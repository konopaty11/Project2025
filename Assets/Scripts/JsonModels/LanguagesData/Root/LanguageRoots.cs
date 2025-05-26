using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class LanguageRoots
{
    [JsonProperty("Languages")]
    public Dictionary<string, Dictionary<string, string>> Languages;
}
