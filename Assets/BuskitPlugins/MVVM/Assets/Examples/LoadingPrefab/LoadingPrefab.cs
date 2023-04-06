/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：LoadingPrefab
* 创建日期：2020-03-05 14:15:09
* 作者名称：
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：动态加载动态删除
******************************************************************************/
using UnityEngine;
using System.Collections;
using Com.Rainier.Buskit3D;
using Com.Rainier.Buskit.Unity.Architecture.Aop;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Mvvm
{   
	/// <summary>
    /// 
    /// </summary>
	public class LoadingPrefab : CommonLogic 
	{
		/// <summary>
        /// MVVM上下文环境
        /// </summary>
        MvvmContext context;

        public static int index = 0;

        /// <summary>
        /// 添加按钮
        /// </summary>
        private ButtonViewModel addItemView;
        /// <summary>
        /// 删除按钮
        /// </summary>
        private ButtonViewModel subItemView;
        /// <summary>
        /// item选项
        /// </summary>
        public Object item;
        /// <summary>
        /// item的父物体
        /// </summary>
        private Transform itemParent;
        /// <summary>
        /// 所有的viewmodel字典
        /// </summary>
        private Dictionary<int, GameObject> itemDic = new Dictionary<int, GameObject>();
        /// <summary>
        /// 当前选中的GameObject
        /// </summary>
        private List<int> selItem = new List<int>();

        /// <summary>
        /// 获取MVVM上下文环境
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            context = GetComponent<MvvmContext>();
        }

        IEnumerator Start()
        {
            yield return null;
            addItemView = context.FindViewModel<ButtonViewModel>("AddItem");
            subItemView = context.FindViewModel<ButtonViewModel>("SubItem");
            itemParent = GameObject.Find("Content").transform;
        }

        /// <summary>
        /// 处理业务逻辑
        /// </summary>
        /// <param name="evt"></param>
        public override void ProcessLogic(IEvent evt)
        {
            //忽略所有属性初始化事件、Diable事件、有Destroy事件
            if ((evt is PropertyInitEvent) || evt.EventName.Equals("OnDisable") || evt.EventName.Equals("OnDestroy"))	return;

            //忽略除ViewModel外其他Entity发出的事件
            if (!(evt.EventSource is ViewModel))	return;

			//获取事件源
            ViewModel vm = evt.EventSource as ViewModel;

            if (evt.EventSource.Equals(addItemView))
            {
                AddItem();
                return;
            }
            if (evt.EventSource.Equals(subItemView))
            {
                SubItem();
                return;
            }

            if (vm is ToggleViewModel)
            {

                ToggleViewModel toggle1 = context.FindViewModel<ToggleViewModel>("toggle1");
                ToggleViewModel toggleView = vm as ToggleViewModel;

                if (vm.Equals(toggle1))
                {
                    Debug.Log("toggle1");
                }

                //获取Toogle对象的ID
                var index = System.Convert.ToInt32(vm.transform.parent.name);
                //toggle勾选时的操作
                if (toggleView.Value)
                {
                    if (!selItem.Contains(index))
                    {
                        selItem.Add(index);
                    }
                }
                //取消勾选时的操作
                else
                {
                    if (selItem.Contains(index))
                    {
                        selItem.Remove(index);
                    }
                }
            }
        }

        /// <summary>
        /// 动态添加一个Item
        /// </summary>
        private void AddItem()
        {
            index++;
            var _item = Instantiate(item, itemParent) as GameObject;
            _item.name = index.ToString();
            _item.transform.Find("Toggle/Context/Text").GetComponent<Text>().text = _item.name;
            _item.transform.GetChild(0).name = "toggle" + index;
            itemDic.Add(index, _item.gameObject);
            //动态绑定UI
            context.Initialize(_item.transform);
        }

        /// <summary>
        /// 删除当前选中的Item
        /// </summary>
        private void SubItem()
        {
            for(int i=0;i< selItem.Count;i++)
            {
                Destroy(itemDic[selItem[i]]);
            }
            selItem = new List<int>();
        }
    }
}
