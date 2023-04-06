/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：SliderViewModel
* 创建日期：2020-10-10 15:02:48
* 作者名称：
* 功能描述：Slider的ViewModel组件，发送Slider.onValueChanged事件
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// Slider的ViewModel组件，发送Slider.onValueChanged事件
    /// </summary>
    public class ScrollbarViewModel : ViewModel
    {
        /// <summary>
        /// 发送Click事件
        /// </summary>
        [FireInitEvent]
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                float oldValue = _value;
                float newValue = value;
                if(oldValue == newValue)
                {
                    return;
                }
                _value = newValue;
                this.FireEvent("Value", oldValue, newValue);
                RefreshComponent();
            }
        }
        float _value ;

        [FireInitEvent]
        public float Size {
            get {
                return _size;
            }
            set {
                float oldValue = _size;
                float newValue = value;
                if (oldValue == newValue) {
                    return;
                }
                _size = newValue;
                this.FireEvent("Size", oldValue,newValue);
                RefreshComponent();
            }
        }
        float _size ;

        [FireInitEvent]
        public int NumberOfSteps
        {
            get
            {
                return _numberOfSteps;
            }
            set
            {
                int oldValue = _numberOfSteps;
                int newValue = value;
                if (oldValue == newValue)
                {
                    return;
                }
                _numberOfSteps = newValue;
                this.FireEvent("NumberOfSteps", oldValue, newValue);
                RefreshComponent();
            }
        }
        int _numberOfSteps;

        /// <summary>
        /// 初始化View组件
        /// </summary>
        /// <param name="uiBehaviour"></param>
        public override void InitView()
        {
            if (this.UIComponent == null)
            {
                this.UIComponent = GetComponent<Scrollbar>();
            }

            if (!(this.UIComponent is Scrollbar))
            {
                throw new Buskit3DException("ScrollbarViewModel#InitView[Incorrect component type]");
            }

            Scrollbar scrollbar = (Scrollbar)this.UIComponent;
            scrollbar.onValueChanged.AddListener((value) =>
            {
                onValueChanged(value);
            });

            this.Value = scrollbar.value;
        }

        /// <summary>
        /// 广播ScrollBar的滑动事件
        /// </summary>
        protected void onValueChanged(float value)
        {
            Value = value;
        }

        /// <summary>
        /// 更新组件界面显示
        /// </summary>
        protected override void RefreshComponent()
        {
            Scrollbar scrollbar = (Scrollbar)UIComponent;
            if(scrollbar.value != Value)
            {
                scrollbar.value = Value;
            }
            //由于在ScrollView中对Slider的封装操作，暂时关闭对scrollbar.size和scrollbar.numberOfSteps的更新
            //if (scrollbar.size != Size) {
            //    scrollbar.size = Size;
            //}
            //if (scrollbar.numberOfSteps != NumberOfSteps) {
            //    scrollbar.numberOfSteps = NumberOfSteps;
            //}
        }
    }
}
