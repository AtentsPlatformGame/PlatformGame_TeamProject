using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TestData : MonoBehaviour
{
    public string fileName = "positions.json";

    [System.Serializable]
    public class PositionData
    {
        public float x;
        public float y;
        public float z;
    }

    [System.Serializable]
    public class PositionList
    {
        public List<PositionData> positions;
    }

    // Method to save positions to JSON file
    public void SavePositions(List<Vector3> positions)
    {
        PositionList positionList = new PositionList();
        positionList.positions = new List<PositionData>();

        foreach (Vector3 pos in positions)
        {
            PositionData positionData = new PositionData();
            positionData.x = pos.x;
            positionData.y = pos.y;
            positionData.z = pos.z;
            positionList.positions.Add(positionData);
        }

        string json = JsonUtility.ToJson(positionList);
        File.WriteAllText(fileName, json);
    }

    // Method to load positions from JSON file
    public List<Vector3> LoadPositions()
    {
        List<Vector3> positions = new List<Vector3>();

        if (File.Exists(fileName))
        {
            string json = File.ReadAllText(fileName);
            PositionList positionList = JsonUtility.FromJson<PositionList>(json);

            foreach (PositionData positionData in positionList.positions)
            {
                Vector3 position = new Vector3(positionData.x, positionData.y, positionData.z);
                positions.Add(position);
            }
        }
        else
        {
            Debug.LogWarning("JSON file does not exist: " + fileName);
        }

        return positions;
    }
}
