﻿namespace WinFormsClient
{
    partial class FrmClient
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.grpMessaging = new System.Windows.Forms.GroupBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.grpMembership = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLeaveGroup = new System.Windows.Forms.Button();
            this.btnJoinGroup = new System.Windows.Forms.Button();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.grpServer1 = new System.Windows.Forms.GroupBox();
            this.btnNotReady = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.labelReadyServer1 = new System.Windows.Forms.Label();
            this.readyServerButton1 = new System.Windows.Forms.Button();
            this.labelServerPlayers = new System.Windows.Forms.Label();
            this.leaveServerButton1 = new System.Windows.Forms.Button();
            this.joinServerButton1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grpMessaging.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpMembership.SuspendLayout();
            this.grpServer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.txtUrl);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 107);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(79, 45);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(242, 20);
            this.txtUserName.TabIndex = 9;
            this.txtUserName.Text = "user";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "User name";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(213, 71);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(108, 23);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Visible = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(79, 19);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(242, 20);
            this.txtUrl.TabIndex = 6;
            this.txtUrl.Text = "http://localhost:8080/signalr";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Url";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(79, 71);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(108, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // grpMessaging
            // 
            this.grpMessaging.Controls.Add(this.btnSend);
            this.grpMessaging.Controls.Add(this.label2);
            this.grpMessaging.Controls.Add(this.txtMessage);
            this.grpMessaging.Location = new System.Drawing.Point(12, 125);
            this.grpMessaging.Name = "grpMessaging";
            this.grpMessaging.Size = new System.Drawing.Size(403, 79);
            this.grpMessaging.TabIndex = 1;
            this.grpMessaging.TabStop = false;
            this.grpMessaging.Text = "Messaging";
            this.grpMessaging.Enter += new System.EventHandler(this.grpMessaging_Enter);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(76, 45);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(239, 23);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Command";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(77, 19);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(238, 20);
            this.txtMessage.TabIndex = 7;
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtLog);
            this.groupBox3.Location = new System.Drawing.Point(12, 408);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(403, 101);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(6, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(391, 185);
            this.txtLog.TabIndex = 5;
            // 
            // grpMembership
            // 
            this.grpMembership.Controls.Add(this.label4);
            this.grpMembership.Controls.Add(this.btnLeaveGroup);
            this.grpMembership.Controls.Add(this.btnJoinGroup);
            this.grpMembership.Controls.Add(this.txtGroupName);
            this.grpMembership.Enabled = false;
            this.grpMembership.Location = new System.Drawing.Point(12, 210);
            this.grpMembership.Name = "grpMembership";
            this.grpMembership.Size = new System.Drawing.Size(403, 84);
            this.grpMembership.TabIndex = 3;
            this.grpMembership.TabStop = false;
            this.grpMembership.Text = "Group Membership";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Group name";
            // 
            // btnLeaveGroup
            // 
            this.btnLeaveGroup.Location = new System.Drawing.Point(212, 53);
            this.btnLeaveGroup.Name = "btnLeaveGroup";
            this.btnLeaveGroup.Size = new System.Drawing.Size(102, 23);
            this.btnLeaveGroup.TabIndex = 10;
            this.btnLeaveGroup.Text = "Leave";
            this.btnLeaveGroup.UseVisualStyleBackColor = true;
            this.btnLeaveGroup.Click += new System.EventHandler(this.btnLeaveGroup_Click);
            // 
            // btnJoinGroup
            // 
            this.btnJoinGroup.Location = new System.Drawing.Point(75, 53);
            this.btnJoinGroup.Name = "btnJoinGroup";
            this.btnJoinGroup.Size = new System.Drawing.Size(111, 23);
            this.btnJoinGroup.TabIndex = 9;
            this.btnJoinGroup.Text = "Join";
            this.btnJoinGroup.UseVisualStyleBackColor = true;
            this.btnJoinGroup.Click += new System.EventHandler(this.btnJoinGroup_Click);
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(76, 27);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(238, 20);
            this.txtGroupName.TabIndex = 8;
            // 
            // grpServer1
            // 
            this.grpServer1.Controls.Add(this.btnNotReady);
            this.grpServer1.Controls.Add(this.btnUndo);
            this.grpServer1.Controls.Add(this.btnPlay);
            this.grpServer1.Controls.Add(this.labelReadyServer1);
            this.grpServer1.Controls.Add(this.readyServerButton1);
            this.grpServer1.Controls.Add(this.labelServerPlayers);
            this.grpServer1.Controls.Add(this.leaveServerButton1);
            this.grpServer1.Controls.Add(this.joinServerButton1);
            this.grpServer1.Enabled = false;
            this.grpServer1.Location = new System.Drawing.Point(12, 300);
            this.grpServer1.Name = "grpServer1";
            this.grpServer1.Size = new System.Drawing.Size(397, 102);
            this.grpServer1.TabIndex = 4;
            this.grpServer1.TabStop = false;
            this.grpServer1.Text = "Server 1";
            // 
            // btnNotReady
            // 
            this.btnNotReady.Enabled = false;
            this.btnNotReady.Location = new System.Drawing.Point(75, 73);
            this.btnNotReady.Name = "btnNotReady";
            this.btnNotReady.Size = new System.Drawing.Size(111, 23);
            this.btnNotReady.TabIndex = 7;
            this.btnNotReady.Text = "Not ready";
            this.btnNotReady.UseVisualStyleBackColor = true;
            this.btnNotReady.Visible = false;
            this.btnNotReady.Click += new System.EventHandler(this.btnNotReady_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Enabled = false;
            this.btnUndo.Location = new System.Drawing.Point(203, 73);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(111, 23);
            this.btnUndo.TabIndex = 6;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Visible = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(75, 47);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(111, 23);
            this.btnPlay.TabIndex = 5;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Visible = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // labelReadyServer1
            // 
            this.labelReadyServer1.AutoSize = true;
            this.labelReadyServer1.Location = new System.Drawing.Point(12, 52);
            this.labelReadyServer1.Name = "labelReadyServer1";
            this.labelReadyServer1.Size = new System.Drawing.Size(58, 13);
            this.labelReadyServer1.TabIndex = 4;
            this.labelReadyServer1.Text = "Ready 0/2";
            // 
            // readyServerButton1
            // 
            this.readyServerButton1.Location = new System.Drawing.Point(75, 47);
            this.readyServerButton1.Name = "readyServerButton1";
            this.readyServerButton1.Size = new System.Drawing.Size(111, 23);
            this.readyServerButton1.TabIndex = 3;
            this.readyServerButton1.Text = "Ready";
            this.readyServerButton1.UseVisualStyleBackColor = true;
            this.readyServerButton1.Visible = false;
            this.readyServerButton1.Click += new System.EventHandler(this.readyServerButton1_Click);
            // 
            // labelServerPlayers
            // 
            this.labelServerPlayers.AutoSize = true;
            this.labelServerPlayers.Location = new System.Drawing.Point(9, 25);
            this.labelServerPlayers.Name = "labelServerPlayers";
            this.labelServerPlayers.Size = new System.Drawing.Size(61, 13);
            this.labelServerPlayers.TabIndex = 2;
            this.labelServerPlayers.Text = "Players 0/2";
            // 
            // leaveServerButton1
            // 
            this.leaveServerButton1.Location = new System.Drawing.Point(203, 47);
            this.leaveServerButton1.Name = "leaveServerButton1";
            this.leaveServerButton1.Size = new System.Drawing.Size(111, 23);
            this.leaveServerButton1.TabIndex = 1;
            this.leaveServerButton1.Text = "Leave";
            this.leaveServerButton1.UseVisualStyleBackColor = true;
            this.leaveServerButton1.Visible = false;
            this.leaveServerButton1.Click += new System.EventHandler(this.leaveServerButton1_Click);
            // 
            // joinServerButton1
            // 
            this.joinServerButton1.Location = new System.Drawing.Point(75, 47);
            this.joinServerButton1.Name = "joinServerButton1";
            this.joinServerButton1.Size = new System.Drawing.Size(111, 23);
            this.joinServerButton1.TabIndex = 0;
            this.joinServerButton1.Text = "Join";
            this.joinServerButton1.UseVisualStyleBackColor = true;
            this.joinServerButton1.Click += new System.EventHandler(this.joinServerButton1_Click);
            // 
            // FrmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 519);
            this.Controls.Add(this.grpServer1);
            this.Controls.Add(this.grpMembership);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpMessaging);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmClient";
            this.Text = "Client";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpMessaging.ResumeLayout(false);
            this.grpMessaging.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpMembership.ResumeLayout(false);
            this.grpMembership.PerformLayout();
            this.grpServer1.ResumeLayout(false);
            this.grpServer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpMessaging;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpMembership;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLeaveGroup;
        private System.Windows.Forms.Button btnJoinGroup;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.GroupBox grpServer1;
        private System.Windows.Forms.Button readyServerButton1;
        private System.Windows.Forms.Label labelServerPlayers;
        private System.Windows.Forms.Button leaveServerButton1;
        private System.Windows.Forms.Button joinServerButton1;
        private System.Windows.Forms.Label labelReadyServer1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnNotReady;
    }
}
