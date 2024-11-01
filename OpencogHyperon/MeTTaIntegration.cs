using UnityEngine;
using System.Diagnostics;
using System.IO;

public class MeTTaIntegration : MonoBehaviour
{
    // Path to the MeTTa interpreter
    private string mettaInterpreterPath = "/home/basketo/.local/bin/metta";
    
    // Path to the .metta script in "Assets/Opencog Hyperon/"
    private string mettaScriptPath = "Assets/OpencogHyperon/test.metta";

    void Start()
    {
        RunMeTTaScript();
    }

    void RunMeTTaScript()
    {
        // Create a new process to run the MeTTa interpreter
        Process process = new Process();
        process.StartInfo.FileName = mettaInterpreterPath;
        process.StartInfo.Arguments = mettaScriptPath;
        
        // Redirect the standard output to get the result
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        // Start the process and read the output
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (!string.IsNullOrEmpty(error))
        {
            UnityEngine.Debug.LogError($"MeTTa Error: {error}");
        }
        else
        {
            UnityEngine.Debug.Log($"MeTTa Output: {output}");
        }
    }
}

