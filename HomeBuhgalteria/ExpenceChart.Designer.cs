
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
            this.addExpenseButton = new System.Windows.Forms.Button();
            this.expenseAmount = new System.Windows.Forms.TextBox();
            this.note = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainListBox
            // 
            this.mainListBox.FormattingEnabled = true;
            this.mainListBox.ItemHeight = 15;
            this.mainListBox.Location = new System.Drawing.Point(227, 103);
            this.mainListBox.Name = "mainListBox";
            this.mainListBox.Size = new System.Drawing.Size(252, 184);
            this.mainListBox.TabIndex = 0;
            this.mainListBox.SelectedIndexChanged += new System.EventHandler(this.mainListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(227, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // timePeriodComboBox
            // 
            this.timePeriodComboBox.FormattingEnabled = true;
            this.timePeriodComboBox.Location = new System.Drawing.Point(100, 132);
            this.timePeriodComboBox.Name = "timePeriodComboBox";
            this.timePeriodComboBox.Size = new System.Drawing.Size(121, 23);
            this.timePeriodComboBox.TabIndex = 2;
            this.timePeriodComboBox.SelectedIndexChanged += new System.EventHandler(this.TimePeriodComboBoxSelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time period";
            // 
            // accountComboBox
            // 
            this.accountComboBox.FormattingEnabled = true;
            this.accountComboBox.Location = new System.Drawing.Point(100, 103);
            this.accountComboBox.Name = "accountComboBox";
            this.accountComboBox.Size = new System.Drawing.Size(121, 23);
            this.accountComboBox.TabIndex = 4;
            this.accountComboBox.SelectedIndexChanged += new System.EventHandler(this.AccountComboBoxSelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Account";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(24, 161);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(197, 23);
            this.dateTimePicker.TabIndex = 7;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // addExpenseButton
            // 
            this.addExpenseButton.Location = new System.Drawing.Point(618, 293);
            this.addExpenseButton.Name = "addExpenseButton";
            this.addExpenseButton.Size = new System.Drawing.Size(92, 34);
            this.addExpenseButton.TabIndex = 8;
            this.addExpenseButton.Text = "Add Expense";
            this.addExpenseButton.UseVisualStyleBackColor = true;
            this.addExpenseButton.Click += new System.EventHandler(this.addExpenseButton_Click);
            // 
            // expenseAmount
            // 
            this.expenseAmount.Location = new System.Drawing.Point(557, 164);
            this.expenseAmount.Multiline = true;
            this.expenseAmount.Name = "expenseAmount";
            this.expenseAmount.Size = new System.Drawing.Size(153, 23);
            this.expenseAmount.TabIndex = 9;
            this.expenseAmount.TextChanged += new System.EventHandler(this.expenseAmount_TextChanged);
            this.expenseAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.expenseAmount_KeyPress);
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(557, 202);
            this.note.Multiline = true;
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(153, 85);
            this.note.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(515, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Note:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(497, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Amount:";
            // 
            // ExpenceChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 506);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.note);
            this.Controls.Add(this.expenseAmount);
            this.Controls.Add(this.addExpenseButton);
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
        private System.Windows.Forms.Button addExpenseButton;
        private System.Windows.Forms.TextBox expenseAmount;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}