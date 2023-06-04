using UnityEngine;

public static class ComponentDebugger
{
    public static void DisplayComponents(GameObject obj)
    {
        Component[] components = obj.GetComponents<Component>();

        Debug.Log("_______________________________________");
        Debug.Log($"Components attached to {obj.name}:");
        foreach (Component component in components)
        {
            var comp = component.GetType().ToString();
            if (comp.Contains("Fish") || comp.Contains("Unity"))
                continue;
            var isEnabled = (component as Behaviour)?.enabled ?? true;
            Debug.Log($"{comp} is {(isEnabled ? "enabled" : "disabled")}");
        }
        Debug.Log("_______________________________________");
    }
}
