/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：HelloWorldLogic
* 创建日期：2021-08-12 15:02:48
* 作者名称：
* 功能描述：简单的HelloWorld程序说明MVVM中支持的组件及组价发出事件的处理方法
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;

namespace Com.Rainier.Buskit3D.Example.HelloWorld
{
    /// <summary>
    /// 简单的HelloWorld程序说明MVVM中支持的组件及组价发出事件的处理方法
    /// </summary>
    public class HelloWorldLogic : CommonLogic
    {
        /// <summary>
        /// MVVM上下文环境
        /// </summary>
        MvvmContext context;

        /// <summary>
        /// 界面上的MVVM组件
        /// </summary>
        string viewModels = "";

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
            if ((evt is PropertyInitEvent) ||
                evt.EventName.Equals("OnDisable") ||
                evt.EventName.Equals("OnDestroy"))
            {
                return;
            }

            //忽略除ViewModel外其他Entity发出的事件
            if (!(evt.EventSource is ViewModel))
            {
                return;
            }

            //获取事件源
            ViewModel vm = evt.EventSource as ViewModel;

            //获取名称为Text的TextViewModel用于打印一些信息，以便说明MVVM中其他组件事件已经起效
            TextViewModel tvm = context.FindViewModel<TextViewModel>("ContentText");
            ScrollbarViewModel scrollbarViewModel = context.FindViewModel<ScrollbarViewModel>("Scrollbar");
            ButtonViewModel buttonViewModel = context.FindViewModel<ButtonViewModel>("Button");
            

            //处理所有属性初始化事件
            if ((evt is PropertyInitEvent))
            {
                viewModels += "Hello:[" + vm.GetType().Name + "] ViewModel.\n";
                tvm.Value = viewModels;
                return;
            }

            //处理除了TextViewModel自己发出的事件
            if (!(vm is TextViewModel))
            {
                tvm.Value = "EventSource:[" + vm.GetType().Name + "] ViewModel.";

                //if (vm is SliderViewModel)
                //{
                //    SliderViewModel sv = context.FindViewModel<SliderViewModel>("Slider");
                //    scrollbarViewModel.Size = sv.Value;
                //    return;
                //}

                //if (vm is ButtonViewModel)
                //{
                //    scrollbarViewModel.NumberOfSteps += 1;
                //    Debug.Log(scrollbarViewModel.NumberOfSteps);
                //}
            }
        }
    }
}

