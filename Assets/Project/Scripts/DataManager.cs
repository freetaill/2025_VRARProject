using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (null == instance)
            {
                instance = FindAnyObjectByType<DataManager>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    instance = singleton.AddComponent<DataManager>();
                    singleton.name = typeof(DataManager).ToString() + " (Singleton)";

                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }
    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Init()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public int spownpointNum = 0;
    /*
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                ToGameJson();
            }
            return _gameData;
        }
    }
    public PlayData _playData;
    public PlayData playData
    {
        get
        {
            if (_playData == null)
            {
                LoadPlayData(gameData.cur_save_file_ID);
                ToPlayJson(gameData.cur_save_file_ID);
            }
            return _playData;
        }
    }
    StreamWriter SW = null;

    private string GetGameDataFilePath()
    {
        //return Application.persistentDataPath + $"/GameData.json";
        return Application.dataPath + "/Scripts/JSON/GameData.json";
    }
    private string GetPlayDataFilePath(string playDataId)
    {
        //return Application.persistentDataPath + $"/{playDataId}.json";
        return Application.dataPath + "/Scripts/JSON/" + playDataId + ".json";
    }
    public void LoadGameData()
    {
        string filePath = GetGameDataFilePath();

        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            print(filePath);
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            // 새 파일이 없을 경우, 새로운 GameData 객체 생성
            _gameData = new GameData();  // 기본 GameData 객체 생성
            ToGameJson(); // 생성된 GameData를 JSON 파일로 저장
        }
    }

    [ContextMenu("To Game Json")]
    public void ToGameJson()
    {
        File.WriteAllText(GetGameDataFilePath(), JsonUtility.ToJson(gameData, true));
    }

    public void LoadPlayData(string playDataID)
    {
        string filePath = GetPlayDataFilePath(playDataID);
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            Debug.Log(filePath);
            string FromJsonData = File.ReadAllText(filePath);
            _playData = JsonUtility.FromJson<PlayData>(FromJsonData);
        }
        else
        {
            // 새 파일이 없을 경우, 새로운 PlayData 객체 생성
            _playData = new PlayData();  // 기본 PlayData 객체 생성
            ToPlayJson(playDataID); // 생성된 PlayData를 JSON 파일로 저장
            gameData.save_files_IDs.Add(playDataID);
        }
    }

    public void ToPlayJson(string playDataID)
    {
        File.WriteAllText(GetPlayDataFilePath(playDataID), JsonUtility.ToJson(playData, true));
    }
    public void DeletePlayData(string playDataID)
    {
        string filePath = GetPlayDataFilePath(playDataID); // JSON 파일 경로
        string metaFilePath = filePath + ".meta"; // 메타 파일 경로

        // JSON 파일 삭제
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"Deleted JSON file: {filePath}");
        }

        // 메타 파일 삭제
        if (File.Exists(metaFilePath))
        {
            File.Delete(metaFilePath);
            Debug.Log($"Deleted meta file: {metaFilePath}");
        }

        // gameData에서 ID 제거
        if (gameData.save_files_IDs.Contains(playDataID))
        {
            gameData.save_files_IDs.Remove(playDataID);
            Debug.Log($"Removed {playDataID} from save_files_IDs.");
        }
    }
    public IEnumerator SaveGameData()
    {
        while (true)
        {
            File.WriteAllText(GetGameDataFilePath(), JsonUtility.ToJson(gameData, true));

            try
            {
                SW = new StreamWriter(GetGameDataFilePath());
                //SW = new System.IO.StreamWriter(Application.dataPath + "/Scripts/JSON/GameData.json");
            }
            catch (Exception exp)
            {
                Debug.Log(exp);
            }
            finally
            {
                if (SW != null)
                {
                    SW.Close();
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator SavePlayData(string playDataID)
    {
        while (true)
        {
            File.WriteAllText(GetPlayDataFilePath(playDataID), JsonUtility.ToJson(playData, true));

            try
            {
                SW = new StreamWriter(GetPlayDataFilePath(playDataID));
                //SW = new System.IO.StreamWriter(Application.dataPath + "/Scripts/JSON/PlayData.json");
            }
            catch (Exception exp)
            {
                Debug.Log(exp);
            }
            finally
            {
                if (SW != null)
                {
                    SW.Close();
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    void OnApplicationQuit()
    {
        ToGameJson();
        ToPlayJson(gameData.cur_save_file_ID);//종료할때 있어야함
    }
    */
}
