using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Readme_Chapter", menuName = "ScriptableObjects/Readme_Chapter", order = 5)]
public class ReadmeChapter : ScriptableObject
{
    public Texture2D icon;
    public string title;
    public Section[] sections;
    public bool loadedLayout;

    [Serializable]
    public class Section
    {
        public string heading, text, linkText, url;
        public Texture2D icon;
        public string scene;
        public List<ExerciceCheckList> checkList;
    }

    [Serializable]
    public class ExerciceCheckList
    {
        [ReadOnly]
        public bool check;
        public string scene;
    }
}
