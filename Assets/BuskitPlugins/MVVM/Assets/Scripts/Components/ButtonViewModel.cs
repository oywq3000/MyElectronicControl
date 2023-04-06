/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：ButtonViewModel
* 创建日期：2020-08-12 15:02:48
* 作者名称：
* 功能描述：Button的ViewModel组件，发送Button.onClick事件
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// Button的ViewModel组件，发送Button.onClick事件
    /// </summary>
    public class ButtonViewModel : ViewModel
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
                if(oldValue == newValue)
                {
                    return;
                }
                this._value = newValue;
                this.FireEvent("Value", oldValue, newValue);
                RefreshComponent();
            }
        }
        private int _value;

        /// <summary>
        /// 初始化View组件
        /// </summary>
        /// <param name="uiBehaviour"></param>
        public override void InitView()
        {
            if (this.UIComponent == null)
            {
                this.UIComponent = GetComponent<Button>();
            }

            if (!(this.UIComponent is Button))
            {
                throw new Buskit3DException("ButtonViewModel#InitView[Incorrect component type]");
            }

            Button btn = (Button)this.UIComponent;
            btn.onClick.AddListener(()=>{
                this.onClickCB();
            });
            Value = -1;
        }

        /// <summary>
        /// 广播Button的点击事件
        /// </summary>
        protected void onClickCB()
        {
            Value += 1;
        }
    }
}
