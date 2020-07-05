namespace SimpleChess
{
    partial class PlayMode
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
            this.btnPvp = new System.Windows.Forms.Button();
            this.btnCvp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPvp
            // 
            this.btnPvp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPvp.Location = new System.Drawing.Point(77, 67);
            this.btnPvp.Name = "btnPvp";
            this.btnPvp.Size = new System.Drawing.Size(247, 80);
            this.btnPvp.TabIndex = 0;
            this.btnPvp.Text = "Player vs Player";
            this.btnPvp.UseVisualStyleBackColor = true;
            this.btnPvp.Click += new System.EventHandler(this.btnPvp_Click);
            // 
            // btnCvp
            // 
            this.btnCvp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCvp.Location = new System.Drawing.Point(77, 185);
            this.btnCvp.Name = "btnCvp";
            this.btnCvp.Size = new System.Drawing.Size(246, 76);
            this.btnCvp.TabIndex = 1;
            this.btnCvp.Text = "Computer vs Player";
            this.btnCvp.UseVisualStyleBackColor = true;
            this.btnCvp.Click += new System.EventHandler(this.btnCvp_Click);
            // 
            // PlayMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 341);
            this.Controls.Add(this.btnCvp);
            this.Controls.Add(this.btnPvp);
            this.Name = "PlayMode";
            this.Text = "PlayMode";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPvp;
        private System.Windows.Forms.Button btnCvp;
    }
}