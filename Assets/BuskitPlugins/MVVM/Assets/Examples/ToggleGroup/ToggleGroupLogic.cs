/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：ToggleGroupLogic
* 创建日期：2020-08-12 15:02:48
* 作者名称：
* 功能描述：编写一个简单的单项选择题业务逻辑，带有题目切换和判分功能
* 修改记录：
* 描述：
******************************************************************************/

using UnityEngine;

namespace Com.Rainier.Buskit3D.Example.BasicDemo
{
    /// <summary>
    /// 处理选择题业务逻辑
    /// </summary>
    public class ToggleGroupLogic : CommonLogic
    {
        /// <summary>
        /// 选择题实体类
        /// </summary>
        public class ChoiceQuestion : Entity
        {
            public string BodyText;
            public string ChoiceA;
            public string ChoiceB;
            public string ChoiceC;
            public string ChoiceD;
            public string Selected;
            public string CorrectChoice;
        }

        /// <summary>
        /// MVVM上下文环境
        /// </summary>
        MvvmContext     Context;

        /// <summary>
        /// BodyText ViewModel组件
        /// </summary>
        TextViewModel   textBody = null;

        /// <summary>
        /// TitleText ViewModel组件
        /// </summary>
        TextViewModel   textTitle = null;

        /// <summary>
        /// 选项1 ViewModel组件
        /// </summary>
        ToggleViewModel toggleViewModel1 = null;

        /// <summary>
        /// 选项2 ViewModel组件
        /// </summary>
        ToggleViewModel toggleViewModel2 = null;

        /// <summary>
        /// 选项3 ViewModel组件
        /// </summary>
        ToggleViewModel toggleViewModel3 = null;

        /// <summary>
        /// 选项4 ViewModel组件
        /// </summary>
        ToggleViewModel toggleViewModel4 = null;

        /// <summary>
        /// OK按钮 ViewModel组件
        /// </summary>
        ButtonViewModel buttonViewModel = null;

        /// <summary>
        /// toggleGroupViewModel组件
        /// </summary>
        ToggleGroupViewModel toggleGroupViewModel = null;

        /// <summary>
        /// 初始化10个选择题
        /// </summary>
        public ChoiceQuestion[] Questions = new ChoiceQuestion[]
        {
            new ChoiceQuestion()
            {
                BodyText = "选择题1",
                ChoiceA  = "选择题1:选项A",
                ChoiceB  = "选择题1:选项B",
                ChoiceC  = "选择题1:选项C",
                ChoiceD  = "选择题1:选项D",
                Selected = "",
                CorrectChoice = "选择题1:选项C",
            },

            new ChoiceQuestion()
            {
                BodyText = "选择题2",
                ChoiceA  = "选择题2:选项A",
                ChoiceB  = "选择题2:选项B",
                ChoiceC  = "选择题2:选项C",
                ChoiceD  = "选择题2:选项D",
                Selected = "",
                CorrectChoice = "选择题2:选项A",
            },

            new ChoiceQuestion()
            {
                BodyText = "选择题3",
                ChoiceA  = "选择题3:选项A",
                ChoiceB  = "选择题3:选项B",
                ChoiceC  = "选择题3:选项C",
                ChoiceD  = "选择题3:选项D",
                CorrectChoice = "选择题3:选项A",
                Selected = ""
            },

            new ChoiceQuestion()
            {
                BodyText = "选择题4",
                ChoiceA  = "选择题4:选项A",
                ChoiceB  = "选择题4:选项B",
                ChoiceC  = "选择题4:选项C",
                ChoiceD  = "选择题4:选项D",
                CorrectChoice = "选择题4:选项C",
                Selected = ""
            },

            new ChoiceQuestion()
            {
                BodyText = "选择题5",
                ChoiceA  = "选择题5:选项A",
                ChoiceB  = "选择题5:选项B",
                ChoiceC  = "选择题5:选项C",
                ChoiceD  = "选择题5:选项D",
                CorrectChoice = "选择题5:选项C",
                Selected = ""
            },

            new ChoiceQuestion()
            {
                BodyText = "选择题6",
                ChoiceA  = "选择题6:选项A",
                ChoiceB  = "选择题6:选项B",
                ChoiceC  = "选择题6:选项C",
                ChoiceD  = "选择题6:选项D",
                Selected = "",
                CorrectChoice = "选择题6:选项D",
            },

            new ChoiceQuestion()
            {
                BodyText = "选择题7",
                ChoiceA  = "选择题7:选项A",
                ChoiceB  = "选择题7:选项B",
                ChoiceC  = "选择题7:选项C",
                ChoiceD  = "选择题7:选项D",
                Selected = "",
                CorrectChoice = "选择题7:选项B",
            },

            new ChoiceQuestion()
            {
                BodyText = "选择题8",
                ChoiceA  = "选择题8:选项A",
                ChoiceB  = "选择题8:选项B",
                ChoiceC  = "选择题8:选项C",
                ChoiceD  = "选择题8:选项D",
                CorrectChoice = "选择题8:选项D",
                Selected = ""
            },

            new ChoiceQuestion()
            {
                BodyText = "选择题9",
                ChoiceA  = "选择题9:选项A",
                ChoiceB  = "选择题9:选项B",
                ChoiceC  = "选择题9:选项C",
                ChoiceD  = "选择题9:选项D",
                CorrectChoice = "选择题9:选项D",
                Selected = ""
            },

            new ChoiceQuestion()
            {
                BodyText = "选择题10",
                ChoiceA  = "选择题10:选项A",
                ChoiceB  = "选择题10:选项B",
                ChoiceC  = "选择题10:选项C",
                ChoiceD  = "选择题10:选项D",
                CorrectChoice = "选择题10:选项D",
                Selected = ""
            },
        };

        /// <summary>
        /// 选择题计数
        /// </summary>
        public int QuestionCounter = 0;

        /// <summary>
        /// 分数
        /// </summary>
        public int Score = 0;

        /// <summary>
        /// 获取选择题组件ViewModel
        /// </summary>
        private void Start()
        {
            //MVVM上下文环境
            Context = GetComponent<MvvmContext>();

            textBody            = Context.FindViewModel<TextViewModel>("TextBody");
            textTitle           = Context.FindViewModel<TextViewModel>("TextTitle");
            toggleViewModel1    = Context.FindViewModel<ToggleViewModel>("Toggle1");
            toggleViewModel2    = Context.FindViewModel<ToggleViewModel>("Toggle2");
            toggleViewModel3    = Context.FindViewModel<ToggleViewModel>("Toggle3");
            toggleViewModel4    = Context.FindViewModel<ToggleViewModel>("Toggle4");
            buttonViewModel     = Context.FindViewModel<ButtonViewModel>("ButtonOk");
            toggleGroupViewModel= Context.FindViewModel<ToggleGroupViewModel>("Selection");

            //初始化题目
            InitQuestion(QuestionCounter++);
        }

        /// <summary>
        /// 初始化题目
        /// </summary>
        /// <param name="index"></param>
        public void InitQuestion(int index)
        {
            toggleGroupViewModel.SetAllTogglesOff();

            ChoiceQuestion cq = Questions[index];
            textBody.Value = cq.BodyText;
            toggleViewModel1.Label = cq.ChoiceA;
            toggleViewModel2.Label = cq.ChoiceB;
            toggleViewModel3.Label = cq.ChoiceC;
            toggleViewModel4.Label = cq.ChoiceD;

            toggleViewModel1.Value = false;
            toggleViewModel2.Value = false;
            toggleViewModel3.Value = false;
            toggleViewModel4.Value = false;
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

            //处理OK按钮事件
            if (evt.EventSource.Equals(buttonViewModel) && 
                evt.EventName.Equals("Value"))
            {
                if(QuestionCounter <= Questions.Length - 1)
                {
                    InitQuestion(QuestionCounter);
                    QuestionCounter++;
                }
                else
                {
                    toggleViewModel1.transform.localScale = Vector3.zero;
                    toggleViewModel2.transform.localScale = Vector3.zero;
                    toggleViewModel3.transform.localScale = Vector3.zero;
                    toggleViewModel4.transform.localScale = Vector3.zero;

                    for (int i = 0; i < Questions.Length; i++)
                    {
                        ChoiceQuestion cq = Questions[i];
                        if (cq.CorrectChoice.Equals(cq.Selected))
                        {
                            Score += 10;
                        }
                    }
                    textBody.Value = "您的成绩是：" + Score;
                }
                
                return;
            }

            //处理选项1Toggle组件事件
            if(evt.EventSource.Equals(toggleViewModel1) && 
                evt.EventName.Equals("Value"))
            {
                if (toggleViewModel1.Value && QuestionCounter < Questions.Length)
                {
                    this.Questions[QuestionCounter].Selected = 
                        this.Questions[QuestionCounter].ChoiceA;
                }
                return;
            }

            //处理选项2Toggle组件事件
            if (evt.EventSource.Equals(toggleViewModel2) && 
                evt.EventName.Equals("Value"))
            {
                if (toggleViewModel2.Value && QuestionCounter < Questions.Length)
                {
                    this.Questions[QuestionCounter].Selected = 
                        this.Questions[QuestionCounter].ChoiceB;
                }
                return;
            }

            //处理选项3Toggle组件事件
            if (evt.EventSource.Equals(toggleViewModel3) && 
                evt.EventName.Equals("Value"))
            {
                if (toggleViewModel3.Value && QuestionCounter < Questions.Length)
                {
                    this.Questions[QuestionCounter].Selected = 
                        this.Questions[QuestionCounter].ChoiceC;
                }
                return;
            }

            //处理选项4Toggle组件事件
            if (evt.EventSource.Equals(toggleViewModel4) && 
                evt.EventName.Equals("Value"))
            {
                if (toggleViewModel4.Value && QuestionCounter < Questions.Length)
                {
                    this.Questions[QuestionCounter].Selected = 
                        this.Questions[QuestionCounter].ChoiceD;
                }
                return;
            }
        }
    }
}


