namespace hasta_takip
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Menu = new System.Windows.Forms.GroupBox();
            this.AraMenu = new System.Windows.Forms.GroupBox();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(49, 75);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Muayene";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(49, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 46);
            this.button2.TabIndex = 1;
            this.button2.Text = "Randevu";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(49, 243);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(91, 44);
            this.button3.TabIndex = 2;
            this.button3.Text = "Bebek İzlem";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(49, 324);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 41);
            this.button4.TabIndex = 3;
            this.button4.Text = "Hastalar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Menu
            // 
            this.Menu.Controls.Add(this.button1);
            this.Menu.Controls.Add(this.button4);
            this.Menu.Controls.Add(this.button2);
            this.Menu.Controls.Add(this.button3);
            this.Menu.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Menu.Location = new System.Drawing.Point(54, 12);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(200, 420);
            this.Menu.TabIndex = 4;
            this.Menu.TabStop = false;
            this.Menu.Text = "Menu";
            // 
            // AraMenu
            // 
            this.AraMenu.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.AraMenu.Location = new System.Drawing.Point(292, 12);
            this.AraMenu.Name = "AraMenu";
            this.AraMenu.Size = new System.Drawing.Size(778, 420);
            this.AraMenu.TabIndex = 5;
            this.AraMenu.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 572);
            this.Controls.Add(this.AraMenu);
            this.Controls.Add(this.Menu);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox Menu;
        private System.Windows.Forms.GroupBox AraMenu;
    }
}

