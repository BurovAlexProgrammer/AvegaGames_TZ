using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Extensions
{
    public static GameObject CreateAudioEvent(this GameObject gameObject, AudioEvent audioEvent,
        bool isIndependent = true)
    {
        var newAudioEvent = new GameObject("AudioEvent");
        newAudioEvent.transform.SetParent(isIndependent ? gameObject.transform.parent : gameObject.transform);
        var autoDestroy = newAudioEvent.AddComponent<DestroyAfterPlay>();
        autoDestroy.audioEvent = audioEvent;
        return newAudioEvent;
    }

    public static T GetComponentOrNull<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.IsComponentExist<T>() ? gameObject.GetComponent<T>() : null;
    }

    public static void CheckExisting<T>(this GameObject gameObject) where T : Component
    {
        if (!gameObject.IsComponentExist<T>())
            throw new Exception($"{typeof(T).Name} is null.");
    }

    public static void Replace(this GameObject T, GameObject prefab)
    {
        var newGameObject = (GameObject) PrefabUtility.InstantiatePrefab(prefab);
        newGameObject.transform.parent = T.transform.parent;
        newGameObject.transform.position = T.transform.position;
        newGameObject.transform.rotation = T.transform.rotation;
        newGameObject.transform.localScale = T.transform.localScale;
        newGameObject.transform.name = T.transform.name;
        newGameObject.transform.tag = T.transform.tag;
        newGameObject.layer = T.layer;
        Object.Destroy(T);
    }

    public static bool IsComponentExist<T>(this GameObject gameObject) where T : Object
    {
        return gameObject.TryGetComponent(out T component);
    }

    public static bool IsComponentExist<T>(this Collider gameObject) where T : Object
    {
        return gameObject.TryGetComponent(out T component);
    }

    public static bool CompareTagWithParents(this Transform T, string tag)
    {
        var parent = T.parent;
        if (parent != null)
            return parent.CompareTag(tag) || CompareTagWithParents(parent, tag);
        return false;
    }

    public static Transform FindParentWithTag(this Transform T, string tag)
    {
        var parent = T.parent;
        if (parent != null)
            return parent.CompareTag(tag) ? parent : FindParentWithTag(parent, tag);
        return null;
    }
}

[Serializable]
public struct RangedFloat
{
    public float minValue;
    public float maxValue;
}

public class MinMaxRangeAttribute : Attribute
{
    public MinMaxRangeAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public float Min { get; private set; }
    public float Max { get; private set; }
}