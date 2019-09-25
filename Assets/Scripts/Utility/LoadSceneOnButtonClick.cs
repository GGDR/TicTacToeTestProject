using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TicTacToe
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class LoadSceneOnButtonClick : MonoBehaviour
    {
        [SerializeField] private string sceneName = "";

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SceneLoadOnClickListener);
        }

        private void SceneLoadOnClickListener()
        {
            SceneManager.LoadScene(sceneName);
        }
    } 
}
