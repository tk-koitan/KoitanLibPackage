using System;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;

static class AddPackageExample
{
    static AddRequest Request;

    [MenuItem("Window/Add Package Example")]
    static void Add()
    {
        // パッケージをプロジェクトに加える
        Request = Client.Add("com.koitan.koitanlib@https://github.com/tk-koitan/KoitanLibPackage.git?path=Packages/KoitanLib");
        EditorApplication.update += Progress;
    }

    static void Progress()
    {
        if (Request.IsCompleted)
        {
            if (Request.Status == StatusCode.Success)
                Debug.Log("Installed: " + Request.Result.packageId);
            else if (Request.Status >= StatusCode.Failure)
                Debug.Log(Request.Error.message);

            EditorApplication.update -= Progress;
        }
    }
}
