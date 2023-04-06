/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：InputFieldViewModel
* 创建日期：2020-08-12 15:02:48
* 作者名称：
* 功能描述：Text的ViewModel组件
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// InputField的ViewModel组件
    /// </summary>
    public class InputFieldViewModel : ViewModel
    {
        /// <summary>
        /// 获取或设置InputField组件的值
        /// </summary>
        [FireInitEvent]
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                string oldValue = _value;
                string newValue = value;
                if (oldValue.Equals(newValue))
                {
                    return;
                }
                _value = newValue;
                this.FireEvent("Value", oldValue, newValue);
                RefreshComponent();
            }
        }
        string _value = "";

        /// <summary>
        /// 初始化View组件
        /// </summary>
        /// <param name="uiBehaviour"></param>
        public override void InitView()
        {
            if (this.UIComponent == null)
            {
                this.UIComponent = GetComponent<InputField>();
            }

            if (!(this.UIComponent is InputField))
            {
                throw new Buskit3DException("TextViewModel#InitView[Incorrect component type]");
            }

            InputField input = (InputField)this.UIComponent;
            input.onValueChanged.AddListener((value) =>
            {
                this.onValueChanged(value);
            });
            Value = input.text;
        }

        /// <summary>
        /// 广播输入事件
        /// </summary>
        protected void onValueChanged(string value)
        {
            Value = value;
        }

        /// <summary>
        /// 更新组件界面
        /// </summary>
        protected override void RefreshComponent()
        {
            InputField input = (InputField)this.UIComponent;
            if (!input.text.Equals(Value))
            {
                input.text = Value;
            }
        }
    }
}
