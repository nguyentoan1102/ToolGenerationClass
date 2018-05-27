using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace GenerationClass
{
    public partial class Process : Form
    {
        #region

        public Process()
        {
            InitializeComponent();
        }

        public Process(Collection<string> objects)
        {
            this.InitializeComponent();

            this.Objects = objects;

            // Get Defaults.
            this.lblPath.Text = ""; //Settings.Default.DefaultPath;
            if (string.IsNullOrEmpty(this.lblPath.Text))
            {
                this.lblPath.Text = Application.StartupPath;
            }

            //this.txtNameSpace.Text = Settings.Default.NameSpace;
            //if (string.IsNullOrEmpty(this.txtNameSpace.Text))
            //{
            //    this.txtNameSpace.Text = ApplicationAttributes.AssemblyTitle;
            //}

            //this.txtCreator.Text = Settings.Default.Creator;
            //if (string.IsNullOrEmpty(this.txtNameSpace.Text))
            //{
            //    this.txtNameSpace.Text = this.txtNameSpace.Text;
            //}
        }

        #endregion

        #region Properties

        /// <summary>
        /// Internal collection of objects to process.
        /// </summary>
        public Collection<string> Objects { get; set; }

        #endregion

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            // Display the folder browser dialog.
            this.fbdProcess.ShowDialog();

            if (string.IsNullOrEmpty(this.fbdProcess.SelectedPath))
            {
                this.lblPath.Text = Application.StartupPath;
            }
            else
            {
                this.lblPath.Text = this.fbdProcess.SelectedPath;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //this.pbStatus.Visible = true;

            //// Preserve defaults.
            ////Settings.Default.DefaultPath = this.lblPath.Text;
            ////Settings.Default.NameSpace = this.txtNameSpace.Text;
            ////Settings.Default.Creator = this.txtCreator.Text;
            ////Settings.Default.Save();

            //try
            //{
            //    // For each object, instantiate a ProcessObjects class.
            //    foreach (string item in this.Objects)
            //    {
            //        using (ProcessObjects po = new ProcessObjects())
            //        {
            //            // Gather defaults.
            //            po.ObjectName = item;
            //            po.SavePath = this.lblPath.Text;
            //            po.NameSpaceName = this.txtNameSpace.Text;
            //            po.Creator = this.txtCreator.Text;

            //            // Now process the object.
            //            po.ProcessObject();
            //        }
            //    }

            //    this.pbStatus.Visible = false;
            //    this.Cursor = Cursors.Default;

            //    // If we got here we're probably done.
            //    //string created =
            //    //    string.Format(
            //    //        CultureInfo.InvariantCulture,
            //    //        Resources.OBJECTS_CREATED,
            //    //        Environment.NewLine,
            //    //        this.lblPath.Text);

            //    // and display...
            //    //MessageBox.Show(created, ApplicationAttributes.AssemblyTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //catch (Exception ex)
            //{
            //    this.pbStatus.Visible = false;
            //    this.Cursor = Cursors.Default;
            //   // Messages.BadConnection(ex);
            //}
        }
    }
}