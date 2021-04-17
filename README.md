# csharp-ShellcodeLoader
基于csharp实现的免杀shellcode加载器


### Step

把生成的`shellcode`直接放进去，然后将转换后的代码放进一个文本文件中。

![](https://github.com/AirEvan/csharp-ShellcodeLoader/blob/main/images/image1.png)  
  
直接将保存的文本文件当作参数传递给`Loader`即可。

![](https://github.com/AirEvan/csharp-ShellcodeLoader/blob/main/images/image2.png)  

然后就可以看到已经上线了。

![](https://github.com/AirEvan/csharp-ShellcodeLoader/blob/main/images/image3.png)

目前经测试能绕过以下杀毒软件最新病毒版本。`Defender`起初测试还没问题，但现在只能静态绕过，所以要想`bypass`需要进行稍许修改。

![](https://github.com/AirEvan/csharp-ShellcodeLoader/blob/main/images/image4.png)