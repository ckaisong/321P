using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
    public struct PosRotScale
    {
        public float[] m_position;
        public float[] m_rotation;
        public float[] m_scale;
        public PosRotScale(float[] position, 
            float[] rotation, 
            float[] scale)
        {
            m_position = position;
            m_rotation = rotation;
            m_scale = scale;
        }
        public PosRotScale(float _posX, float _posY, float _posZ,
            float _rotX, float _rotY, float _rotZ,
            float _scaleX, float _scaleY, float _scaleZ)
        {
            m_position = new float[3] { _posX, _posY,_posZ};
            m_rotation = new float[3] { _rotX, _rotY, _rotZ};
            m_scale = new float[3] { _scaleX, _scaleY, _scaleZ};
        }
    }
    public struct SaveData
    {
        /* Count of each unique placeable object
         * e.g. 3 object user can use for customization, array will consist [3,1,5]
         */
        public int[] m_CountOfEachObject;
        /* A list of position, rotation and scale value of each object counted in m_CountOfEachObject
         * e.g. (based on above e.g.) the list size will be 9
         */
        //public List<PosRotScale> m_ListOfPosRotScale;
        public List<float[]> m_ListOfPosRotScale;
    }
    [System.Serializable]
    public struct PosScaRot
    {
        /*public float _positionX, _positionY, _positionZ;
        public float _scaleX, _scaleY, _scaleZ;
        public float _rotationX, _rotationY, _rotationZ;*/
        public float[] _position;
        public float[] _scale;
        public float[] _rotation;
        public PosScaRot(Vector3 _pos, Vector3 _sca, Quaternion _rot)
        {
            _position = new float[] { _pos.x, _pos.y, _pos.z };
            _scale = new float[] { _sca.x, _sca.y, _sca.z };
            _rotation = new float[] { _rot.x, _rot.y, _rot.z, _rot.w };
            /*_positionX= _pos.x;
            _positionY= _pos.y;
            _positionZ= _pos.z;

            _scaleX= _sca.x;
            _scaleY= _sca.y;
            _scaleZ= _sca.z;

            _rotationX= _rot.x;
            _rotationY= _rot.y;
            _rotationZ= _rot.z;*/
        }
    }

    public struct GOData
    {
        //public string m_Name;
        public List<PosScaRot> m_PSRWine;
        public List<PosScaRot> m_PSRFlower;
        public GOData(/*string _name,*/ List<PosScaRot> _psrWine, List<PosScaRot> _psrFlower)
        {
            //m_Name = _name;
            m_PSRWine = _psrWine;
            m_PSRFlower = _psrFlower;
        }
    }
    #region Saving in binary
    public static void SaveBin(string _fileName, SaveData _data)
    {
        Debug.Log("Save starting|||");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + _fileName + ".gar";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        formatter.Serialize(stream, _data); 
        stream.Close();
        Debug.Log($"{_fileName} saved");
    }
    public static SaveData/*void */LoadBin(string _fileName/*, SaveData _data*/)
    {
        string path = Application.persistentDataPath + "/" + _fileName + ".gar";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = (SaveData)formatter.Deserialize(stream);

            stream.Close();
            //_data = data;
            return data;
        }
        else
        {
            Debug.Log($"File path [{path}] not found");
            return new SaveData();
        }
    }
    #endregion
    #region Save in json
    public static void SaveJSon(string _fileName, string _data)
    {
        string pathName = Application.persistentDataPath + "/" + _fileName + ".json";
        File.WriteAllText(pathName, _data);
    }
    public static string LoadJSon(string _fileName)
    {
        string pathName = Application.persistentDataPath + "/" + _fileName + ".json";
        if(File.Exists(pathName))
        {
            string loadedData = File.ReadAllText(pathName);
            Debug.Log($"Loaded {loadedData}");
            /*_data = JsonUtility.FromJson<GOData>(loadedData);
            Debug.Log($"Flower: {_data.m_PSRFlower.Count} | Wine: {_data.m_PSRWine.Count}");
            return true;*/
            return loadedData;
        }
        else
        {
            Debug.Log("File load error");
            return "Fail";
        }
    }
    #endregion
}
