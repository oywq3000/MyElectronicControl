/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：RawImageViewModel
* 创建日期：2020-04-23 11:38:48
* 作者名称：
* 功能描述：Image的ViewModel组件
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// Image的ViewModel组件
    /// </summary>
    public class RawImageViewModel : ViewModel
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
                this.UIComponent = GetComponent<RawImage>();
            }

            if (!(this.UIComponent is RawImage))
            {
                throw new Buskit3DException("ImageViewModel#InitView[Incorrect component type]");
            }

            RawImage img = (RawImage)this.UIComponent;
         }
    }
}
