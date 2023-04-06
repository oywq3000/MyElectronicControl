/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：ConfigDialogLogic
* 创建日期：2020-10-11 17:02:48
* 作者名称： 
* 功能描述：UGUI中MvvM综合运用
* 修改记录：
* 描述：
******************************************************************************/
using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D.Example.ConfigDialog
{
    /// <summary>
    /// 业务逻辑处理
    /// </summary>
    public class ConfigDialogLogic : CommonLogic
    {

        /// <summary>
        /// MVVM上下文环境
        /// </summary>
        MvvmContext context;

        /// <summary>
        /// 音量值text
        /// </summary>
        public TextViewModel volumeValue;

        /// <summary>
        /// 信息显示Text;
        /// </summary>
        public Text infoText;

        /// <summary>
        /// 获取MVVM上下文环境
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            context = GetComponent<MvvmContext>();
        }


        /// <summary>
        /// 业务逻辑处理函数
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(IEvent evt)
        {

            //忽略Diable事件、有Destroy事件
            if (evt.EventName.Equals("OnDisable") || evt.EventName.Equals("OnDestroy"))
            {
                return;
            }

            //忽略除ViewModel外其他Entity发出的事件
            if (!(evt.EventSource is ViewModel))
            {
                return;
            }

            //获取事件源
            ViewModel vm = evt.EventSource as ViewModel;

            if (evt is PropertyInitEvent)
            {
                if (vm.name.Equals("音量Slider"))
                {
                    volumeValue.Value = ((PropertyInitEvent)evt).NewValue.ToString();
                }
            }
            else if (evt is PropertyEvent)
            {
                if (vm.name.Equals("音量Slider"))
                {
                    volumeValue.Value = ((PropertyEvent)evt).NewValue.ToString();
                }

                if (vm.name.Equals("Toggle1"))
                {
                    infoText.text = "玩家选择了中文";
                }

                if (vm.name.Equals("Toggle2"))
                {
                    infoText.text = "玩家选择了英文";
                }

                if (vm.name.Equals("区服Dropdown"))
                {
                    var index = (int)((PropertyEvent)evt).NewValue;
                    switch (index)
                    {
                        case 0:
                            infoText.text = "玩家选择了艾欧尼亚区服";
                            break;
                        case 1:
                            infoText.text = "玩家选择了黑色玫瑰区服";
                            break;
                        case 2:
                            infoText.text = "玩家选择了水晶之恒区服";
                            break;
                    }
                }
            }
        }
    }
}
