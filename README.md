# amazonWebServices.ec2.csharp.net
This is a C# .NET 7 WinForm app with AWS SDK EC2 (3.7.137.1) nuget package for AWS EC2 instance start/stop and connect instance.

## amazonWebServices.ec2.csharp.net
**AWS (Amazon Web Services) ec2 instances start and stop (running and stopping) by c# .NET 7**

- [x] --- 繁體中文版 ---

    此程式的功能為提供使用 Amazon Web Service (AWS) 的 EC2 服務的使用者一個簡便的使用者界面，
    能夠快速方便地開或/關機 EC2 Instance 執行個體 (虛擬機器)，並且可以開啟 cmd prompt 使用 ssh 連線。
    此程式會使用預先透過 AWS CLI 的 AWS Configure 指令配置好如下使用說明裡的 credentials 的 "default"
    設定檔名稱載入使用者權限。

    附註: 在點下連線伺服器按鈕時，第一次執行時會詢問連線EC2伺服器的私密金鑰.pem憑證。同時會在本機
    電腦的 %userprofile%\\.aws 目錄下以 設定檔名稱 做為資料夾，在資料夾下再以 instance-id 做為
    檔案名稱，快取暫存一個附檔名為 .json 的檔案。這個客製的文字檔放在 %userprofile%\\.aws\\.cached\\profile
    目錄下以 instance-id 命名，副檔名為.json 的設定檔。裡面會將剛才詢問連線EC2伺服器的私密金鑰.pem
    憑證路徑記錄在檔案裡。同時也可以設定 Account 欄位。如果沒有設定會使用 ec2-user 連線伺服器。因此
    下次再點選連線伺服器按鈕的時候就會以先前的設定為設定。如果設定錯誤，此檔案可以刪除。程式會再重建。

    ![開機狀態](https://github.com/JacquesBlazor/amazonWebServices.ec2.csharp.net/blob/main/2024-01-04_144729.png)


- [x] --- 程式使用說明如下 ---
        
    * 要用 [AWS CLI](https://docs.aws.amazon.com/zh_tw/cli/latest/userguide/getting-started-install.html) 設定 AWS Configure, 這樣會在 %userprofile%/.aws/產生 credentials 這個檔案。
      * [profile name]
      * aws_access_key_id = AKcccccccxxxxxxxx4I
      * aws_secret_access_key = nxxx88xxxxxxud2KAxm
      * region=ap-northeast-1
      * output=json

- [x] --- 其他說明 ---
        
    * 此程式為前一個 Python 版本的改版。為的是可以直接執行。附上可執行檔的 .rar 壓縮。但需要 .net runtime 7.0 以上。
    * 可以自行使用 .net sdk 重新編譯成自帶 .net 的 runtime。例如: dotnet publish -c Release -r win10-x64 --self-contained /p:PublishSingleFile=true
    * 目前的下拉選單會列出本機上所有的 AWS Configure 的設定檔名稱。但改變選單並沒有作用。原定功能為可以切換設定檔。但尚未開發此部份。
    * 其他下拉選單亦同。有關地區或該 Instance 執行個體的網路卡如果超過一張會列出多張卡。改變選單亦無作用。
    * 在 .NET 平台的 .NET SDK下載位址為 [這裡](https://download.visualstudio.microsoft.com/download/pr/2ab1aa68-3e14-401a-b106-833d66fa992b/060457e640f4095acf4723c4593314b6/dotnet-sdk-7.0.304-win-x64.exe)。.NET 平台的 .NET runtime下載位址為 [這裡](https://download.visualstudio.microsoft.com/download/pr/ce1d21d9-d3fb-451f-84b1-95f365bcbc2c/23748d17eed2e1c63fdbb6b29d147c2d/dotnet-runtime-7.0.7-win-x64.exe)。所有相關的下載列表在 [這裡](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)。

- [x] --- English Version ---

    This program provides a user-friendly interface for users of Amazon Web Service (AWS) EC2 service.
    It allows users to quickly and conveniently start or stop EC2 instances (virtual machines) and
    open a cmd prompt for SSH connections. The program loads user permissions using the "default"
    configuration file name mentioned in the Program usage instructions below, which is preconfigured
    using the AWS Configure command through AWS CLI.

    Note: When clicking the "Connect to Server" button for the first time, it will prompt for the
    private key (.pem) certificate for connecting to the EC2 server. At the same time, a folder will be
    created in the %userprofile%\.aws directory using the configuration file name, and within that folder,
    a file with the instance ID as the file name will be cached with the extension .json. This customized
    text file is stored in the %userprofile%\.aws\.cached\profile directory, named after the instance ID
    and with the extension .json, and it records the path of the previously prompted private key (.pem)
    certificate. The Account field can also be set. If not set, it will use "ec2-user" to connect to the server.
    Therefore, the next time the "Connect to Server" button is clicked, it will use the previous configuration.
    If the configuration is incorrect, this file can be deleted, and the program will rebuild it.

- [x] --- Program Usage Instructions ---

    * To configure AWS using the AWS CLI, it will generate a file called "credentials" in the %userprofile%/.aws/ directory.
       * [profile name]
       * aws_access_key_id = AKcccccccxxxxxxxx4I
       * aws_secret_access_key = nxxx88xxxxxxud2KAxm
       * region=ap-northeast-1
       * output=json
     
- [x] --- Additional Information ---
    * This program is a modified version of the previous Python version, designed to be directly executable.
      The executable file is provided in a compressed .rar format.
      However, it requires .NET Runtime 7.0 or above.
    * You can recompile it with the .NET SDK to include the .NET Runtime.
      For example: dotnet publish -c Release -r win10-x64 --self-contained /p:PublishSingleFile=true
    * The current dropdown menu displays the names of all AWS Configure configuration files on the local machine.
      However, changing the selection does not have any effect.
    * The intended functionality was to switch between configuration files, but it has not been developed yet.
    * The same applies to other dropdown menus. For regions or network interfaces of the instance,
      if there are multiple interfaces, they will be listed, but changing the selection also has no effect.      
