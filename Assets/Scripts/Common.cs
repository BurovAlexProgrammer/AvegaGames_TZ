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

        if (isIndependent)
        {
            if (gameObject.transform.parent != null) //if parent is root
                newAudioEvent.transform.position = gameObject.transform.position;
            else
                newAudioEvent.transform.SetParent(gameObject.transform.parent);
        }
        else
        {
            newAudioEvent.transform.SetParent(gameObject.transform);
        }

        var autoDestroy = newAudioEvent.AddComponent<DestroyAfterPlay>();
        autoDestroy.audioEvent = audioEvent;
        return newAudioEvent;
    }

    public static T GetComponentOrNull<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.IsComponentExist<T>() ? gameObject.GetComponent<T>() : null;
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