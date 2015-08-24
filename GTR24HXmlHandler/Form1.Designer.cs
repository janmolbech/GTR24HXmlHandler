namespace GTR24HXmlHandler
{
    partial class Form1
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
            this.tbFileLocation = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnStartWatch = new System.Windows.Forms.Button();
            this.btnHandleExisting = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbFileLocation
            // 
            this.tbFileLocation.Location = new System.Drawing.Point(31, 31);
            this.tbFileLocation.Name = "tbFileLocation";
            this.tbFileLocation.Size = new System.Drawing.Size(253, 20);
            this.tbFileLocation.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBrowse.Location = new System.Drawing.Point(309, 27);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowse_Click);
            // 
            // btnStartWatch
            // 
            this.btnStartWatch.Location = new System.Drawing.Point(31, 71);
            this.btnStartWatch.Name = "btnStartWatch";
            this.btnStartWatch.Size = new System.Drawing.Size(108, 23);
            this.btnStartWatch.TabIndex = 2;
            this.btnStartWatch.Text = "Start watching";
            this.btnStartWatch.UseVisualStyleBackColor = true;
            this.btnStartWatch.Click += new System.EventHandler(this.btnStartWatch_Click);
            // 
            // btnHandleExisting
            // 
            this.btnHandleExisting.Location = new System.Drawing.Point(189, 70);
            this.btnHandleExisting.Name = "btnHandleExisting";
            this.btnHandleExisting.Size = new System.Drawing.Size(133, 23);
            this.btnHandleExisting.TabIndex = 3;
            this.btnHandleExisting.Text = "Process existing files";
            this.btnHandleExisting.UseVisualStyleBackColor = true;
            this.btnHandleExisting.Click += new System.EventHandler(this.btnHandleExisting_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.ForeColor = System.Drawing.Color.Crimson;
            this.lblInfo.Location = new System.Drawing.Point(31, 124);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 20);
            this.lblInfo.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnHandleExisting);
            this.Controls.Add(this.btnStartWatch);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbFileLocation);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFileLocation;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnStartWatch;
        private System.Windows.Forms.Button btnHandleExisting;
        private System.Windows.Forms.Label lblInfo;
    }
}

