﻿namespace _2EksamensProjekt.FORMS.secretary;

partial class secretarySP
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
        this.label2 = new System.Windows.Forms.Label();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.dataGridView2 = new System.Windows.Forms.DataGridView();
        this.label1 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
        this.SuspendLayout();
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.BackColor = System.Drawing.SystemColors.Control;
        this.label2.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.label2.ForeColor = System.Drawing.Color.DarkOrange;
        this.label2.Location = new System.Drawing.Point(101, 3);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(0, 24);
        this.label2.TabIndex = 5;
        // 
        // dataGridView1
        // 
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Location = new System.Drawing.Point(12, 40);
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.RowHeadersWidth = 62;
        this.dataGridView1.RowTemplate.Height = 33;
        this.dataGridView1.Size = new System.Drawing.Size(775, 658);
        this.dataGridView1.TabIndex = 8;
        // 
        // dataGridView2
        // 
        this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView2.Location = new System.Drawing.Point(793, 40);
        this.dataGridView2.Name = "dataGridView2";
        this.dataGridView2.RowHeadersWidth = 62;
        this.dataGridView2.RowTemplate.Height = 33;
        this.dataGridView2.Size = new System.Drawing.Size(338, 658);
        this.dataGridView2.TabIndex = 9;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
        this.label1.Location = new System.Drawing.Point(278, -1);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(166, 38);
        this.label1.TabIndex = 10;
        this.label1.Text = "Residencies";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
        this.label3.Location = new System.Drawing.Point(914, -1);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(117, 38);
        this.label3.TabIndex = 11;
        this.label3.Text = "Waitlist";
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(793, 704);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(338, 34);
        this.button1.TabIndex = 12;
        this.button1.Text = "Create";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(12, 704);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(775, 34);
        this.button2.TabIndex = 13;
        this.button2.Text = "Print";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.label4.ForeColor = System.Drawing.Color.Black;
        this.label4.Location = new System.Drawing.Point(12, 9);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(96, 25);
        this.label4.TabIndex = 14;
        this.label4.Text = "Welcome:";
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
        this.label5.ForeColor = System.Drawing.Color.DarkOrange;
        this.label5.Location = new System.Drawing.Point(101, 9);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(59, 25);
        this.label5.TabIndex = 15;
        this.label5.Text = "label5";
        // 
        // secretarySP
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1143, 750);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.button1);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.dataGridView2);
        this.Controls.Add(this.dataGridView1);
        this.Controls.Add(this.label2);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
        this.Name = "secretarySP";
        this.Text = "secretarySP";
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion
    private Label label2;
    private DataGridView dataGridView1;
    private DataGridView dataGridView2;
    private Label label1;
    private Label label3;
    private Button button1;
    private Button button2;
    private Label label4;
    private Label label5;
}