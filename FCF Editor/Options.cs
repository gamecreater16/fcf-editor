using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCF_Editor
{
    public partial class options : Form
    {
        private Control currentButton;

        public options()
        {
            InitializeComponent();

            // FOR ARROWS!
            Bitmap arrowsBitmap = new Bitmap(arrowsColor.Image);
            for (int Xcount = 0; Xcount < arrowsBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < arrowsBitmap.Height; Ycount++)
                {
                    arrowsBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.arrows_color);
                }
            } arrowsColor.Image = arrowsBitmap;

            // FOR SCENES!
            Bitmap sceneBitmap = (Bitmap)sceneColor.Image;
            for (int Xcount = 0; Xcount < sceneBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < sceneBitmap.Height; Ycount++)
                {
                    sceneBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.scene_color);
                }
            } 
            sceneColor.Image = sceneBitmap;
            sceneColor.Font = Properties.Settings.Default.scene_font;
            sceneColor.ForeColor = Properties.Settings.Default.scene_fore_color;
            /////////////////////////////////////////////////////////////////////////

            // FOR SELECTERS!
            Bitmap selecterBitmap = (Bitmap)selecterColor.Image;
            for (int Xcount = 0; Xcount < selecterBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < selecterBitmap.Height; Ycount++)
                {
                    selecterBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.selecter_color);
                }
            }
            selecterColor.Image = selecterBitmap;
            selecterColor.Font = Properties.Settings.Default.selecter_font;
            selecterColor.ForeColor = Properties.Settings.Default.selecter_fore_color;
            //////////////////////////////////////////////////////////////////////////////

            // FOR OUTERLABELs
            Bitmap outerlabelBitmap = (Bitmap)outerlabelColor.Image;
            for (int Xcount = 0; Xcount < outerlabelBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < outerlabelBitmap.Height; Ycount++)
                {
                    outerlabelBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.outerlabel_color);
                }
            }
            outerlabelColor.Image = outerlabelBitmap;
            outerlabelColor.Font = Properties.Settings.Default.outerlabel_font;
            outerlabelColor.ForeColor = Properties.Settings.Default.outerlabel_fore_color;
            /////////////////////////////////////////////////////////////////////////////////////

            variable_file_path.Text = Properties.Settings.Default.Variable_filepath;
            folder_path.Text = Properties.Settings.Default.Flowchart_folderpath;
        }

        private void variable_browse_Click(object sender, EventArgs e)
        {
            open_variables_file.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            System.IO.FileInfo fInfo = new System.IO.FileInfo(open_variables_file.FileName);
            Properties.Settings.Default.Variable_filepath = fInfo.FullName;
            variable_file_path.Text = Properties.Settings.Default.Variable_filepath;
            Properties.Settings.Default.Save();
        }

        private void folder_browse_Click(object sender, EventArgs e)
        {
            fcf_folder_browser.SelectedPath = Properties.Settings.Default.Flowchart_folderpath;
            DialogResult result = fcf_folder_browser.ShowDialog();
            Properties.Settings.Default.Flowchart_folderpath = fcf_folder_browser.SelectedPath;// +'\\';
            folder_path.Text = Properties.Settings.Default.Flowchart_folderpath;
            Properties.Settings.Default.Save();
        }

        private void save_options_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void folder_path_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Flowchart_folderpath = folder_path.Text;
            Properties.Settings.Default.Save();
        }

        private void variable_file_path_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Variable_filepath = variable_file_path.Text;
            Properties.Settings.Default.Save();
        }

        private void arrowsColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = Properties.Settings.Default.arrows_color;
            DialogResult result = colorDialog.ShowDialog();
            Properties.Settings.Default.arrows_color = colorDialog.Color;

            Bitmap arrowsBitmap = (Bitmap)arrowsColor.Image;
            for (int Xcount = 0; Xcount < arrowsBitmap.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < arrowsBitmap.Height; Ycount++)
                {
                    arrowsBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.arrows_color);
                }
            }
            arrowsColor.Image = arrowsBitmap;
            Properties.Settings.Default.Save();
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chooseFontOrColor.SourceControl != null)
            {
                currentButton = chooseFontOrColor.SourceControl;
            }
            if (currentButton == sceneColor)
            {
                fontDialog.Color = Properties.Settings.Default.scene_fore_color;
                fontDialog.Font = Properties.Settings.Default.scene_font;
                DialogResult result = fontDialog.ShowDialog();

                Properties.Settings.Default.scene_font = fontDialog.Font;
                Properties.Settings.Default.scene_fore_color = fontDialog.Color;
                sceneColor.Font = Properties.Settings.Default.scene_font;
                sceneColor.ForeColor = Properties.Settings.Default.scene_fore_color;
            }
            else if (currentButton == selecterColor)
            {
                fontDialog.Color = Properties.Settings.Default.selecter_fore_color;
                fontDialog.Font = Properties.Settings.Default.selecter_font;
                DialogResult result = fontDialog.ShowDialog();

                Properties.Settings.Default.selecter_font = fontDialog.Font;
                Properties.Settings.Default.selecter_fore_color = fontDialog.Color;
                selecterColor.Font = Properties.Settings.Default.selecter_font;
                selecterColor.ForeColor = Properties.Settings.Default.selecter_fore_color;
            }
            else
            {
                fontDialog.Color = Properties.Settings.Default.outerlabel_fore_color;
                fontDialog.Font = Properties.Settings.Default.outerlabel_font;
                DialogResult result = fontDialog.ShowDialog();

                Properties.Settings.Default.outerlabel_font = fontDialog.Font;
                Properties.Settings.Default.outerlabel_fore_color = fontDialog.Color;
                outerlabelColor.Font = Properties.Settings.Default.outerlabel_font;
                outerlabelColor.ForeColor = Properties.Settings.Default.outerlabel_fore_color;
            }

            Properties.Settings.Default.Save();
            currentButton = null;
        }

        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chooseFontOrColor.SourceControl != null)
            {
                currentButton = chooseFontOrColor.SourceControl;
            }

            if (currentButton == sceneColor)
            {
                colorDialog.Color = Properties.Settings.Default.scene_color;
                DialogResult result = colorDialog.ShowDialog();
                Properties.Settings.Default.scene_color = colorDialog.Color;

                Bitmap sceneBitmap = (Bitmap)sceneColor.Image;
                for (int Xcount = 0; Xcount < sceneBitmap.Width; Xcount++)
                {
                    for (int Ycount = 0; Ycount < sceneBitmap.Height; Ycount++)
                    {
                        sceneBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.scene_color);
                    }
                }
                sceneColor.Image = sceneBitmap;
            }
            else if (currentButton == selecterColor)
            {
                colorDialog.Color = Properties.Settings.Default.selecter_color;
                DialogResult result = colorDialog.ShowDialog();
                Properties.Settings.Default.selecter_color = colorDialog.Color;

                Bitmap selecterBitmap = (Bitmap)selecterColor.Image;
                for (int Xcount = 0; Xcount < selecterBitmap.Width; Xcount++)
                {
                    for (int Ycount = 0; Ycount < selecterBitmap.Height; Ycount++)
                    {
                        selecterBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.selecter_color);
                    }
                }
                selecterColor.Image = selecterBitmap;
            }
            else
            {
                colorDialog.Color = Properties.Settings.Default.outerlabel_color;
                DialogResult result = colorDialog.ShowDialog();
                Properties.Settings.Default.outerlabel_color = colorDialog.Color;

                Bitmap outerlabelBitmap = (Bitmap)outerlabelColor.Image;
                for (int Xcount = 0; Xcount < outerlabelBitmap.Width; Xcount++)
                {
                    for (int Ycount = 0; Ycount < outerlabelBitmap.Height; Ycount++)
                    {
                       outerlabelBitmap.SetPixel(Xcount, Ycount, Properties.Settings.Default.outerlabel_color);
                    }
                }
                outerlabelColor.Image = outerlabelBitmap;
            }
            
            Properties.Settings.Default.Save();
            currentButton = null;
        }

        private void sceneColor_Click(object sender, EventArgs e)
        {
            currentButton = sceneColor;
            chooseFontOrColor.Show(Cursor.Position);
        }

        private void selecterColor_Click(object sender, EventArgs e)
        {
            currentButton = selecterColor;
            chooseFontOrColor.Show(Cursor.Position);
        }

        private void outerlabelColor_Click(object sender, EventArgs e)
        {
            currentButton = outerlabelColor;
            chooseFontOrColor.Show(Cursor.Position);
        }


# region DESIGNER

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(options));
            this.label1 = new System.Windows.Forms.Label();
            this.variable_file_path = new System.Windows.Forms.TextBox();
            this.open_variables_file = new System.Windows.Forms.OpenFileDialog();
            this.variable_browse = new System.Windows.Forms.Button();
            this.save_options = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.folder_path = new System.Windows.Forms.TextBox();
            this.folder_browse = new System.Windows.Forms.Button();
            this.fcf_folder_browser = new System.Windows.Forms.FolderBrowserDialog();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.arrowsColor = new System.Windows.Forms.Button();
            this.sceneColor = new System.Windows.Forms.Button();
            this.chooseFontOrColor = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fontColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.selecterColor = new System.Windows.Forms.Button();
            this.outerlabelColor = new System.Windows.Forms.Button();
            this.chooseFontOrColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Variables file:";
            // 
            // variable_file_path
            // 
            this.variable_file_path.Location = new System.Drawing.Point(112, 26);
            this.variable_file_path.Name = "variable_file_path";
            this.variable_file_path.Size = new System.Drawing.Size(298, 20);
            this.variable_file_path.TabIndex = 17;
            this.variable_file_path.TextChanged += new System.EventHandler(this.variable_file_path_TextChanged);
            // 
            // open_variables_file
            // 
            this.open_variables_file.DefaultExt = "txt";
            this.open_variables_file.FileName = "variables.txt";
            this.open_variables_file.Filter = "Variables file|variables.txt";
            this.open_variables_file.Title = "Read the Variables\' Info";
            this.open_variables_file.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // variable_browse
            // 
            this.variable_browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.variable_browse.Location = new System.Drawing.Point(416, 24);
            this.variable_browse.Name = "variable_browse";
            this.variable_browse.Size = new System.Drawing.Size(59, 23);
            this.variable_browse.TabIndex = 18;
            this.variable_browse.Text = "Browse";
            this.variable_browse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.variable_browse.UseVisualStyleBackColor = true;
            this.variable_browse.Click += new System.EventHandler(this.variable_browse_Click);
            // 
            // save_options
            // 
            this.save_options.Location = new System.Drawing.Point(183, 163);
            this.save_options.Name = "save_options";
            this.save_options.Size = new System.Drawing.Size(102, 31);
            this.save_options.TabIndex = 19;
            this.save_options.Text = "Save";
            this.save_options.UseVisualStyleBackColor = true;
            this.save_options.Click += new System.EventHandler(this.save_options_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(3, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Flowchart folder:";
            // 
            // folder_path
            // 
            this.folder_path.Location = new System.Drawing.Point(111, 59);
            this.folder_path.Name = "folder_path";
            this.folder_path.Size = new System.Drawing.Size(298, 20);
            this.folder_path.TabIndex = 21;
            this.folder_path.TextChanged += new System.EventHandler(this.folder_path_TextChanged);
            // 
            // folder_browse
            // 
            this.folder_browse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.folder_browse.Location = new System.Drawing.Point(416, 58);
            this.folder_browse.Name = "folder_browse";
            this.folder_browse.Size = new System.Drawing.Size(59, 23);
            this.folder_browse.TabIndex = 22;
            this.folder_browse.Text = "Browse";
            this.folder_browse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.folder_browse.UseVisualStyleBackColor = true;
            this.folder_browse.Click += new System.EventHandler(this.folder_browse_Click);
            // 
            // fcf_folder_browser
            // 
            this.fcf_folder_browser.Description = "Browse to the folder containing all the flowchart files";
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.SolidColorOnly = true;
            // 
            // arrowsColor
            // 
            this.arrowsColor.AutoSize = true;
            this.arrowsColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.arrowsColor.Image = ((System.Drawing.Image)(resources.GetObject("arrowsColor.Image")));
            this.arrowsColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.arrowsColor.Location = new System.Drawing.Point(12, 97);
            this.arrowsColor.Name = "arrowsColor";
            this.arrowsColor.Size = new System.Drawing.Size(84, 32);
            this.arrowsColor.TabIndex = 23;
            this.arrowsColor.Text = "Arrows";
            this.arrowsColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.arrowsColor.UseVisualStyleBackColor = true;
            this.arrowsColor.Click += new System.EventHandler(this.arrowsColor_Click);
            // 
            // sceneColor
            // 
            this.sceneColor.AutoSize = true;
            this.sceneColor.ContextMenuStrip = this.chooseFontOrColor;
            this.sceneColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.sceneColor.Image = ((System.Drawing.Image)(resources.GetObject("sceneColor.Image")));
            this.sceneColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sceneColor.Location = new System.Drawing.Point(112, 97);
            this.sceneColor.Name = "sceneColor";
            this.sceneColor.Size = new System.Drawing.Size(88, 32);
            this.sceneColor.TabIndex = 24;
            this.sceneColor.Text = "SCENE";
            this.sceneColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.sceneColor.UseVisualStyleBackColor = true;
            this.sceneColor.Click += new System.EventHandler(this.sceneColor_Click);
            // 
            // chooseFontOrColor
            // 
            this.chooseFontOrColor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontColorToolStripMenuItem,
            this.backColorToolStripMenuItem});
            this.chooseFontOrColor.Name = "chooseFontOrColor";
            this.chooseFontOrColor.ShowImageMargin = false;
            this.chooseFontOrColor.Size = new System.Drawing.Size(107, 48);
            // 
            // fontColorToolStripMenuItem
            // 
            this.fontColorToolStripMenuItem.Name = "fontColorToolStripMenuItem";
            this.fontColorToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.fontColorToolStripMenuItem.Text = "Font Color";
            this.fontColorToolStripMenuItem.Click += new System.EventHandler(this.fontColorToolStripMenuItem_Click);
            // 
            // backColorToolStripMenuItem
            // 
            this.backColorToolStripMenuItem.Name = "backColorToolStripMenuItem";
            this.backColorToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.backColorToolStripMenuItem.Text = "Back Color";
            this.backColorToolStripMenuItem.Click += new System.EventHandler(this.backColorToolStripMenuItem_Click);
            // 
            // fontDialog
            // 
            this.fontDialog.FontMustExist = true;
            this.fontDialog.ShowApply = true;
            this.fontDialog.ShowColor = true;
            // 
            // selecterColor
            // 
            this.selecterColor.AutoSize = true;
            this.selecterColor.ContextMenuStrip = this.chooseFontOrColor;
            this.selecterColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.selecterColor.Image = ((System.Drawing.Image)(resources.GetObject("selecterColor.Image")));
            this.selecterColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.selecterColor.Location = new System.Drawing.Point(219, 97);
            this.selecterColor.Name = "selecterColor";
            this.selecterColor.Size = new System.Drawing.Size(110, 32);
            this.selecterColor.TabIndex = 27;
            this.selecterColor.Text = "SELECTER";
            this.selecterColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.selecterColor.UseVisualStyleBackColor = true;
            this.selecterColor.Click += new System.EventHandler(this.selecterColor_Click);
            // 
            // outerlabelColor
            // 
            this.outerlabelColor.AutoSize = true;
            this.outerlabelColor.ContextMenuStrip = this.chooseFontOrColor;
            this.outerlabelColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.outerlabelColor.Image = ((System.Drawing.Image)(resources.GetObject("outerlabelColor.Image")));
            this.outerlabelColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.outerlabelColor.Location = new System.Drawing.Point(344, 97);
            this.outerlabelColor.Name = "outerlabelColor";
            this.outerlabelColor.Size = new System.Drawing.Size(126, 32);
            this.outerlabelColor.TabIndex = 28;
            this.outerlabelColor.Text = "OUTERLABEL";
            this.outerlabelColor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.outerlabelColor.UseVisualStyleBackColor = true;
            this.outerlabelColor.Click += new System.EventHandler(this.outerlabelColor_Click);
            // 
            // options
            // 
            this.AcceptButton = this.save_options;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 206);
            this.Controls.Add(this.outerlabelColor);
            this.Controls.Add(this.selecterColor);
            this.Controls.Add(this.sceneColor);
            this.Controls.Add(this.arrowsColor);
            this.Controls.Add(this.folder_browse);
            this.Controls.Add(this.folder_path);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.save_options);
            this.Controls.Add(this.variable_browse);
            this.Controls.Add(this.variable_file_path);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(509, 245);
            this.Name = "options";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.chooseFontOrColor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox variable_file_path;
        private System.Windows.Forms.OpenFileDialog open_variables_file;
        private System.Windows.Forms.Button variable_browse;
        private Button save_options;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox folder_path;
        private System.Windows.Forms.Button folder_browse;
        private System.Windows.Forms.FolderBrowserDialog fcf_folder_browser;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button arrowsColor;
        private System.Windows.Forms.Button sceneColor;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ContextMenuStrip chooseFontOrColor;
        private System.Windows.Forms.ToolStripMenuItem fontColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backColorToolStripMenuItem;
        private System.Windows.Forms.Button selecterColor;
        private System.Windows.Forms.Button outerlabelColor;

        # endregion

    }
}