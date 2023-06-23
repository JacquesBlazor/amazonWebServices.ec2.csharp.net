// Authoer 作者: Alvin Lin, Email 電子郵件: alvin.constantine@outlook.com, Date 日期: 2023-6-22 18:30pm, Version 版本: 0.1.0, License 授權: The MIT License
// Modified: Date 日期: 2023-6-24 00:46am 把和 Form 元件不直接關聯的方法移到這個檔案, 更改對話框為固定大小。

using Amazon.EC2;  // AmazonEC2Client
using Amazon.EC2.Model;  // DescribeInstancesRequest
using Amazon.Runtime;  // AWSCredentials
using Amazon.Runtime.CredentialManagement;  //CredentialProfileStoreChain

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
}

