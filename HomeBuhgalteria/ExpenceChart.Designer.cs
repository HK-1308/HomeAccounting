
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.timePeriodComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.accountComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.addExpenseButton = new System.Windows.Forms.Button();
            this.expenseAmountTextBox = new System.Windows.Forms.TextBox();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ExpenseAdding = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.incomesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.incomesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.incomesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.ExpenseAdding.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(34, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // timePeriodComboBox
            // 
            this.timePeriodComboBox.FormattingEnabled = true;
            this.timePeriodComboBox.Location = new System.Drawing.Point(96, 29);
            this.timePeriodComboBox.Name = "timePeriodComboBox";
            this.timePeriodComboBox.Size = new System.Drawing.Size(153, 23);
            this.timePeriodComboBox.TabIndex = 2;
            this.timePeriodComboBox.SelectedIndexChanged += new System.EventHandler(this.TimePeriodComboBoxSelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time period";
            // 
            // accountComboBox
            // 
            this.accountComboBox.FormattingEnabled = true;
            this.accountComboBox.Location = new System.Drawing.Point(631, 51);
            this.accountComboBox.Name = "accountComboBox";
            this.accountComboBox.Size = new System.Drawing.Size(153, 23);
            this.accountComboBox.TabIndex = 4;
            this.accountComboBox.SelectedIndexChanged += new System.EventHandler(this.AccountComboBoxSelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(573, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Account";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(96, 58);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(153, 23);
            this.dateTimePicker.TabIndex = 7;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // addExpenseButton
            // 
            this.addExpenseButton.Location = new System.Drawing.Point(161, 181);
            this.addExpenseButton.Name = "addExpenseButton";
            this.addExpenseButton.Size = new System.Drawing.Size(92, 34);
            this.addExpenseButton.TabIndex = 8;
            this.addExpenseButton.Text = "Add Expense";
            this.addExpenseButton.UseVisualStyleBackColor = true;
            this.addExpenseButton.Click += new System.EventHandler(this.addExpenseButton_Click);
            // 
            // expenseAmountTextBox
            // 
            this.expenseAmountTextBox.Location = new System.Drawing.Point(100, 61);
            this.expenseAmountTextBox.Multiline = true;
            this.expenseAmountTextBox.Name = "expenseAmountTextBox";
            this.expenseAmountTextBox.Size = new System.Drawing.Size(153, 23);
            this.expenseAmountTextBox.TabIndex = 9;
            this.expenseAmountTextBox.TextChanged += new System.EventHandler(this.expenseAmount_TextChanged);
            this.expenseAmountTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.expenseAmount_KeyPress);
            // 
            // noteTextBox
            // 
            this.noteTextBox.Location = new System.Drawing.Point(100, 90);
            this.noteTextBox.Multiline = true;
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(153, 85);
            this.noteTextBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(58, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Note:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Amount:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(34, 54);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(491, 361);
            this.dataGridView1.TabIndex = 13;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ExpenseAdding
            // 
            this.ExpenseAdding.Controls.Add(this.label6);
            this.ExpenseAdding.Controls.Add(this.categoryComboBox);
            this.ExpenseAdding.Controls.Add(this.expenseAmountTextBox);
            this.ExpenseAdding.Controls.Add(this.label5);
            this.ExpenseAdding.Controls.Add(this.addExpenseButton);
            this.ExpenseAdding.Controls.Add(this.label4);
            this.ExpenseAdding.Controls.Add(this.noteTextBox);
            this.ExpenseAdding.Location = new System.Drawing.Point(531, 92);
            this.ExpenseAdding.Name = "ExpenseAdding";
            this.ExpenseAdding.Size = new System.Drawing.Size(259, 214);
            this.ExpenseAdding.TabIndex = 14;
            this.ExpenseAdding.TabStop = false;
            this.ExpenseAdding.Text = "Expense adding";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "Category:";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(100, 32);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(153, 23);
            this.categoryComboBox.TabIndex = 13;
            this.categoryComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryComboBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.timePeriodComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker);
            this.groupBox1.Location = new System.Drawing.Point(535, 315);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 100);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Period for report";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incomesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(120, 26);
            // 
            // incomesToolStripMenuItem
            // 
            this.incomesToolStripMenuItem.Name = "incomesToolStripMenuItem";
            this.incomesToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.incomesToolStripMenuItem.Text = "Incomes";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incomesToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(120, 26);
            // 
            // incomesToolStripMenuItem1
            // 
            this.incomesToolStripMenuItem1.Name = "incomesToolStripMenuItem1";
            this.incomesToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.incomesToolStripMenuItem1.Text = "Incomes";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incomesToolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(862, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // incomesToolStripMenuItem2
            // 
            this.incomesToolStripMenuItem2.Name = "incomesToolStripMenuItem2";
            this.incomesToolStripMenuItem2.Size = new System.Drawing.Size(64, 20);
            this.incomesToolStripMenuItem2.Text = "Incomes";
            this.incomesToolStripMenuItem2.Click += new System.EventHandler(this.incomesToolStripMenuItem2_Click);
            // 
            // ExpenceChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 473);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ExpenseAdding);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.accountComboBox);
            this.Controls.Add(this.label1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ExpenceChart";
            this.Text = "ExpenceChart";
            this.Load += new System.EventHandler(this.ExpenceChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ExpenseAdding.ResumeLayout(false);
            this.ExpenseAdding.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox timePeriodComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox accountComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Button addExpenseButton;
        private System.Windows.Forms.TextBox expenseAmountTextBox;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox ExpenseAdding;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem incomesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem incomesToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem incomesToolStripMenuItem2;
    }
}