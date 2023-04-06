/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：ViewModelRegistry
* 创建日期：
* 作者名称：
* 功能描述：ViewModel注册器
* 修改记录：
* 描述：
******************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 根据不同UIBehaviour组件创建不同的ViewModel
    /// </summary>
    public class ViewModelRegistry
    {
        /// <summary>
        /// ViewModelRegistry单利
        /// </summary>
        private static ViewModelRegistry instance = null;

        /// <summary>
        /// 类型对照表
        /// </summary>
        private Dictionary<Type, Type> typeMap = new Dictionary<Type, Type>();

        /// <summary>
        /// 构造函数
        /// </summary>
        private ViewModelRegistry()
        {
            //注册类型
            typeMap.Add(typeof(Button)      , typeof(ButtonViewModel));
            typeMap.Add(typeof(Slider)      , typeof(SliderViewModel));
            typeMap.Add(typeof(Text)        , typeof(TextViewModel));
            typeMap.Add(typeof(Toggle)      , typeof(ToggleViewModel));
            typeMap.Add(typeof(Image)       , typeof(ImageViewModel));
            typeMap.Add(typeof(ToggleGroup) , typeof(ToggleGroupViewModel));
            typeMap.Add(typeof(InputField), typeof(InputFieldViewModel));
            typeMap.Add(typeof(Scrollbar) , typeof(ScrollbarViewModel));
            typeMap.Add(typeof(Dropdown) , typeof(DropdownViewModel));
        }

        /// <summary>
        /// ViewModelRegistry获取单利
        /// </summary>
        /// <returns></returns>
        public static ViewModelRegistry NewInstance()
        {
            if(instance == null)
            {
                instance = new ViewModelRegistry();
            }
            return instance;
        }

        /// <summary>
        /// 搜索正确的ViewModel类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Type FindViewModel(Type type)
        {
            Type viewModelType = null;
            if (!typeMap.TryGetValue(type, out viewModelType))
            {
                viewModelType = typeof(ViewModel);
            }
            return viewModelType;
        }

        /// <summary>
        /// 搜索正确的ViewModel类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Type FindViewModel<T>() where T: UIBehaviour
        {
            return FindViewModel(typeof(T));
        }
    }
}
