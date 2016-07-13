using UnityEngine; //using some logging feature

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using osu.GameplayElements.HitObjects;

public class OsuParser {
    public const string HeaderPattern = @"^\[([a-zA-Z0-9]+)\]$";
    public const string ValuePattern = @"^([a-zA-Z0-9]+)[ ]*:[ ]*(.+)$";

    public static List<int> CachedList;


    public static bool ParseOsuFile(string filePath) {
        Regex headeRegex = new Regex(HeaderPattern);
        //var fileStream = File.Open(filePath, FileMode.Open);
        using (var fileStream = new StreamReader(filePath)) {

            var fileInfo = new OsuFileInfo();


            string keyWord = string.Empty;

            while (!fileStream.EndOfStream) {
                string line = fileStream.ReadLine();
                if (line == null) return false;
                var match = headeRegex.Match(line);
                if (match.Success) {
                    keyWord = match.Groups[0].Value;
                    continue;
                }

                switch (keyWord) {
                        case "Editor": //discard
                            break;
                        case "General":
                        case "Metadata":
                        case "Difficulty":
                            OsuFileInfo.ParseOsuFileInfo(fileInfo,line,keyWord);
                            break;
                        case "Events":
                            break;
                        case "TimingPoints":
                            break;
                        case "HitObjects":
                            break;
                        default: //when the file start
                        break;

                    }
            }



        }

        return true;
    }



    public static void LoadCachedList(string savePath) {
        //cachedList = new List<int>;

        XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
        using (var stream = File.Open(savePath, FileMode.Open)) {
            OsuParser.CachedList = (List<int>)serializer.Deserialize(stream);
            stream.Close();
        }
    }

    public static void SaveCachedList(string savePath) {
        XmlSerializer serializer = new XmlSerializer(typeof(List<int>));
        using (var stream = File.Open(savePath, FileMode.Create)) {
            serializer.Serialize(stream, OsuParser.CachedList);
            stream.Close();
        }
    }
}

[Serializable]
public class OsuFileInfo {
    #region General

    public string AudioFilename = string.Empty;
    public int AudioLeadIn = -1;
    public int PreviewTime = -1;
    public string SampleSet = string.Empty;

    #endregion


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
    public int BeatMapId = -1;
    #endregion

    
    /// <summary>
    /// methods for parsing Osu file information, this assume a single line string to pass to this parser
    /// </summary>
    /// <param name="info">OsuFileInfo to receive parsed data</param>
    /// <param name="toParse">the line of string to parse</param>
    /// <param name="type">type of info to parse</param>
    public static void ParseOsuFileInfo(OsuFileInfo info, string toParse, string type) {

        var match = Regex.Match(toParse, OsuParser.ValuePattern);
        var attriName   = match.Groups[0].Value;
        var attriValue  = match.Groups[1].Value;

        switch (type) {
            case "General":
                switch (attriName) {
                    case "AudioFilename":
                        info.AudioFilename = attriValue;
                        break;
                    case "AudioLeadIn":
                        info.AudioLeadIn = int.Parse(attriValue);
                        break;
                    case "PreviewTime":
                        info.PreviewTime = int.Parse(attriValue);
                        break;
                    case "SampleSet":
                        info.SampleSet = attriValue;
                        break;
                    default:
                        break;
                }

                break;
            case "Difficulty":
                switch (attriName) {
                    case "DifficultyOverall":
                        info.DifficultyOverall = int.Parse(attriValue);
                        break;
                    default:
                        break;
                }

                break;
            case "Metadata": 
                switch (attriName) {
                    case "Artist":
                        info.Artist = attriValue;
                        break;
                    case "Tags":
                        info.Tags = attriValue;
                        break;
                    case "Title":
                        info.Title = attriValue;
                        break;
                    default:
                        break;
                }

                break;
            default:
                Debug.LogError("this type should never be parse" + type);
                break;
        }

    }

}

[Serializable]
public class OsuFile {
    public readonly OsuFileInfo Info;
    //TODO: more serialiable fields

    [field: NonSerialized]
    private bool _isLoaded = false;
    public bool IsLoaded {
        get { return _isLoaded; }
        private set { _isLoaded = value; }
    }

    [field: NonSerialized]
    private List<MHitObject> _hitObjects;

    public OsuFile() {
        this.Info = new OsuFileInfo();
        //this._hitObjects = new List<MHitObject>();
    }

    public OsuFile(OsuFileInfo info) {
        this.Info = info;
        //this._hitObjects = new List<MHitObject>();

    }

    public void LoadHitObject(List<MHitObject> hitObjects) {
        if (IsLoaded) return;

        this._hitObjects = hitObjects;
        this.IsLoaded = true;
    }

}


