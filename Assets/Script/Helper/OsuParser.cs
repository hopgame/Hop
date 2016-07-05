using System;
using System.Collections;
using System.Collections.Generic;
using osu.GameplayElements.HitObjects;

public static class OsuParser {

}

[Serializable]
public class OsuFileInfo {
    #region Difficulty Settings
    //public float DifficultyApproachRate = 5;
    //public float DifficultyCircleSize = 5;
    //public float DifficultyHpDrainRate = 5;
    public float DifficultyOverall = 5;
    //public double DifficultySliderMultiplier = 1.4;
    //public double DifficultySliderTickRate = 1;
    #endregion

    #region Metadata
    public string Artist = string.Empty;

    public string Tags = string.Empty;
    public string Title = string.Empty;
    #endregion
}

[Serializable]
public class OsuFile {
    public readonly OsuFileInfo Info;
    private bool isLoaded = false;
    public bool IsLoaded {
        get { return isLoaded; }
        private set { isLoaded = value; }
    }


    private List<HitObjectBase> hitObjects;


    public OsuFile(OsuFileInfo info) {
        this.Info = info;
    }

    public void LoadHitObject(List<HitObjectBase> hitObjects) {
        if(IsLoaded) return;

        this.hitObjects = hitObjects;
        this.IsLoaded = true;
    }

}


