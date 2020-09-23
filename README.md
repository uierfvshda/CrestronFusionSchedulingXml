# CrestronFusionSchedulingXml
该模块可以读取并分析Crestron Fusion与Processor间通讯内容，并解析为当天全部会议信息，上限可达30条，其中包括每个会议的起止时间、运行状态、主题、组织者信息。

<HR style="FILTER: alpha(opacity=100,finishopacity=0,style=3)" width="80%" color=#987cb9 SIZE=3>

使用说明：
<ol>
<li>将压缩包内所有文件解压至程序目录，在Project Modules中发现该模块。</li>
<li>按照正常编程方式在程序Ethernet下添加Fusion Room。</li>
<li>添加Fusion SSI主模块，并完成与Fusion Room的信号连接。</li>
<li>添加Fusion SSI Scheduling Awareness模块，并配置Scheduler ID，保证Allow Push Registration开启。</li>
<li>添加本帖中的“Fusion_Scheduling”模块并按说明填写所有信号。</li>
</ol>

<HR style="FILTER: alpha(opacity=100,finishopacity=0,style=3)" width="80%" color=#987cb9 SIZE=3>

引脚说明：<br>

参数：<br>
Buffer_Input_Wait_Time: 来自Fusion信息的缓存时间，一般默认即可。<br>
Schedule_Order: 根据需求自己选择，会议信息可按照时间顺序或倒序进行输出。<br>

String input:<br>
Schedule_Response$: 连接Fusion Room中RoomView Schedulingng Data模块下的ScheduleResponse$引脚。<br>

Analog output:<br>
Meeting_Number: 模拟量输出当天会议总数。<br>
Meeting_State_1 - 30: 模拟量输出当前该条会议的状态，0表示未开始，1表示正在进行中，2表示已结束。<br>

String output:<br>
Meeting_Subject_1 - 30: 串量输出会议名称，可直接连接触摸屏。<br>
Meeting_Organizer_1 - 30: 串量输出会议组织者，可直接连接触摸屏。<br>
Meeting_Start_Time_1 - 30: 串量输出会议开始时间。<br>
Meeting_End_Time_1 - 30: 串量输出会议结束时间。 <br>
