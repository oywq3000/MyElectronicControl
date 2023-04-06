/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：TextViewModel
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
    /// Text的ViewModel组件
    /// </summary>
    public class TextViewModel : ViewModel
    {
        /// <summary>
        /// 获取或设置Text组件的值
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
                this.UIComponent = GetComponent<Text>();
            }

            if (!(this.UIComponent is Text))
            {
                throw new Buskit3DException("TextViewModel#InitView[Incorrect component type]");
            }

            Text text = (Text)this.UIComponent;           
            Value = text.text;
        }

        /// <summary>
        /// 更新组件界面
        /// </summary>
        protected override void RefreshComponent()
        {
            Text text = (Text)this.UIComponent;
            if (!text.text.Equals(Value))
            {
                text.text = Value;
            }
        }
    }
}
