using LanguageExt.UnsafeValueAccess;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using static LanguageExt.Prelude;
using Object = UnityEngine.Object;

public class TransformUnityOptionDrawer<T> : UnityOptionDrawer<T> where T : Object
{
	protected override T DrawField(Rect rect, T value) => EditorGUI.ObjectField(rect, value, typeof(T), true) as T;
}

public class IntUnityOptionDrawer : UnityOptionDrawer<int>
{
	protected override int DrawField(Rect rect, int value) => EditorGUI.IntField(rect, value);
}

public class StringUnityOptionDrawer : UnityOptionDrawer<string>
{
	protected override string DrawField(Rect rect, string value) => EditorGUI.TextField(rect, value);
}

public class FloatUnityOptionDrawer : UnityOptionDrawer<float>
{
	protected override float DrawField(Rect rect, float value) => EditorGUI.FloatField(rect, value);
}

public class ColorUnityOptionDrawer : UnityOptionDrawer<Color>
{
	protected override Color DrawField(Rect rect, Color value) => EditorGUI.ColorField(rect, value);
}

public class AnimationCurveUnityOptionDrawer : UnityOptionDrawer<AnimationCurve>
{
	protected override AnimationCurve DrawField(Rect rect, AnimationCurve value) => EditorGUI.CurveField(rect, value);
}

public class Vector2UnityOptionDrawer : UnityOptionDrawer<Vector2>
{
	protected override Vector2 DrawField(Rect rect, Vector2 value) => EditorGUI.Vector2Field(rect, GUIContent.none, value);
}

public class Vector3UnityOptionDrawer : UnityOptionDrawer<Vector3>
{
	protected override Vector3 DrawField(Rect rect, Vector3 value) => EditorGUI.Vector3Field(rect, GUIContent.none, value);
}

public abstract class UnityOptionDrawer<T> : OdinValueDrawer<UnityOption<T>>
{
	protected abstract T DrawField(Rect rect, T value);

	protected override void DrawPropertyLayout(GUIContent label)
    {
		var rect = EditorGUILayout.GetControlRect();
		if (label != null)
        {
			rect = EditorGUI.PrefixLabel(rect, label);
		}

		var unityOption = ValueEntry.SmartValue;
		var isSome = EditorGUI.Toggle(rect.AlignLeft(rect.width * .1f), unityOption.IsSome);

		GUIHelper.PushLabelWidth(20);

		if (isSome)
        {
			var rightRect = rect.AlignRight(rect.width * .9f);
			var valueOrDefault = unityOption.Value.IsSome ? unityOption.Value.ValueUnsafe() : default;
			var newValue = DrawField(rightRect, valueOrDefault);
			unityOption = UnityOption<T>.FromOption(newValue == null ? None : Some(newValue));
		}
		else
        {
			unityOption = UnityOption<T>.None;
		}

		GUIHelper.PopLabelWidth();
		ValueEntry.SmartValue = unityOption;
	}
}