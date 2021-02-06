using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUIManager : MonoBehaviour
{
    public GameObject[] Tasks;

    public void NextTask()
    {
        for (int i = 0; i < Tasks.Length; i++)
        {
            if (Tasks[i].activeInHierarchy)
            {
                Tasks[i].SetActive(false);

                if (i == Tasks.Length - 1)
                    i = 0;
                else
                    i++;
                Tasks[i].SetActive(true);
            }
        }
    }

    public void PreviousTask()
    {
        for (int i = 0; i < Tasks.Length; i++)
        {
            if (Tasks[i].activeInHierarchy)
            {
                Tasks[i].SetActive(false);
                if (i == 0)
                    i = Tasks.Length - 1;
                else
                    i--;
                Tasks[i].SetActive(true);
            }
        }
    }
}
