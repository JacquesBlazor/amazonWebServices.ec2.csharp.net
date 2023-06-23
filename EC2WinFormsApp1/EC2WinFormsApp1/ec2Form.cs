// Authoer 作者: Alvin Lin, Email 電子郵件: alvin.constantine@outlook.com, Date 日期: 2023-6-22 18:30pm, Version 版本: 0.1.0, License 授權: The MIT License
// Modified: Date 日期: 2023-6-24 00:46am 把和 Form 元件不直接關聯的方法移到另一個檔案, 更改對話框為固定大小。

using Amazon.EC2;  // AmazonEC2Client
using Amazon.EC2.Model;  // DescribeInstancesRequest
using Amazon.Runtime;  // AWSCredentials
using Amazon.Runtime.CredentialManagement;  //CredentialProfileStoreChain
using System.Text.Json;  // JsonSerializer.Deserialize, JsonSerializer.Serialize

/*
Copyright <2023> <COPYRIGHT Alvin Lin (alvin.constantine@outlook.com)>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the “Software”),
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace EC2WinFormsApp1;

public partial class ec2Form : Form
{
    public ec2Form()
    {
        InitializeComponent();
        InitializeTimer();
    }

    private void ec2Form_Load(object sender, EventArgs e)
    {
        CredentialProfileStoreChain profile_chain = new CredentialProfileStoreChain();
        List<CredentialProfile> profile_list = profile_chain.ListProfiles();

        if (profile_list.Count == 0)
        {
            MessageBox.Show($"請先使用 AWS CLI 的 AWS Configure 設定 Credential 否則 {nameof(profile_list)} 是空的！");
            return;
        }
        else
        {
            foreach (CredentialProfile profile in profile_list)
            {
                profile_comboBox.Items.Add(profile.Name);
            }
            load_profile = profile_list.Find(profile_item => profile_item.Name == profileName)!;
            if (load_profile == null)
            {
                load_profile = profile_list[0];
            }
            profileName = load_profile.Name;
            if (!profile_chain.TryGetAWSCredentials(profileName, out aws_credential))
            {
                MessageBox.Show($"無法取得使用者 [{profileName}] 的 AWS 認證資訊 Credential！");
                return;
            }

            instanceRegion_textBox.Text = load_profile.Region.SystemName;
            profile_comboBox.Text = profileName;
            switch_Button.Text = string.Empty;
            connect_Button.Text = string.Empty;
            counter_Label.Text = string.Empty;
            instance_status_refreshing(aws_credential);
        }
    }

    private async void switch_Button_Click(object sender, EventArgs e)
    {
        if (aws_credential != null)
        {
            counter_Label.Text = "- * -";
            switch_Button.Enabled = false;
            connect_Button.Enabled = false;
            using (AmazonEC2Client eC2_client = new AmazonEC2Client(aws_credential))
            {
                if (instanceState_textBox.Text == "running")
                {
                    var response = await eC2_client.StopInstancesAsync(new StopInstancesRequest
                    {
                        InstanceIds = new List<string> {
                            instance_comboBox.Text
                        }
                    });
                    if (response.StoppingInstances.Count > 0)
                    {
                        var instances = response.StoppingInstances;
                        instances.ForEach(instance =>
                        {
                            instanceState_textBox.Text = instance.CurrentState.Name;
                        });
                    }
                }
                else
                {
                    var response = await eC2_client.StartInstancesAsync(new StartInstancesRequest
                    {
                        InstanceIds = new List<string> {
                            instance_comboBox.Text
                        }
                    });
                    if (response.StartingInstances.Count > 0)
                    {
                        var instances = response.StartingInstances;
                        instances.ForEach(instance =>
                        {
                            instanceState_textBox.Text = instance.CurrentState.Name;
                        });
                    }
                }
                if (!(timer!.Enabled))
                {
                    // Start the timer to call Timer_Tick every 2 seconds
                    timer.Start();
                    counter = 1;
                }
            }
        }
    }

    private void connect_Button_Click(object sender, EventArgs e)
    {
        if (cachedJsonFile == string.Empty || json_config == null)
        {
            DirectoryInfo cachedConfigPath = new DirectoryInfo(Path.Combine(userProfilePath, Path.Combine(cachedConfigPaths), profileName));
            if (!cachedConfigPath.Exists)
            {
                try
                {
                    cachedConfigPath.Create();
                    cachedConfigPath.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"無法建立 {cachedConfigPath} 目錄！錯誤 {ex.Message}。");
                    return;
                }
            }

            FileInfo[] configure_files = cachedConfigPath.GetFiles("*.json");
            if (configure_files.Length > 0)
            {
                foreach (FileInfo configure_file in configure_files)
                {
                    try
                    {
                        using (FileStream openStream = File.OpenRead(configure_file.FullName))
                        {
                            aws_config? json_load = JsonSerializer.Deserialize<aws_config>(openStream);
                            if (json_load != null)
                            {
                                config_list.Add(json_load);
                                if (json_load.Instance_id == instance_comboBox.Text)
                                {
                                    json_config = json_load;
                                    cachedJsonFile = configure_file.FullName;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"無法讀取 {configure_file.FullName} 檔案！錯誤 {ex.Message}。");
                        return;
                    }
                }
            }
            else
            {
                FileInfo configure_file = new FileInfo(Path.Combine(userProfilePath, Path.Combine(userProfilePath, ".aws", ".cached", profileName), $"{instance_comboBox.Text}.json"));
                json_config = new()
                {
                    Profile = profileName,
                    Region = load_profile!.Region.SystemName,
                    Instance_id = instance_comboBox.Text,
                    Credential = String.Empty,
                    Account = String.Empty
                };
                cachedJsonFile = configure_file.FullName;
                try
                {
                    using (FileStream createStream = File.Create(cachedJsonFile))
                    {
                        JsonSerializer.Serialize(createStream, json_config);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"無法寫入 {cachedJsonFile} 檔案！錯誤 {ex.Message}。");
                    return;
                }
            }
        }
        if (json_config != null && json_config.Credential == string.Empty)
        {
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                openFileDialog.Filter = "EC2 key pair pem file (*.pem)|*.pem|All files (*.*)|*.*";
                openFileDialog.Title = "選取執行個體的金鑰.pem 檔案";
                openFileDialog.RestoreDirectory = true;
                openFileDialog.FilterIndex = 1;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }
            if (filePath == string.Empty)
            {
                MessageBox.Show("未請選擇要連接的執行個體的金鑰.pem 檔案！");
                return;
            }
            else
            {
                json_config.Credential = filePath;
                try
                {
                    using (FileStream createStream = File.Create(cachedJsonFile))
                    {
                        JsonSerializer.Serialize(createStream, json_config);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"無法寫入 {cachedJsonFile} 檔案！錯誤 {ex.Message}。");
                    return;
                }
            }
        }
        if (json_config != null && json_config.Credential != string.Empty)
        {
            string account;
            if (json_config.Account != string.Empty)
            {
                account = json_config.Account!;
            }
            else
            {
                account = "ec2-user";
            }
            System.Diagnostics.Process.Start("cmd.exe", $"/c ssh -o \"ServerAliveInterval 40\" -o StrictHostKeyChecking=no -i \"{json_config.Credential}\" {account}@{instanceFqdn_textBox.Text}\n");
        }
        else
        {
            MessageBox.Show("配置錯誤無法連線執行個體！");
        }
    }

    private void refresh_button_Click(object sender, EventArgs e)
    {
        if (aws_credential != null)
        {
            instance_status_refreshing(aws_credential);
            MessageBox.Show("已更新執行個體狀態！");
        }
    }
}