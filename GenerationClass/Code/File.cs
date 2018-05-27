using System.IO;
using System.Windows.Forms;

namespace GenerationClass.Code
{
    internal class File
    {
        private string pathConnect = "";

        public void Save(string Name, string Path, string folder, string info, string pasvand)
        {
            pathConnect = System.IO.Path.Combine(Path, folder);

            if (System.IO.File.Exists(pathConnect))
            {
                if (System.IO.File.Exists(Path + "\\" + folder + "\\" + Name + "." + pasvand))
                {
                    if (MessageBox.Show("This File Is Already Exist...\nAre You Want Replace This File?", "File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        System.IO.File.Delete(Path + "\\" + folder + "\\" + Name + "." + pasvand);
                    }
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(pathConnect);

                if (System.IO.File.Exists(Path + "\\" + folder + "\\" + Name + "." + pasvand))
                {
                    if (MessageBox.Show("This File Is Already Exist...\nAre You Want Replace This File?", "File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        System.IO.File.Delete(Path + "\\" + folder + "\\" + Name + "." + pasvand);
                    }
                }
            }

            FileStream f = new FileStream(Path + "\\" + folder + "\\" + Name + "." + pasvand, FileMode.CreateNew, FileAccess.Write);
            StreamWriter fso = new StreamWriter(f);
            fso.Write(info);
            fso.Close();
        }
    }
}