namespace DesenhaPrimitivas
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
            panel1 = new Panel();
            label1 = new Label();
            panel4 = new Panel();
            label3 = new Label();
            panel2 = new Panel();
            label2 = new Label();
            panel3 = new Panel();
            label4 = new Label();
            bt1 = new Button();
            bt2 = new Button();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.PaleGoldenrod;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(400, 300);
            panel1.TabIndex = 2;
            panel1.MouseClick += panel1_MouseClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Enabled = false;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(43, 145);
            label1.Name = "label1";
            label1.Size = new Size(318, 21);
            label1.TabIndex = 0;
            label1.Text = "Click onde desejar para desenhar o poligono";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            panel4.BackColor = Color.DarkKhaki;
            panel4.Controls.Add(label3);
            panel4.Location = new Point(12, 329);
            panel4.Name = "panel4";
            panel4.Size = new Size(300, 180);
            panel4.TabIndex = 2;
            panel4.Paint += panel1_Paint;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Enabled = false;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.DarkSlateGray;
            label3.Location = new Point(79, 81);
            label3.Name = "label3";
            label3.Size = new Size(142, 21);
            label3.TabIndex = 0;
            label3.Text = "Desenho recortado";
            // 
            // panel2
            // 
            panel2.BackColor = Color.PaleGoldenrod;
            panel2.Controls.Add(label2);
            panel2.Location = new Point(454, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(400, 300);
            panel2.TabIndex = 1;
            panel2.MouseClick += panel2_MouseClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Enabled = false;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(43, 145);
            label2.Name = "label2";
            label2.Size = new Size(323, 21);
            label2.TabIndex = 0;
            label2.Text = "Click onde desejar para desenhar o retangulo";
            // 
            // panel3
            // 
            panel3.BackColor = Color.DarkKhaki;
            panel3.Controls.Add(label4);
            panel3.Location = new Point(454, 329);
            panel3.Name = "panel3";
            panel3.Size = new Size(300, 180);
            panel3.TabIndex = 1;
            panel3.Paint += panel2_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Enabled = false;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = Color.DarkSlateGray;
            label4.Location = new Point(79, 81);
            label4.Name = "label4";
            label4.Size = new Size(142, 21);
            label4.TabIndex = 0;
            label4.Text = "Desenho recortado";
            // 
            // bt1
            // 
            bt1.Location = new Point(337, 329);
            bt1.Name = "bt1";
            bt1.Size = new Size(75, 23);
            bt1.TabIndex = 3;
            bt1.Text = "Limpar";
            bt1.UseVisualStyleBackColor = true;
            bt1.Click += bt1_Click;
            // 
            // bt2
            // 
            bt2.Location = new Point(779, 329);
            bt2.Name = "bt2";
            bt2.Size = new Size(75, 23);
            bt2.TabIndex = 4;
            bt2.Text = "Limpar";
            bt2.UseVisualStyleBackColor = true;
            bt2.Click += bt2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(873, 522);
            Controls.Add(bt2);
            Controls.Add(bt1);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Recorte figuras";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Panel panel2;
        private Panel panel4;
        private Panel panel3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button bt1;
        private Button bt2;
    }
}