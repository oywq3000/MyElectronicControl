# Changelog

## [1.0.0] - 2021-03-12
### Added

* Mvvm基本使用说明


## [1.0.1] - 2021-03-26
### Fixed

 * Bug修复：初始化过程中，若隐藏物体有子物体（包含viewmodel组件），本身没有viewmodel组件时的运行报错问题
 * 文件重命名：将Mvvm/Assets/Application重名为Mvvm/Assets/Examples，统一目录结构
### Added

 * 增加动态生成UI的初始化，添加到MvvmContext的ViewModel容器列表中
 
		//实例化对象
		var _item = Instantiate(item, itemParent) as Transform;
		//动态绑定UI
        context.Initialize(_item);


## [1.0.2] - 2021-04-14
### Fixed

 * 文件删除: 删除MVVM/Assets/Scripts/Editor文件夹以及WebGLInputField脚本
 * Bug修复： 实现ViewModel中的OnDestory()方法，当物体销毁时，将自身从GameObject对象池中删除


## [1.0.3] - 2021-05-15
### Fixed

 * 功能删除: 删除viewmodel及其子类中的[RequireComponent(typeof(UniqueID))]属性
 * 功能删除：删除小窗口显示MVVM初始化选项功能，改为邮件菜单直接初始化

### Added

 * 增加扩展方法： 添加右键一键绑定UniqueID


 ## [1.0.4] - 2021-06-1
### Fixed

 * Bug修复: 修复物体销毁没有从对象池中移除的问题
 * Bug修复: 隐藏物体初始化UniqueID没有赋值

### Added