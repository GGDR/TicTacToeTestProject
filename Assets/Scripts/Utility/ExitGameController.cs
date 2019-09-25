using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitGameController : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ExitGameOnClickListener);
    }

    private void ExitGameOnClickListener()
    {
        Application.Quit();
    }
}
