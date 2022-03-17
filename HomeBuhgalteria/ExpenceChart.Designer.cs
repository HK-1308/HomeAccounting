
namespace WinFormsApp1
{
    partial class ExpenceChart
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
            this.mainListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.timePeriodComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.accountComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.mainListBox.FormattingEnabled = true;
            this.mainListBox.ItemHeight = 15;
            this.mainListBox.Location = new System.Drawing.Point(279, 189);
            this.mainListBox.Name = "mainListBox";
            this.mainListBox.Size = new System.Drawing.Size(252, 184);
            this.mainListBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(279, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // comboBox1
            // 
            this.timePeriodComboBox.FormattingEnabled = true;
            this.timePeriodComboBox.Location = new System.Drawing.Point(58, 163);
            this.timePeriodComboBox.Name = "timePeriodComboBox";
            this.timePeriodComboBox.Size = new System.Drawing.Size(121, 23);
            this.timePeriodComboBox.TabIndex = 2;
            this.timePeriodComboBox.SelectedIndexChanged += new System.EventHandler(this.TimePeriodComboBoxSelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time period";
            // 
            // comboBox2
            // 
            this.accountComboBox.FormattingEnabled = true;
            this.accountComboBox.Location = new System.Drawing.Point(279, 60);
            this.accountComboBox.Name = "accountComboBox";
            this.accountComboBox.Size = new System.Drawing.Size(121, 23);
            this.accountComboBox.TabIndex = 4;
            this.accountComboBox.SelectedIndexChanged += new System.EventHandler(this.AccountComboBoxSelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(279, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Account";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(566, 200);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(156, 23);
            this.dateTimePicker.TabIndex = 7;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // ExpenceChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 506);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.accountComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.timePeriodComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainListBox);
            this.Name = "ExpenceChart";
            this.Text = "ExpenceChart";
            this.Load += new System.EventHandler(this.ExpenceChart_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox mainListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox timePeriodComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox accountComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}