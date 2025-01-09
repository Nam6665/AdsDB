namespace AdsDB
{
    partial class MainForm
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
            this.btnAdd_Click = new System.Windows.Forms.Button();
            this.btnEdit_Click = new System.Windows.Forms.Button();
            this.btnDelete_Click = new System.Windows.Forms.Button();
            this.dgvAds = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAds)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd_Click
            // 
            this.btnAdd_Click.Location = new System.Drawing.Point(26, 39);
            this.btnAdd_Click.Name = "btnAdd_Click";
            this.btnAdd_Click.Size = new System.Drawing.Size(75, 23);
            this.btnAdd_Click.TabIndex = 0;
            this.btnAdd_Click.Text = "Add";
            this.btnAdd_Click.UseVisualStyleBackColor = true;
            this.btnAdd_Click.Click += new System.EventHandler(this.btnAdd_Click_Click);
            // 
            // btnEdit_Click
            // 
            this.btnEdit_Click.Location = new System.Drawing.Point(26, 68);
            this.btnEdit_Click.Name = "btnEdit_Click";
            this.btnEdit_Click.Size = new System.Drawing.Size(75, 23);
            this.btnEdit_Click.TabIndex = 1;
            this.btnEdit_Click.Text = "Edit";
            this.btnEdit_Click.UseVisualStyleBackColor = true;
            this.btnEdit_Click.Click += new System.EventHandler(this.btnEdit_Click_Click);
            // 
            // btnDelete_Click
            // 
            this.btnDelete_Click.Location = new System.Drawing.Point(26, 97);
            this.btnDelete_Click.Name = "btnDelete_Click";
            this.btnDelete_Click.Size = new System.Drawing.Size(75, 23);
            this.btnDelete_Click.TabIndex = 2;
            this.btnDelete_Click.Text = "Delete";
            this.btnDelete_Click.UseVisualStyleBackColor = true;
            this.btnDelete_Click.Click += new System.EventHandler(this.btnDelete_Click_Click);
            // 
            // dgvAds
            // 
            this.dgvAds.AllowUserToOrderColumns = true;
            this.dgvAds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAds.Location = new System.Drawing.Point(169, 39);
            this.dgvAds.Name = "dgvAds";
            this.dgvAds.Size = new System.Drawing.Size(418, 274);
            this.dgvAds.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvAds);
            this.Controls.Add(this.btnDelete_Click);
            this.Controls.Add(this.btnEdit_Click);
            this.Controls.Add(this.btnAdd_Click);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd_Click;
        private System.Windows.Forms.Button btnEdit_Click;
        private System.Windows.Forms.Button btnDelete_Click;
        private System.Windows.Forms.DataGridView dgvAds;
    }
}

