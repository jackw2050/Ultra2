namespace SerialPortTerminal
{
    partial class AutoStart
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.springTensionTargetNumericTextBoxAutostart = new SerialPortTerminal.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Continue";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter spring tension from mechanical counter below.\r\nPress enter to continue";
            // 
            // springTensionTargetNumericTextBoxAutostart
            // 
            this.springTensionTargetNumericTextBoxAutostart.AllowSpace = false;
            this.springTensionTargetNumericTextBoxAutostart.Location = new System.Drawing.Point(33, 98);
            this.springTensionTargetNumericTextBoxAutostart.Name = "springTensionTargetNumericTextBoxAutostart";
            this.springTensionTargetNumericTextBoxAutostart.Size = new System.Drawing.Size(116, 20);
            this.springTensionTargetNumericTextBoxAutostart.TabIndex = 108;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 109;
            this.label2.Text = "Spring Tension";
            // 
            // AutoStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 144);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.springTensionTargetNumericTextBoxAutostart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "AutoStart";
            this.Text = "Startup Wizard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public NumericTextBox springTensionTargetNumericTextBoxAutostart;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
    }
}