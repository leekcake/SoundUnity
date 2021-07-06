
namespace WindowsClient
{
    partial class Form1
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
            this.TestLocalhostButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TestLocalhostButton
            // 
            this.TestLocalhostButton.Location = new System.Drawing.Point(12, 12);
            this.TestLocalhostButton.Name = "TestLocalhostButton";
            this.TestLocalhostButton.Size = new System.Drawing.Size(137, 23);
            this.TestLocalhostButton.TabIndex = 0;
            this.TestLocalhostButton.Text = "Test with Localhost";
            this.TestLocalhostButton.UseVisualStyleBackColor = true;
            this.TestLocalhostButton.Click += new System.EventHandler(this.TestLocalhostButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 96);
            this.Controls.Add(this.TestLocalhostButton);
            this.Name = "Form1";
            this.Text = "SoundUnity Windows Client";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestLocalhostButton;
    }
}

