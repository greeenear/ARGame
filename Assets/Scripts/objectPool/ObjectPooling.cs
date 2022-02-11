using System.Collections.Generic;
using UnityEngine;

public enum ObjectType {
    Point,
    Count
}

public struct ObjectInfo {
    public ObjectType type;
    public GameObject prefab;
}

public class ObjectPooling : MonoBehaviour {
    [SerializeField]
    private GameObject[] prefabs;
    private List<ObjectInfo> objects = new List<ObjectInfo>();

    public GameObject GetObject(ObjectType type) {
        if (objects == null) {
            Debug.LogError("GetObject: objects is null");
            return null;
        }

        foreach (var obj in objects) {
            if (obj.type == type && obj.prefab.gameObject.activeSelf == false) {
                return obj.prefab;
            }
        }
        return AddObject(type).prefab;
    }

    private ObjectInfo AddObject(ObjectType type) {
        var newObject = Instantiate(prefabs[(int)type], Vector3.zero, Quaternion.identity);
        newObject.SetActive(false);

        var newObjectInf = new ObjectInfo { type = type, prefab = newObject };
        if (objects == null) {
            Debug.LogError("GetObject: arrays is null");
            return newObjectInf;
        }

        objects.Add(newObjectInf);
        return newObjectInf;
    }
}
