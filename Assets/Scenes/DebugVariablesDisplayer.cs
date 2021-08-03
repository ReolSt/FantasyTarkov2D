using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVariablesDisplayer : MonoBehaviour
{
    private Dictionary<string, string> variables = new Dictionary<string, string>();
    // Start is called before the first frame update
    void Start()
    {
        // BOB Do Something!
    }

    public void AddVariable(string name, string value="")
    {
        this.variables.Add(name, value);
    }

    public void UpdateVariable(string name, string value)
    {
        this.variables[name] = value;
    }

    public void RemoveVariable(string name)
    {
        this.variables.Remove(name);
    }

    private void OnGUI()
    {
        string stringToDisplay = "";

        foreach (var pair in this.variables)
        {
            stringToDisplay += pair.Key + ": " + pair.Value + "\n";
        }

        GUILayout.Label(stringToDisplay);
    }
}
