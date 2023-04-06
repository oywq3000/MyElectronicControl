/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：ImageViewModel
* 创建日期：2020-08-12 15:02:48
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
    public class ImageViewModel : ViewModel
    {
        /// <summary>
        /// 初始化View组件
        /// </summary>
        /// <param name="uiBehaviour"></param>
        public override void InitView()
        {
            if (this.UIComponent == null)
            {
                this.UIComponent = GetComponent<Image>();
            }

            if (!(this.UIComponent is Image))
            {
                throw new Buskit3DException("ImageViewModel#InitView[Incorrect component type]");
            }

            Image img = (Image)this.UIComponent;
         }
    }
}
