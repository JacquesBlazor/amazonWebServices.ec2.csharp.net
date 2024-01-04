namespace EC2WinFormsApp1
{
    partial class ec2Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ec2Form));
            profile_Frame = new GroupBox();
            instanceRegion_textBox = new TextBox();
            instance_comboBox = new ComboBox();
            profile_comboBox = new ComboBox();
            instanceRegion_Label = new Label();
            instance_Label = new Label();
            profile_Label = new Label();
            switch_Button = new Button();
            instanceStatus_Frame = new GroupBox();
            counter_Label = new Label();
            instanceState_textBox = new TextBox();
            instanceState_Label = new Label();
            instanceIp_comboBox = new ComboBox();
            instanceIp_Lable = new Label();
            instanceFqnd_Lable = new Label();
            connect_Button = new Button();
            instanceStatistics_Frame = new GroupBox();
            instanceIp_textBox = new TextBox();
            instanceFqdn_textBox = new TextBox();
            refresh_button = new Button();
            profile_Frame.SuspendLayout();
            instanceStatus_Frame.SuspendLayout();
            instanceStatistics_Frame.SuspendLayout();
            SuspendLayout();
            // 
            // profile_Frame
            // 
            profile_Frame.Controls.Add(instanceRegion_textBox);
            profile_Frame.Controls.Add(instance_comboBox);
            profile_Frame.Controls.Add(profile_comboBox);
            profile_Frame.Controls.Add(instanceRegion_Label);
            profile_Frame.Controls.Add(instance_Label);
            profile_Frame.Controls.Add(profile_Label);
            profile_Frame.Location = new Point(12, 12);
            profile_Frame.Name = "profile_Frame";
            profile_Frame.Size = new Size(290, 105);
            profile_Frame.TabIndex = 0;
            profile_Frame.TabStop = false;
            profile_Frame.Text = "設定檔";
            // 
            // instanceRegion_textBox
            // 
            instanceRegion_textBox.Location = new Point(186, 71);
            instanceRegion_textBox.Name = "instanceRegion_textBox";
            instanceRegion_textBox.ReadOnly = true;
            instanceRegion_textBox.Size = new Size(99, 23);
            instanceRegion_textBox.TabIndex = 6;
            instanceRegion_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // instance_comboBox
            // 
            instance_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            instance_comboBox.FormattingEnabled = true;
            instance_comboBox.Location = new Point(128, 46);
            instance_comboBox.Name = "instance_comboBox";
            instance_comboBox.RightToLeft = RightToLeft.Yes;
            instance_comboBox.Size = new Size(156, 23);
            instance_comboBox.TabIndex = 2;
            // 
            // profile_comboBox
            // 
            profile_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            profile_comboBox.FormattingEnabled = true;
            profile_comboBox.Location = new Point(157, 21);
            profile_comboBox.Name = "profile_comboBox";
            profile_comboBox.RightToLeft = RightToLeft.Yes;
            profile_comboBox.Size = new Size(127, 23);
            profile_comboBox.TabIndex = 1;
            profile_comboBox.SelectedIndexChanged += profile_comboBox_SelectedIndexChanged;
            // 
            // instanceRegion_Label
            // 
            instanceRegion_Label.AutoSize = true;
            instanceRegion_Label.Location = new Point(8, 76);
            instanceRegion_Label.Name = "instanceRegion_Label";
            instanceRegion_Label.Size = new Size(175, 15);
            instanceRegion_Label.TabIndex = 0;
            instanceRegion_Label.Text = "EC2 執行個體 Region 所在地區";
            // 
            // instance_Label
            // 
            instance_Label.AutoSize = true;
            instance_Label.Location = new Point(7, 50);
            instance_Label.Name = "instance_Label";
            instance_Label.Size = new Size(95, 15);
            instance_Label.TabIndex = 1;
            instance_Label.Text = "EC2 執行個體 ID";
            // 
            // profile_Label
            // 
            profile_Label.AutoSize = true;
            profile_Label.Location = new Point(7, 24);
            profile_Label.Name = "profile_Label";
            profile_Label.Size = new Size(144, 15);
            profile_Label.TabIndex = 0;
            profile_Label.Text = "設定檔名稱 Profile Name";
            // 
            // switch_Button
            // 
            switch_Button.Location = new Point(12, 291);
            switch_Button.Name = "switch_Button";
            switch_Button.Size = new Size(67, 23);
            switch_Button.TabIndex = 4;
            switch_Button.UseVisualStyleBackColor = true;
            switch_Button.Click += switch_Button_Click;
            // 
            // instanceStatus_Frame
            // 
            instanceStatus_Frame.Controls.Add(counter_Label);
            instanceStatus_Frame.Controls.Add(instanceState_textBox);
            instanceStatus_Frame.Controls.Add(instanceState_Label);
            instanceStatus_Frame.Location = new Point(12, 123);
            instanceStatus_Frame.Name = "instanceStatus_Frame";
            instanceStatus_Frame.Size = new Size(290, 63);
            instanceStatus_Frame.TabIndex = 2;
            instanceStatus_Frame.TabStop = false;
            instanceStatus_Frame.Text = "執行個體狀態";
            // 
            // counter_Label
            // 
            counter_Label.AutoSize = true;
            counter_Label.Location = new Point(157, 28);
            counter_Label.Name = "counter_Label";
            counter_Label.Size = new Size(28, 15);
            counter_Label.TabIndex = 8;
            counter_Label.Text = "- - -";
            // 
            // instanceState_textBox
            // 
            instanceState_textBox.Location = new Point(205, 24);
            instanceState_textBox.Name = "instanceState_textBox";
            instanceState_textBox.ReadOnly = true;
            instanceState_textBox.Size = new Size(78, 23);
            instanceState_textBox.TabIndex = 7;
            instanceState_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // instanceState_Label
            // 
            instanceState_Label.AutoSize = true;
            instanceState_Label.Location = new Point(7, 28);
            instanceState_Label.Name = "instanceState_Label";
            instanceState_Label.Size = new Size(128, 15);
            instanceState_Label.TabIndex = 5;
            instanceState_Label.Text = "EC2 執行個體啟動狀態";
            // 
            // instanceIp_comboBox
            // 
            instanceIp_comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            instanceIp_comboBox.FormattingEnabled = true;
            instanceIp_comboBox.Location = new Point(55, 22);
            instanceIp_comboBox.Name = "instanceIp_comboBox";
            instanceIp_comboBox.RightToLeft = RightToLeft.Yes;
            instanceIp_comboBox.Size = new Size(122, 23);
            instanceIp_comboBox.TabIndex = 3;
            // 
            // instanceIp_Lable
            // 
            instanceIp_Lable.AutoSize = true;
            instanceIp_Lable.Location = new Point(7, 25);
            instanceIp_Lable.Name = "instanceIp_Lable";
            instanceIp_Lable.Size = new Size(44, 15);
            instanceIp_Lable.TabIndex = 1;
            instanceIp_Lable.Text = "IP 位址";
            // 
            // instanceFqnd_Lable
            // 
            instanceFqnd_Lable.AutoSize = true;
            instanceFqnd_Lable.Location = new Point(7, 54);
            instanceFqnd_Lable.Name = "instanceFqnd_Lable";
            instanceFqnd_Lable.Size = new Size(45, 15);
            instanceFqnd_Lable.TabIndex = 0;
            instanceFqnd_Lable.Text = "FQDN ";
            // 
            // connect_Button
            // 
            connect_Button.Location = new Point(205, 291);
            connect_Button.Name = "connect_Button";
            connect_Button.Size = new Size(97, 23);
            connect_Button.TabIndex = 5;
            connect_Button.UseVisualStyleBackColor = true;
            connect_Button.Click += connect_Button_Click;
            // 
            // instanceStatistics_Frame
            // 
            instanceStatistics_Frame.Controls.Add(instanceIp_textBox);
            instanceStatistics_Frame.Controls.Add(instanceFqdn_textBox);
            instanceStatistics_Frame.Controls.Add(instanceIp_comboBox);
            instanceStatistics_Frame.Controls.Add(instanceIp_Lable);
            instanceStatistics_Frame.Controls.Add(instanceFqnd_Lable);
            instanceStatistics_Frame.Location = new Point(12, 191);
            instanceStatistics_Frame.Name = "instanceStatistics_Frame";
            instanceStatistics_Frame.Size = new Size(290, 80);
            instanceStatistics_Frame.TabIndex = 3;
            instanceStatistics_Frame.TabStop = false;
            instanceStatistics_Frame.Text = "目前 EC2 Instance 細節";
            // 
            // instanceIp_textBox
            // 
            instanceIp_textBox.Location = new Point(186, 22);
            instanceIp_textBox.Name = "instanceIp_textBox";
            instanceIp_textBox.ReadOnly = true;
            instanceIp_textBox.Size = new Size(98, 23);
            instanceIp_textBox.TabIndex = 8;
            instanceIp_textBox.TextAlign = HorizontalAlignment.Right;
            // 
            // instanceFqdn_textBox
            // 
            instanceFqdn_textBox.Location = new Point(50, 50);
            instanceFqdn_textBox.Name = "instanceFqdn_textBox";
            instanceFqdn_textBox.ReadOnly = true;
            instanceFqdn_textBox.Size = new Size(234, 23);
            instanceFqdn_textBox.TabIndex = 9;
            // 
            // refresh_button
            // 
            refresh_button.Location = new Point(85, 291);
            refresh_button.Name = "refresh_button";
            refresh_button.Size = new Size(114, 23);
            refresh_button.TabIndex = 4;
            refresh_button.Text = "更新執行個體狀態";
            refresh_button.UseVisualStyleBackColor = true;
            refresh_button.Click += refresh_button_Click;
            // 
            // ec2Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(314, 331);
            Controls.Add(connect_Button);
            Controls.Add(instanceStatistics_Frame);
            Controls.Add(instanceStatus_Frame);
            Controls.Add(refresh_button);
            Controls.Add(switch_Button);
            Controls.Add(profile_Frame);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "ec2Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AWS EC2 啟動與停止和連線";
            Load += ec2Form_Load;
            profile_Frame.ResumeLayout(false);
            profile_Frame.PerformLayout();
            instanceStatus_Frame.ResumeLayout(false);
            instanceStatus_Frame.PerformLayout();
            instanceStatistics_Frame.ResumeLayout(false);
            instanceStatistics_Frame.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox profile_Frame;
        private Label instance_Label;
        private Label profile_Label;
        private Button switch_Button;
        private GroupBox instanceStatus_Frame;
        private Label instanceRegion_Label;
        private Label instanceIp_Lable;
        private Label instanceFqnd_Lable;
        private Button connect_Button;
        private Label instanceState_Label;
        private ComboBox instance_comboBox;
        private ComboBox profile_comboBox;
        private GroupBox instanceStatistics_Frame;
        private ComboBox instanceIp_comboBox;
        private TextBox instanceRegion_textBox;
        private TextBox instanceState_textBox;
        private TextBox instanceFqdn_textBox;
        private TextBox instanceIp_textBox;
        private Button refresh_button;
        private Label counter_Label;
    }
}