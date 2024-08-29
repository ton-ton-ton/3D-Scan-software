<!--底下標籤來源參考寫法可至：https://github.com/Envoy-VC/awesome-badges#github-stats -->


>技術架構 <div style="text-align:center;vertical-align:bottom">![](https://img.shields.io/badge/語法-C%23-blue) &nbsp; ![](https://img.shields.io/badge/框架-.Net&nbsp;Framework&nbsp;4.8-blue) &nbsp; ![](https://img.shields.io/badge/作業系統-Windows-blue) &nbsp; ![](https://img.shields.io/badge/UI-Windos&nbsp;Forms-blue)</div>

## 介紹

![專案封面圖](doc/MainForm.png)

主要功能為**即時顯示掃描時的點雲數據**，其他輔助功能還包含**串口序列埠、串口圖像序列埠、光學位移感測之影像輸出、讀取各類型點雲數據**以及**串口控制感測設備**，下列將依上圖中顯示的各區塊詳述其功能：
> ps. 需與本研究自主開發的 3D 點雲擷取設備搭配使用

### 其他輔助工具區
- 讀取各類型點雲數據：可讀取常見的點雲儲存格式，例如 | * *.ply |* * *.obj* | *  *.pcd* | * *.txt*。

- 串口序列埠：即時顯示串口上讀取到的數據資料。

- 串口圖像序列埠：即時將串口上的數據資料以**折線圖**的圖表方式呈現。

- 光學位移感測之影像輸出：讀取滑鼠光學位移感測器上的即時影像，目前僅接受 30 X 30 像素 (pixel) 的畫面輸出。

- 數據分析表：針對感興趣的數據來顯示。


### 裝置功能區
依據不同掃描流程，此區會顯示不同功能頁面


|  | <div style="text-align:center; vertical-align:bottom;">功能描述</div>|
| ------ | -------- |
| 頁面 1   | 包含串口通訊設置與校正慣性感測器與光學位移感測器的功能按鈕|
| 頁面 2   | 掃描模式切換 (2D、3D)、掃描開始\暫停\清除點雲等功能按鈕、3D 點雲擷取設備|
| 頁面 3   | 點雲數據預處理|
| 頁面 4   | 輸出特定格式的點雲數據檔案|

&emsp;
![不同頁面](doc/SubMode_total.png)
### 狀態顯示
顯示操作本系統的執行日誌，用以確認當下執行流程與程式執行狀態
### 操作流程區
從左至右依序執行點雲掃描流程，從一開始的基礎設定 **`"Setting"`**，接續掃描點雲數據的 **`"Scan"`**，掃描後的點雲數據預處理 **`"Curve/Filting"`**，最後依特定格式輸出的 **`"Output"`**
### 點雲顯示區
顯示操作本系統的執行日誌，用以確認當下執行流程與程式執行狀態


## 第三方開源函式庫
![](https://img.shields.io/badge/Accord-3.8.2-greenyellow) &nbsp; ![](https://img.shields.io/badge/Activiz.NET-5.8.0-greenyellow) &nbsp; ![](https://img.shields.io/badge/FontAwesome.Sharp-6.6.0-greenyellow) &nbsp; ![](https://img.shields.io/badge/OxyPlot-1.0.0-greenyellow) &nbsp; ![](https://img.shields.io/badge/PCL-1.12.0-greenyellow) 

## 聯絡作者、後記

>如有疑問或是建議，歡迎隨時在論壇內發布消息或寄送電子郵件至 kenny3271879@gmail.com。如要回報技術上的錯誤，也可在 Github 上提交問題。

