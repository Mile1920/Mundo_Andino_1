#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class FixMagentaMaterials : EditorWindow
{
    [MenuItem("Tools/Fix Magenta Materials")]
    static void FixMaterials()
    {
        Shader urpLit = Shader.Find("Universal Render Pipeline/Lit");
        
        if (urpLit == null)
        {
            Debug.LogError("URP/Lit shader no encontrado. Verifica que URP esté instalado.");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:Material", 
            new string[] { "Assets/Flooded_Grounds" });
        
        int fixed_count = 0;
        
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            
            if (mat != null && mat.shader.name.Contains("Hidden/InternalErrorShader"))
            {
                mat.shader = urpLit;
                EditorUtility.SetDirty(mat);
                fixed_count++;
                Debug.Log($"Arreglado: {path}");
            }
        }
        
        AssetDatabase.SaveAssets();
        Debug.Log($"✓ {fixed_count} materiales arreglados!");
        EditorUtility.DisplayDialog("Listo", 
            $"{fixed_count} materiales magenta arreglados!", "OK");
    }
}
#endif