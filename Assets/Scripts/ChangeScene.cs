using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public int scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI ()
	{
        if (GUILayout.Button("Debug Scene Shader"))
        {
            SceneManager.LoadScene("DebugScene Shader");
		}
        else if(GUILayout.Button("Debug Scene AI"))
        {
            SceneManager.LoadScene("DebugScene AI");
        }
        else if (GUILayout.Button("Debug Scene Hole Game"))
        {
            SceneManager.LoadScene("DebugSceneHoleGame");
        }
    }
}
