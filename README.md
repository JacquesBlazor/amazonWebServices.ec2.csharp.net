# amazonWebServices.ec2.csharp.net
This is a C# .NET 7 with AWS SDK EC2 (3.7.137.1) nuget for AWS EC2 instance start and stop WinForm app
## amazonWebServices.ec2.csharp.net
**AWS (Amazon Web Services) ec2 instances start and stop (running and stopping) by c# .NET 7**

- [x] --- 繁體中文版 ---

    此程式的功能為提供使用 Amazon Web Service (AWS) 的 EC2 服務的使用者一個簡便的使用者界面，
    能夠快速方便地開或/關機 EC2 Instance 執行個體 (虛擬機器)。此程式會使用預先透過 AWS CLI 的
    AWS Configure 指令配置好如下使用說明裡的 credentials 的 "default" 設定檔名稱載入使用者權限。

    附註: 在點下連線伺服器按錄時，第一次執行時會詢問連線EC2伺服器的私密金鑰.pem憑證。同時會在本機
    電腦的 %userprofile%\\.aws 目錄下以 設定檔名稱 做為資料夾，在資料夾下再以 instance-id 做為
    檔案名稱，快取暫存一個附檔名為 .json 的檔案。這個客製的文字檔放在 %userprofile%\\.aws\\.cached
    目錄下以 instance-id 命名，副檔名為.json 的設定檔。裡面會將剛才詢問連線EC2伺服器的私密金鑰.pem
    憑證路徑記錄在檔案裡。同時也可以設定 Account 欄位。如果沒有設定會使用 ec2-user 連線伺服器。

    ![開機狀態](https://github.com/JacquesBlazor/amazonWebServices.ec2.csharp.net/blob/main/2023-06-22_183918.png)


- [x] --- 程式使用說明如下 ---
        
    * 要用aws cli設定 AWS Configure, 這樣會在 %userprofile%/.aws/產生 credentials 這個檔案。
      * [profile name]
      * aws_access_key_id = AKcccccccxxxxxxxx4I
      * aws_secret_access_key = nxxx88xxxxxxud2KAxm
      * region=ap-northeast-1
      * output=json
