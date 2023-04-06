/*******************************************************************************
* 版权声明：科技有限公司，保留所有版权
* 版本声明：v1.0.0
* 类 名 称：UILogic
* 创建日期：2023-04-04 09:04:28
* 作者名称：欧阳
* CLR 版本：4.0.30319.42000
* 修改记录：
* 描述：
******************************************************************************/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Com.Rainier.Buskit3D;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class UILogic : CommonLogic 
{

	#region Context Evironment
	//homePage bg
	private MvvmContext _homePageContext;
	//Top operation panel
	private MvvmContext _topPanelContext;
	//Mode selection
	private MvvmContext _modeSelectionContext;
	//Study mode
	private MvvmContext _studyModeContext;
	//Exam mode
	private MvvmContext _examModeContext;
	#endregion

	#region Panel Stack

	private Stack<Transform> _paneStack = new Stack<Transform>();
	
	#endregion

	#region HomePage Component

	private ButtonViewModel _startBtn;
	#endregion
	#region ModeSelection Component

	private ButtonViewModel _studyModeBtn;
	private ButtonViewModel _examModeBtn;
	#endregion

	#region TopOperationPanel

	//top side
	private ButtonViewModel _fullScreenBtn;
	private ButtonViewModel _helpBtn;
	private ButtonViewModel _settingBtn;
	private ButtonViewModel _homePageBtn;
	
	private ImageViewModel _maskPanel;
	#endregion
	//right side
	private ButtonViewModel _returnLevelBtn;
	//pop panel
	private Transform _returnHomePanel;
	private ButtonViewModel _confirmReturnHomePanel;
	private ButtonViewModel _cancelReturnHomePanel;
	
	private Transform _settingPanel;
	private SliderViewModel _lightAdjustSlider;
	private TextViewModel _lightPercentageText;
	private ButtonViewModel _applySettingBtn;

	private Transform _operationHelpPanel;
	private ButtonViewModel _operationHelpBtn;

	
	/// <summary>
    /// MVVM上下文环境
    /// </summary>
    MvvmContext context;
		
    /// <summary>
    /// 获取MVVM上下文环境
    /// </summary>
    protected override void Awake()
    {
         base.Awake();
         
         #region Init Context
         _homePageContext = transform.Find("HomePage").GetComponent<MvvmContext>();
         _topPanelContext= transform.Find("TopOperation").GetComponent<MvvmContext>();
         _modeSelectionContext= transform.Find("ModeSelection").GetComponent<MvvmContext>();
        
         _studyModeContext= transform.Find("StudyMode").GetComponent<MvvmContext>();
         _examModeContext = transform.Find("ExamMode").GetComponent<MvvmContext>();
         #endregion
    }

    private IEnumerator Start()
    {
	    //delay one frame
	    yield return null;
	    //HomePage
	    _startBtn = _homePageContext.FindViewModel<ButtonViewModel>("StartBtn");
	    //ModeSelection
	    _studyModeBtn = _modeSelectionContext.transform.Find("EntryStudyMode").GetComponent<ButtonViewModel>();
	    Debug.Log("Find:EntryExamMode"+_studyModeBtn);
	    _examModeBtn = _modeSelectionContext.transform.Find("EntryExamMode").GetComponent<ButtonViewModel>();

	    #region Top Operation
		//Top side
		_fullScreenBtn = _topPanelContext.FindViewModel<ButtonViewModel>("Extend");
		_helpBtn = _topPanelContext.FindViewModel<ButtonViewModel>("Help");
		_settingBtn = _topPanelContext.FindViewModel<ButtonViewModel>("Setting");
		_homePageBtn = _topPanelContext.FindViewModel<ButtonViewModel>("MainPanel");
	    
		//right side
		_returnLevelBtn =  _topPanelContext.FindViewModel<ButtonViewModel>("Return");

	    //pop message
	    _returnHomePanel = _topPanelContext.transform.Find("TopMessage/ReturnMenu");
	    _confirmReturnHomePanel = _topPanelContext.FindViewModel<ButtonViewModel>("Confirm");
	    _cancelReturnHomePanel = _topPanelContext.FindViewModel<ButtonViewModel>("Cancel");
	    _operationHelpPanel = _topPanelContext.transform.Find("TopMessage/HelpPanel");
	    _operationHelpBtn = _operationHelpPanel.transform.Find("Confirm").GetComponent<ButtonViewModel>();
	    _settingPanel = _topPanelContext.transform.Find("TopMessage/SystemSetting");
	    _lightAdjustSlider = _topPanelContext.FindViewModel<SliderViewModel>("LightAdaptionSlider");
	    _applySettingBtn = _topPanelContext.FindViewModel<ButtonViewModel>("ApplyBtn");
	    _lightPercentageText = _topPanelContext.FindViewModel<TextViewModel>("Percentage");
	    _maskPanel = transform.Find("GlobalMask").GetComponent<ImageViewModel>();
	    #endregion
	    
	    
	    yield return null;
	    //Init Stack and panel
	    _paneStack.Push(_homePageContext.transform); //homepage push to stack
	    _modeSelectionContext.gameObject.SetActive(false);
	    _studyModeContext.gameObject.SetActive(false);
	    _examModeContext.gameObject.SetActive(false);
    }

    /// <summary>
    /// 处理业务逻辑
    /// </summary>
    /// <param name="evt"></param>
    public override void ProcessLogic(IEvent evt)
    {
        //忽略所有属性初始化事件、OnDiable事件、有OnDestroy事件、有OnEnable事件
        if ((evt is PropertyInitEvent) || evt.EventName.Equals("OnDisable")|| evt.EventName.Equals("OnDestroy") || evt.EventName.Equals("OnEnable"))	return;

        //忽略除ViewModel外其他Entity发出的事件
        if (!(evt.EventSource is ViewModel))	return;

		//获取事件源
        ViewModel vm = evt.EventSource as ViewModel;
        Debug.Log("Business ViewModel Event:"+vm);

        if (vm is ButtonViewModel)
        {
	      
	        //homePage
	        if (vm.Equals(_startBtn))
	        {
		        OpenPanel(_modeSelectionContext.transform);
		        return;
	        }
	        //ModeSelection
	        Debug.Log("current:"+vm+" "+"target:"+_studyModeBtn);
	        if (vm.Equals(_studyModeBtn))
	        {
		        OpenPanel(_studyModeContext.transform);
		        return;
	        }
	        if (vm.Equals(_examModeBtn))
	        {
		        OpenPanel(_examModeContext.transform);
		        return;
	        }

	        #region TopOperationPanel Business
	        //return 
	        if (vm.Equals(_returnLevelBtn))
	        {
		        OpenPanel(_returnHomePanel.transform,false);
		        return;
	        }
	        
	        //entry a tip panel that confirm return 
	        if (vm.Equals(_homePageBtn))
	        {
		        OpenPanel(_returnHomePanel.transform,false);
		        return;
	        }
	        //confirm return homePage
	        if (vm.Equals(_confirmReturnHomePanel))
	        {
		        while (!_paneStack.Peek().Equals(_homePageContext.transform))
		        {
			        ClosePanel();
		        }
		        return;
	        }

	        //cancel return home panel
	        if (vm.Equals(_cancelReturnHomePanel))
	        {
		        ClosePanel();
		        return;
	        }
	        
	        //system Setting: light adjust, light percentage, sensitive adjust
	        if (vm.Equals(_settingBtn))
	        {
		        OpenPanel(_settingPanel.transform,false);
		        return;
	        }

	        if (vm.Equals(_applySettingBtn))
	        {
		        ClosePanel();
		        return;
	        }
	        
	        //helpPanel
	   
	        if (vm.Equals(_helpBtn))
	        {
		        OpenPanel(_operationHelpPanel.transform,false);
		        return;
	        }

	      
	        if (vm.Equals(_operationHelpBtn))
	        {
		        ClosePanel();
		        return;
	        }
	        //full screen and non_full screen
	        if (vm.Equals(_fullScreenBtn))
	        {
		        if (Screen.fullScreen)
		        {
			        //switch to window mode
			        Screen.fullScreenMode = FullScreenMode.Windowed;
		        }
		        else
		        {
			        //switch to full screen mode
			        Resolution[] resolutions = Screen.resolutions;
			        Screen.SetResolution(resolutions[resolutions.Length-1].width,
				        resolutions[resolutions.Length-1].height,FullScreenMode.FullScreenWindow);
		        }
		        return;
	        }
	        #endregion
        }
        else if (vm is SliderViewModel)
        {
	        //  light adjust, light percentage,

	        if (vm.Equals(_lightAdjustSlider))
	        {
		        _maskPanel.GetComponent<Image>().color
			        = new Color(0, 0, 0, (float)((1- _lightAdjustSlider.Value)*0.7));
		        _lightPercentageText.GetComponent<Text>().text
			        = (int)(_lightAdjustSlider.Value *100)+ "%";
	        }
        }
    }

    #region Event Handle Method

    /// <summary>
    /// Open new panel method
    /// </summary>
    /// <param name="newPanel">new Panel</param>
    /// <param name="isCloseThis">whether need to close current panel</param>
    private void OpenPanel(Transform newPanel,bool isCloseCurrent = true)
    {
	    if (isCloseCurrent&&_paneStack.Count>0)
	    {
		    _paneStack.Peek().gameObject.SetActive(false);
	    }
	    newPanel.gameObject.SetActive(true);
	    if (!_paneStack.Contains(newPanel))
	    {
		    _paneStack.Push(newPanel);
	    }
    }

    /// <summary>
    /// return stack element and close it and shows the last panel whether you close it
    /// </summary>
    private void ClosePanel()
    {
	    if (_paneStack.Count>1)
	    {
		    _paneStack.Pop().gameObject.SetActive(false);
		    _paneStack.Peek().gameObject.SetActive(true);
	    }
    }
    
    #endregion
    
  
}
