# amazonWebServices.ec2.csharp.net
This is a C# .NET 7 with AWS SDK EC2 (3.7.137.1) nuget for AWS EC2 instance start and stop WinForm app
## amazonWebServices.ec2.csharp.net
**AWS (Amazon Web Services) ec2 instances start and stop (running and stopping) by c# .NET 7**

- [x] --- 繁體中文版 ---

    此程的功能為提供使用 Amazon Web Service (AWS) 的 EC2 服務的使用者一個簡便的使用者界面
    能夠快速方便地開或/關機 EC2 Instance 執行個體 (虛擬機器)。
    附註: 此程式會使用 AWS CLI 的 AWS Configure 好的 default Profile 來載入使用者權限。
    同時在連線EC2伺服器時會建立暫存一個一個客製的文字檔放在 %userprofile%\.aws\.cached 目錄
    下以 instance-id 命名副檔名為.json 的設定檔。

    ![關機狀態](https://github.com/spectreConstantine/amazonWebServices.ec2.python/blob/master/2020-04-27_094454.png)


- [x] --- 程式使用說明如下 ---
        
    * 要用aws cli設定 AWS Configure, 這樣會在 %userprofile%/.aws/產生 credentials 這個檔案。
      * [profile name]
      * aws_access_key_id = AKcccccccxxxxxxxx4I
      * aws_secret_access_key = nxxx88xxxxxxud2KAxm
      * region=ap-northeast-1
      * output=json
