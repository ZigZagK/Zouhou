#  Zouhou

## 项目介绍

Unity练手项目，弹幕游戏。

可执行文件(exe)在`Zouhou`文件夹下，感兴趣的话可以尝试游玩。

## 开发日志

### v0.1

一个勉强可以玩的版本。

### v0.2

增加了开始界面、帮助界面、游戏结束界面。

### v0.3

修复了敌机撤退到屏幕外后并没有销毁的bug。

固定游戏窗口大小，在游戏结束界面增加结算面板。

### v0.4

优化代码结构，增加模式选择界面，以及符卡模式（BOSS模式）。

提高自机子弹透明度，减少视野干扰。

### v0.5

新增4张符卡，添加了激光子弹。

### v0.6

新增子机，可以在选择界面选择子机，与玩家协同攻击。

诱导子机：发射追踪子弹攻击敌机，攻击力较低。

激光子机：发射往上的激光攻击敌机（可贯通，同时攻击直线上的多个敌机），攻击力较高。

修改了敌机移动方式（修正插值的使用方法），解决了之前在目标位置抖动的情况。