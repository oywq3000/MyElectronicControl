using Com.Rainier.Buskit3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnapsackLogic : CommonLogic
{
    /// <summary>
    /// 上下文环境
    /// </summary>
    MvvmContext context;

    /// <summary>
    /// 选项All ViewModel组件
    /// </summary>
    ToggleViewModel itemALL = null;

    /// <summary>
    /// 选项A ViewModel组件
    /// </summary>
    ToggleViewModel itemA = null;

    /// <summary>
    /// 选项B ViewModel组件
    /// </summary>
    ToggleViewModel itemB = null;

    /// <summary>
    /// 选项C ViewModel组件
    /// </summary>
    ToggleViewModel itemC = null;

    /// <summary>
    /// 选项D ViewModel组件
    /// </summary>
    ToggleViewModel itemD = null;

    /// <summary>
    /// toggleGroupViewModel组件
    /// </summary>
    ToggleGroupViewModel toggleGroupViewModel = null;

    /// <summary>
    /// InputFieldViewModel组件
    /// </summary>
    InputFieldViewModel InputFieldViewModel = null;

    /// <summary>
    /// ButtonViewModel组件
    /// </summary>
    ButtonViewModel searchBtn = null;


    /// <summary>
    /// 界面上的MVVM组件
    /// </summary>
    string viewModels = "";

    /// <summary>
    /// 获取MVVM上下文环境
    /// </summary>
    IEnumerator Start()
    {
        context = GetComponent<MvvmContext>();
        yield return null;
        toggleGroupViewModel = context.FindViewModel<ToggleGroupViewModel>("Select");
        itemALL = context.FindViewModel<ToggleViewModel>("ALL");
        itemA   = context.FindViewModel<ToggleViewModel>("A");
        itemB   = context.FindViewModel<ToggleViewModel>("B");
        itemC   = context.FindViewModel<ToggleViewModel>("C");
        itemD   = context.FindViewModel<ToggleViewModel>("D");
        searchBtn = context.FindViewModel<ButtonViewModel>("SearchBtn");
        InputFieldViewModel = context.FindViewModel<InputFieldViewModel>("SearchInputField");
    }

    /// <summary>
    /// 处理业务逻辑
    /// </summary>
    /// <param name="evt"></param>
    public override void ProcessLogic(IEvent evt)
    {
        //忽略所有属性初始化事件、Diable事件、有Destroy事件
        if ((evt is PropertyInitEvent) ||
            evt.EventName.Equals("OnDisable") ||
            evt.EventName.Equals("OnDestroy"))
        {
            return;
        }

        //忽略除ViewModel外其他Entity发出的事件
        if (!(evt.EventSource is ViewModel))
        {
            return;
        }

        //处理选项itemALL组件事件
        if (evt.EventSource.Equals(itemALL))
        {
            if (itemALL.Value)
            {
                Search("ALL");
            }
        }
        //处理选项itemA组件事件
        if (evt.EventSource.Equals(itemA))
        {
            if (itemA.Value)
            {
                Search("A");
            }
        }
        //处理选项itemB组件事件
        if (evt.EventSource.Equals(itemB))
        {
            if (itemB.Value)
            {
                Search("B");
            }
        }
        //处理选项itemC组件事件
        if (evt.EventSource.Equals(itemC))
        {
            if (itemC.Value)
            {
                Search("C");
            }
        }
        //处理选项itemD组件事件
        if (evt.EventSource.Equals(itemD))
        {
            if (itemD.Value)
            {
                Search("D");
            }
        }
        //处理选项Button组件事件
        if (evt.EventSource.Equals(searchBtn) && evt.EventName.Equals("Value"))
        {
            Search(InputFieldViewModel.Value);
        }
    }

    private void Search(string value) {
        Transform trans = transform.Find("Content");
        switch (value) {

            case "ALL":
                for (int i = 0; i < trans.childCount; i++)
                {
                    trans.GetChild(i).gameObject.SetActive(true);
                }
                break;

            case "A":
                for (int i = 0; i < trans.childCount; i++) {
                    if (trans.GetChild(i).name.ToCharArray()[0].Equals('A')) {
                        trans.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                        trans.GetChild(i).gameObject.SetActive(false);
                }
                break;

            case "B":
                for (int i = 0; i < trans.childCount; i++)
                {
                    if (trans.GetChild(i).name.ToCharArray()[0].Equals('B'))
                    {
                        trans.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                        trans.GetChild(i).gameObject.SetActive(false);
                }
                break;

            case "C":
                for (int i = 0; i < trans.childCount; i++)
                {
                    if (trans.GetChild(i).name.ToCharArray()[0].Equals('C'))
                    {
                        trans.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                        trans.GetChild(i).gameObject.SetActive(false);
                }
                break;

            case "D":
                for (int i = 0; i < trans.childCount; i++)
                {
                    if (trans.GetChild(i).name.ToCharArray()[0].Equals('D'))
                    {
                        trans.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                        trans.GetChild(i).gameObject.SetActive(false);
                }
                break;
            default:
                for (int i = 0; i < trans.childCount; i++)
                {
                    if (trans.GetChild(i).name.Equals(value))
                    {
                        trans.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                        trans.GetChild(i).gameObject.SetActive(false);
                }
                break;
        }
    }
}
