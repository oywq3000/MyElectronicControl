/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：MVVMInitializer
* 创建日期：2022-08-12 15:02:48
* 作者名称：
* 功能描述：作为MVVM初始化器
* 修改记录：
* 描述：
******************************************************************************/
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.Rainier.Buskit3D
{
    
    /// <summary>
    /// 作为MVVM初始化器
    /// </summary>
#if UNITY_EDITOR
    public class MVVMInitializer : EditorWindow
    {

        /// <summary>
        /// 初始化MVVM配置对话框
        /// </summary>
        [MenuItem("GameObject/Rainier/初始化MVVM配置", priority = 0)]
        public static void Initialize()
        {
            /// <summary>
            /// 当前选中的物体
            /// </summary>
            GameObject[] selectionGameObjects = new GameObject[] { };

            /// <summary>
            /// 根节点
            /// </summary>
            GameObject root = null;

            selectionGameObjects = Selection.gameObjects;

            //只能选中一个物体
            if (selectionGameObjects.Length != 1)
            {
                Debug.Log("请选择一个UI根物体作为MvvmContext");
                root = null;
                return;
            }

            //获取根组件
            root = selectionGameObjects[0];
            //判断选择的根组件是否含有UIBehaviour组件,如果没有则提示错误信息
            if (root.GetComponent<RectTransform>() == null)
            {
                Debug.Log("请选择一个UI根物体作为MvvmContext");
                root = null;
                return;
            }

            Debug.Log("MVVMContext Root Object[ " + root.name + " ]");

            List<Transform> allNodes = new List<Transform>();
            FindAllNodes(root.transform, ref allNodes);

            //初始化MVVM上下文环境
            if (root.GetComponent<MvvmContext>() == null)
            {
                root.AddComponent<MvvmContext>();
            }

            //给所有UI节点添加ViewModel
            foreach (var trans in allNodes)
            {
                //删除丢失引用的脚本，UniqueId存在这种情况
                CleanUpMisssingScript(trans.gameObject);

                UIBehaviour[] uiComponents = trans.GetComponents<UIBehaviour>();

                foreach (var uic in uiComponents)
                {
                    //根据UI组件类型创建合适的ViewModel类型
                    Type viewModelType = ViewModelRegistry.NewInstance().
                        FindViewModel(uic.GetType());

                    //如果创建ViewModel类型不是继承自ViewModel则抛出异常
                    if (!(viewModelType.IsSubclassOf(typeof(ViewModel)) ||
                        (viewModelType == typeof(ViewModel))))
                    {
                        Debug.Log("MVVM内部错误未能完成初始化！");
                    }

                    //给物体添加ViewModel脚本并设置MVVMContext对象
                    if (viewModelType != null)
                    {
                        //如果已经存在ViewModel则继续
                        if (trans.gameObject.GetComponent(viewModelType) != null)
                        {
                            continue;
                        }

                        //添加ViewModel脚本
                        trans.gameObject.AddComponent(viewModelType);

                        //获取ViewModel对象
                        ViewModel viewModel = trans.gameObject.GetComponent(viewModelType) as ViewModel;

                        //初始化ViewModel
                        MvvmContext ctx = root.GetComponent<MvvmContext>();
                        viewModel.Context = ctx;

                        //如果是根节点则初始化根ViewModel对象
                        if (trans.gameObject == root)
                        {
                            ctx.RootViewModel = viewModel;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 初始化MVVM配置对话框
        /// </summary>
        [MenuItem("GameObject/Rainier/取消初始化MVVM配置", priority = 0)]
        public static void CanalInitialize()
        {
            /// <summary>
            /// 当前选中的物体
            /// </summary>
            GameObject[] selectionGameObjects = new GameObject[] { };

            /// <summary>
            /// 根节点
            /// </summary>
            GameObject root = null;

            selectionGameObjects = Selection.gameObjects;

            //只能选中一个物体
            if (selectionGameObjects.Length != 1)
            {
                Debug.Log("请选择一个UI根物体作为MvvmContext");
                root = null;
                return;
            }

            //获取根组件
            root = selectionGameObjects[0];
            //判断选择的根组件是否含有UIBehaviour组件,如果没有则提示错误信息
            if (root.GetComponent<RectTransform>() == null)
            {
                Debug.Log("请选择一个UI根物体作为MvvmContext");
                root = null;
                return;
            }

            Debug.Log("MVVMContext Root Object[ " + root.name + " ]");

            List<Transform> allNodes = new List<Transform>();
            FindAllNodes(root.transform, ref allNodes);

            //初始化MVVM上下文环境
            if (root.GetComponent<MvvmContext>() == null)
            {
                root.AddComponent<MvvmContext>();
            }

            FindAllNodes(root.transform, ref allNodes);
            for (int i = 0; i < allNodes.Count; i++)
            {
                if (allNodes[i].GetComponent<ViewModel>())
                {
                    var viewModel = allNodes[i].GetComponents<ViewModel>();
                    for (int j = 0; j < viewModel.Length; j++)
                    {
                        DestroyImmediate(viewModel[j].GetComponent<ViewModel>());
                    }
                }
                if (allNodes[i].GetComponent<MvvmContext>())
                {
                    DestroyImmediate(allNodes[i].GetComponent<MvvmContext>());
                }
                if (allNodes[i].GetComponent<UniqueID>())
                {
                    DestroyImmediate(allNodes[i].GetComponent<UniqueID>());
                }
            }

        }

        /// <summary>
        /// 初始化MVVM配置对话框
        /// </summary>
        [MenuItem("GameObject/Rainier/一键绑定UniqueID脚本", priority = 0)]
        public static void AddUniqueID()
        {
            /// <summary>
            /// 当前选中的物体
            /// </summary>
            GameObject[] selectionGameObjects = new GameObject[] { };

            /// <summary>
            /// 根节点
            /// </summary>
            GameObject root = null;

            selectionGameObjects = Selection.gameObjects;

            //只能选中一个物体
            if (selectionGameObjects.Length != 1)
            {
                Debug.Log("请选择一个UI根物体作为MvvmContext");
                root = null;
                return;
            }

            //获取根组件
            root = selectionGameObjects[0];
            //判断选择的根组件是否含有UIBehaviour组件,如果没有则提示错误信息
            if (root.GetComponent<RectTransform>() == null)
            {
                Debug.Log("请选择一个UI根物体作为MvvmContext");
                root = null;
                return;
            }

            Debug.Log("MVVMContext Root Object[ " + root.name + " ]");

            List<Transform> allNodes = new List<Transform>();
            FindAllNodes(root.transform, ref allNodes);

            //初始化MVVM上下文环境
            if (root.GetComponent<MvvmContext>() == null)
            {
                root.AddComponent<MvvmContext>();
            }

            FindAllNodes(root.transform, ref allNodes);
            for (int i = 0; i < allNodes.Count; i++)
            {
                if (allNodes[i].GetComponent<ViewModel>())
                {
                    var viewModels = allNodes[i].GetComponents<ViewModel>();
                    foreach (var viewModel in viewModels)
                    {
                        if (viewModel.GetComponent<UniqueID>())
                        {
                            continue;
                        }
                        else
                        {
                            viewModel.gameObject.AddComponent<UniqueID>();
                        }
                    }
                }

            }

            ResetUniqueIDNumber();
        }

        /// <summary>
        /// 遍历所有子节点获取Transform
        /// </summary>
        /// <param name="parent"></param>
        private static void FindAllNodes(Transform parent, ref List<Transform> nodes)
        {
            //队列存放需要被遍历的节点
            //加入的逻辑为：当前节点的子节点，从第一个子节点到最后一个子节点顺序加入
            Queue<Transform> queue = new Queue<Transform>();
            queue.Enqueue(parent);
            while (queue.Count > 0)
            {
                //当前访问的子节点
                Transform current = queue.Dequeue();
                nodes.Add(current);
                //如果当前节点有子节点，则将其加入队列，留待以后遍历
                for (int i = 0; i < current.childCount; i++)
                {
                    queue.Enqueue(current.GetChild(i));
                }
            }
        }

        /// <summary>
        /// 删除物体上引用丢失的脚本
        /// </summary>
        /// <param name="go"></param>
        private static void CleanUpMisssingScript(GameObject go)
        {
            SerializedObject so = new SerializedObject(go);
            var soProperties = so.FindProperty("m_Component");
            var components = go.GetComponents<Component>();
            int propertyIndex = 0;
            foreach (var c in components)
            {
                if (c == null)
                {
                    soProperties.DeleteArrayElementAtIndex(propertyIndex);
                }
                ++propertyIndex;
            }
            so.ApplyModifiedProperties();
        }


        /// <summary>
        /// 有些物体未激活状态下，id为-1,需要在编辑下激活进行编号 
        /// </summary>
        /*[MenuItem("Rainier/Reset UniqueID")]*/
        static void ResetUniqueIDNumber()
        {

            GameObject[] roots = EditorSceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject item in roots)
            {

                UniqueID[] uniqueIDs = item.GetComponentsInChildren<UniqueID>(true);

                foreach (var uid in uniqueIDs)
                {
                    System.Reflection.MethodInfo minfo = uid.GetType().GetMethod("Awake", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly);
                    minfo.Invoke(uid, null);
                }
            }
        }
    }
#endif
}