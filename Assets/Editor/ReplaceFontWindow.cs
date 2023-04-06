using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ReplaceFontWindow : EditorWindow
{
#if UNITY_EDITOR
    [MenuItem("Tools/更换字体")]
    public static void Open()
    {
        GetWindow(typeof(ReplaceFontWindow)); //开启新窗口
    }

    Font toChange; //替换字体（面板赋值）
    FontStyle toFontStyle;//字体样式（面板赋值）
    static Font toChangeFont;
    static FontStyle toChangeFontStyle;

    //

    void OnGUI()
    {
        toChange = (Font)EditorGUILayout.ObjectField(toChange, typeof(Font), true, GUILayout.MinWidth(100f));
        toFontStyle = (FontStyle)EditorGUILayout.EnumPopup(toFontStyle, GUILayout.MinWidth(100f));
        //赋值
        toChangeFont = toChange;
        toChangeFontStyle = toFontStyle;

        //按钮
        if (GUILayout.Button("更换"))
        {
            Transform canvas = GameObject.Find("Canvas").transform;
            if (!toChangeFont)
            {
                Debug.Log("NO Font");
                return;
            }

            if (!canvas)
            {
                Debug.Log("NO Canvas");
                return;
            }

            SetFonts(canvas);

            Debug.Log("Font replacement succeeded");
        }
    }

    Transform childObj;
    public void SetFonts(Transform obj)
    {
        for (int i = 0; i < obj.childCount; i++)
        {
            childObj = obj.GetChild(i);
            Text t = childObj.GetComponent<Text>();
            if (t)
            {
                //将对象放到撤销记录中，不加此代码 无法还原替换前的操作
                Undo.RecordObject(t, t.name);
                t.font = toChange;
                t.fontStyle = toChangeFontStyle;
                //刷新下
                EditorUtility.SetDirty(childObj);
            }

            //递归查询
            if (childObj.childCount > 0)
            {
                SetFonts(childObj);
            }
        }
    }
#endif
}