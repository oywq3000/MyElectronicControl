/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：UIPanel1
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
	public class UIPanel : CommonLogic 
	{
		/// <summary>
        /// MVVM上下文环境
        /// </summary>
        MvvmContext context;     
        
        /// <summary>
        /// 面板
        /// </summary>
        public  MvvmContext[] contexts;

        /// <summary>
        /// panel1按钮
        /// </summary>
        public ButtonViewModel btnPanel1View;
        /// <summary>
        /// panel2按钮
        /// </summary>
        public ButtonViewModel btnPanel2View;
        /// <summary>
        /// panel3按钮
        /// </summary>
        public ButtonViewModel btnPanel3View;


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
            if ((evt is PropertyInitEvent) || evt.EventName.Equals("OnDisable") || evt.EventName.Equals("OnDestroy"))	return;

            //忽略除ViewModel外其他Entity发出的事件
            if (!(evt.EventSource is ViewModel))	return;

			//获取事件源
            ViewModel vm = evt.EventSource as ViewModel;

            if (vm.Equals(btnPanel1View))
            {
                Debug.Log("btnPanel1View");
                ControllerShow(0);
            }
            else if (vm.Equals(btnPanel2View))
            {
                Debug.Log("btnPanel2View");
                ControllerShow(1);
            }
            else if(vm.Equals(btnPanel3View))
            {
                Debug.Log("btnPanel3View");
                ControllerShow(2);
            }
        }

        private void ControllerShow(int index)
        {
            Debug.Log(index);
            for (int i = 0; i < contexts.Length; i++)
            {
                if (index == i)
                {
                    contexts[i].gameObject.SetActive(true);
                }
                else
                {
                    contexts[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
