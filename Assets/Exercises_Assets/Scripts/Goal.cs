using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private ReadmeChapter m_chapter;

    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        for(int i = 0; i < m_chapter.sections.Count(); i++)
        {
            for (int j = 0; j < m_chapter.sections[i].checkList.Count; j++)
            {
                if (m_chapter.sections[i].checkList[j].scene == scene.path)
                {
                    SelectReadme();
                    m_chapter.sections[i].checkList[j].check = true;
                }
            }
        }
    }

    void SelectReadme()
    {
        if (m_chapter != null)
        {
            var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GetAssetPath(m_chapter));

            Selection.objects = new UnityEngine.Object[] { readmeObject };

            //return (ReadmeChapter)readmeObject;
        }
        else
        {
            Debug.Log("Couldn't find a readme");
            //return null;
        }
    }
}
