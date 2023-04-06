/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：ToggleGroupViewModel
* 创建日期：2020-08-12 15:02:48
* 作者名称：
* 功能描述：ToggleGroup的ViewModel组件
* 修改记录：
* 描述：
******************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D
{
    /// <summary>
    /// ToggleGroup的ViewModel组件
    /// </summary>
    public class ToggleGroupViewModel : ViewModel
    {

        /// <summary>
        /// 获取所有活动的Toggle
        /// </summary>
        public IEnumerable<Toggle> ActiveToggles
        {
            get
            {
                ToggleGroup grp = (ToggleGroup)UIComponent;
                return grp.ActiveToggles();
            }
        }

        /// <summary>
        /// 是否有Toggle处于On状态
        /// </summary>
        public bool AnyTogglesOn
        {
            get
            {
                ToggleGroup grp = (ToggleGroup)UIComponent;
                return grp.AnyTogglesOn();
            }
        }

        /// <summary>
        /// 初始化View组件
        /// </summary>
        /// <param name="uiBehaviour"></param>
        public override void InitView()
        {
            if (this.UIComponent == null)
            {
                this.UIComponent = GetComponent<ToggleGroup>();
            }

            if (!(this.UIComponent is ToggleGroup))
            {
                throw new Buskit3DException("ToggleGroupViewModel#InitView[Incorrect component type]");
            }
        }

        /// <summary>
        /// 设置所有Toggle处于关闭状态并广播Method事件
        /// </summary>
        public void SetAllTogglesOff()
        {
            ToggleGroup group = (ToggleGroup)this.UIComponent;
            group.SetAllTogglesOff();
            this.FireEvent("SetAllTogglesOff", new object[] { });
        }
    }
}
