using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PromptData
{
    [JsonProperty("Prompts")]
    public Dictionary<string, Dictionary<string, string>> Prompts;
}