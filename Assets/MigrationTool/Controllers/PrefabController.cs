﻿#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using migrationtool.models;
using migrationtool.utility;

namespace migrationtool.controllers
{
    public class PrefabController
    {
        private readonly Constants constants = Constants.Instance;

        /// <summary>
        /// Get all prefabs in the projects and parses them to prefab models
        /// </summary>
        /// <param name="path">AssetPath of the project</param>
        /// <returns></returns>
        public List<PrefabModel> ExportPrefabs(string path)
        {
            //Get all prefabs
            string[] prefabMetaFiles = Directory.GetFiles(path, "*.prefab.meta", SearchOption.AllDirectories);

            //Parse all guids
            List<PrefabModel> prefabModels = new List<PrefabModel>(prefabMetaFiles.Length);
            for (var i = 0; i < prefabMetaFiles.Length; i++)
            {
                prefabModels.Add(ParsePrefabFile(prefabMetaFiles[i]));
            }

            return prefabModels;
        }

        /// <summary>
        /// Generates a PrefabModel from a .prefab.meta file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        private PrefabModel ParsePrefabFile(string file)
        {
            IEnumerable<string> lines = File.ReadLines(file);
            foreach (string line in lines)
            {
                Match match = constants.RegexGuid.Match(line);
                if (!match.Success) continue;

                return new PrefabModel(file, match.Value);
            }

            throw new NullReferenceException("Could not find GUID in prefab meta file : " + file);
        }

        #region old implementation
//
////    public bool CopyAllPrefabs(string originalPath, string destinationPath)
////    {
////        try
////        {
////            string[] prefabMetaFiles = Directory.GetFiles(originalPath, "*.prefab.meta", SearchOption.AllDirectories);
////            foreach (string file in prefabMetaFiles)
////            {
////                CopyPrefab(file, destinationPath);
////            }
////        }
////        catch (Exception e)
////        {
////            Debug.LogError("Could not copy the prefab files, Error : \r\n" + e);
////            return false;
////        }
////
////        return true;
////    }
//    public void CopyPrefab(string originalPath, string destinationPath, List<ClassModel> oldIDs,
//        List<ClassModel> newIDs, ref List<FoundScript> foundScripts)
//    {
//        if (!originalPath.EndsWith(".prefab.meta"))
//        {
//            Debug.LogError("Can not move file that does not have a meta file. Given path : " + originalPath);
//            return;
//        }
//
//        string metaPrefabName = originalPath;
//        string prefabName = originalPath.Replace(".meta", "");
//
//        string[] newPrefab = idController.TransformIDs(prefabName, oldIDs, newIDs, ref foundScripts); // todo : will this hang because it has a wait on the main thread in it?
////        FieldMappingController.Instance.ReplaceFieldsByMergeNodes()
////        FieldMappingController.Instance.ReplaceFieldsByMergeNodes(newPrefab,foundScripts,)
//
//        CopyFile(metaPrefabName, destinationPath);
//        CopyFile(prefabName, destinationPath);
//    }
//
//    private void CopyFile(string originalFile, string destinationPath)
//    {
//        string fileDestination = Path.Combine(destinationPath, Path.GetFileName(originalFile));
//        if (File.Exists(fileDestination))
//        {
//            if (EditorUtility.DisplayDialog("Prefab already exists",
//                "Prefab file already exists, Do you want to overwrite the file : \r\n" + fileDestination +
//                " \r\r Original location : " + originalFile, "Overwrite", "Ignore"))
//            {
//                File.Copy(originalFile, fileDestination, true);
//            }
//            else
//            {
//                Debug.Log("Skipped prefab : " + originalFile);
//            }
//        }
//        else
//        {
//            File.Copy(originalFile, fileDestination);
//        }
//    }

        #endregion
    }
}
#endif