/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：DropdownViewModel
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
    /// Dropdown的ViewModel组件，发送dropdown.onValueChanged事件
    /// </summary>
    public class DropdownViewModel : ViewModel
    {
        /// <summary>
        /// 发送Click事件
        /// </summary>
        [FireInitEvent]
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                int oldValue = _value;
                int newValue = value;
                if (oldValue == newValue)
                {
                    return;
                }
                _value = newValue;
                this.FireEvent("Value", oldValue, newValue);
                RefreshComponent();
            }
        }
        int _value;


        /// <summary>
        /// 初始化View组件
        /// </summary>
        /// <param name="uiBehaviour"></param>
        public override void InitView()
        {
            if (this.UIComponent == null)
            {
                this.UIComponent = GetComponent<Dropdown>();
            }

            if (!(this.UIComponent is Dropdown))
            {
                throw new Buskit3DException("DropdownViewModel#InitView[Incorrect component type]");
            }

            Dropdown dropdown = (Dropdown)this.UIComponent;
            dropdown.onValueChanged.AddListener((value) =>
            {
                onValueChanged(value);
            });
            
            this.Value = dropdown.value;
        }

        /// <summary>
        /// 广播ScrollBar的滑动事件
        /// </summary>
        protected void onValueChanged(int value)
        {
            Value = value;
        }                

        /// <summary>
        /// 更新组件界面显示
        /// </summary>
        protected override void RefreshComponent()
        {
            Dropdown dropdown = (Dropdown)UIComponent;
            if (dropdown.value != Value)
            {
                dropdown.value = Value;
            }
        }
    }    
}
