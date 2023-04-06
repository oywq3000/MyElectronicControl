/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：ToggleViewModel
* 创建日期：2020-08-12 15:02:48
* 作者名称：
* 功能描述：Toggle的ViewModel组件
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// Toggle的ViewModel组件
    /// </summary>
    public class ToggleViewModel : ViewModel
    {
        /// <summary>
        /// 对应Toggle.isOn属性
        /// </summary>
        [FireInitEvent]
        public bool Value
        {
            get
            {
                return _value;
            }
            set
            {
                bool oldValue = _value;
                bool newValue = value;
                if(oldValue == newValue)
                {
                    return;
                }
                _value = newValue;
                this.FireEvent("Value", oldValue, newValue);
                RefreshComponent();
            }
        }
        bool _value = false;

        /// <summary>
        /// 设置Label信息
        /// </summary>
        [FireInitEvent]
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                string oldValue = _label;
                string newValue = value;
                if (oldValue == newValue)
                {
                    return;
                }
                _label = newValue;
                this.FireEvent("Label", oldValue, newValue);
                RefreshComponent();
            }
        }
        string _label;

        /// <summary>
        /// 初始化View组件
        /// </summary>
        /// <param name="uiBehaviour"></param>
        public override void InitView()
        {
            if (this.UIComponent == null)
            {
                this.UIComponent = GetComponent<Toggle>();
            }

            if (!(this.UIComponent is Toggle))
            {
                throw new Buskit3DException("ToggleViewModel#InitView[Incorrect component type]");
            }

            Toggle toggle = (Toggle)this.UIComponent;
            toggle.onValueChanged.AddListener((value) =>
            {
                onValueChanged(value);
            });

            this.Value = toggle.isOn;
        }

        /// <summary>
        /// 广播Button的点击事件
        /// </summary>
        protected void onValueChanged(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// 更新界面组件显示
        /// </summary>
        protected override void RefreshComponent()
        {
            Toggle toggle = (Toggle)UIComponent;

            if (toggle.isOn != Value)
            {
                toggle.isOn = Value;
            }

            if (toggle.GetComponentInChildren<Text>() != null)
            {
                Text text = toggle.GetComponentInChildren<Text>();
                if (!text.text.Equals(Label) && !string.IsNullOrEmpty(Label))
                {
                    text.text = Label;
                }
            }
        }
    }
}
