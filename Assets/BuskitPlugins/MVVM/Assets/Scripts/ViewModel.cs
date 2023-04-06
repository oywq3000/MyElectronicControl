/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：ViewModel
* 创建日期：
* 作者名称：
* 功能描述：作为MVVM中UI控件的View组件，提供组件回调函数、组件显示更新等功能
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine.EventSystems;
using Com.Rainier.Buskit.Unity.Architecture.Injector;
using UnityEngine;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// 作为MVVM中UI控件的View组件，提供组件回调函数、组件显示更新等功能
    /// </summary>
    public class ViewModel : EventSupportBehaviour
    {
        /// <summary>
        /// MVVM上下文环境
        /// </summary>
        public MvvmContext Context = null; 

        /// <summary>
        /// UI组件
        /// </summary>
        public UIBehaviour UIComponent;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (Context != null)
                Context.DeleteViewModel(this);
            InjectService.Get<GameObjectPool>().RemoveObject(gameObject);
        }

        /// <summary>
        /// 初始化EventSource、执行依赖注入、绑定Entity
        /// </summary>
        protected override void Awake()
        {
            //实例化EventSource
            this.eventSource = new EventSource(this);

            //将GameObject注册到物体对象池
            InjectService.Get<GameObjectPool>().RegisterObject(gameObject);

            //初始化ViewModel
            this.InitView();
        }

        /// <summary>
        /// 初始化View
        /// </summary>
        /// <param name="uiBehaviour">UI组件</param>
        /// <param name="ctx">MVVM上下文环境</param>
        public virtual void InitView()
        {

        }

        /// <summary>
        /// 更新组件现实
        /// </summary>
        protected virtual void RefreshComponent()
        {

        }
    }
}
