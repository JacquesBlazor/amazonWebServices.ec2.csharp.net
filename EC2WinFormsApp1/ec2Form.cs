// Authoer 作者: Alvin Lin, Email 電子郵件: alvin.constantine@outlook.com, Date 日期: 2023-6-22 18:30pm, Version 版本: 0.1.0, License: The MIT License

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

namespace EC2WinFormsApp1
{
    public partial class ec2Form : Form
    {
        private string userProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private string[] cachedConfigPaths = { ".aws", ".cached" };
        private string profileName = "default";
        private string cachedJsonFile = string.Empty;
        private CredentialProfile? load_profile;
        private AWSCredentials? aws_credential;
        private System.Windows.Forms.Timer? timer;        
        private int interval = 2500; // 2.5 seconds
        private int counter = 0;
        private class aws_config
        {
            public string? Profile { get; set; }
            public string? Region { get; set; }
            public string? Instance_id { get; set; }
            public string? Credential { get; set; }
            public string? Account { get; set; }
        }
        aws_config? json_config;
        List<aws_config> config_list = new();

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

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += Timer_Tick!;
            timer.Interval = interval;
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Perform the desired action every 'interval' seconds
            // This method will be called until timer.Stop() is called
            if (aws_credential != null)
            {
                instance_status_refreshing(aws_credential);
                counter_Label.Text = $"- {counter} -";
                counter += 1;
            }
        }

        private async void instance_status_refreshing(AWSCredentials aws_credential)
        {
            // Use awsCredentials to create an Amazon EC2 service client           
            using (AmazonEC2Client eC2_client = new AmazonEC2Client(aws_credential, new AmazonEC2Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(load_profile!.Region.SystemName)
            }))
            {
                var paginator = eC2_client.Paginators.DescribeInstances(new DescribeInstancesRequest());
                await foreach (var response in paginator.Responses)
                {
                    if (response.Reservations.Count != 1)
                    {
                        MessageBox.Show($"在 response.Reservations.Count 並不僅為 1: {response.Reservations.Count}！");
                    }
                    foreach (var reservation in response.Reservations)
                    {
                        if (reservation.Instances.Count != 1)
                        {
                            MessageBox.Show($"在 reservation.Instances.Count 並不僅為 1: {reservation.Instances.Count}！");
                        }
                        if (instance_comboBox.Items.Count < 1)
                        {
                            foreach (var instance in reservation.Instances)
                            {
                                instance_comboBox.Items.Add(instance.InstanceId);
                            }
                        }                          
                        instance_comboBox.Text = reservation.Instances[0].InstanceId;
                        instanceState_textBox.Text = reservation.Instances[0].State.Name;
                        switch (instanceState_textBox.Text)
                        {
                            case "running":
                                switch_Button.Text = "關機";
                                connect_Button.Text = "連線伺服器";
                                if (instanceIp_comboBox.Items.Count < 1)
                                {
                                    foreach (var networkInterface in reservation.Instances[0].NetworkInterfaces)
                                    {
                                        instanceIp_comboBox.Items.Add(networkInterface.Association.PublicIp);
                                    }
                                }
                                instanceIp_comboBox.Text = reservation.Instances[0].NetworkInterfaces[0].Association.PublicIp;
                                instanceIp_textBox.Text = reservation.Instances[0].NetworkInterfaces[0].Association.PublicIp;
                                if (reservation.Instances[0].NetworkInterfaces.Count != 1)
                                {
                                    MessageBox.Show($"在 instance.NetworkInterfaces.Count 並不僅為 1: {reservation.Instances[0].NetworkInterfaces.Count}");
                                }
                                instanceFqdn_textBox.Text = reservation.Instances[0].NetworkInterfaces[0].Association.PublicDnsName;
                                if (timer!.Enabled)
                                {
                                    // If the timer is already running, stop it
                                    timer.Stop();
                                    switch_Button.Enabled = true;
                                    connect_Button.Enabled = true;
                                    counter_Label.Text = string.Empty;
                                }
                                break;
                            case "stopped":
                                switch_Button.Text = "開機";
                                connect_Button.Text = " - ";
                                connect_Button.Enabled = false;
                                instanceIp_textBox.Text = string.Empty;
                                instanceFqdn_textBox.Text = string.Empty;
                                instanceIp_comboBox.Text = string.Empty;
                                if (timer!.Enabled)
                                {
                                    // If the timer is already running, stop it
                                    timer.Stop();
                                    switch_Button.Enabled = true;
                                    connect_Button.Enabled = false;
                                    counter_Label.Text = string.Empty;
                                }
                                break;
                        }
                    }
                }
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
}