using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstTaskManager : MonoBehaviour
{

    public InputField input;
    public Text Output;

    private int[] givenArray;

    public void Submit()
    {
        Output.color = Color.yellow;
        try
        {
            givenArray = Array.ConvertAll(input.text.Split(','), s => int.Parse(s));
            Output.text = "Sub-arrays are listed below:";
            for (int i = 0; i < givenArray.Length - 1; i++) //Base loop.
            {
                string subsequentNumbers = "";
                if (givenArray[i + 1] == givenArray[i] + 1 && !Output.text.Contains(givenArray[i].ToString())) //Check base value compared to next one && check if value is found in pre-calculated arrays 
                {
                    subsequentNumbers = givenArray[i].ToString(); //Add base value.
                    for (int j = i + 1; j < givenArray.Length; j++) //Subsequence loop.
                    {
                        if (givenArray[j] == givenArray[j - 1] + 1 && !Output.text.Contains(givenArray[j].ToString()))   //Check subsequence value && check if value is found in pre-calculated arrays 
                            subsequentNumbers += "," + givenArray[j]; //Add subsequence value
                        else //Subsequence broken, print collected values and move base to last index
                        {
                            Output.text += "\n" + subsequentNumbers;
                            i = j - 1;
                            break;
                        }

                        if (j == givenArray.Length - 1) //Reached last element, print collected values.
                        {
                            i = j - 1;
                            Output.text += "\n" + subsequentNumbers;
                        }
                    }
                }
            }
        }
        catch
        {
            Output.color = Color.red;
            Output.text = "Error, input must be comma separated and only contain numbers";
        }
    }
}
