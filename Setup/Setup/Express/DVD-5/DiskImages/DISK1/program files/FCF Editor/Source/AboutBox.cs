using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCF_Editor
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            //this.labelCopyright.Text = AssemblyCopyright;
            //this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = Properties.Resources.README;
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void changelog_Click(object sender, EventArgs e)
        {
            # region FORM changelog
            Form changelog = new Form();
            changelog.AutoScroll = true;
            changelog.Width = 400;
            changelog.Height = 600;
            changelog.MaximizeBox = false;
            changelog.MinimizeBox = false;
            changelog.MaximumSize = new System.Drawing.Size(400, 600);
            changelog.MinimumSize = new System.Drawing.Size(400, 600);
            changelog.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            # endregion

            # region flowLayout
            FlowLayoutPanel flowLayout = new FlowLayoutPanel();
            flowLayout.Width = changelog.Width - 40;
            flowLayout.Dock = DockStyle.Fill;
            flowLayout.AutoScroll = true;
            # endregion

            string[] changeData = Properties.Resources.Changelog.Split('\n');
            int count = 0;
            for (int i = 0; i < changeData.Length; i++)
            {
                string version = "";
                string date = "";
                string status = "";

                # region versionInfo
                CollapsiblePanel versionInfo = new CollapsiblePanel();
                versionInfo.BackColor = System.Drawing.Color.Transparent;
                versionInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
                versionInfo.HeaderCornersRadius = 5;
                versionInfo.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
                versionInfo.HeaderImage = null;
                versionInfo.HeaderTextColor = System.Drawing.Color.Black;
                versionInfo.Location = new System.Drawing.Point(40, 40);
                versionInfo.RoundedCorners = true;
                versionInfo.Size = new System.Drawing.Size(340, 200);
                versionInfo.TabIndex = 0;
                versionInfo.Top = count * 10;
                versionInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                versionInfo.Font = new Font("Calibri", 12, FontStyle.Bold);
                versionInfo.Width = flowLayout.Width - 10;
                versionInfo.Collapse = true;
                //versionInfo.UseAnimation = true;
                # endregion

                if (changeData[i].Contains("<===="))
                {
                    string[] versionHeader = changeData[i].Split(new string[] { "<====" }, StringSplitOptions.RemoveEmptyEntries)[0].Split(new string[] { "====>" }, StringSplitOptions.RemoveEmptyEntries);
                    version = versionHeader[0].Split('(')[0];
                    date = versionHeader[0].Split('(')[1].Split(')')[0];
                    status = versionHeader[1].Replace("*", "");

                    count++;

                    versionInfo.HeaderText = version;
                    versionInfo.Name = version;

                    # region detail
                    Label detail = new Label();
                    detail.Text = "Release Date: " + date + ". Status: " + status;
                    detail.Top = 40;// 20;
                    detail.Left = 20;
                    detail.Width = versionInfo.Width;
                    detail.AutoSize = true;
                    detail.Font = new Font("Calibri", 10, FontStyle.Bold);
                    # endregion
                    versionInfo.Controls.Add(detail);

                    # region changes
                    TextBox changes = new TextBox();
                    changes.Top = 60;// 40;
                    changes.Left = 15;
                    changes.Width = 320;
                    changes.Multiline = true;
                    changes.WordWrap = false;
                    changes.ScrollBars = ScrollBars.Both;
                    changes.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                    changes.Height = 120;
                    changes.ReadOnly = true;
                    # endregion

                    for (int j = i; j < changeData.Length; j++)
                    {
                        if (changeData[i] != changeData[j])
                        {
                            if (!changeData[j].Contains("<====") && !changeData[j].Contains(Environment.NewLine))
                            {
                                changes.Text += (changeData[j].First().ToString()) + changeData[j].Remove(0, 1);
                                changes.Text += Environment.NewLine;
                            }
                            else break;
                        }
                    }
                    // To remove empty lines!
                    string[] lines = changes.Lines;
                    lines = lines.Where(w => w != "").ToArray();
                    changes.Lines = lines;

                    versionInfo.Controls.Add(changes);

                    flowLayout.Controls.Add(versionInfo);
                }
            }
            changelog.Controls.Add(flowLayout);
            changelog.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            changelog.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            changelog.ResumeLayout(false);
            changelog.ShowDialog();
            return;
        }
    }
}
