using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

public static class UnityTagConstants
{
    // Default Tags
    public const string UNTAGGED = "Untagged";
    public const string RESPAWN = "Respawn";
    public const string FINISH = "Finish";
    public const string EDITORONLY = "EditorOnly";
    public const string MAINCAMERA = "MainCamera";
    public const string PLAYER = "Player";
    public const string GAMECONTROLLER = "GameController";

    //Project specific tags
    public const string FRIENDLY = "Friendly";


    public static string[] getAllTagConstants()
    {
        var tagConstants = new List<string>();
        var defaultTags = new string[] {
            UNTAGGED,
            RESPAWN,
            FINISH,
            EDITORONLY,
            MAINCAMERA,
            PLAYER,
            GAMECONTROLLER,
        };

        var projectSpecificTags = new string[] {
            FRIENDLY,
        };

        tagConstants.AddRange(defaultTags);
        tagConstants.AddRange(projectSpecificTags);
        return tagConstants.ToArray();
    }
}

// ensure class initializer is called whenever scripts recompile
[InitializeOnLoadAttribute]
public static class UnityTagConstantsValidator
{
    // register an event handler when the class is initialized
    static UnityTagConstantsValidator()
    {
        EditorApplication.playModeStateChanged += ValidateTagConstants;
    }

    private static void ValidateTagConstants(PlayModeStateChange state)
    {
        if(state == PlayModeStateChange.EnteredPlayMode)
        {
            var tagConstants = UnityTagConstants.getAllTagConstants();
            var unityTags = GetUnityTags();

            var tagConstantsSet = new HashSet<string>(tagConstants);
            var unityTagsSet = new HashSet<string>(unityTags);

            if(!tagConstantsSet.Equals(unityTagsSet))
            {
                if(tagConstantsSet.Count > unityTagsSet.Count)
                {
                    tagConstantsSet.RemoveWhere(tag => unityTagsSet.Contains(tag));
                    foreach (string tag in tagConstantsSet)
                    {
                        Debug.LogError("Tag overflow in TagConstants: " + tag);
                    }
                } else
                {
                    unityTagsSet.RemoveWhere(tag => tagConstantsSet.Contains(tag));
                    foreach (string tag in unityTagsSet)
                    {
                        Debug.LogError("Tag missing in TagConstants: " + tag);
                    }
                }
            }
        }
    }

    private static string[] GetUnityTags()
    {
        return UnityEditorInternal.InternalEditorUtility.tags;
    }

}