﻿namespace UDPServer
{
  partial class FormMain
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.textBoxUDP = new System.Windows.Forms.TextBox();
            this.buttonClear = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.labelDB = new System.Windows.Forms.Label();
            this.updateDB = new System.Windows.Forms.Button();
            this.buttonAddIP = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.buttonConsole = new System.Windows.Forms.Button();
            this.labelDBStatus = new System.Windows.Forms.Label();
            this.buttonConStr = new System.Windows.Forms.Button();
            this.timerControlUnit = new System.Windows.Forms.Timer(this.components);
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUDP
            // 
            this.textBoxUDP.AcceptsReturn = true;
            this.textBoxUDP.AcceptsTab = true;
            this.textBoxUDP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUDP.Location = new System.Drawing.Point(12, 12);
            this.textBoxUDP.MaxLength = 10000;
            this.textBoxUDP.Multiline = true;
            this.textBoxUDP.Name = "textBoxUDP";
            this.textBoxUDP.ReadOnly = true;
            this.textBoxUDP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxUDP.Size = new System.Drawing.Size(416, 427);
            this.textBoxUDP.TabIndex = 99;
            this.textBoxUDP.TabStop = false;
            this.textBoxUDP.WordWrap = false;
            this.textBoxUDP.TextChanged += new System.EventHandler(this.textBoxUDP_TextChanged);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(15, 19);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(240, 23);
            this.buttonClear.TabIndex = 8;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(138, 51);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(93, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "Enable Sound";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Listening Port";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(88, 79);
            this.textBox2.MaxLength = 5;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(143, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(106, 137);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Start / Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(88, 111);
            this.textBox3.MaxLength = 5;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(143, 20);
            this.textBox3.TabIndex = 2;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Sending Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Status : Ready";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(156, 341);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 23);
            this.button3.TabIndex = 7;
            this.button3.TabStop = false;
            this.button3.Text = "Commands";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelDB
            // 
            this.labelDB.AutoSize = true;
            this.labelDB.Location = new System.Drawing.Point(12, 295);
            this.labelDB.Name = "labelDB";
            this.labelDB.Size = new System.Drawing.Size(89, 13);
            this.labelDB.TabIndex = 10;
            this.labelDB.Text = "DB: Not Updated";
            // 
            // updateDB
            // 
            this.updateDB.Location = new System.Drawing.Point(15, 311);
            this.updateDB.Name = "updateDB";
            this.updateDB.Size = new System.Drawing.Size(97, 23);
            this.updateDB.TabIndex = 4;
            this.updateDB.TabStop = false;
            this.updateDB.Text = "Update DB";
            this.updateDB.UseVisualStyleBackColor = true;
            this.updateDB.Click += new System.EventHandler(this.updateDB_Click);
            // 
            // buttonAddIP
            // 
            this.buttonAddIP.Location = new System.Drawing.Point(15, 341);
            this.buttonAddIP.Name = "buttonAddIP";
            this.buttonAddIP.Size = new System.Drawing.Size(97, 23);
            this.buttonAddIP.TabIndex = 5;
            this.buttonAddIP.TabStop = false;
            this.buttonAddIP.Text = "Manualy Add IP";
            this.buttonAddIP.UseVisualStyleBackColor = true;
            this.buttonAddIP.Click += new System.EventHandler(this.buttonAddIP_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.buttonConsole);
            this.groupBox1.Controls.Add(this.labelDBStatus);
            this.groupBox1.Controls.Add(this.buttonConStr);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.buttonAddIP);
            this.groupBox1.Controls.Add(this.buttonClear);
            this.groupBox1.Controls.Add(this.updateDB);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.labelDB);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(434, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 414);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(15, 51);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(87, 17);
            this.checkBox2.TabIndex = 16;
            this.checkBox2.Text = "Text File Log";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // buttonConsole
            // 
            this.buttonConsole.Location = new System.Drawing.Point(15, 370);
            this.buttonConsole.Name = "buttonConsole";
            this.buttonConsole.Size = new System.Drawing.Size(97, 23);
            this.buttonConsole.TabIndex = 15;
            this.buttonConsole.Text = "Console";
            this.buttonConsole.UseVisualStyleBackColor = true;
            this.buttonConsole.Click += new System.EventHandler(this.buttonConsole_Click);
            // 
            // labelDBStatus
            // 
            this.labelDBStatus.AutoSize = true;
            this.labelDBStatus.Location = new System.Drawing.Point(12, 273);
            this.labelDBStatus.Name = "labelDBStatus";
            this.labelDBStatus.Size = new System.Drawing.Size(58, 13);
            this.labelDBStatus.TabIndex = 14;
            this.labelDBStatus.Text = "DB Status:";
            // 
            // buttonConStr
            // 
            this.buttonConStr.Location = new System.Drawing.Point(156, 311);
            this.buttonConStr.Name = "buttonConStr";
            this.buttonConStr.Size = new System.Drawing.Size(99, 23);
            this.buttonConStr.TabIndex = 6;
            this.buttonConStr.TabStop = false;
            this.buttonConStr.Text = "Connection string";
            this.buttonConStr.UseVisualStyleBackColor = true;
            this.buttonConStr.Click += new System.EventHandler(this.buttonConStr_Click);
            // 
            // timerControlUnit
            // 
            this.timerControlUnit.Enabled = true;
            this.timerControlUnit.Interval = 1000;
            this.timerControlUnit.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(671, 432);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(58, 17);
            this.checkBoxDebug.TabIndex = 17;
            this.checkBoxDebug.Text = "Debug";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 461);
            this.Controls.Add(this.checkBoxDebug);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBoxUDP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NTPM Hive Alarms Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBoxUDP;
    private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label labelDB;
        private System.Windows.Forms.Button updateDB;
        private System.Windows.Forms.Button buttonAddIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timerControlUnit;
        private System.Windows.Forms.Button buttonConStr;
        private System.Windows.Forms.Label labelDBStatus;
        private System.Windows.Forms.Button buttonConsole;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBoxDebug;
    }
}

