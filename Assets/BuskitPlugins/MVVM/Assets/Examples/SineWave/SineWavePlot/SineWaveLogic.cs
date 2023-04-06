/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：SineWaveLogic
* 创建日期：2020-08-12 15:02:48
* 作者名称：
* 功能描述：处理正弦波业务逻辑
* 修改记录：
* 描述：
******************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Rainier.Buskit3D.Example.SineWavePlot
{
    /// <summary>
    /// 处理正弦波业务逻辑
    /// </summary>
    public class SineWaveLogic : CommonLogic
    {
        /// <summary>
        /// 数据最大点数
        /// </summary>
        const int MaxDataCount = 1024;

        /// <summary>
        /// 原始数据
        /// </summary>
        public List<Vector3> datas = new List<Vector3>();

        /// <summary>
        /// 打印数据
        /// </summary>
        List<Vector2> plotDatas = new List<Vector2>();

        /// <summary>
        /// MVVM上下文环境
        /// </summary>
        private MvvmContext Context;

        /// <summary>
        /// 波形面板
        /// </summary>
        ImageViewModel wavePanel;

        /// <summary>
        /// XDiv调节
        /// </summary>
        SliderViewModel viewModelXDiv;

        /// <summary>
        /// YDiv调节
        /// </summary>
        SliderViewModel viewModelYDiv;

        /// <summary>
        /// 重置按钮
        /// </summary>
        ButtonViewModel viewModelResetButton;

        /// <summary>
        /// 初始化LineRenderer和数据
        /// </summary>
        private void Start()
        {
            //获取MVVM上下文环境和相关ViewModel组件
            Context = GetComponent<MvvmContext>();
            viewModelXDiv = Context.FindViewModel<SliderViewModel>("SliderXDiv");
            viewModelYDiv = Context.FindViewModel<SliderViewModel>("SliderYDiv");
            viewModelResetButton = Context.FindViewModel<ButtonViewModel>("ButtonReset");
            wavePanel = Context.FindViewModel<ImageViewModel>("WavePanel");

            //生成正弦波数据并生成打印数据
            GenerateData();
            Recalculate();
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

            //判断事件源物体名称如果为SliderXDiv则对xDiv赋值并重新绘制波形
            if (evt.EventSource.Equals(viewModelXDiv))
            {
                SliderViewModel svm = (SliderViewModel)evt.EventSource;
                TextViewModel tvm = Context.FindViewModel<TextViewModel>("TextXDiv");
                tvm.Value = "X/DIV:" + svm.Value;
                Recalculate();
                return;
            }

            //判断事件源物体名称如果为SliderYDiv则对yDiv赋值并重新绘制波形
            if (evt.EventSource.Equals(viewModelYDiv))
            {
                SliderViewModel svm = (SliderViewModel)evt.EventSource;
                TextViewModel tvm = Context.FindViewModel<TextViewModel>("TextYDiv");
                tvm.Value = "Y/DIV:" + svm.Value;
                Recalculate();
                return;
            }

            //如果是Reset按钮发出的事件
            if (evt.EventSource.Equals(viewModelResetButton))
            {
                SliderViewModel[] svms = Context.FindViewModels<SliderViewModel>();
                foreach (SliderViewModel svm in svms)
                {
                    if (svm.gameObject.name.Equals("SliderXDiv"))
                    {
                        svm.Value = 1.0f;
                        TextViewModel tvm = Context.FindViewModel<TextViewModel>("TextXDiv");
                        tvm.Value = tvm.Value = "X/DIV:" + svm.Value;
                    }
                    else if (svm.gameObject.name.Equals("SliderYDiv"))
                    {
                        svm.Value = 0.2f;
                        TextViewModel tvm = Context.FindViewModel<TextViewModel>("TextYDiv");
                        tvm.Value = "Y/DIV:" + svm.Value;
                    }
                }
                Recalculate();
            }
        }

        /// <summary>
        /// 重新计算打印数据
        /// </summary>
        private void Recalculate()
        {
            plotDatas.Clear();
            Image img = wavePanel.gameObject.GetComponent<Image>();
            foreach (Vector2 v2 in datas)
            {
                float newX = v2.x / viewModelXDiv.Value + img.rectTransform.position.x - img.rectTransform.rect.width / 2;
                float newY = v2.y / viewModelYDiv.Value + img.rectTransform.position.y;
                plotDatas.Add(new Vector2(newX, newY));
            }
        }

        /// <summary>
        /// 绘制打印数据
        /// </summary>
        private void OnGUI()
        {
            for (int i = 0; i < (plotDatas.Count - 1); i++)
            {
                Vector2 pA = plotDatas[i];
                Vector2 pB = plotDatas[i + 1];
                Drawing.DrawLine(pA, pB, Color.red, 1f);
            }
        }

        /// <summary>
        /// 生成正弦波数据
        /// </summary>
        private void GenerateData()
        {
            float time = 0.1f;
            for (int i = 0; i < MaxDataCount; i++)
            {
                time = 0.1f * i;
                float y = 1.5f * Mathf.Sin(time);
                datas.Add(new Vector2(time, y));
            }
        }
    }
}