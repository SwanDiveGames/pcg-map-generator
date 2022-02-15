using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class NewMap : MonoBehaviour
{
    public void MakeNewMap()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
