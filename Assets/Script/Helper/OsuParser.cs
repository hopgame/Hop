using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System.Xml.Linq;
using System.Xml.Serialization;
using osu.GameplayElements.HitObjects;

public static class OsuParser {
	public static List<int> cachedList;


	public static void ParseOsuFile(FileStream fileStream) {
		
	}



	public static void LoadCachedList(string savePath) {
		//cachedList = new List<int>;

		XmlSerializer serializer = new XmlSerializer (typeof(List<int>));
		using (var stream = File.Open(savePath, FileMode.Open)) {
			OsuParser.cachedList = (List<int>)serializer.Deserialize(stream);
			stream.Close ();
		}
	}

	public static void SaveCachedList(string savePath) {
		XmlSerializer serializer = new XmlSerializer (typeof(List<int>));
		using (var stream = File.Open(savePath, FileMode.Create)) {
			serializer.Serialize (stream, OsuParser.cachedList);
			stream.Close ();
		}
	}
}

[Serializable]
public class OsuFileInfo {
    #region Difficulty Settings
    //public float DifficultyApproachRate = 5;
    //public float DifficultyCircleSize = 5;
    //public float DifficultyHpDrainRate = 5;
    public float DifficultyOverall = -1;
    //public double DifficultySliderMultiplier = 1.4;
    //public double DifficultySliderTickRate = 1;
    #endregion

    #region Metadata
    public string Artist = string.Empty;

    public string Tags = string.Empty;
    public string Title = string.Empty;
	public int beatMapId = -1;
    #endregion
}

[Serializable]
public class OsuFile {
    public readonly OsuFileInfo Info;
	//TODO: more serialiable fields

	[field:NonSerialized]
	private bool isLoaded = false;
    public bool IsLoaded {
		get{ return isLoaded;}
		private set { isLoaded = value;}
    }

	[field:NonSerialized]
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


