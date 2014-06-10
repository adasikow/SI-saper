namespace DecisionTree
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
            this.components = new System.ComponentModel.Container();
            this._rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this._txtSourceFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._btnBrowse = new System.Windows.Forms.Button();
            this._btnBuildTree = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _rtxtOutput
            // 
            this._rtxtOutput.BackColor = System.Drawing.SystemColors.Info;
            this._rtxtOutput.Location = new System.Drawing.Point(15, 38);
            this._rtxtOutput.Name = "_rtxtOutput";
            this._rtxtOutput.Size = new System.Drawing.Size(577, 577);
            this._rtxtOutput.TabIndex = 0;
            this._rtxtOutput.Text = "";
            // 
            // _txtSourceFile
            // 
            this._txtSourceFile.BackColor = System.Drawing.Color.WhiteSmoke;
            this._txtSourceFile.Location = new System.Drawing.Point(88, 9);
            this._txtSourceFile.Name = "_txtSourceFile";
            this._txtSourceFile.Size = new System.Drawing.Size(383, 20);
            this._txtSourceFile.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Zbiór testowy";
            // 
            // _btnBrowse
            // 
            this._btnBrowse.Location = new System.Drawing.Point(477, 9);
            this._btnBrowse.Name = "_btnBrowse";
            this._btnBrowse.Size = new System.Drawing.Size(115, 23);
            this._btnBrowse.TabIndex = 4;
            this._btnBrowse.Text = "Przegl¹daj";
            this._btnBrowse.UseVisualStyleBackColor = true;
            this._btnBrowse.Click += new System.EventHandler(this._btnBrowse_Click);
            // 
            // _btnBuildTree
            // 
            this._btnBuildTree.Location = new System.Drawing.Point(392, 627);
            this._btnBuildTree.Name = "_btnBuildTree";
            this._btnBuildTree.Size = new System.Drawing.Size(200, 23);
            this._btnBuildTree.TabIndex = 5;
            this._btnBuildTree.Text = "Zbuduj drzewo";
            this._btnBuildTree.UseVisualStyleBackColor = true;
            this._btnBuildTree.Click += new System.EventHandler(this.onBuildTreeButtonClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(604, 662);
            this.Controls.Add(this._btnBuildTree);
            this.Controls.Add(this._btnBrowse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._txtSourceFile);
            this.Controls.Add(this._rtxtOutput);
            this.Name = "Form1";
            this.Text = "ID3";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox _rtxtOutput;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox _txtSourceFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _btnBrowse;
        private System.Windows.Forms.Button _btnBuildTree;
    }
}

