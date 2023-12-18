namespace WindowsFormsApp6
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxTargetFolder = new System.Windows.Forms.TextBox();
            this.textBoxCopyFolder = new System.Windows.Forms.TextBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.listBoxForbiddenWords = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonTarget = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxTargetFolder
            // 
            this.textBoxTargetFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.textBoxTargetFolder.Location = new System.Drawing.Point(12, 12);
            this.textBoxTargetFolder.Name = "textBoxTargetFolder";
            this.textBoxTargetFolder.Size = new System.Drawing.Size(450, 44);
            this.textBoxTargetFolder.TabIndex = 0;
            // 
            // textBoxCopyFolder
            // 
            this.textBoxCopyFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.textBoxCopyFolder.Location = new System.Drawing.Point(610, 12);
            this.textBoxCopyFolder.Name = "textBoxCopyFolder";
            this.textBoxCopyFolder.Size = new System.Drawing.Size(542, 44);
            this.textBoxCopyFolder.TabIndex = 1;
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 62);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(223, 60);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "buttonLoad";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(241, 62);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(221, 62);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "buttonStart";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // listBoxForbiddenWords
            // 
            this.listBoxForbiddenWords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.listBoxForbiddenWords.FormattingEnabled = true;
            this.listBoxForbiddenWords.ItemHeight = 32;
            this.listBoxForbiddenWords.Location = new System.Drawing.Point(12, 130);
            this.listBoxForbiddenWords.Name = "listBoxForbiddenWords";
            this.listBoxForbiddenWords.Size = new System.Drawing.Size(450, 708);
            this.listBoxForbiddenWords.TabIndex = 4;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 850);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1274, 23);
            this.progressBar.TabIndex = 5;
            // 
            // buttonTarget
            // 
            this.buttonTarget.Location = new System.Drawing.Point(468, 12);
            this.buttonTarget.Name = "buttonTarget";
            this.buttonTarget.Size = new System.Drawing.Size(136, 44);
            this.buttonTarget.TabIndex = 6;
            this.buttonTarget.Text = "Target Folder";
            this.buttonTarget.UseVisualStyleBackColor = true;
            this.buttonTarget.Click += new System.EventHandler(this.buttonTarget_Click);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(1158, 12);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(128, 44);
            this.buttonCopy.TabIndex = 7;
            this.buttonCopy.Text = "Copy Folder";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(610, 130);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 62;
            this.dataGridView.RowTemplate.Height = 28;
            this.dataGridView.Size = new System.Drawing.Size(676, 708);
            this.dataGridView.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 885);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonTarget);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.listBoxForbiddenWords);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.textBoxCopyFolder);
            this.Controls.Add(this.textBoxTargetFolder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTargetFolder;
        private System.Windows.Forms.TextBox textBoxCopyFolder;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.ListBox listBoxForbiddenWords;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonTarget;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}

