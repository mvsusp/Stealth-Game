using UnityEditor;
using UnityEngine;

public class ShowHideWireframe : EditorWindow
{
    [MenuItem("Editor/Show WireFrame")]
    public static void ShowWire()
    {
        ToggleWireframe(false);
    }

    [MenuItem("Editor/Show WireFrame", true)]
    public static bool CheckShow()
    {
        return Selection.activeGameObject != null;
    }

    private static void ToggleWireframe(bool show)
    {
        foreach (var gameObject in Selection.gameObjects)
        {
            var rend = gameObject.GetComponent<Renderer>();
            if (rend != null)
            {
                EditorUtility.SetSelectedWireframeHidden(rend, show);
            }
        }
    }

    [MenuItem("Editor/Hide WireFrame")]
    public static void HideWire()
    {
        ToggleWireframe(true);
    }

    [MenuItem("Editor/Hide WireFrame", true)]
    public static bool CheckHide()
    {
        return Selection.activeGameObject != null;
    }
}