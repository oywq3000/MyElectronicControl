using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class Appload
{
    static Appload()
    {
        bool hasKey = PlayerPrefs.HasKey("Mvvm");
        if (hasKey==false)
        {
            //EditorApplication.update += Update;
            PlayerPrefs.SetInt("Mvvm", 1);
            WelcomeScreen.ShowWindow();
        }
    }

    //static void Update() 
    //{
    //    bool isSuccess = EditorApplication.ExecuteMenuItem("Welcome Screen");
    //    if (isSuccess) EditorApplication.update -= Update;
    //}
}

public class WelcomeScreen : EditorWindow
{
    private Texture mSamplesImage;
    private Rect imageRect = new Rect(30f, 90f, 350f, 350f);
    private Rect textRect = new Rect(15f, 15f, 380f, 100f);

    public void OnEnable()
    {
        //this.mWelcomeScreenImage = EditorGUIUtility.Load("WelcomeScreenHeader.png") as Texture;
        //BehaviorDesignerUtility.LoadTexture("WelcomeScreenHeader.png", false, this);
        //this.mSamplesImage = LoadTexture("wechat.jpg");
    }

    
    //Texture LoadTexture(string name)
    //{
    //    string path = "Assets/Demigiant/Editor/";
    //    return (Texture)AssetDatabase.LoadAssetAtPath(path + name, typeof(Texture));
    //}

    public void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 14;
        style.normal.textColor = Color.white;
        GUI.Label(this.textRect, "Welcome to the resource pack of the Mvvm framework" +
            "\n\nThis page will only be displayed once", style);
        //GUI.DrawTexture(this.imageRect, this.mSamplesImage);
    }
    
    public static void ShowWindow()
    {
        WelcomeScreen window = EditorWindow.GetWindow<WelcomeScreen>(true, "Hello ");
        window.minSize = window.maxSize = new Vector2(410f, 400f);
        UnityEngine.Object.DontDestroyOnLoad(window);
    }
}


