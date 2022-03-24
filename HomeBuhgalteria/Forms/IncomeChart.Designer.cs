
namespace WinFormsApp1
{
    partial class IncomeChart
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
            this.expensesToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.incomesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.incomesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timePeriodComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.incomeAmountTextBox = new System.Windows.Forms.TextBox();
            this.IncomeAdding = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.addIncomeButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.accountComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.IncomeAdding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // expensesToolStripMenuItem2
            // 
            this.expensesToolStripMenuItem2.Name = "expensesToolStripMenuItem2";
            this.expensesToolStripMenuItem2.Size = new System.Drawing.Size(67, 20);
            this.expensesToolStripMenuItem2.Text = "Expenses";
            this.expensesToolStripMenuItem2.Click += new System.EventHandler(this.expensesToolStripMenuItem2_Click);
            // 
            // incomesToolStripMenuItem1
            // 
            this.incomesToolStripMenuItem1.Name = "incomesToolStripMenuItem1";
            this.incomesToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.incomesToolStripMenuItem1.Text = "Incomes";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incomesToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(120, 26);
            // 
            // incomesToolStripMenuItem
            // 
            this.incomesToolStripMenuItem.Name = "incomesToolStripMenuItem";
            this.incomesToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.incomesToolStripMenuItem.Text = "Incomes";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incomesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(120, 26);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.timePeriodComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker);
            this.groupBox1.Location = new System.Drawing.Point(535, 333);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(255, 100);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Period for report";
            // 
            // timePeriodComboBox
            // 
            this.timePeriodComboBox.FormattingEnabled = true;
            this.timePeriodComboBox.Location = new System.Drawing.Point(96, 29);
            this.timePeriodComboBox.Name = "timePeriodComboBox";
            this.timePeriodComboBox.Size = new System.Drawing.Size(153, 23);
            this.timePeriodComboBox.TabIndex = 2;
            this.timePeriodComboBox.SelectedIndexChanged += new System.EventHandler(this.timePeriodComboBox_SelectedIndexChanged);
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
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(96, 58);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(153, 23);
            this.dateTimePicker.TabIndex = 7;
            this.dateTimePicker.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
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
            // incomeAmountTextBox
            // 
            this.incomeAmountTextBox.Location = new System.Drawing.Point(100, 61);
            this.incomeAmountTextBox.Multiline = true;
            this.incomeAmountTextBox.Name = "incomeAmountTextBox";
            this.incomeAmountTextBox.Size = new System.Drawing.Size(153, 23);
            this.incomeAmountTextBox.TabIndex = 9;
            this.incomeAmountTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.incomeAmount_KeyPress);
            // 
            // IncomeAdding
            // 
            this.IncomeAdding.Controls.Add(this.label6);
            this.IncomeAdding.Controls.Add(this.categoryComboBox);
            this.IncomeAdding.Controls.Add(this.incomeAmountTextBox);
            this.IncomeAdding.Controls.Add(this.label5);
            this.IncomeAdding.Controls.Add(this.addIncomeButton);
            this.IncomeAdding.Controls.Add(this.label4);
            this.IncomeAdding.Controls.Add(this.noteTextBox);
            this.IncomeAdding.Location = new System.Drawing.Point(531, 110);
            this.IncomeAdding.Name = "IncomeAdding";
            this.IncomeAdding.Size = new System.Drawing.Size(259, 214);
            this.IncomeAdding.TabIndex = 23;
            this.IncomeAdding.TabStop = false;
            this.IncomeAdding.Text = "Income adding";
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
            // addIncomeButton
            // 
            this.addIncomeButton.Location = new System.Drawing.Point(161, 181);
            this.addIncomeButton.Name = "addIncomeButton";
            this.addIncomeButton.Size = new System.Drawing.Size(92, 34);
            this.addIncomeButton.TabIndex = 8;
            this.addIncomeButton.Text = "Add Income";
            this.addIncomeButton.UseVisualStyleBackColor = true;
            this.addIncomeButton.Click += new System.EventHandler(this.addIncomeButton_Click);
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
            // noteTextBox
            // 
            this.noteTextBox.Location = new System.Drawing.Point(100, 90);
            this.noteTextBox.Multiline = true;
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(153, 85);
            this.noteTextBox.TabIndex = 10;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(34, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(491, 361);
            this.dataGridView1.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(573, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 21;
            this.label3.Text = "Account";
            // 
            // accountComboBox
            // 
            this.accountComboBox.FormattingEnabled = true;
            this.accountComboBox.Location = new System.Drawing.Point(631, 69);
            this.accountComboBox.Name = "accountComboBox";
            this.accountComboBox.Size = new System.Drawing.Size(153, 23);
            this.accountComboBox.TabIndex = 20;
            this.accountComboBox.SelectedIndexChanged += new System.EventHandler(this.accountComboBox_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expensesToolStripMenuItem2});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(34, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 28);
            this.label1.TabIndex = 19;
            this.label1.Text = "label1";
            // 
            // IncomeChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.IncomeAdding);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.accountComboBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Name = "IncomeChart";
            this.Text = "IncomeChart";
            this.Load += new System.EventHandler(this.IncomeChart_Load);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.IncomeAdding.ResumeLayout(false);
            this.IncomeAdding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem expensesToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem incomesToolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem incomesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox timePeriodComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.TextBox incomeAmountTextBox;
        private System.Windows.Forms.GroupBox IncomeAdding;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button addIncomeButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox accountComboBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label1;
    }
}