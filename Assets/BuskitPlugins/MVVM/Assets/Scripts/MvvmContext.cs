/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：MvvmContext
* 创建日期：2020-08-12 15:02:48
* 作者名称：
* 功能描述：作为MVVM中ViewModel，接收Entity和View事件并相互转发
* 修改记录：
* 描述：
******************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 作为MVVM中ViewModel，接收Entity和View事件并相互转发
    /// </summary>
    public class MvvmContext : DataModelBehaviour
    {
        /// <summary>
        /// 以当前物体为根节点，所有子物体的ViewModel组件列表（含自己）
        /// </summary>
        private List<ViewModel> ViewModelBehaviours = new List<ViewModel>();

        /// <summary>
        /// 根ViewModel
        /// </summary>
        public ViewModel RootViewModel;

        /// <summary>
        /// 绑定监听关系
        /// </summary>
        protected override void Start()
        {
            base.Start();
            //绑定UI
            Initialize(this.transform);
        }

        /// <summary>
        /// 添加ViewModel组件
        /// </summary>
        /// <param name="vm"></param>
        private void AddViewBehaviour(ViewModel vm)
        {
            if (!vm.Contains(this))
            {
                vm.AddEventListener(this);
            }
            ViewModelBehaviours.Add(vm);
            vm.Context = this;
        }

        /// <summary>
        /// 添加ViewModel组件
        /// </summary>
        /// <param name="vm"></param>
        public void DeleteViewModel(ViewModel vm)
        {
            if(ViewModelBehaviours.Contains(vm))
                ViewModelBehaviours.Remove(vm);
        }

        /// <summary>
        /// 遍历所有子节点获取Transform
        /// </summary>
        /// <param name="parent"></param>
        private void FindAllNodes(Transform parent, ref List<Transform> nodes)
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
        /// 搜索特定类型的ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] FindViewModels<T>() where T : ViewModel
        {
            List<T> viewModels = new List<T>();
            foreach (var item in ViewModelBehaviours)
            {
                if(item.GetType().Equals(typeof(T)))
                {
                    viewModels.Add((T)item);
                }
            }
            return viewModels.ToArray();
        }

        /// <summary>
        /// 通过类型搜索指定类型的ViewModel
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ViewModel[] FindViewModels(Type type)
        {
            List<ViewModel> viewModels = new List<ViewModel>();
            foreach (var item in ViewModelBehaviours)
            {
                if (item.GetType().Equals(type))
                {
                    viewModels.Add(item);
                }
            }
            return viewModels.ToArray();
        }

        /// <summary>
        /// 通过指定类型和物体名称查找ViewModel
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ViewModel[] FindViewModels(Type type,string name)
        {
            ViewModel[] _viewModels = FindViewModels(type);
            List<ViewModel> viewModels = new List<ViewModel>();
            foreach (ViewModel item in _viewModels)
            {
                if (item.gameObject.name.Equals(name))
                {
                    viewModels.Add(item);
                }
            }
            return viewModels.ToArray();
        }

        /// <summary>
        /// 搜索特定类型的ViewModel
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="name">组件名称</param>
        /// <returns></returns>
        public T[] FindViewModels<T>(string name) where T : ViewModel
        {
            T[] _viewModels = FindViewModels<T>();

            List<T> viewModels = new List<T>();
            foreach (T item in _viewModels)
            {
                if (item.gameObject.name.Equals(name))
                {
                    viewModels.Add(item);
                }
            }
            return viewModels.ToArray();
        }

        /// <summary>
        /// 搜索特定类型的UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] FindUIBehaviours<T>() where T : UIBehaviour
        {
            List<T> uiBehaviours = new List<T>();
            foreach (var item in ViewModelBehaviours)
            {
                if (item.UIComponent.GetType().Equals(typeof(T)))
                {
                    uiBehaviours.Add((T)item.UIComponent);
                }
            }
            return uiBehaviours.ToArray();
        }

        /// <summary>
        /// 搜索特定类型的UI组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public UIBehaviour[] FindUIBehaviours(Type type)
        {
            List<UIBehaviour> uiBehaviours = new List<UIBehaviour>();
            foreach (var item in ViewModelBehaviours)
            {
                if (item.UIComponent.GetType().Equals(type))
                {
                    uiBehaviours.Add(item.UIComponent);
                }
            }
            return uiBehaviours.ToArray();
        }

        /// <summary>
        /// 搜索特定类型的UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T[] FindUIBehaviours<T>(string name) where T : UIBehaviour
        {
            T[] _uiBihaviours = FindUIBehaviours<T>();

            List<T> uiBihaviours = new List<T>();
            foreach (T item in _uiBihaviours)
            {
                if (item.gameObject.name.Equals(name))
                {
                    uiBihaviours.Add(item);
                }
            }
            return uiBihaviours.ToArray();
        }

        /// <summary>
        /// 搜索特定类型的UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public UIBehaviour[] FindUIBehaviours(Type type,string name)
        {
            UIBehaviour[] _uiBihaviours = FindUIBehaviours(type);

            List<UIBehaviour> uiBihaviours = new List<UIBehaviour>();
            foreach (UIBehaviour item in _uiBihaviours)
            {
                if (item.gameObject.name.Equals(name))
                {
                    uiBihaviours.Add(item);
                }
            }
            return uiBihaviours.ToArray();
        }

        /// <summary>
        /// 搜索特定类型的ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FindViewModel<T>() where T : ViewModel
        {
            foreach (var item in ViewModelBehaviours)
            {
                if (item.GetType().Equals(typeof(T)))
                {
                    return item as T;
                }
            }
            return null;
        }

        /// <summary>
        /// 搜索特定类型的ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ViewModel FindViewModel(Type type)
        {
            foreach (var item in ViewModelBehaviours)
            {
                if (item.GetType().Equals(type))
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 搜索特定类型的ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T FindViewModel<T>(string name) where T : ViewModel
        {
            T[] viewModels = FindViewModels<T>(name);
            if(viewModels.Length > 0)
            {
                return viewModels[0];
            }
            return null;
        }

        /// <summary>
        /// 搜索特定类型的ViewModel
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public ViewModel FindViewModel(Type type,string name)
        {
            ViewModel[] viewModels = FindViewModels(type,name);
            if (viewModels.Length > 0)
            {
                return viewModels[0];
            }
            return null;
        }

        /// <summary>
        /// 搜索特定类型的UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FindUIBehaviour<T>() where T : UIBehaviour
        {
            foreach (var item in ViewModelBehaviours)
            {
                if (item.UIComponent.GetType().Equals(typeof(T)))
                {
                    return (T)item.UIComponent;
                }
            }
            return null;
        }

        /// <summary>
        /// 搜索特定类型的UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public UIBehaviour FindUIBehaviour(Type type)
        {
            foreach (var item in ViewModelBehaviours)
            {
                if (item.UIComponent.GetType().Equals(type))
                {
                    return item.UIComponent;
                }
            }
            return null;
        }

        /// <summary>
        /// 搜索特定类型的UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FindUIBehaviour<T>(string name) where T : UIBehaviour
        {
            T[] uiBehaviours = FindUIBehaviours<T>(name);
            if(uiBehaviours.Length > 0)
            {
                return uiBehaviours[0];
            }
            return null;
        }

        /// <summary>
        /// 搜索特定类型的UI组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public UIBehaviour FindUIBehaviour(Type type,string name)
        {
            UIBehaviour[] uiBehaviours = FindUIBehaviours(type,name);
            if (uiBehaviours.Length > 0)
            {
                return uiBehaviours[0];
            }
            return null;
        }

        /// <summary>
        /// 绑定动态UI
        /// </summary>
        /// <param name="viewModel"></param>
        public void Initialize(Transform trans)
        {
            //搜索所有子ViewModel
            List<Transform> nodes = new List<Transform>();
            FindAllNodes(trans, ref nodes);
            //记录当前隐藏的物体
            List<GameObject> _gameList = new List<GameObject>();
            //遍历子ViewModel并设置MvvmContext
            foreach (Transform tr in nodes)
            {
                ViewModel[] vms = tr.GetComponents<ViewModel>();
                if (tr.childCount > 0 && vms.Length <= 0)
                {
                    if (!tr.gameObject.activeSelf)
                    {
                        tr.gameObject.SetActive(true);
                        _gameList.Add(tr.gameObject);
                        continue;
                    }
                }

                foreach (ViewModel vm in vms)
                {
                    if (vm == null)
                    {
                        continue;
                    }
                    //如果当前物体是隐藏状态，激活，添加监听放入隐藏物体列表
                    if (!vm.isActiveAndEnabled)
                    {
                        vm.gameObject.SetActive(true);
                        _gameList.Add(vm.gameObject);
                    }

                    AddViewBehaviour(vm);
                    if ((tr == this.transform))
                    {
                        RootViewModel = vm;
                    }
                }
            }
            //遍历隐藏物体列表，将所有物体重新隐藏
            for (int i = 0; i < _gameList.Count; i++)
            {
                _gameList[i].SetActive(false);
            }
            //销毁隐藏物体列表
            _gameList = null;
        }

        /// <summary>
        /// 销毁监听关系
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            foreach (var item in ViewModelBehaviours)
            {
                item.Context = null;
                item.RemoveEventListener(this);
            }
        }
    }
}