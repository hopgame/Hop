using UnityEngine; //using some logging feature

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
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
                if (line == null) throw new Exception("string from fileStream is null");
                if (line.StartsWith(@"\\")) continue; //comment line, ignore
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
                            OsuFileInfo.ParseOsuFileEvents(fileInfo,line);
                            break;
                        case "TimingPoints":
                            OsuFileInfo.ParseOsuTimingPoints(fileInfo,line);
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

    #region Events

    public string BackGround = string.Empty;
    public string Video = string.Empty;
    public List<KeyValuePair<int, int>> BreakTimes = null;

    #endregion

    #region TimingPoints

    public List<OsuTimingPoint> TimingPoints;

    #endregion

    /// <summary>
    /// parse OsuFile [TimingPoints] into OsuFileInfo
    /// </summary>
    /// <param name="info">OsuFileInfo to receive parsed data</param>
    /// <param name="toParse">the line of string to parse</param>
    public static void ParseOsuTimingPoints(OsuFileInfo info, string toParse) {
        if(info.TimingPoints == null) info.TimingPoints = new List<OsuTimingPoint>();


        string[] keys = toParse.Split(',');

        int offset = int.Parse(keys[0]);
        float mSecPerBeat = float.Parse(keys[1]);
        int meter = int.Parse(keys[2]);
        int sampleType = int.Parse(keys[3]);
        int sampleSet = int.Parse(keys[4]);
        int volume = int.Parse(keys[5]); //0-100
        bool inherited = keys[6] == "1";
        bool kiaiMode = keys[7] == "1";


        if (inherited) {
            //BEWARE: velocity is ignore
            if (mSecPerBeat <= 0) {
                mSecPerBeat = info.TimingPoints.Last().MSecPerBeat;
            }
        }
        var beatPerMinute = Mathf.Round(60000/mSecPerBeat);

        info.TimingPoints.Add(new OsuTimingPoint(offset,mSecPerBeat,beatPerMinute,meter,sampleType,sampleSet,volume,inherited,kiaiMode));


    }

    /// <summary>
    /// parse OsuFile [events] into OsuFileInfo
    /// </summary>
    /// <param name="info">OsuFileInfo to receive parsed data</param>
    /// <param name="toParse">the line of string to parse</param>
    public static void ParseOsuFileEvents(OsuFileInfo info, string toParse) {
        string[] keys = toParse.Split(',');
        switch (keys[0]) {
            case "0":
                if (keys[1] == "0") {
                    if (keys[2].StartsWith("\"") && keys[2].EndsWith("\"")) {
                        info.BackGround = keys[2].Substring(1, keys[2].Length - 2);
                    }
                    else {
                        info.BackGround = keys[2];
                    }
                    Debug.Log("BG Name:" + info.BackGround);

                }
                break;
            case "2":
                if(!Regex.IsMatch(keys[2], @"^[0-9]+$")  || !Regex.IsMatch(keys[1], @"^[0-9]+$")) goto default;
                if (info.BreakTimes == null) {
                    info.BreakTimes = new List<KeyValuePair<int, int>>();
                }
                info.BreakTimes.Add(new KeyValuePair<int, int>(int.Parse(keys[1]), int.Parse(keys[2])));
                break;
            case "Video":
                break;
            default:
                Debug.Log("Dropped Events type:" + keys[0]);
                break;
        }
    }

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
        TimingPoints = new Queue<OsuTimingPoint>();
        //this._hitObjects = new List<MHitObject>();
    }

    public OsuFile(OsuFileInfo info) {
        this.Info = info;
        TimingPoints = new Queue<OsuTimingPoint>();
        
    }

    public void LoadHitObject(List<MHitObject> hitObjects) {
        if (IsLoaded) return;

        this._hitObjects = hitObjects;
        this.IsLoaded = true;
    }

}

[Serializable]
public struct OsuTimingPoint {
    public OsuTimingPoint(int offset, float mSecPerBeat, float beatPerMinute, int meter, int sampleType, int sampleSet, int volume, bool inherited, bool kiaiMode) {
        Offset = offset;
        MSecPerBeat = mSecPerBeat;
        BeatPerMinute = beatPerMinute;
        Meter = meter;
        SampleType = sampleType;
        SampleSet = sampleSet;
        Volume = volume;
        Inherited = inherited;
        KiaiMode = kiaiMode;
    }

    public readonly int Offset;
    public readonly float MSecPerBeat;
    public readonly float BeatPerMinute;
    public readonly int Meter;
    public readonly int SampleType;
    public readonly int SampleSet;
    public readonly int Volume; //0-100
    public readonly bool Inherited;
    public readonly bool KiaiMode;
}