/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：SliderViewModel
* 创建日期：2020-08-12 15:02:48
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
    public class SliderViewModel: ViewModel
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

        /// <summary>
        /// 初始化View组件
        /// </summary>
        /// <param name="uiBehaviour"></param>
        public override void InitView()
        {
            if (this.UIComponent == null)
            {
                this.UIComponent = GetComponent<Slider>();
            }

            if (!(this.UIComponent is Slider))
            {
                throw new Buskit3DException("SliderViewModel#InitView[Incorrect component type]");
            }

            Slider slider = (Slider)this.UIComponent;
            slider.onValueChanged.AddListener((value) =>
            {
                onValueChanged(value);
            });

            this.Value = slider.value;
        }

        /// <summary>
        /// 广播Button的点击事件
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
            Slider slider = (Slider)UIComponent;
            if(slider.value != Value)
            {
                slider.value = Value;
            }
        }
    }
}
