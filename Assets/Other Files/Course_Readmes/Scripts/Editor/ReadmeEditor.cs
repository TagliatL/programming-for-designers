using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(ReadmeChapter))]
[InitializeOnLoad]
public class ReadmeEditor : Editor
{
    static string s_ShowedReadmeSessionStateName = "ReadmeEditor.showedReadme";
    
    //static string s_ReadmeSourceDirectory = "Assets/TutorialInfo";

    const float k_Space = 16f;

    static ReadmeEditor()
    {
        EditorApplication.delayCall += SelectReadmeAutomatically;
    }

    static void SelectReadmeAutomatically()
    {
        if (!SessionState.GetBool(s_ShowedReadmeSessionStateName, false))
        {
            var readme = SelectReadme();
            SessionState.SetBool(s_ShowedReadmeSessionStateName, true);

            if (readme && !readme.loadedLayout)
            {
                LoadLayout();
                readme.loadedLayout = true;
            }
        }
    }

    static void LoadLayout()
    {
        var assembly = typeof(EditorApplication).Assembly;
        var windowLayoutType = assembly.GetType("UnityEditor.WindowLayout", true);
        var method = windowLayoutType.GetMethod("LoadWindowLayout", BindingFlags.Public | BindingFlags.Static);
        method.Invoke(null, new object[] { Path.Combine(Application.dataPath, "TutorialInfo/Layout.wlt"), false });
    }

    static ReadmeChapter SelectReadme()
    {
        var ids = AssetDatabase.FindAssets("Readme t:Readme");
        if (ids.Length == 1)
        {
            var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));

            Selection.objects = new UnityEngine.Object[] { readmeObject };

            return (ReadmeChapter)readmeObject;
        }
        else
        {
            Debug.Log("Couldn't find a readme");
            return null;
        }
    }

    protected override void OnHeaderGUI()
    {
        var readme = (ReadmeChapter)target;
        Init();

        var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 4f - 20f, 256f);

        GUILayout.BeginHorizontal("In BigTitle");
        {
            if (readme.icon != null)
            {
                GUILayout.Space(k_Space);
                GUILayout.Label(readme.icon, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
            }
            GUILayout.Space(k_Space);
            GUILayout.BeginVertical();
            {

                GUILayout.FlexibleSpace();
                GUILayout.Label(readme.title, TitleStyle);
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
        }
        GUILayout.EndHorizontal();
    }

    public override void OnInspectorGUI()
    {
        var readme = (ReadmeChapter)target;
        Init();
        GUILayout.Space(k_Space);

        foreach (var section in readme.sections)
        {
            if (!string.IsNullOrEmpty(section.heading))
            {
                GUILayout.Label(section.heading, HeadingStyle);
            }
            GUILayout.Space(k_Space);

            if (!string.IsNullOrEmpty(section.text))
            {
                GUILayout.Label(section.text, BodyStyle);
            }

            if (section.links.Count>0)
            {
                for(int i = 0; i < section.links.Count; i++)
                {
                    if (LinkLabel(new GUIContent(section.links[i].linkText)))
                    {
                        Application.OpenURL(section.links[i].url);
                    }
                }              
            }

            GUILayout.Space(k_Space);

            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            int iconWidth = (int)(EditorGUIUtility.currentViewWidth - EditorGUIUtility.currentViewWidth * .1f);

            if (section.icon != null)
            {

                Texture2D newText = new Texture2D(iconWidth, iconWidth / 4);
                GUILayout.Label(section.icon, SceneIconStyle, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth/2));
            }   

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();


            if (!string.IsNullOrEmpty(section.scene))
            {
                string sceneName = SceneManager.GetSceneByPath(section.scene).name;
                if (GUILayout.Button("Open Scene", ButtonStyle, GUILayout.Width(iconWidth)))
                {
                    EditorSceneManager.OpenScene(section.scene);
                }

            }

            GUILayout.FlexibleSpace();

            GUILayout.EndHorizontal();


            GUILayout.Space(k_Space);

            int exerciseButtonWidth = (int)(EditorGUIUtility.currentViewWidth * .3f);

            for (int i = 0; i < section.checkList.Count; i++)
            {
                if (section.checkList[i] != null)
                {
                    string sceneNameExercise = SceneManager.GetSceneByPath(section.checkList[i].scene).name;
                    GUILayout.BeginHorizontal();

                    GUILayout.FlexibleSpace();
                    GUI.backgroundColor = Color.white;
                    if (GUILayout.Button("Exercise " + "0"+(i+1), ButtonStyle, GUILayout.Width(exerciseButtonWidth)))
                    {
                        EditorSceneManager.OpenScene(section.checkList[i].scene);
                    }
                    string checkText = section.checkList[i].check ? "Done!" : "To do";
                    if (section.checkList[i].check)
                    {
                        GUI.backgroundColor = Color.green;
                    }
                    else
                    {
                        GUI.backgroundColor = Color.red;
                    }
                    
                    GUILayout.Toggle(section.checkList[i].check, checkText);
                    GUI.backgroundColor = Color.white;

                    GUILayout.FlexibleSpace();

                    GUILayout.EndHorizontal();

                }
            }
           
        }
    }

    bool m_Initialized;

    GUIStyle LinkStyle
    {
        get { return m_LinkStyle; }
    }

    [SerializeField]
    GUIStyle m_LinkStyle;

    GUIStyle TitleStyle
    {
        get { return m_TitleStyle; }
    }

    [SerializeField]
    GUIStyle m_TitleStyle;

    GUIStyle HeadingStyle
    {
        get { return m_HeadingStyle; }
    }

    [SerializeField]
    GUIStyle m_HeadingStyle;

    GUIStyle BodyStyle
    {
        get { return m_BodyStyle; }
    }

    [SerializeField]
    GUIStyle m_BodyStyle;

    GUIStyle ButtonStyle
    {
        get { return m_ButtonStyle; }
    }

    [SerializeField]
    GUIStyle m_ButtonStyle;

    GUIStyle SceneIconStyle
    {
        get { return m_SceneIconStyle; }
    }

    [SerializeField]
    GUIStyle m_SceneIconStyle;

    void Init()
    {
        if (m_Initialized)
            return;
        m_BodyStyle = new GUIStyle(EditorStyles.label);
        m_BodyStyle.wordWrap = true;
        m_BodyStyle.fontSize = 14;
        m_BodyStyle.richText = true;

        m_TitleStyle = new GUIStyle(m_BodyStyle);
        m_TitleStyle.fontSize = 22;

        m_HeadingStyle = new GUIStyle(m_BodyStyle);
        m_HeadingStyle.fontStyle = FontStyle.Bold;
        m_HeadingStyle.fontSize = 16;

        m_LinkStyle = new GUIStyle(m_BodyStyle);
        m_LinkStyle.wordWrap = false;

        // Match selection color which works nicely for both light and dark skins
        m_LinkStyle.normal.textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f);
        m_LinkStyle.stretchWidth = false;

        m_ButtonStyle = new GUIStyle(EditorStyles.miniButtonMid);
        m_ButtonStyle.fontStyle = FontStyle.Bold;


        m_SceneIconStyle = new GUIStyle(m_BodyStyle);
        m_SceneIconStyle.alignment = TextAnchor.MiddleCenter;


        m_Initialized = true;
    }

    bool LinkLabel(GUIContent label, params GUILayoutOption[] options)
    {
        var position = GUILayoutUtility.GetRect(label, LinkStyle, options);

        Handles.BeginGUI();
        Handles.color = LinkStyle.normal.textColor;
        Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
        Handles.color = Color.white;
        Handles.EndGUI();

        EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);

        return GUI.Button(position, label, LinkStyle);
    }
}
