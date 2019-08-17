using UnityEditor;
using UnityEngine.UIElements;

public class ExampleWindow : EditorWindow
{
	[MenuItem("UIElements-Pug/Template Window")]
	public static void ShowWindow()
	{
		var w = GetWindow(typeof(ExampleWindow));

		var uiAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Example/Template.uxml");

		VisualElement ui = uiAsset.CloneTree();

		w.rootVisualElement.Add(ui);
	}
}