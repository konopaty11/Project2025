using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Settings
{
    public int resolutionOld;
    public bool isWindowedOld;
    public bool isActiveSoundOld;
    public int languageOld;

    public Settings(int resolutionOld, bool isWindowedOld, bool isActiveSoundOld, int languageOld)
    {
        this.resolutionOld = resolutionOld;
        this.isWindowedOld = isWindowedOld;
        this.isActiveSoundOld = isActiveSoundOld;
        this.languageOld = languageOld;
    }
}
