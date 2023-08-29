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
            panel4 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.PapayaWhip;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(400, 300);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            panel1.MouseClick += panel1_MouseClick;
            // 
            // panel4
            // 
            panel4.BackColor = Color.PeachPuff;
            panel4.Location = new Point(12, 12);
            panel4.Name = "panel4";
            panel4.Size = new Size(200, 150);
            panel4.TabIndex = 2;
            panel4.Paint += panel1_Paint;
            panel4.MouseClick += panel1_MouseClick;
            // 
            // panel2
            // 
            panel2.BackColor = Color.PapayaWhip;
            panel2.Location = new Point(454, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(400, 300);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            panel2.MouseClick += panel2_MouseClick;
            // 
            // panel3
            // 
            panel3.BackColor = Color.PeachPuff;
            panel3.Location = new Point(454, 12);
            panel3.Name = "panel3";
            panel3.Size = new Size(200, 150);
            panel3.TabIndex = 1;
            panel3.Paint += panel2_Paint;
            panel3.MouseClick += panel2_MouseClick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(869, 333);
            Controls.Add(panel3);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }


        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel4;
        private Panel panel3;
    }
}