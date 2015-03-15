using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using Arrows;
using System.Runtime.InteropServices;

namespace FCF_Editor
{
    public class Labels
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        # region PROPERTIES
        public string type;
        public int id;
        public string title;
        public int left;
        public int top;
        public int right;
        public int bottom;
        public List<String[]> jumps;
        public List<String[]> flagOperations;

        /// <summary>
        /// Alternative choices, used in Selecters.
        /// </summary>
        /// <remarks>
        /// A List, which is made of string arrays.
        /// Each array must have two elements:
        /// 0: Text choice to display.
        /// 1: Target Jump ID, jumped to when the paired choice is selected.
        /// </remarks>
        public List<String[]> alts;
        public string file;
        public int target;

        //public List<String[]> checks; // For FHA SELECTERs
        # endregion

        /// <summary>
        /// Creates a Scene Object.
        /// </summary>
        /// <remarks>Assigns all the passed parameters to the public variables.</remarks>
        /// <param name="type">Specifies the Type. It's "SCENE" in this case.</param>
        public Labels(string type, int id, string title, int left, int top, int right, int bottom, Point point, List<String[]> jumps, List<String[]> flagOperations)
        {
            this.type = type;
            this.id = id;
            this.title = title;
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.jumps = jumps;
            this.flagOperations = flagOperations;
        }

        /// <summary>
        /// Creates a Selecter Object.
        /// </summary>
        /// <remarks>Assigns all the passed parameters to the public variables.</remarks>
        /// <param name="type">Specifies the Type. It's "SELECTER" in this case.</param>
        public Labels(string type, int id, string title, int left, int top, int right, int bottom, Point point, List<String[]> alts)
        {
            this.type = type;
            this.id = id;
            this.title = title;
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.alts = alts;
        }

        /// <summary>
        /// Creates a OuterLabel Object.
        /// </summary>
        /// <remarks>Assigns all the passed parameters to the public variables.</remarks>
        /// <param name="type">Specifies the Type. It's "OUTERLABEL" in this case.</param>
        public Labels(string type, int id, int left, int top, int right, int bottom, Point point, string file, int target)
        {
            this.type = type;
            this.id = id;
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.file = file;
            this.target = target;
        }

        /// <summary>
        /// Copies ALL the properties fields from another 'Labels' class object.
        /// </summary>
        /// <remarks>Assigns all the properties of the passed parameter to the new object.</remarks>
        /// <param name="label">The Labels object to clone.</param>
        public Labels(Labels label)
        {
            this.id = label.id;
            this.type = label.type;
            this.title = label.title;
            this.file = label.file;
            this.target = label.target;
            this.left = label.left;
            this.top = label.top;
            this.right = label.right;
            this.bottom = label.bottom;
            if (label.alts != null)
            {
                this.alts = new List<string[]>();
                for (int i = 0; i < label.alts.Count; i++) this.alts.Add(label.alts[i].Clone() as string[]);
            }

            if (label.jumps != null)
            {
                this.jumps = new List<string[]>();
                for (int i = 0; i < label.jumps.Count; i++) this.jumps.Add(label.jumps[i].Clone() as string[]);
            }

            if (label.flagOperations != null)
            {
                this.flagOperations = new List<string[]>();
                for (int i = 0; i < label.flagOperations.Count; i++) this.flagOperations.Add(label.flagOperations[i].Clone() as string[]);
            }
        }

        /// <summary>
        /// Creates a unique SELECTER Object for Fate/hollow Ataraxia.
        /// </summary>
        /// <remarks>Assigns all the passed parameters to the public variables.</remarks>
        /// <param name="type">Specifies the Type. It's "SELECTER" in this case.</param>
        public Labels(string type, int id, string title, int left, int top, int right, int bottom, List<String[]> alts, List<string[]> check)
        {
            this.type = type;
            this.id = id;
            this.title = title;
            this.left = left;
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.alts = alts;
            this.jumps = check;
        }

        /// <summary>
        /// Creates a TextBox object with the properties of Scene object.
        /// </summary>
        /// <remarks>
        /// The Name of the TextBox is "SCENE_" + id.
        /// The Text of the TextBox is "SCENE: " + id.
        /// </remarks>
        /// <returns>Returns a TextBox.</returns>
        public TextBox drawSceneButton()
        {
            TextBox scene = new TextBox();
            scene.AutoSize = true;
            scene.ReadOnly = true;
            scene.BackColor = Properties.Settings.Default.scene_color;
            scene.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            scene.Cursor = System.Windows.Forms.Cursors.Hand;
            scene.Font = Properties.Settings.Default.scene_font;
            scene.ForeColor = Properties.Settings.Default.scene_fore_color;
            scene.Name = "SCENE_" + id;
            scene.TabIndex = 0;
            scene.Text = "SCENE: " + id;
            scene.TabStop = true;
            scene.AcceptsTab = false;
            //scene.SelectedText = "";
            //scene.Enabled = false;
            scene.TextAlign = HorizontalAlignment.Center;
            scene.GotFocus += textbox_GotFocus;

            return scene;
        }

        /// <summary>
        /// Creates a TextBox object with the properties of Selecter object.
        /// </summary>
        /// <remarks>
        /// The Name of the TextBox is "SELECTER_" + id.
        /// The Text of the TextBox is "SELECTER: " + id.
        /// </remarks>
        /// <returns>Returns a TextBox.</returns>
        public TextBox drawSelecterButton()
        {
            TextBox selecter = new TextBox();
            selecter.AutoSize = true;
            selecter.ReadOnly = true;
            selecter.BackColor = Properties.Settings.Default.selecter_color;
            selecter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            selecter.Cursor = System.Windows.Forms.Cursors.Hand;
            selecter.Font = Properties.Settings.Default.selecter_font;
            selecter.ForeColor = Properties.Settings.Default.selecter_fore_color;
            selecter.Name = "SELECTER_" + id;
            selecter.TabIndex = 0;
            selecter.TabStop = true;
            selecter.AcceptsTab = false;
            selecter.Text = "SELECTER: " + id;
            //selecter.SelectedText = "";
            //selecter.Enabled = false;
            selecter.TextAlign = HorizontalAlignment.Center;
            selecter.GotFocus += textbox_GotFocus;

            return selecter;
        }

        /// <summary>
        /// Creates a TextBox object with the properties of OuterLabel object.
        /// </summary>
        /// <remarks>
        /// The Name of the TextBox is "OUTERLABEL_" + id.
        /// The Text of the TextBox is "OUTERLABEL: " + id.
        /// </remarks>
        /// <returns>Returns a TextBox.</returns>
        public TextBox drawOuterLabel()
        {
            TextBox outerlabel = new TextBox();
            outerlabel.AutoSize = true;
            outerlabel.ReadOnly = true;
            outerlabel.BackColor = Properties.Settings.Default.outerlabel_color;
            outerlabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            outerlabel.Cursor = System.Windows.Forms.Cursors.Hand;
            outerlabel.Font = Properties.Settings.Default.outerlabel_font;
            outerlabel.ForeColor = Properties.Settings.Default.outerlabel_fore_color;
            outerlabel.Name = "OUTERLABEL_" + id;
            outerlabel.TabIndex = 0;
            outerlabel.TabStop = true;
            outerlabel.AcceptsTab = false;
            outerlabel.Text = "OUTERLABEL: " + id;
            //outerlabel.SelectedText = "";
            //outerlabel.Enabled = false;
            outerlabel.TextAlign = HorizontalAlignment.Center;
            outerlabel.GotFocus += textbox_GotFocus;

            return outerlabel;
        }

        private void textbox_GotFocus(object sender, EventArgs e)
        {
            HideCaret(((TextBox)sender).Handle);
        }
    }
}
