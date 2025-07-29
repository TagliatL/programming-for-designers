using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExerciseComplete : MonoBehaviour
{
    [SerializeField] private GameObject m_completeMessage;

    [SerializeField] private ReadmeChapter m_chapter;

    [SerializeField]
    Vector3[] positions = new Vector3[5];

    private Scene scene;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void Complete()
    {
        Start();
        StartCoroutine(CompleteRoutine());
    }

    IEnumerator CompleteRoutine()
    {
        m_completeMessage.SetActive(true);
        for (int i = 0; i < m_chapter.sections.Count(); i++)
        {
            for (int j = 0; j < m_chapter.sections[i].checkList.Count; j++)
            {
                if (AssetDatabase.GetAssetPath(m_chapter.sections[i].checkList[j].scene) == scene.path)
                {
                    yield return new WaitForSeconds(1f);
                    SelectReadme();
                    yield return new WaitForSeconds(1f);
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
