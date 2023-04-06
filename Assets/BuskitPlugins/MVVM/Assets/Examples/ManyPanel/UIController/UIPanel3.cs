/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：UIPanel3
* 创建日期：2020-03-06 17:31:45
* 作者名称：
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;

namespace Mvvm
{   
	/// <summary>
    /// 
    /// </summary>
	public class UIPanel3 : CommonLogic
    {
        /// <summary>
        /// MVVM上下文环境
        /// </summary>
        MvvmContext context;

        /// <summary>
        /// closeView
        /// </summary>
        public ButtonViewModel closeView;

        /// <summary>
        /// 获取MVVM上下文环境
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            context = GetComponent<MvvmContext>();
        }

        /// <summary>
        /// 处理业务逻辑
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(IEvent evt)
        {
            //忽略所有属性初始化事件、Diable事件、有Destroy事件
            if ((evt is PropertyInitEvent) || evt.EventName.Equals("OnDisable") || evt.EventName.Equals("OnDestroy")) return;

            //忽略除ViewModel外其他Entity发出的事件
            if (!(evt.EventSource is ViewModel)) return;

            //获取事件源
            ViewModel vm = evt.EventSource as ViewModel;

            if (vm.Equals(closeView))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
