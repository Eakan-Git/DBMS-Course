
namespace DBMS_G15
{
    partial class DriverForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DriverForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.btnSaveProduct = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.btnDeleteProduct);
            this.panel1.Controls.Add(this.btnSaveProduct);
            this.panel1.Controls.Add(this.btnAddProduct);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(736, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(72, 583);
            this.panel1.TabIndex = 2;
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDeleteProduct.FlatAppearance.BorderSize = 0;
            this.btnDeleteProduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDeleteProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDeleteProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteProduct.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteProduct.Image")));
            this.btnDeleteProduct.Location = new System.Drawing.Point(0, 380);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(72, 190);
            this.btnDeleteProduct.TabIndex = 18;
            this.btnDeleteProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnDeleteProduct.UseVisualStyleBackColor = true;
            // 
            // btnSaveProduct
            // 
            this.btnSaveProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSaveProduct.FlatAppearance.BorderSize = 0;
            this.btnSaveProduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSaveProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSaveProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveProduct.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveProduct.Image")));
            this.btnSaveProduct.Location = new System.Drawing.Point(0, 190);
            this.btnSaveProduct.Name = "btnSaveProduct";
            this.btnSaveProduct.Size = new System.Drawing.Size(72, 190);
            this.btnSaveProduct.TabIndex = 17;
            this.btnSaveProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSaveProduct.UseVisualStyleBackColor = true;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddProduct.FlatAppearance.BorderSize = 0;
            this.btnAddProduct.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAddProduct.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAddProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProduct.Image = ((System.Drawing.Image)(resources.GetObject("btnAddProduct.Image")));
            this.btnAddProduct.Location = new System.Drawing.Point(0, 0);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(72, 190);
            this.btnAddProduct.TabIndex = 16;
            this.btnAddProduct.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnAddProduct.UseVisualStyleBackColor = true;
            // 
            // DriverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(808, 583);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DriverForm";
            this.Text = "DriverForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Button btnSaveProduct;
        private System.Windows.Forms.Button btnAddProduct;
    }
}