using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Morph.Editor
{
    public class MorphApplicationWindow : EditorWindow
    {
        [Serializable]
        public struct ApplicationData
        {
            public string Name;
            public string Scene;
            public List<string> Dependencies;

            public override bool Equals(object obj)
            {
                return obj != null && Equals( (ApplicationData) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (Name != null ? Name.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (Scene != null ? Scene.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (Dependencies != null ? Dependencies.GetHashCode() : 0);
                    return hashCode;
                }
            }

            public bool Equals(ApplicationData other)
            {
                return string.Equals(Name, other.Name) && Equals(Dependencies, other.Dependencies);
            }

            
        }

        private List<ApplicationData> _applications;

        private int _selectedApplication;

        [MenuItem("Morph/Application")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<MorphApplicationWindow>();
        }

        private DirectoryInfo CreateFolderIfNeeded(string folderPath)
        {
            bool hasDirectory = Directory.Exists(folderPath);
            if (!hasDirectory)
            {
                Debug.LogWarningFormat("Morph couldn't find directory {0}. Creating one.", folderPath);
                DirectoryInfo info = Directory.CreateDirectory(folderPath);
                if (!info.Exists)
                {
                    Debug.LogErrorFormat("Morph failed to create directory at path {0}", folderPath);
                    return null;
                }

                return info;
            }

            return new DirectoryInfo(folderPath);
        }

        private DirectoryInfo CreateApplicationsFolderIfNeeded()
        {
            string applicationsPath = Path.Combine(Application.dataPath, "..", "Applications");
            return CreateFolderIfNeeded(applicationsPath);
        }

        private DirectoryInfo CreateDependenciesFolderIfNeeded()
        {
            string dependenciesPath = Path.Combine(Application.dataPath, "..", "Dependencies");
            return CreateFolderIfNeeded(dependenciesPath);
        }

        void Awake()
        {
            _applications = new List<ApplicationData>();

            //Applications
            DirectoryInfo applicationsDirectoryInfo = CreateApplicationsFolderIfNeeded();
            if (applicationsDirectoryInfo == null) return;

            string[] applicationsFiles = Directory.GetFiles(applicationsDirectoryInfo.FullName, "*.json");
            if (applicationsFiles.Length == 0)
            {
                Debug.LogWarning("Morph couldn't find any applications in the folder");
                return;
            }

            Debug.LogFormat("Morph has found {0} applications", applicationsFiles.Length);

            //Dependencies
            DirectoryInfo dependenciesDirectoryInfo = CreateDependenciesFolderIfNeeded();
            if (dependenciesDirectoryInfo == null) return;

            //Parse applications files
            foreach (var applicationFile in applicationsFiles)
            {
                string fileContent = File.ReadAllText(applicationFile);
                ApplicationData data = JsonUtility.FromJson<ApplicationData>(fileContent);
                _applications.Add(data);

                Debug.LogFormat("Morph found application {0}", data.Name);
            }
        }

        void OnGUI()
        {
            //Application
            GUILayout.Label("Application Settings", EditorStyles.boldLabel);

            IEnumerable<string> content = _applications.Select(application => application.Name);
            int selectedApplication = EditorGUILayout.Popup("Application", _selectedApplication, content.ToArray());

            //User has changed application
            if (selectedApplication != _selectedApplication)
            {
                _selectedApplication = selectedApplication;
                Debug.Log("Switch application");
            }
        }
    }
}
